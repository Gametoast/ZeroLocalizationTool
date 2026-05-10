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
using Microsoft.WindowsAPICodePack.Dialogs;
using ZeroLocalizationToolGUI.Modules;
using ZeroLocalizationToolShared.Modules;

namespace ZeroLocalizationToolGUI
{
	public partial class MainForm : Form
	{
		public CommonOpenFileDialog openDlg_AddProjectPrompt = new CommonOpenFileDialog();
		public LocalizationConfig commentsConfig;
		public Dictionary<string, LocalizationConfig> localizationConfigs = new Dictionary<string, LocalizationConfig>();

		bool isEnglishSelected = false;
		TreeNode selectedNode;
		bool isUpdatingUI = false;
        bool isEditingNewNode = false;
        bool isEditingNodeScope = false;
        bool dirtyChanges = false;

        public MainForm()
		{
			InitializeComponent();
		}

		enum ELocalizationTextFieldType
		{
			Comment,
			Original,
			Translation
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			lbl_NodePath.Text = string.Empty;

			openDlg_AddProjectPrompt.Title = "Open Localization Folder";
			openDlg_AddProjectPrompt.IsFolderPicker = true;

			bool englishFound = false;

            CommonFileDialogResult dialogResult = openDlg_AddProjectPrompt.ShowDialog();


            if (dialogResult == CommonFileDialogResult.Ok)
			{
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

				if (englishFound)
                {
                    cmb_CurLanguage.SelectedIndex = 0;
				}
                else
                {
                    DialogResult errorResult = MessageBox.Show("English.cfg is required but was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (errorResult == DialogResult.OK)
                    {
                        Application.Exit();
                    }
                }
			}
            else
            {
                Application.Exit();
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                switch (e.KeyCode)
                {
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

		}

        private void treeView_Database_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selectedNode = e.Node;

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

                    Key commentKey = commentsConfig.LocalizationDataBase.GetKey(keyPath);
                    rtb_Comments.Text = commentKey.GetValue();
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
                        rtb_Comments.Enabled = true;
                        rtb_OriginalText.Enabled = true;
                        rtb_TranslatedText.Visible = false;
                        rtb_TranslatedText.Enabled = false;
                    }
                    else if (curLang != "english")
                    {
                        rtb_Comments.Enabled = true;
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
                    Key commentKey = commentsConfig.LocalizationDataBase.GetKey(keyPath);
                    commentKey.SetValue(rtb_Comments.Text);
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
            if (isUpdatingUI)
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
            if (MessageBox.Show("Are you sure you want to overwrite the localization files with your changes?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Command_Save();
            }
        }

        void Command_Save()
        {
            commentsConfig.LocalizationDataBase.WriteToFile(commentsConfig.FilePath);

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
            if (MessageBox.Show(string.Format("Are you sure you want to delete '{0}' and all of its children?", node.FullPath), "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                MarkDirtyChanges(true);

                if (node.Tag is Scope)
                {
                    commentsConfig.LocalizationDataBase.DeleteScope(node.FullPath);
                }
                else
                {
                    commentsConfig.LocalizationDataBase.DeleteKey(node.FullPath);
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

        void Command_RenameNode(TreeNode node)
        {
            MarkDirtyChanges(true);

            isEditingNodeScope = node.Tag is Scope;
            node.BeginEdit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            localizationConfigs["english"].LocalizationDataBase.AddScope("somebullshit");
            localizationConfigs["english"].LocalizationDataBase.AddKey("somebullshit.myNewKey");

            Debug.WriteLine("hello");
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
    }
}
