namespace ZeroLocalizationToolGUI.Forms
{
    partial class FindForm
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
            this.cmb_SearchExpression = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chk_MatchWholeExpression = new System.Windows.Forms.CheckBox();
            this.chk_MatchCase = new System.Windows.Forms.CheckBox();
            this.chklist_Languages = new System.Windows.Forms.CheckedListBox();
            this.lbl_LanguagesToSearch = new System.Windows.Forms.Label();
            this.btn_FindAll = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rad_TranslationText = new System.Windows.Forms.RadioButton();
            this.rad_KeyScopeNames = new System.Windows.Forms.RadioButton();
            this.list_Results = new System.Windows.Forms.ListView();
            this.ResultLanguage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ResultPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ResultText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmb_SearchExpression
            // 
            this.cmb_SearchExpression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_SearchExpression.FormattingEnabled = true;
            this.cmb_SearchExpression.Location = new System.Drawing.Point(84, 18);
            this.cmb_SearchExpression.Name = "cmb_SearchExpression";
            this.cmb_SearchExpression.Size = new System.Drawing.Size(579, 24);
            this.cmb_SearchExpression.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Find what:";
            // 
            // chk_MatchWholeExpression
            // 
            this.chk_MatchWholeExpression.AutoSize = true;
            this.chk_MatchWholeExpression.Location = new System.Drawing.Point(12, 138);
            this.chk_MatchWholeExpression.Name = "chk_MatchWholeExpression";
            this.chk_MatchWholeExpression.Size = new System.Drawing.Size(200, 20);
            this.chk_MatchWholeExpression.TabIndex = 2;
            this.chk_MatchWholeExpression.Text = "Match whole expression only";
            this.chk_MatchWholeExpression.UseVisualStyleBackColor = true;
            // 
            // chk_MatchCase
            // 
            this.chk_MatchCase.AutoSize = true;
            this.chk_MatchCase.Location = new System.Drawing.Point(12, 164);
            this.chk_MatchCase.Name = "chk_MatchCase";
            this.chk_MatchCase.Size = new System.Drawing.Size(98, 20);
            this.chk_MatchCase.TabIndex = 3;
            this.chk_MatchCase.Text = "Match case";
            this.chk_MatchCase.UseVisualStyleBackColor = true;
            // 
            // chklist_Languages
            // 
            this.chklist_Languages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chklist_Languages.FormattingEnabled = true;
            this.chklist_Languages.Location = new System.Drawing.Point(642, 74);
            this.chklist_Languages.Name = "chklist_Languages";
            this.chklist_Languages.Size = new System.Drawing.Size(171, 191);
            this.chklist_Languages.TabIndex = 5;
            // 
            // lbl_LanguagesToSearch
            // 
            this.lbl_LanguagesToSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_LanguagesToSearch.AutoSize = true;
            this.lbl_LanguagesToSearch.Location = new System.Drawing.Point(642, 49);
            this.lbl_LanguagesToSearch.Name = "lbl_LanguagesToSearch";
            this.lbl_LanguagesToSearch.Size = new System.Drawing.Size(136, 16);
            this.lbl_LanguagesToSearch.TabIndex = 5;
            this.lbl_LanguagesToSearch.Text = "Languages to search:";
            // 
            // btn_FindAll
            // 
            this.btn_FindAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_FindAll.Location = new System.Drawing.Point(669, 15);
            this.btn_FindAll.Name = "btn_FindAll";
            this.btn_FindAll.Size = new System.Drawing.Size(144, 28);
            this.btn_FindAll.TabIndex = 8;
            this.btn_FindAll.Text = "Find All";
            this.btn_FindAll.UseVisualStyleBackColor = true;
            this.btn_FindAll.Click += new System.EventHandler(this.btn_FindAll_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rad_TranslationText);
            this.groupBox1.Controls.Add(this.rad_KeyScopeNames);
            this.groupBox1.Location = new System.Drawing.Point(12, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 84);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search for:";
            // 
            // rad_TranslationText
            // 
            this.rad_TranslationText.AutoSize = true;
            this.rad_TranslationText.Location = new System.Drawing.Point(7, 49);
            this.rad_TranslationText.Name = "rad_TranslationText";
            this.rad_TranslationText.Size = new System.Drawing.Size(118, 20);
            this.rad_TranslationText.TabIndex = 1;
            this.rad_TranslationText.Text = "Translation text";
            this.rad_TranslationText.UseVisualStyleBackColor = true;
            this.rad_TranslationText.CheckedChanged += new System.EventHandler(this.rad_TranslationText_CheckedChanged);
            // 
            // rad_KeyScopeNames
            // 
            this.rad_KeyScopeNames.AutoSize = true;
            this.rad_KeyScopeNames.Checked = true;
            this.rad_KeyScopeNames.Location = new System.Drawing.Point(7, 22);
            this.rad_KeyScopeNames.Name = "rad_KeyScopeNames";
            this.rad_KeyScopeNames.Size = new System.Drawing.Size(139, 20);
            this.rad_KeyScopeNames.TabIndex = 0;
            this.rad_KeyScopeNames.TabStop = true;
            this.rad_KeyScopeNames.Text = "Key/Scope names";
            this.rad_KeyScopeNames.UseVisualStyleBackColor = true;
            this.rad_KeyScopeNames.CheckedChanged += new System.EventHandler(this.rad_KeyScopeNames_CheckedChanged);
            // 
            // list_Results
            // 
            this.list_Results.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_Results.AutoArrange = false;
            this.list_Results.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ResultLanguage,
            this.ResultPath,
            this.ResultText});
            this.list_Results.FullRowSelect = true;
            this.list_Results.GridLines = true;
            this.list_Results.HideSelection = false;
            this.list_Results.Location = new System.Drawing.Point(12, 271);
            this.list_Results.Name = "list_Results";
            this.list_Results.Size = new System.Drawing.Size(801, 212);
            this.list_Results.TabIndex = 4;
            this.list_Results.UseCompatibleStateImageBehavior = false;
            this.list_Results.View = System.Windows.Forms.View.Details;
            this.list_Results.ItemActivate += new System.EventHandler(this.list_Results_ItemActivate);
            // 
            // ResultLanguage
            // 
            this.ResultLanguage.Text = "Language";
            this.ResultLanguage.Width = 110;
            // 
            // ResultPath
            // 
            this.ResultPath.Text = "Path";
            this.ResultPath.Width = 250;
            // 
            // ResultText
            // 
            this.ResultText.Text = "Text";
            this.ResultText.Width = 400;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 249);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Search results:";
            // 
            // FindForm
            // 
            this.AcceptButton = this.btn_FindAll;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 495);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.list_Results);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_FindAll);
            this.Controls.Add(this.lbl_LanguagesToSearch);
            this.Controls.Add(this.chklist_Languages);
            this.Controls.Add(this.chk_MatchCase);
            this.Controls.Add(this.chk_MatchWholeExpression);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_SearchExpression);
            this.Name = "FindForm";
            this.Text = "Find";
            this.Load += new System.EventHandler(this.FindForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_SearchExpression;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chk_MatchWholeExpression;
        private System.Windows.Forms.CheckBox chk_MatchCase;
        private System.Windows.Forms.CheckedListBox chklist_Languages;
        private System.Windows.Forms.Label lbl_LanguagesToSearch;
        private System.Windows.Forms.Button btn_FindAll;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rad_TranslationText;
        private System.Windows.Forms.RadioButton rad_KeyScopeNames;
        private System.Windows.Forms.ListView list_Results;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader ResultLanguage;
        private System.Windows.Forms.ColumnHeader ResultPath;
        private System.Windows.Forms.ColumnHeader ResultText;
    }
}