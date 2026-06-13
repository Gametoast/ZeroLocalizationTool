using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZeroLocalizationToolGUI.Forms;
using ZeroLocalizationToolGUI.Modules;
using ZeroLocalizationToolGUI.Properties;
using ZeroLocalizationToolShared.Modules;

namespace ZeroLocalizationToolGUI
{
	public partial class MainForm : Form
	{
		public CommonOpenFileDialog openDlg_AddProjectPrompt = new CommonOpenFileDialog();
		public LocalizationConfig commentsConfig;
		public Dictionary<string, LocalizationConfig> localizationConfigs;

        bool isProjectLoaded = false;
		bool isEnglishSelected = false;
		TreeNode selectedNode;
		bool isUpdatingUI = false;
        bool isEditingNewNode = false;
        bool isEditingNodeScope = false;
        bool dirtyChanges = false;

        public MainForm()
		{
			InitializeComponent();

            rtb_OriginalText.EnableContextMenu();
            rtb_TranslatedText.EnableContextMenu();
            rtb_Comments.EnableContextMenu();
        }

		enum ELocalizationTextFieldType
		{
			Comment,
			Original,
			Translation
        }

        public struct NodeNameSearchQuery
        {
            public string Expression;
            public bool MatchWholeExpression;
            public bool IsRegex;
        }

        public struct NodeNameSearchResult
        {
            public string NodePath;
        }

        public struct TranslationSearchQuery
        {
            public string Expression;
            public bool MatchWholeExpression;
            public bool MatchCase;
            public bool IsRegex;
            public string[] Languages;
        }

        public struct TranslationSearchResult
        {
            public string NodePath;
            public string Language;
            public string Text;
        }

