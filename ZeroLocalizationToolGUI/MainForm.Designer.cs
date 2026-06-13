
namespace ZeroLocalizationToolGUI
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addScopeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findReplaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyKeyPathToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyEnglishToOtherLanguagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.submitIssueOnGitHubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_NodePath = new System.Windows.Forms.Label();
            this.cntxt_KeyPath = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyKeyPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView_Database = new System.Windows.Forms.TreeView();
            this.cntxt_RootLevel = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cntxt_RootAddKey = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxt_RootAddScope = new System.Windows.Forms.ToolStripMenuItem();
            this.cmb_CurLanguage = new System.Windows.Forms.ComboBox();
            this.rtb_TranslatedText = new System.Windows.Forms.RichTextBox();
            this.rtb_Comments = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cntxt_Scope = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cntxt_Scope_AddKey = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxt_Scope_AddScope = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxt_Scope_DeleteScope = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxt_Scope_RenameScope = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.cntxt_Key = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cntxt_Key_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxt_Key_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_CopyEnglishValueToOtherLangs = new System.Windows.Forms.Button();
            this.splitContainer_EnglishComments = new System.Windows.Forms.SplitContainer();
            this.rtb_OriginalText = new System.Windows.Forms.RichTextBox();
            this.splitContainer_Main = new System.Windows.Forms.SplitContainer();
            this.splitContainer_Languages = new System.Windows.Forms.SplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.cntxt_KeyPath.SuspendLayout();
            this.cntxt_RootLevel.SuspendLayout();
            this.cntxt_Scope.SuspendLayout();
            this.cntxt_Key.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_EnglishComments)).BeginInit();
            this.splitContainer_EnglishComments.Panel1.SuspendLayout();
            this.splitContainer_EnglishComments.Panel2.SuspendLayout();
            this.splitContainer_EnglishComments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Main)).BeginInit();
            this.splitContainer_Main.Panel1.SuspendLayout();
            this.splitContainer_Main.Panel2.SuspendLayout();
            this.splitContainer_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Languages)).BeginInit();
            this.splitContainer_Languages.Panel1.SuspendLayout();
            this.splitContainer_Languages.Panel2.SuspendLayout();
            this.splitContainer_Languages.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_File,
            this.keysToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(755, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem_File
            // 
            this.toolStripMenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.toolStripMenuItem_File.Name = "toolStripMenuItem_File";
            this.toolStripMenuItem_File.Size = new System.Drawing.Size(37, 22);
            this.toolStripMenuItem_File.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // keysToolStripMenuItem
            // 
            this.keysToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addScopeToolStripMenuItem,
            this.addKeyToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.keysToolStripMenuItem.Name = "keysToolStripMenuItem";
            this.keysToolStripMenuItem.Size = new System.Drawing.Size(43, 22);
            this.keysToolStripMenuItem.Text = "Keys";
            // 
            // addScopeToolStripMenuItem
            // 
            this.addScopeToolStripMenuItem.Name = "addScopeToolStripMenuItem";
            this.addScopeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.addScopeToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.addScopeToolStripMenuItem.Text = "Add Scope";
            this.addScopeToolStripMenuItem.Click += new System.EventHandler(this.addScopeToolStripMenuItem_Click);
            // 
            // addKeyToolStripMenuItem
            // 
            this.addKeyToolStripMenuItem.Name = "addKeyToolStripMenuItem";
            this.addKeyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.addKeyToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.addKeyToolStripMenuItem.Text = "Add Key";
            this.addKeyToolStripMenuItem.Click += new System.EventHandler(this.addKeyToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findReplaceToolStripMenuItem,
            this.copyKeyPathToolStripMenuItem1,
            this.copyEnglishToOtherLanguagesToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 22);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // findReplaceToolStripMenuItem
            // 
            this.findReplaceToolStripMenuItem.Name = "findReplaceToolStripMenuItem";
            this.findReplaceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.findReplaceToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.findReplaceToolStripMenuItem.Text = "Find";
            this.findReplaceToolStripMenuItem.Click += new System.EventHandler(this.findReplaceToolStripMenuItem_Click);
            // 
            // copyKeyPathToolStripMenuItem1
            // 
            this.copyKeyPathToolStripMenuItem1.Enabled = false;
            this.copyKeyPathToolStripMenuItem1.Name = "copyKeyPathToolStripMenuItem1";
            this.copyKeyPathToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.copyKeyPathToolStripMenuItem1.Size = new System.Drawing.Size(259, 22);
            this.copyKeyPathToolStripMenuItem1.Text = "Copy Key Path";
            this.copyKeyPathToolStripMenuItem1.Click += new System.EventHandler(this.copyKeyPathToolStripMenuItem_Click);
            // 
            // copyEnglishToOtherLanguagesToolStripMenuItem
            // 
            this.copyEnglishToOtherLanguagesToolStripMenuItem.Name = "copyEnglishToOtherLanguagesToolStripMenuItem";
            this.copyEnglishToOtherLanguagesToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.copyEnglishToOtherLanguagesToolStripMenuItem.Text = "Copy English to Other Languages...";
            this.copyEnglishToOtherLanguagesToolStripMenuItem.Click += new System.EventHandler(this.copyEnglishToOtherLanguagesToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.submitIssueOnGitHubToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 22);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // submitIssueOnGitHubToolStripMenuItem
            // 
            this.submitIssueOnGitHubToolStripMenuItem.Name = "submitIssueOnGitHubToolStripMenuItem";
            this.submitIssueOnGitHubToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.submitIssueOnGitHubToolStripMenuItem.Text = "Report Issue...";
            this.submitIssueOnGitHubToolStripMenuItem.ToolTipText = "Report a bug or request a feature.";
            this.submitIssueOnGitHubToolStripMenuItem.Click += new System.EventHandler(this.submitIssueOnGitHubToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // lbl_NodePath
            // 
            this.lbl_NodePath.AutoSize = true;
            this.lbl_NodePath.ContextMenuStrip = this.cntxt_KeyPath;
            this.lbl_NodePath.Location = new System.Drawing.Point(3, 0);
            this.lbl_NodePath.Name = "lbl_NodePath";
            this.lbl_NodePath.Size = new System.Drawing.Size(47, 13);
            this.lbl_NodePath.TabIndex = 3;
            this.lbl_NodePath.Text = "KeyPath";
            // 
            // cntxt_KeyPath
            // 
            this.cntxt_KeyPath.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cntxt_KeyPath.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyKeyPathToolStripMenuItem});
            this.cntxt_KeyPath.Name = "cntxt_KeyPath";
            this.cntxt_KeyPath.Size = new System.Drawing.Size(226, 26);
            // 
            // copyKeyPathToolStripMenuItem
            // 
            this.copyKeyPathToolStripMenuItem.Name = "copyKeyPathToolStripMenuItem";
            this.copyKeyPathToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.copyKeyPathToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.copyKeyPathToolStripMenuItem.Text = "Copy Key Path";
            this.copyKeyPathToolStripMenuItem.Click += new System.EventHandler(this.copyKeyPathToolStripMenuItem_Click);
            // 
            // treeView_Database
            // 
            this.treeView_Database.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView_Database.ContextMenuStrip = this.cntxt_RootLevel;
            this.treeView_Database.Enabled = false;
            this.treeView_Database.LabelEdit = true;
            this.treeView_Database.Location = new System.Drawing.Point(2, 1);
            this.treeView_Database.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.treeView_Database.Name = "treeView_Database";
            this.treeView_Database.PathSeparator = ".";
            this.treeView_Database.Size = new System.Drawing.Size(172, 550);
            this.treeView_Database.TabIndex = 0;
            this.treeView_Database.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_Database_AfterLabelEdit);
            this.treeView_Database.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_Database_AfterSelect);
            this.treeView_Database.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_Database_NodeMouseClick);
            this.treeView_Database.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView_Database_KeyDown);
            // 
            // cntxt_RootLevel
            // 
            this.cntxt_RootLevel.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cntxt_RootLevel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cntxt_RootAddKey,
            this.cntxt_RootAddScope});
            this.cntxt_RootLevel.Name = "cntxt_RootLevel";
            this.cntxt_RootLevel.Size = new System.Drawing.Size(190, 48);
            // 
            // cntxt_RootAddKey
            // 
            this.cntxt_RootAddKey.Name = "cntxt_RootAddKey";
            this.cntxt_RootAddKey.Size = new System.Drawing.Size(189, 22);
            this.cntxt_RootAddKey.Text = "Add Root Level Key";
            this.cntxt_RootAddKey.Click += new System.EventHandler(this.cntxt_RootAddKey_Click);
            // 
            // cntxt_RootAddScope
            // 
            this.cntxt_RootAddScope.Name = "cntxt_RootAddScope";
            this.cntxt_RootAddScope.Size = new System.Drawing.Size(189, 22);
            this.cntxt_RootAddScope.Text = "Add Root Level Scope";
            this.cntxt_RootAddScope.Click += new System.EventHandler(this.cntxt_RootAddScope_Click);
            // 
            // cmb_CurLanguage
            // 
            this.cmb_CurLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_CurLanguage.FormattingEnabled = true;
            this.cmb_CurLanguage.Location = new System.Drawing.Point(59, 6);
            this.cmb_CurLanguage.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmb_CurLanguage.MaxDropDownItems = 99;
            this.cmb_CurLanguage.Name = "cmb_CurLanguage";
            this.cmb_CurLanguage.Size = new System.Drawing.Size(183, 21);
            this.cmb_CurLanguage.TabIndex = 2;
            this.cmb_CurLanguage.SelectedIndexChanged += new System.EventHandler(this.cmb_CurLanguage_SelectedIndexChanged);
            // 
            // rtb_TranslatedText
            // 
            this.rtb_TranslatedText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_TranslatedText.Enabled = false;
            this.rtb_TranslatedText.Location = new System.Drawing.Point(3, 33);
            this.rtb_TranslatedText.Name = "rtb_TranslatedText";
            this.rtb_TranslatedText.Size = new System.Drawing.Size(548, 322);
            this.rtb_TranslatedText.TabIndex = 3;
            this.rtb_TranslatedText.Text = "";
            this.rtb_TranslatedText.Visible = false;
            this.rtb_TranslatedText.TextChanged += new System.EventHandler(this.rtb_TranslatedText_TextChanged);
            // 
            // rtb_Comments
            // 
            this.rtb_Comments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_Comments.Enabled = false;
            this.rtb_Comments.Location = new System.Drawing.Point(3, 16);
            this.rtb_Comments.Name = "rtb_Comments";
            this.rtb_Comments.Size = new System.Drawing.Size(211, 162);
            this.rtb_Comments.TabIndex = 4;
            this.rtb_Comments.Text = "";
            this.rtb_Comments.TextChanged += new System.EventHandler(this.rtb_Comments_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Comments:";
            // 
            // cntxt_Scope
            // 
            this.cntxt_Scope.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cntxt_Scope.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cntxt_Scope_AddKey,
            this.cntxt_Scope_AddScope,
            this.cntxt_Scope_DeleteScope,
            this.cntxt_Scope_RenameScope});
            this.cntxt_Scope.Name = "cntxt_Scope";
            this.cntxt_Scope.Size = new System.Drawing.Size(172, 92);
            // 
            // cntxt_Scope_AddKey
            // 
            this.cntxt_Scope_AddKey.Name = "cntxt_Scope_AddKey";
            this.cntxt_Scope_AddKey.Size = new System.Drawing.Size(171, 22);
            this.cntxt_Scope_AddKey.Text = "Add Key";
            this.cntxt_Scope_AddKey.Click += new System.EventHandler(this.cntxt_Scope_AddKey_Click);
            // 
            // cntxt_Scope_AddScope
            // 
            this.cntxt_Scope_AddScope.Name = "cntxt_Scope_AddScope";
            this.cntxt_Scope_AddScope.Size = new System.Drawing.Size(171, 22);
            this.cntxt_Scope_AddScope.Text = "Add Scope";
            this.cntxt_Scope_AddScope.Click += new System.EventHandler(this.cntxt_Scope_AddScope_Click);
            // 
            // cntxt_Scope_DeleteScope
            // 
            this.cntxt_Scope_DeleteScope.Name = "cntxt_Scope_DeleteScope";
            this.cntxt_Scope_DeleteScope.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.cntxt_Scope_DeleteScope.Size = new System.Drawing.Size(171, 22);
            this.cntxt_Scope_DeleteScope.Text = "Delete Scope";
            this.cntxt_Scope_DeleteScope.Click += new System.EventHandler(this.cntxt_Node_Delete_Click);
            // 
            // cntxt_Scope_RenameScope
            // 
            this.cntxt_Scope_RenameScope.Name = "cntxt_Scope_RenameScope";
            this.cntxt_Scope_RenameScope.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.cntxt_Scope_RenameScope.Size = new System.Drawing.Size(171, 22);
            this.cntxt_Scope_RenameScope.Text = "Rename Scope";
            this.cntxt_Scope_RenameScope.Click += new System.EventHandler(this.cntxt_Node_Rename_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Language";
            // 
            // cntxt_Key
            // 
            this.cntxt_Key.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cntxt_Key.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cntxt_Key_Delete,
            this.cntxt_Key_Rename});
            this.cntxt_Key.Name = "cntxt_Key";
            this.cntxt_Key.Size = new System.Drawing.Size(159, 48);
            // 
            // cntxt_Key_Delete
            // 
            this.cntxt_Key_Delete.Name = "cntxt_Key_Delete";
            this.cntxt_Key_Delete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.cntxt_Key_Delete.Size = new System.Drawing.Size(158, 22);
            this.cntxt_Key_Delete.Text = "Delete Key";
            this.cntxt_Key_Delete.Click += new System.EventHandler(this.cntxt_Node_Delete_Click);
            // 
            // cntxt_Key_Rename
            // 
            this.cntxt_Key_Rename.Name = "cntxt_Key_Rename";
            this.cntxt_Key_Rename.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.cntxt_Key_Rename.Size = new System.Drawing.Size(158, 22);
            this.cntxt_Key_Rename.Text = "Rename Key";
            this.cntxt_Key_Rename.Click += new System.EventHandler(this.cntxt_Node_Rename_Click);
            // 
            // btn_CopyEnglishValueToOtherLangs
            // 
            this.btn_CopyEnglishValueToOtherLangs.AutoSize = true;
            this.btn_CopyEnglishValueToOtherLangs.Enabled = false;
            this.btn_CopyEnglishValueToOtherLangs.Location = new System.Drawing.Point(245, 3);
            this.btn_CopyEnglishValueToOtherLangs.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_CopyEnglishValueToOtherLangs.Name = "btn_CopyEnglishValueToOtherLangs";
            this.btn_CopyEnglishValueToOtherLangs.Size = new System.Drawing.Size(202, 25);
            this.btn_CopyEnglishValueToOtherLangs.TabIndex = 14;
            this.btn_CopyEnglishValueToOtherLangs.Text = "Copy English to Other Languages...";
            this.btn_CopyEnglishValueToOtherLangs.UseVisualStyleBackColor = true;
            this.btn_CopyEnglishValueToOtherLangs.Click += new System.EventHandler(this.btn_CopyEnglishValueToOtherLangs_Click);
            // 
            // splitContainer_EnglishComments
            // 
            this.splitContainer_EnglishComments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer_EnglishComments.Location = new System.Drawing.Point(2, 3);
            this.splitContainer_EnglishComments.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.splitContainer_EnglishComments.Name = "splitContainer_EnglishComments";
            // 
            // splitContainer_EnglishComments.Panel1
            // 
            this.splitContainer_EnglishComments.Panel1.Controls.Add(this.lbl_NodePath);
            this.splitContainer_EnglishComments.Panel1.Controls.Add(this.rtb_OriginalText);
            // 
            // splitContainer_EnglishComments.Panel2
            // 
            this.splitContainer_EnglishComments.Panel2.Controls.Add(this.label1);
            this.splitContainer_EnglishComments.Panel2.Controls.Add(this.rtb_Comments);
            this.splitContainer_EnglishComments.Size = new System.Drawing.Size(551, 180);
            this.splitContainer_EnglishComments.SplitterDistance = 334;
            this.splitContainer_EnglishComments.SplitterWidth = 3;
            this.splitContainer_EnglishComments.TabIndex = 15;
            // 
            // rtb_OriginalText
            // 
            this.rtb_OriginalText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_OriginalText.Enabled = false;
            this.rtb_OriginalText.Location = new System.Drawing.Point(3, 16);
            this.rtb_OriginalText.Name = "rtb_OriginalText";
            this.rtb_OriginalText.Size = new System.Drawing.Size(329, 162);
            this.rtb_OriginalText.TabIndex = 4;
            this.rtb_OriginalText.Text = "";
            this.rtb_OriginalText.TextChanged += new System.EventHandler(this.rtb_OriginalText_TextChanged);
            // 
            // splitContainer_Main
            // 
            this.splitContainer_Main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer_Main.Location = new System.Drawing.Point(9, 23);
            this.splitContainer_Main.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.splitContainer_Main.Name = "splitContainer_Main";
            // 
            // splitContainer_Main.Panel1
            // 
            this.splitContainer_Main.Panel1.Controls.Add(this.treeView_Database);
            // 
            // splitContainer_Main.Panel2
            // 
            this.splitContainer_Main.Panel2.Controls.Add(this.splitContainer_Languages);
            this.splitContainer_Main.Size = new System.Drawing.Size(737, 552);
            this.splitContainer_Main.SplitterDistance = 175;
            this.splitContainer_Main.SplitterWidth = 3;
            this.splitContainer_Main.TabIndex = 16;
            // 
            // splitContainer_Languages
            // 
            this.splitContainer_Languages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer_Languages.Location = new System.Drawing.Point(2, 3);
            this.splitContainer_Languages.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.splitContainer_Languages.Name = "splitContainer_Languages";
            this.splitContainer_Languages.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_Languages.Panel1
            // 
            this.splitContainer_Languages.Panel1.Controls.Add(this.splitContainer_EnglishComments);
            // 
            // splitContainer_Languages.Panel2
            // 
            this.splitContainer_Languages.Panel2.Controls.Add(this.button1);
            this.splitContainer_Languages.Panel2.Controls.Add(this.label2);
            this.splitContainer_Languages.Panel2.Controls.Add(this.cmb_CurLanguage);
            this.splitContainer_Languages.Panel2.Controls.Add(this.rtb_TranslatedText);
            this.splitContainer_Languages.Panel2.Controls.Add(this.btn_CopyEnglishValueToOtherLangs);
            this.splitContainer_Languages.Size = new System.Drawing.Size(555, 547);
            this.splitContainer_Languages.SplitterDistance = 186;
            this.splitContainer_Languages.SplitterWidth = 3;
            this.splitContainer_Languages.TabIndex = 17;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(468, 6);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 19);
            this.button1.TabIndex = 15;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 586);
            this.Controls.Add(this.splitContainer_Main);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "MainForm";
            this.Text = "Zero Localization Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.cntxt_KeyPath.ResumeLayout(false);
            this.cntxt_RootLevel.ResumeLayout(false);
            this.cntxt_Scope.ResumeLayout(false);
            this.cntxt_Key.ResumeLayout(false);
            this.splitContainer_EnglishComments.Panel1.ResumeLayout(false);
            this.splitContainer_EnglishComments.Panel1.PerformLayout();
            this.splitContainer_EnglishComments.Panel2.ResumeLayout(false);
            this.splitContainer_EnglishComments.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_EnglishComments)).EndInit();
            this.splitContainer_EnglishComments.ResumeLayout(false);
            this.splitContainer_Main.Panel1.ResumeLayout(false);
            this.splitContainer_Main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Main)).EndInit();
            this.splitContainer_Main.ResumeLayout(false);
            this.splitContainer_Languages.Panel1.ResumeLayout(false);
            this.splitContainer_Languages.Panel2.ResumeLayout(false);
            this.splitContainer_Languages.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Languages)).EndInit();
            this.splitContainer_Languages.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_File;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.TreeView treeView_Database;
		private System.Windows.Forms.Label lbl_NodePath;
        private System.Windows.Forms.ComboBox cmb_CurLanguage;
        private System.Windows.Forms.RichTextBox rtb_TranslatedText;
        private System.Windows.Forms.RichTextBox rtb_Comments;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip cntxt_Scope;
        private System.Windows.Forms.ToolStripMenuItem cntxt_Scope_AddKey;
        private System.Windows.Forms.ToolStripMenuItem cntxt_Scope_AddScope;
        private System.Windows.Forms.ToolStripMenuItem cntxt_Scope_DeleteScope;
        private System.Windows.Forms.ToolStripMenuItem cntxt_Scope_RenameScope;
        private System.Windows.Forms.ContextMenuStrip cntxt_RootLevel;
        private System.Windows.Forms.ToolStripMenuItem cntxt_RootAddKey;
        private System.Windows.Forms.ToolStripMenuItem cntxt_RootAddScope;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip cntxt_Key;
        private System.Windows.Forms.ToolStripMenuItem cntxt_Key_Delete;
        private System.Windows.Forms.ToolStripMenuItem cntxt_Key_Rename;
        private System.Windows.Forms.ToolStripMenuItem keysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addScopeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addKeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findReplaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyEnglishToOtherLanguagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem submitIssueOnGitHubToolStripMenuItem;
        private System.Windows.Forms.Button btn_CopyEnglishValueToOtherLangs;
        private System.Windows.Forms.SplitContainer splitContainer_EnglishComments;
        private System.Windows.Forms.RichTextBox rtb_OriginalText;
        private System.Windows.Forms.SplitContainer splitContainer_Main;
        private System.Windows.Forms.SplitContainer splitContainer_Languages;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip cntxt_KeyPath;
        private System.Windows.Forms.ToolStripMenuItem copyKeyPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyKeyPathToolStripMenuItem1;
    }
}

