using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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

		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			openDlg_AddProjectPrompt.Title = "Open Localization File";
			//openDlg_AddProjectPrompt.IsFolderPicker = true;

			if (openDlg_AddProjectPrompt.ShowDialog() == CommonFileDialogResult.Ok)
			{
				LoadDatabase(openDlg_AddProjectPrompt.FileName);
			}
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void treeView_Database_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			string keyPath = e.Node.FullPath;
			Key key = db.GetKey(keyPath);

			if (key != null)
			{
				rtb_KeyValue.Enabled = true;
				lbl_NodePath.Text = keyPath;
				rtb_KeyValue.Text = key.GetValue();
			}
			else
			{
				rtb_KeyValue.Text = string.Empty;
				rtb_KeyValue.Enabled = false;
			}
		}

		void LoadDatabase(string fileName)
		{
			db = LocalizationParser.ParseDataBase(fileName);

			foreach (Scope rootScope in db.Scopes)
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
	}
}