        private void MainForm_Load(object sender, EventArgs e)
		{
            MessageBox.Show("This is application is licensed under the BSD 3-Clause license; by continuing to use it you agree to its terms. This application is a work in progress and some features may not work as expected. It comes with NO WARRANTY and the creators are not responsible for any loss of data. Always make backups before using experimental tools in your production workflows.\n\nPlease report any issues at https://github.com/Gametoast/ZeroLocalizationTool\n\nSee the About page for the full license terms.", "Disclaimer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			lbl_NodePath.Text = string.Empty;

            ShowDialog_Open();
        }

        void ShowDialog_Open()
        {

            openDlg_AddProjectPrompt.Title = "Open 'Localize' folder containing .CFG files";
            openDlg_AddProjectPrompt.IsFolderPicker = true;

            bool englishFound = false;

            CommonFileDialogResult dialogResult = openDlg_AddProjectPrompt.ShowDialog();

            if (dialogResult == CommonFileDialogResult.Ok)
            {
                ResetForm();

                string[] cfgFiles = Directory.GetFiles(openDlg_AddProjectPrompt.FileName, "*.cfg");

                foreach (string cfgFile in cfgFiles)
                {
                    string languageName = Path.GetFileNameWithoutExtension(cfgFile).ToLower();

                    if (languageName == "comments")
                    {
                        commentsConfig = new LocalizationConfig();
                        commentsConfig.FilePath = cfgFile;
                        commentsConfig.LocalizationDataBase = LocalizationParser.ParseDataBase(cfgFile);
                        Debug.WriteLine(commentsConfig.LocalizationDataBase.GetKey("cheats.ammo_off"));
                    }
                    else
                    {
                        if (localizationConfigs == null)
                        {
                            localizationConfigs = new Dictionary<string, LocalizationConfig>();
                        }
                        LocalizationConfig config = new LocalizationConfig();
                        config.FilePath = cfgFile;
                        config.LocalizationDataBase = LocalizationParser.ParseDataBase(cfgFile);
                        localizationConfigs.Add(languageName, config);

                        cmb_CurLanguage.Items.Add(languageName);

                        if (languageName == "english")
                        {
                            englishFound = true;
                            PopulateTreeViewFromDatabase(config.LocalizationDataBase);
                        }
                    }
                }

                if (commentsConfig == null)
                {
                    rtb_Comments.Enabled = false;
                }

                if (englishFound)
                {
                    SetIsProjectLoaded();
                }
                else
                {
                    DialogResult errorResult = MessageBox.Show("English.cfg is required but was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (errorResult == DialogResult.OK)
                    {
                        ResetForm();
                    }
                }
            }
            else
            {
                //Application.Exit();
            }
        }

        void ShowDialog_ProjectNotLoaded()
        {
            if (!isProjectLoaded)
            {
                DialogResult dialogResult = MessageBox.Show("A localization project must be loaded in order to do this.", "Nice try, slick...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (dialogResult == DialogResult.OK)
                {
                    ShowDialog_Open();
                }
            }
        }

        void ResetForm()
        {
            commentsConfig = null;
            localizationConfigs = null;

            isProjectLoaded = false;
            isEnglishSelected = false;
            selectedNode = null;
            isUpdatingUI = false;
            isEditingNewNode = false;
            isEditingNodeScope = false;
            dirtyChanges = false;

            lbl_NodePath.Text = string.Empty;
            cmb_CurLanguage.Items.Clear();
            treeView_Database.Enabled = false;
            treeView_Database.Nodes.Clear();
            rtb_Comments.Text = string.Empty;
            rtb_OriginalText.Text = string.Empty;
            rtb_TranslatedText.Text = string.Empty;
        }

        void SetIsProjectLoaded()
        {
            cmb_CurLanguage.SelectedIndex = 0;
            treeView_Database.Enabled = true;
            isProjectLoaded = true;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.F:
                        ShowWindow_Find();
                        e.Handled = true;
                        break;
                    case Keys.O:
                        ShowDialog_Open();
                        e.Handled = true;
                        break;
                    case Keys.S:
                        ShowDialog_Save();
                        e.Handled = true;
                        break;
                    case Keys.Q:
                        Application.Exit();
                        e.Handled = true;
                        break;
                }
            }

            if (e.Modifiers == (Keys.Control | Keys.Shift))
            {
                switch (e.KeyCode)
                {
                    case Keys.C:
                        Command_CopyKeyPath();
                        e.Handled = true;
                        break;
                }
            }

            switch (e.KeyCode)
            {
                case Keys.F12:
                    Command_ShowAboutForm();
                    e.Handled = true;
                    break;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dirtyChanges)
            {
                ShowDialog_Quit(e);
            }
        }

        void ShowDialog_Quit(FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("You have unsaved changes. Save now?", "Quit Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Command_Save();
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        void ShowWindow_Find()
        {
            if (!isProjectLoaded)
            {
                ShowDialog_ProjectNotLoaded();
                return;
            }

            FindForm findForm = new FindForm();
            findForm.Show(this);
        }

        void MarkDirtyChanges(bool dirty)
        {
            dirtyChanges = dirty;

            string windowName = "Zero Localization Tool";

            if (dirty)
            {
                windowName = "* Zero Localization Tool (unsaved changes)";
            }

            this.Text = windowName;
            this.Update();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
            ShowDialog_Open();
		}

        private void treeView_Database_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selectedNode = e.Node;

            if (e.Node.Tag is Scope)
            {
                addScopeToolStripMenuItem.Enabled = true;
                addKeyToolStripMenuItem.Enabled = true;
                btn_CopyEnglishValueToOtherLangs.Enabled = false;
                copyKeyPathToolStripMenuItem1.Enabled = false;
            }
            else if (e.Node.Tag is Key)
            {
                addScopeToolStripMenuItem.Enabled = false;
                addKeyToolStripMenuItem.Enabled = true;
                btn_CopyEnglishValueToOtherLangs.Enabled = true;
                copyKeyPathToolStripMenuItem1.Enabled = true;
            }

            string keyPath = e.Node.FullPath;
			UpdateKeyValueViews(keyPath);
        }

        void PopulateTreeViewFromDatabase(DataBase dataBase)
		{
			foreach (Scope rootScope in dataBase.Scopes)
			{
				AddDatabaseNodesForScope(rootScope);
			}
		}

		void AddDatabaseNodesForScope(Scope scope)
		{
			AddDatabaseNodesForScope(scope, null);
		}

		void AddDatabaseNodesForScope(Scope scope, TreeNode node)
		{
			// Add the scope
			TreeNode scopeNode;
			if (node != null)
            {
                scopeNode = node.Nodes.Add(scope.Name);
                scopeNode.Tag = scope;
                scopeNode.ContextMenuStrip = cntxt_Scope;
            }
			else
            {
                scopeNode = treeView_Database.Nodes.Add(scope.Name);
                scopeNode.Tag = scope;
                scopeNode.ContextMenuStrip = cntxt_Scope;
            }

			// Add any subscopes
			if (scope.Scopes.Count > 0)
			{
				foreach (Scope subScope in scope.Scopes)
				{
					AddDatabaseNodesForScope(subScope, scopeNode);
				}
			}

			// Add any keys
			foreach (Key key in scope.Keys)
			{
				TreeNode keyNode = scopeNode.Nodes.Add(key.Name);
                keyNode.Tag = key;
                keyNode.ContextMenuStrip = cntxt_Key;
			}
		}

		void UpdateKeyValueViews(string keyPath)
        {
			isUpdatingUI = true;

			try
			{
                string curLang = GetCurrentLanguage();
                bool isScopeSelected = false;

                Key originalKey = localizationConfigs["english"].LocalizationDataBase.GetKey(keyPath);

                // Is this a key?
                if (originalKey != null)
                {
                    lbl_NodePath.Text = keyPath;
                    cmb_CurLanguage.Enabled = true;
                    rtb_OriginalText.Text = originalKey.GetValue();

                    if (!isEnglishSelected)
                    {
                        Key translatedKey = localizationConfigs[curLang].LocalizationDataBase.GetKey(keyPath);
                        rtb_TranslatedText.Text = translatedKey.GetValue();
                    }

                    if (commentsConfig != null)
                    {
                        Key commentKey = commentsConfig.LocalizationDataBase.GetKey(keyPath);
                        rtb_Comments.Text = commentKey.GetValue();
                    }
                }
                // Or is it a scope?
                else
                {
                    isScopeSelected = true;

                    rtb_Comments.Text = string.Empty;
                    rtb_OriginalText.Text = string.Empty;
                    rtb_TranslatedText.Text = string.Empty;
                }

                // Update UI controls usability/visibility
                if (isScopeSelected)
                {
                    lbl_NodePath.Text = string.Empty;
                    rtb_Comments.Enabled = false;
                    rtb_OriginalText.Enabled = false;
                    rtb_TranslatedText.Visible = false;
                    rtb_TranslatedText.Enabled = false;
                }
                else
                {
                    if (curLang == string.Empty)
                    {
                        rtb_Comments.Enabled = false;
                        rtb_OriginalText.Enabled = false;
                        rtb_TranslatedText.Visible = false;
                        rtb_TranslatedText.Enabled = false;
                    }
                    else if (curLang == "english")
                    {
                        rtb_Comments.Enabled = commentsConfig != null;
                        rtb_OriginalText.Enabled = true;
                        rtb_TranslatedText.Visible = false;
                        rtb_TranslatedText.Enabled = false;
                    }
                    else if (curLang != "english")
                    {
                        rtb_Comments.Enabled = commentsConfig != null;
                        rtb_OriginalText.Enabled = false;
                        rtb_TranslatedText.Visible = true;
                        rtb_TranslatedText.Enabled = true;
                    }
                }
            }
			finally
			{
				isUpdatingUI = false;
			}
            
        }

		void SetKeyValueFromSelectedNode(ELocalizationTextFieldType fieldType)
		{
			if (selectedNode != null)
			{
                MarkDirtyChanges(true);

				string keyPath = selectedNode.FullPath;

				// Comment text
				if (fieldType == ELocalizationTextFieldType.Comment)
                {
                    if (commentsConfig != null)
                    {
                        Key commentKey = commentsConfig.LocalizationDataBase.GetKey(keyPath);
                        commentKey.SetValue(rtb_Comments.Text);
                    }
                }
				else
                {
                    // Translations text
                    string lang = GetCurrentLanguage();
                    Key key = localizationConfigs[lang].LocalizationDataBase.GetKey(keyPath);

                    if (lang == "english" && fieldType == ELocalizationTextFieldType.Original)
                    {
                        key.SetValue(rtb_OriginalText.Text);
                    }
                    else if (fieldType == ELocalizationTextFieldType.Translation)
                    {
                        key.SetValue(rtb_TranslatedText.Text);
                    }
                }
			}
		}

        int CopyEnglishKeyValueToOtherLanguages(string keyPath, bool overwriteTranslations)
        {
            int results = 0;

            Key englishKey = localizationConfigs["english"].LocalizationDataBase.GetKey(keyPath);
            string englishValue = englishKey.GetValue();

            if (englishValue != string.Empty)
            {
                foreach (string lang in localizationConfigs.Keys)
                {
                    if (lang.ToLower() != "english")
                    {
                        Key translationKey = localizationConfigs[lang].LocalizationDataBase.GetKey(keyPath);
                        string translatedValue = translationKey.GetValue();

                        if (overwriteTranslations)
                        {
                            if (translatedValue != englishValue)
                            {
                                translationKey.SetValue(englishValue);
                                results++;
                            }
                        }
                        else
                        {
                            if (translatedValue == string.Empty && translatedValue != englishValue)
                            {
                                translationKey.SetValue(englishValue);
                                results++;
                            }
                        }
                    }
                }
            }

            return results;
        }

        private void cmb_CurLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
			string curLang = GetCurrentLanguage();
            if (curLang == string.Empty)
            {
                isEnglishSelected = false;
            }
            else if (curLang == "english")
            {
                isEnglishSelected = true;
            }
            else if (curLang != "english")
            {
                isEnglishSelected = false;
            }

            if (selectedNode != null)
			{
                UpdateKeyValueViews(selectedNode.FullPath);
            }
        }

		string GetCurrentLanguage()
		{
			return cmb_CurLanguage.Text;
		}

        private void rtb_OriginalText_TextChanged(object sender, EventArgs e)
        {
			if (isUpdatingUI)
				return;

			SetKeyValueFromSelectedNode(ELocalizationTextFieldType.Original);
        }

        private void rtb_TranslatedText_TextChanged(object sender, EventArgs e)
        {
            if (isUpdatingUI)
                return;

            SetKeyValueFromSelectedNode(ELocalizationTextFieldType.Translation);
        }

        private void rtb_Comments_TextChanged(object sender, EventArgs e)
        {
            if (isUpdatingUI || commentsConfig == null)
                return;

            SetKeyValueFromSelectedNode(ELocalizationTextFieldType.Comment);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDialog_Save();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void ShowDialog_Save()
        {
            if (isProjectLoaded)
            {
                if (MessageBox.Show("Are you sure you want to overwrite the localization files with your changes?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Command_Save();
                }
            }
            else
            {
                ShowDialog_ProjectNotLoaded();
            }
        }

        void Command_Save()
        {
            if (commentsConfig != null)
            {
                commentsConfig.LocalizationDataBase.WriteToFile(commentsConfig.FilePath);
            }

            foreach (string lang in localizationConfigs.Keys)
            {
                localizationConfigs[lang].LocalizationDataBase.WriteToFile(localizationConfigs[lang].FilePath);
            }

            MarkDirtyChanges(false);
        }

        private void treeView_Database_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            string label = e.Label ?? e.Node.Text;

            if (label.Length == 0)
            {
                e.CancelEdit = true;
                MessageBox.Show("Invalid name. The Key/Scope cannot be blank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Node.BeginEdit();

                return;
            }

            // Start and end with and allow alphanumeric, dashes, underscores
            string pattern = @"^[A-Za-z0-9](?:[A-Za-z0-9_-]*[A-Za-z0-9])?$";
            bool isValid = Regex.IsMatch(label, pattern);

            if (!isValid)
            {
                e.CancelEdit = true;
                MessageBox.Show("Invalid name. Key/Scope names must start and end with an alphanumeric and can only include alphanumerical, dash, or underscore characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Node.BeginEdit();
                return;
            }

            string oldPath = e.Node.FullPath;

            if (!isEditingNewNode)
            {
                isEditingNodeScope = e.Node.Tag is Scope;
            }

            // this is so fucking stupid but whatever, cheers microslop x
            BeginInvoke(new Action(() => NodeAfterEditCommitted(e.Node, oldPath)));
        }

        void NodeAfterEditCommitted(TreeNode node, string oldPath)
        {
            MarkDirtyChanges(true);

            string nodePath = node.FullPath;

            if (commentsConfig != null)
            {
                if (isEditingNewNode)
                {
                    if (isEditingNodeScope)
                    {
                        Scope scope = commentsConfig.LocalizationDataBase.AddScope(nodePath);
                    }
                    else
                    {
                        Key key = commentsConfig.LocalizationDataBase.AddKey(nodePath);
                    }
                }
                else
                {
                    if (isEditingNodeScope)
                    {
                        Scope scope = commentsConfig.LocalizationDataBase.GetScope(oldPath);
                        scope.Rename(node.Text);
                    }
                    else
                    {
                        Key key = commentsConfig.LocalizationDataBase.GetKey(oldPath);
                        key.Rename(node.Text);
                    }
                }
            }

            foreach (string lang in localizationConfigs.Keys)
            {
                if (isEditingNewNode)
                {
                    if (isEditingNodeScope)
                    {
                        Scope scope = localizationConfigs[lang].LocalizationDataBase.AddScope(nodePath);
                        if (lang == "english")
                        {
                            node.Tag = scope;
                        }
                    }
                    else
                    {
                        Key key = localizationConfigs[lang].LocalizationDataBase.AddKey(nodePath);
                        if (lang == "english")
                        {
                            node.Tag = key;
                        }
                    }
                }
                else
                {
                    if (isEditingNodeScope)
                    {
                        Scope scope = localizationConfigs[lang].LocalizationDataBase.GetScope(oldPath);
                        scope.Rename(node.Text);
                    }
                    else
                    {
                        Key key = localizationConfigs[lang].LocalizationDataBase.GetKey(oldPath);
                        key.Rename(node.Text);
                    }
                }
            }
            isEditingNewNode = false;
            treeView_Database.SelectedNode = node;
            selectedNode = node;
            UpdateKeyValueViews(nodePath);
        }

        private void cntxt_Node_Delete_Click(object sender, EventArgs e)
        {
            // Try to cast the sender to a ToolStripItem
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                if (menuItem.Owner is ContextMenuStrip owner)
                {
                    // Get the control that is displaying this context menu
                    TreeView treeView = owner.SourceControl as TreeView;
                    TreeNode node = treeView.SelectedNode;

                    Command_DeleteNode(node);
                }
            }
        }

        private void cntxt_Node_Rename_Click(object sender, EventArgs e)
        {
            // Try to cast the sender to a ToolStripItem
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                if (menuItem.Owner is ContextMenuStrip owner)
                {
                    // Get the control that is displaying this context menu
                    TreeView treeView = owner.SourceControl as TreeView;
                    TreeNode node = treeView.SelectedNode;

                    Command_RenameNode(node);
                }
            }
        }

        private void cntxt_Scope_AddKey_Click(object sender, EventArgs e)
        {
            // Try to cast the sender to a ToolStripItem
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                if (menuItem.Owner is ContextMenuStrip owner)
                {
                    // Get the control that is displaying this context menu
                    TreeView treeView = owner.SourceControl as TreeView;
                    TreeNode node = treeView.SelectedNode;

                    Command_AddNode(node, false);
                }
            }
        }

        private void cntxt_Scope_AddScope_Click(object sender, EventArgs e)
        {
            // Try to cast the sender to a ToolStripItem
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                if (menuItem.Owner is ContextMenuStrip owner)
                {
                    // Get the control that is displaying this context menu
                    TreeView treeView = owner.SourceControl as TreeView;
                    TreeNode node = treeView.SelectedNode;

                    Command_AddNode(node, true);
                }
            }
        }

