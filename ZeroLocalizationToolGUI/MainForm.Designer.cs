
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_NodePath = new System.Windows.Forms.Label();
            this.rtb_OriginalText = new System.Windows.Forms.RichTextBox();
            this.treeView_Database = new System.Windows.Forms.TreeView();
            this.cmb_CurLanguage = new System.Windows.Forms.ComboBox();
            this.rtb_TranslatedText = new System.Windows.Forms.RichTextBox();
            this.rtb_Comments = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
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
            this.menuStrip1.Size = new System.Drawing.Size(1228, 30);
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
            this.treeView_Database.LabelEdit = true;
            this.treeView_Database.Location = new System.Drawing.Point(12, 30);
            this.treeView_Database.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.treeView_Database.Name = "treeView_Database";
            this.treeView_Database.PathSeparator = ".";
            this.treeView_Database.Size = new System.Drawing.Size(295, 613);
            this.treeView_Database.TabIndex = 2;
            this.treeView_Database.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_Database_AfterSelect);
            // 
            // cmb_CurLanguage
            // 
            this.cmb_CurLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_CurLanguage.FormattingEnabled = true;
            this.cmb_CurLanguage.Location = new System.Drawing.Point(319, 321);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 652);
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
    }
}

