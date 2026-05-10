using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
        bool isNewNodeScope = false;

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

			if (openDlg_AddProjectPrompt.ShowDialog() == CommonFileDialogResult.Ok)
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
				cmb_CurLanguage.SelectedIndex = 0;

				if (!englishFound)
				{
					MessageBox.Show("English.cfg is required but was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
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
			commentsConfig.LocalizationDataBase.WriteToFile(commentsConfig.FilePath);

			foreach (string lang in localizationConfigs.Keys)
			{
				localizationConfigs[lang].LocalizationDataBase.WriteToFile(localizationConfigs[lang].FilePath);
			}
        }

        private void treeView_Database_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            // TODO: add name validation here (no spaces, etc.)

            // this is so fucking stupid but whatever, cheers microslop x
            BeginInvoke(new Action(() => NodeAfterEditCommitted(e.Node)));
        }

        void NodeAfterEditCommitted(TreeNode node)
        {
            string nodePath = node.FullPath;

            if (isEditingNewNode)
            {
                if (isNewNodeScope)
                {
                    Scope scope = commentsConfig.LocalizationDataBase.AddScope(nodePath);
                }
                else
                {
                    Key key = commentsConfig.LocalizationDataBase.AddKey(nodePath);
                }
            }

            foreach (string lang in localizationConfigs.Keys)
            {
                if (isEditingNewNode)
                {
                    if (isNewNodeScope)
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
                    Key key = localizationConfigs[lang].LocalizationDataBase.GetKey(nodePath);
                    if (key != null)
                    {
                        key.Rename(node.Text);
                    }
                    else
                    {
                        Scope scope = localizationConfigs[lang].LocalizationDataBase.GetScope(nodePath);
                        scope.Rename(node.Text);
                    }
                }
            }
            isEditingNewNode = false;
            treeView_Database.SelectedNode = node;
        }

        private void cntxt_Key_Delete_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("hello");
        }

        private void cntxt_Key_Rename_Click(object sender, EventArgs e)
        {

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
                    TreeNode newNode = node.Nodes.Add("");
                    newNode.ContextMenuStrip = cntxt_Key;
                    node.Expand();

                    isEditingNewNode = true;
                    isNewNodeScope = false;
                    newNode.BeginEdit();
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
                    TreeNode newNode = node.Nodes.Add("");
                    newNode.ContextMenuStrip = cntxt_Scope;
                    node.Expand();

                    isEditingNewNode = true;
                    isNewNodeScope = true;
                    newNode.BeginEdit();
                }
            }
        }

        private void cntxt_Scope_DeleteScope_Click(object sender, EventArgs e)
        {

        }

        private void cntxt_Scope_RenameScope_Click(object sender, EventArgs e)
        {

        }

        private void cntxt_RootAddKey_Click(object sender, EventArgs e)
        {

        }

        private void cntxt_RootAddScope_Click(object sender, EventArgs e)
        {

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
    }
}