        private void cntxt_RootAddKey_Click(object sender, EventArgs e)
        {
            Command_AddNode(null, false);
        }

        private void cntxt_RootAddScope_Click(object sender, EventArgs e)
        {
            Command_AddNode(null, true);
        }

        void Hotkey_AddNode(bool isScope)
        {
            if (!isProjectLoaded)
            {
                ShowDialog_ProjectNotLoaded();
                return; 
            }

            TreeNode node = treeView_Database.SelectedNode;

            if (node != null)
            {
                if (node.Tag is Scope)
                {
                    Command_AddNode(node, isScope);
                }
                else if (node.Tag is Key)
                {
                    // Can't add a Scope to a Key!
                    if (!isScope)
                    {
                        // Add a Key to the parent Scope
                        Command_AddNode(node.Parent, false);
                    }
                }
            }
        }

        void Command_AddNode(TreeNode node, bool isScope)
        {
            TreeNode newNode;

            if (node == null || node.Tag is Scope)
            {
                MarkDirtyChanges(true);

                if (node == null)
                {
                    newNode = treeView_Database.Nodes.Add("");
                }
                else
                {
                    newNode = node.Nodes.Add("");
                }

                ContextMenuStrip contextMenuStrip;
                if (isScope)
                {
                    contextMenuStrip = cntxt_Scope;
                }
                else
                {
                    contextMenuStrip = cntxt_Key;
                }
                newNode.ContextMenuStrip = contextMenuStrip;
                
                if (node != null)
                {
                    node.Expand();
                }
                isEditingNewNode = true;
                isEditingNodeScope = isScope;
                newNode.BeginEdit();
            }
        }

