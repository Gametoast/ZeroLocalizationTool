
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_NodePath = new System.Windows.Forms.Label();
            this.rtb_OriginalText = new System.Windows.Forms.RichTextBox();
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
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.cntxt_RootLevel.SuspendLayout();
            this.cntxt_Scope.SuspendLayout();
            this.cntxt_Key.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_File});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1228, 26);
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
            this.toolStripMenuItem_File.Size = new System.Drawing.Size(46, 24);
            this.toolStripMenuItem_File.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // lbl_NodePath
            // 
            this.lbl_NodePath.AutoSize = true;
            this.lbl_NodePath.Location = new System.Drawing.Point(316, 34);
            this.lbl_NodePath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_NodePath.Name = "lbl_NodePath";
            this.lbl_NodePath.Size = new System.Drawing.Size(44, 16);
            this.lbl_NodePath.TabIndex = 3;
            this.lbl_NodePath.Text = "label1";
            // 
            // rtb_OriginalText
            // 
            this.rtb_OriginalText.Enabled = false;
            this.rtb_OriginalText.Location = new System.Drawing.Point(316, 55);
            this.rtb_OriginalText.Margin = new System.Windows.Forms.Padding(4);
            this.rtb_OriginalText.Name = "rtb_OriginalText";
            this.rtb_OriginalText.Size = new System.Drawing.Size(446, 258);
            this.rtb_OriginalText.TabIndex = 4;
            this.rtb_OriginalText.Text = "";
            this.rtb_OriginalText.TextChanged += new System.EventHandler(this.rtb_OriginalText_TextChanged);
            // 
            // treeView_Database
            // 
            this.treeView_Database.ContextMenuStrip = this.cntxt_RootLevel;
            this.treeView_Database.LabelEdit = true;
            this.treeView_Database.Location = new System.Drawing.Point(12, 30);
            this.treeView_Database.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.treeView_Database.Name = "treeView_Database";
            this.treeView_Database.PathSeparator = ".";
            this.treeView_Database.Size = new System.Drawing.Size(295, 613);
            this.treeView_Database.TabIndex = 2;
            this.treeView_Database.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_Database_AfterLabelEdit);
            this.treeView_Database.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_Database_AfterSelect);
            this.treeView_Database.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_Database_NodeMouseClick);
            // 
            // cntxt_RootLevel
            // 
            this.cntxt_RootLevel.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cntxt_RootLevel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cntxt_RootAddKey,
            this.cntxt_RootAddScope});
            this.cntxt_RootLevel.Name = "cntxt_RootLevel";
            this.cntxt_RootLevel.Size = new System.Drawing.Size(226, 52);
            // 
            // cntxt_RootAddKey
            // 
            this.cntxt_RootAddKey.Name = "cntxt_RootAddKey";
            this.cntxt_RootAddKey.Size = new System.Drawing.Size(225, 24);
            this.cntxt_RootAddKey.Text = "Add Root Level Key";
            this.cntxt_RootAddKey.Click += new System.EventHandler(this.cntxt_RootAddKey_Click);
            // 
            // cntxt_RootAddScope
            // 
            this.cntxt_RootAddScope.Name = "cntxt_RootAddScope";
            this.cntxt_RootAddScope.Size = new System.Drawing.Size(225, 24);
            this.cntxt_RootAddScope.Text = "Add Root Level Scope";
            this.cntxt_RootAddScope.Click += new System.EventHandler(this.cntxt_RootAddScope_Click);
            // 
            // cmb_CurLanguage
            // 
            this.cmb_CurLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_CurLanguage.FormattingEnabled = true;
            this.cmb_CurLanguage.Location = new System.Drawing.Point(390, 318);
            this.cmb_CurLanguage.MaxDropDownItems = 99;
            this.cmb_CurLanguage.Name = "cmb_CurLanguage";
            this.cmb_CurLanguage.Size = new System.Drawing.Size(242, 24);
            this.cmb_CurLanguage.TabIndex = 5;
            this.cmb_CurLanguage.SelectedIndexChanged += new System.EventHandler(this.cmb_CurLanguage_SelectedIndexChanged);
            // 
            // rtb_TranslatedText
            // 
            this.rtb_TranslatedText.Enabled = false;
            this.rtb_TranslatedText.Location = new System.Drawing.Point(316, 352);
            this.rtb_TranslatedText.Margin = new System.Windows.Forms.Padding(4);
            this.rtb_TranslatedText.Name = "rtb_TranslatedText";
            this.rtb_TranslatedText.Size = new System.Drawing.Size(446, 258);
            this.rtb_TranslatedText.TabIndex = 6;
            this.rtb_TranslatedText.Text = "";
            this.rtb_TranslatedText.Visible = false;
            this.rtb_TranslatedText.TextChanged += new System.EventHandler(this.rtb_TranslatedText_TextChanged);
            // 
            // rtb_Comments
            // 
            this.rtb_Comments.Enabled = false;
            this.rtb_Comments.Location = new System.Drawing.Point(770, 55);
            this.rtb_Comments.Margin = new System.Windows.Forms.Padding(4);
            this.rtb_Comments.Name = "rtb_Comments";
            this.rtb_Comments.Size = new System.Drawing.Size(446, 258);
            this.rtb_Comments.TabIndex = 7;
            this.rtb_Comments.Text = "";
            this.rtb_Comments.TextChanged += new System.EventHandler(this.rtb_Comments_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(770, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 16);
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
            this.cntxt_Scope.Size = new System.Drawing.Size(178, 100);
            // 
            // cntxt_Scope_AddKey
            // 
            this.cntxt_Scope_AddKey.Name = "cntxt_Scope_AddKey";
            this.cntxt_Scope_AddKey.Size = new System.Drawing.Size(210, 24);
            this.cntxt_Scope_AddKey.Text = "Add Key";
            this.cntxt_Scope_AddKey.Click += new System.EventHandler(this.cntxt_Scope_AddKey_Click);
            // 
            // cntxt_Scope_AddScope
            // 
            this.cntxt_Scope_AddScope.Name = "cntxt_Scope_AddScope";
            this.cntxt_Scope_AddScope.Size = new System.Drawing.Size(210, 24);
            this.cntxt_Scope_AddScope.Text = "Add Scope";
            this.cntxt_Scope_AddScope.Click += new System.EventHandler(this.cntxt_Scope_AddScope_Click);
            // 
            // cntxt_Scope_DeleteScope
            // 
            this.cntxt_Scope_DeleteScope.Name = "cntxt_Scope_DeleteScope";
            this.cntxt_Scope_DeleteScope.Size = new System.Drawing.Size(210, 24);
            this.cntxt_Scope_DeleteScope.Text = "Delete Scope";
            this.cntxt_Scope_DeleteScope.Click += new System.EventHandler(this.cntxt_Scope_DeleteScope_Click);
            // 
            // cntxt_Scope_RenameScope
            // 
            this.cntxt_Scope_RenameScope.Name = "cntxt_Scope_RenameScope";
            this.cntxt_Scope_RenameScope.Size = new System.Drawing.Size(210, 24);
            this.cntxt_Scope_RenameScope.Text = "Rename Scope";
            this.cntxt_Scope_RenameScope.Click += new System.EventHandler(this.cntxt_Scope_RenameScope_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(316, 321);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 16);
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
            this.cntxt_Key.Size = new System.Drawing.Size(161, 52);
            // 
            // cntxt_Key_Delete
            // 
            this.cntxt_Key_Delete.Name = "cntxt_Key_Delete";
            this.cntxt_Key_Delete.Size = new System.Drawing.Size(210, 24);
            this.cntxt_Key_Delete.Text = "Delete Key";
            this.cntxt_Key_Delete.Click += new System.EventHandler(this.cntxt_Key_Delete_Click);
            // 
            // cntxt_Key_Rename
            // 
            this.cntxt_Key_Rename.Name = "cntxt_Key_Rename";
            this.cntxt_Key_Rename.Size = new System.Drawing.Size(210, 24);
            this.cntxt_Key_Rename.Text = "Rename Key";
            this.cntxt_Key_Rename.Click += new System.EventHandler(this.cntxt_Key_Rename_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(808, 376);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 652);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtb_Comments);
            this.Controls.Add(this.rtb_TranslatedText);
            this.Controls.Add(this.cmb_CurLanguage);
            this.Controls.Add(this.rtb_OriginalText);
            this.Controls.Add(this.lbl_NodePath);
            this.Controls.Add(this.treeView_Database);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "Zero Localization Tool";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.cntxt_RootLevel.ResumeLayout(false);
            this.cntxt_Scope.ResumeLayout(false);
            this.cntxt_Key.ResumeLayout(false);
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
		private System.Windows.Forms.RichTextBox rtb_OriginalText;
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
        private System.Windows.Forms.Button button1;
    }
}

