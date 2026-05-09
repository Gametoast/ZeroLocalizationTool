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
using ZeroLocalizationToolShared.Modules;

namespace ZeroLocalizationToolGUI
{
	public partial class MainForm : Form
	{
		public CommonOpenFileDialog openDlg_AddProjectPrompt = new CommonOpenFileDialog();
		public DataBase db = new DataBase();
		public DataBase commentsDb = new DataBase();
		public Dictionary<string, DataBase> databases = new Dictionary<string, DataBase>();
		bool isEnglishSelected = false;
		TreeNode selectedNode;

		public MainForm()
		{
			InitializeComponent();
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
						commentsDb = LocalizationParser.ParseDataBase(cfgFile);
                    }
					else
					{
						DataBase langDb = LocalizationParser.ParseDataBase(cfgFile);
                        databases.Add(languageName, langDb);
						cmb_CurLanguage.Items.Add(languageName);

						if (languageName == "english")
						{
							englishFound = true;
							PopulateTreeViewFromDatabase(langDb);
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
				scopeNode = node.Nodes.Add(scope.Name);
			else
				scopeNode = treeView_Database.Nodes.Add(scope.Name);

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
			}
		}

		/// <summary>
		/// Populates a TreeView based on a list of file paths.
		/// 
		/// Adapted from https://stackoverflow.com/a/19332770/3639133
		/// </summary>
		/// <param name="treeView">TreeView control to populate.</param>
		/// <param name="paths">List of paths.</param>
		/// <param name="pathSeparator">Character to split the path.</param>
		private void PopulateTreeView(TreeView treeView, List<string> paths, char pathSeparator)
		{
			TreeNode lastNode = null;
			string subPathAgg;
			foreach (string path in paths)
			{
				subPathAgg = string.Empty;
				foreach (string subPath in path.Split(pathSeparator))
				{
					subPathAgg += subPath + pathSeparator;
					TreeNode[] nodes = treeView.Nodes.Find(subPathAgg, true);
					if (nodes.Length == 0)
						if (lastNode == null)
							lastNode = treeView.Nodes.Add(subPathAgg, subPath);
						else
							lastNode = lastNode.Nodes.Add(subPathAgg, subPath);
					else
						lastNode = nodes[0];
				}
				lastNode = null; // This is the place code was changed
			}
		}

		void UpdateKeyValueViews(string keyPath)
        {
            string curLang = GetCurrentLanguage();
			bool isScopeSelected = false;

            Key originalKey = databases["english"].GetKey(keyPath);

			// Is this a key?
            if (originalKey != null)
            {
                lbl_NodePath.Text = keyPath;
				cmb_CurLanguage.Enabled = true;
                rtb_OriginalText.Text = originalKey.GetValue();

				if (!isEnglishSelected)
                {
                    Key translatedKey = databases[curLang].GetKey(keyPath);
					rtb_TranslatedText.Text = translatedKey.GetValue();
                }

				Key commentKey = commentsDb.GetKey(keyPath);
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

			// Update UI controls
			if (isScopeSelected)
            {
				lbl_NodePath.Text = string.Empty;
                rtb_OriginalText.Enabled = false;
                rtb_TranslatedText.Visible = false;
                rtb_TranslatedText.Enabled = false;
            }
			else
            {
                if (curLang == string.Empty)
                {
                    rtb_OriginalText.Enabled = false;
                    rtb_TranslatedText.Visible = false;
                    rtb_TranslatedText.Enabled = false;
                }
                else if (curLang == "english")
                {
                    rtb_OriginalText.Enabled = true;
                    rtb_TranslatedText.Visible = false;
                    rtb_TranslatedText.Enabled = false;
                }
                else if (curLang != "english")
                {
                    rtb_OriginalText.Enabled = false;
                    rtb_TranslatedText.Visible = true;
                    rtb_TranslatedText.Enabled = true;
                }
            }
        }

		void SetKeyValueFromSelectedNode()
		{
			if (selectedNode != null)
			{
				string keyPath = selectedNode.FullPath;
				Key key = db.GetKey(keyPath);	// TODO: this should enumerate through a db dictionary mapped to the different languages
				key.SetValue(rtb_OriginalText.Text);
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
    }
}