        void Command_DeleteNode(TreeNode node)
        {
            if (!isProjectLoaded)
            {
                ShowDialog_ProjectNotLoaded();
                return;
            }

            if (node != null)
            {
                if (MessageBox.Show(string.Format("Are you sure you want to delete '{0}' and all of its children?", node.FullPath), "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    MarkDirtyChanges(true);

                    if (commentsConfig != null)
                    {
                        if (node.Tag is Scope)
                        {
                            commentsConfig.LocalizationDataBase.DeleteScope(node.FullPath);
                        }
                        else
                        {
                            commentsConfig.LocalizationDataBase.DeleteKey(node.FullPath);
                        }
                    }

                    foreach (string lang in localizationConfigs.Keys)
                    {
                        if (node.Tag is Scope)
                        {
                            localizationConfigs[lang].LocalizationDataBase.DeleteScope(node.FullPath);
                        }
                        else
                        {
                            localizationConfigs[lang].LocalizationDataBase.DeleteKey(node.FullPath);
                        }
                    }

                    node.Remove();
                }
            }
        }

        void Command_RenameNode(TreeNode node)
        {
            if (!isProjectLoaded)
            {
                ShowDialog_ProjectNotLoaded();
                return;
            }

            if (node != null)
            {
                MarkDirtyChanges(true);

                isEditingNodeScope = node.Tag is Scope;
                node.BeginEdit();
            }
        }

        private void treeView_Database_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView_Database.SelectedNode = e.Node;
            }
        }

