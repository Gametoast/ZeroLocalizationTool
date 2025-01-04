
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
			this.rtb_KeyValue = new System.Windows.Forms.RichTextBox();
			this.treeView_Database = new System.Windows.Forms.BetterTreeView();
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
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
			this.menuStrip1.Size = new System.Drawing.Size(777, 24);
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
			this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.openToolStripMenuItem.Text = "Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.saveToolStripMenuItem.Text = "Save";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			// 
			// lbl_NodePath
			// 
			this.lbl_NodePath.AutoSize = true;
			this.lbl_NodePath.Location = new System.Drawing.Point(237, 28);
			this.lbl_NodePath.Name = "lbl_NodePath";
			this.lbl_NodePath.Size = new System.Drawing.Size(35, 13);
			this.lbl_NodePath.TabIndex = 3;
			this.lbl_NodePath.Text = "label1";
			// 
			// rtb_KeyValue
			// 
			this.rtb_KeyValue.Enabled = false;
			this.rtb_KeyValue.Location = new System.Drawing.Point(237, 45);
			this.rtb_KeyValue.Name = "rtb_KeyValue";
			this.rtb_KeyValue.Size = new System.Drawing.Size(528, 210);
			this.rtb_KeyValue.TabIndex = 4;
			this.rtb_KeyValue.Text = "";
			// 
			// treeView_Database
			// 
			this.treeView_Database.Location = new System.Drawing.Point(9, 24);
			this.treeView_Database.Margin = new System.Windows.Forms.Padding(2);
			this.treeView_Database.Name = "treeView_Database";
			this.treeView_Database.PathSeparator = ".";
			this.treeView_Database.Size = new System.Drawing.Size(222, 499);
			this.treeView_Database.TabIndex = 2;
			this.treeView_Database.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_Database_NodeMouseClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(777, 530);
			this.Controls.Add(this.rtb_KeyValue);
			this.Controls.Add(this.lbl_NodePath);
			this.Controls.Add(this.treeView_Database);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
		private System.Windows.Forms.BetterTreeView treeView_Database;
		private System.Windows.Forms.Label lbl_NodePath;
		private System.Windows.Forms.RichTextBox rtb_KeyValue;
	}
}