        private void treeView_Database_KeyDown(object sender, KeyEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.D2:
                        Hotkey_AddNode(true);
                        e.Handled = true;
                        break;
                    case Keys.D3:
                        Hotkey_AddNode(false);
                        e.Handled = true;
                        break;
                }
            }
            switch (e.KeyCode)
            {
                case Keys.F2:
                    if (selectedNode != null)
                    {
                        Command_RenameNode(selectedNode);
                        e.Handled = true;
                    }
                    break;
                case Keys.Delete:
                    if (selectedNode != null)
                    {
                        Command_DeleteNode(selectedNode);
                        e.Handled = true;
                    }
                    break;
            }
        }

        private void addScopeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotkey_AddNode(true);
        }

        private void addKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotkey_AddNode(false);
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Command_RenameNode(selectedNode);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Command_DeleteNode(selectedNode);
        }

        private void findReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowWindow_Find();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Command_ShowAboutForm();
        }

        void Command_ShowAboutForm()
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void submitIssueOnGitHubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Resources.IssueLink);
        }

        IEnumerable<TreeNode> Collect(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                yield return node;

                foreach (var child in Collect(node.Nodes))
                    yield return child;
            }
        }

        private void copyEnglishToOtherLanguagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isProjectLoaded)
            {
                ShowDialog_ProjectNotLoaded();
                return;
            }

            List<TreeNode> allKeyNodes = Collect(treeView_Database.Nodes)
                .Where(n => n.Tag is Key)
                .ToList();

            int results = 0;

            DialogResult dialogResult = MessageBox.Show(
                "This will go through all keys in the database and copy the English value into any untranslated (blank) language values for each key. \n\nDo you want to overwrite existing (non-blank) translations as well?",
                "Copy Values Prompt",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (dialogResult == DialogResult.Yes)
            {
                foreach (TreeNode node in allKeyNodes)
                {
                    results += CopyEnglishKeyValueToOtherLanguages(node.FullPath, true);
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                foreach (TreeNode node in allKeyNodes)
                {
                    results += CopyEnglishKeyValueToOtherLanguages(node.FullPath, false);
                }
            }

            MessageBox.Show(string.Format("{0} values changed.", results), "Copy Results", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (selectedNode != null && selectedNode.Tag is Key)
            {
                UpdateKeyValueViews(selectedNode.FullPath);
            }
        }

        private void btn_CopyEnglishValueToOtherLangs_Click(object sender, EventArgs e)
        {
            if (selectedNode != null && selectedNode.Tag is Key)
            {
                int results = 0;

                DialogResult dialogResult = MessageBox.Show(
                    "This will copy the English value into any untranslated (blank) language values for this key. \n\nDo you want to overwrite existing (non-blank) translations as well?",
                    "Copy Values Prompt",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (dialogResult == DialogResult.Yes)
                {
                    results = CopyEnglishKeyValueToOtherLanguages(selectedNode.FullPath, true);
                    UpdateKeyValueViews(selectedNode.FullPath);
                }
                else if (dialogResult == DialogResult.No)
                {
                    results = CopyEnglishKeyValueToOtherLanguages(selectedNode.FullPath, false);
                    UpdateKeyValueViews(selectedNode.FullPath);
                }

                MessageBox.Show(string.Format("{0} values changed.", results), "Copy Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public List<NodeNameSearchResult> BeginNodeNameSearch(NodeNameSearchQuery query)
        {
            List<NodeNameSearchResult> results = new List<NodeNameSearchResult>();
            List<TreeNode> foundNodes = new List<TreeNode>();

            if (query.IsRegex)
            {
                foundNodes = Collect(treeView_Database.Nodes)
                    .Where(n => Regex.IsMatch(n.FullPath, query.Expression))
                    .ToList();
            }
            else
            {
                if (query.MatchWholeExpression)
                {
                    foundNodes = Collect(treeView_Database.Nodes)
                        .Where(n => n.FullPath.Contains(query.Expression))
                        .ToList();
                }
                else
                {
                    string[] parsedExpression = query.Expression.Split(' ');

                    foreach (string expressionPart in parsedExpression)
                    {
                        foundNodes.AddRange(Collect(treeView_Database.Nodes)
                            .Where(n => n.FullPath.Contains(expressionPart))
                            .ToList());
                    }
                }
            }

            foreach (TreeNode node in foundNodes)
            {
                results.Add(new NodeNameSearchResult()
                {
                    NodePath = node.FullPath
                });
                Debug.WriteLine(string.Format("Found match at {0}", node.FullPath));
            }

            return results;
        }

        public List<TranslationSearchResult> BeginTranslationSearch(TranslationSearchQuery query)
        {
            List<TranslationSearchResult> results = new List<TranslationSearchResult>();
            List<TreeNode> allKeyNodes = new List<TreeNode>();

            allKeyNodes = Collect(treeView_Database.Nodes)
                .Where(n => n.Tag is Key)
                .ToList();

            foreach (string lang in query.Languages)
            {
                DataBase dataBase;
                if (lang == "comments")
                {
                    dataBase = commentsConfig.LocalizationDataBase;
                }
                else
                {
                    dataBase = localizationConfigs[lang].LocalizationDataBase;
                }



                if (query.IsRegex)
                {
                    foreach (TreeNode node in allKeyNodes)
                    {
                        Key nodeKey = dataBase.GetKey(node.FullPath);
                        if (nodeKey == null)
                            continue;

                        bool querySuccess = Regex.IsMatch(nodeKey.GetValue(), query.Expression);

                        if (querySuccess)
                        {
                            TranslationSearchResult newResult = new TranslationSearchResult()
                            {
                                Language = lang,
                                NodePath = node.FullPath,
                                Text = nodeKey.GetValue()
                            };
                            results.Add(newResult);
                        }
                    }
                }
                else
                {
                    if (query.MatchWholeExpression)
                    {
                        foreach (TreeNode node in allKeyNodes)
                        {
                            Key nodeKey = dataBase.GetKey(node.FullPath);
                            if (nodeKey == null)
                                continue;

                            bool querySuccess;
                            if (query.MatchCase)
                            {
                                querySuccess = nodeKey.GetValue().Contains(query.Expression);
                            }
                            else
                            {
                                querySuccess = nodeKey.GetValue().ToLower().Contains(query.Expression.ToLower());
                            }

                            if (querySuccess)
                            {
                                TranslationSearchResult newResult = new TranslationSearchResult()
                                {
                                    Language = lang,
                                    NodePath = node.FullPath,
                                    Text = nodeKey.GetValue()
                                };
                                results.Add(newResult);
                            }
                        }
                    }
                    else
                    {
                        string[] parsedExpression = query.Expression.Split(' ');

                        foreach (string expressionPart in parsedExpression)
                        {
                            foreach (TreeNode node in allKeyNodes)
                            {
                                Key nodeKey = dataBase.GetKey(node.FullPath);
                                if (nodeKey == null)
                                    continue;

                                bool querySuccess;
                                if (query.MatchCase)
                                {
                                    querySuccess = nodeKey.GetValue().Contains(expressionPart);
                                }
                                else
                                {
                                    querySuccess = nodeKey.GetValue().ToLower().Contains(expressionPart.ToLower());
                                }

                                if (querySuccess)
                                {
                                    TranslationSearchResult newResult = new TranslationSearchResult()
                                    {
                                        Language = lang,
                                        NodePath = node.FullPath,
                                        Text = nodeKey.GetValue()
                                    };
                                    results.Add(newResult);
                                }
                            }
                        }
                    }
                }
            }

            return results;
        }

        public void JumpToTreeViewNode(string nodePath)
        {
            TreeNode foundNode = GetNodeFromPath(treeView_Database.Nodes, nodePath);
            this.Focus();
            treeView_Database.SelectedNode = foundNode;
            foundNode.EnsureVisible();
            treeView_Database.Select();
        }

        public void JumpToTreeViewNode(string nodePath, string language)
        {
            JumpToTreeViewNode(nodePath);
            cmb_CurLanguage.SelectedIndex = cmb_CurLanguage.FindString(language);
        }

        public TreeNode GetNodeFromPath(TreeNodeCollection tncoll, string fullPath)
        {
            TreeNode tnFound;
            foreach (TreeNode tnCurr in tncoll)
            {
                if (tnCurr.FullPath == fullPath)
                {
                    return tnCurr;
                }
                tnFound = GetNodeFromPath(tnCurr.Nodes, fullPath);
                if (tnFound != null)
                {
                    return tnFound;
                }
            }
            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Key key = localizationConfigs["english"].LocalizationDataBase.GetKey(selectedNode.FullPath);
            Debug.WriteLine(key.Value);
            //foreach (string str in key.BinaryValues)
            //{
            //    Debug.WriteLine(str);
            //}
        }

        private void copyKeyPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Command_CopyKeyPath();
        }

        void Command_CopyKeyPath()
        {
            if (!isProjectLoaded)
            {
                ShowDialog_ProjectNotLoaded();
                return;
            }

            if (!string.IsNullOrEmpty(lbl_NodePath.Text))
            {
                Clipboard.SetText(lbl_NodePath.Text);
            }
        }
    }
}
