using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZeroLocalizationToolShared.Modules;

namespace ZeroLocalizationToolGUI.Forms
{
    public partial class FindForm : Form
    {
        public MainForm mainForm;
        int resultLanguageWidth;
        int resultTextWidth;

        public FindForm()
        {
            InitializeComponent();

            formToolTips.AutoPopDelay = 10000;
            formToolTips.SetToolTip(chk_MatchCase, "Only show results that match the case of each character exactly.");
            formToolTips.SetToolTip(chk_MatchWholeExpression, "Only show results that match the entire search expression as written.");
            formToolTips.SetToolTip(chk_UseRegex, "Use Regular Expression for more powerful results filtering. See https://regexone.com/ or ask an LLM for help with Regular Expressions.");
        }

        private void FindForm_Load(object sender, EventArgs e)
        {
            PopulateLanguages();

            lbl_LanguagesToSearch.Visible = rad_TranslationText.Checked;
            chklist_Languages.Visible = rad_TranslationText.Checked;
            chk_MatchCase.Visible = rad_TranslationText.Checked;

            resultLanguageWidth = ResultLanguage.Width;
            resultTextWidth = ResultText.Width;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter &&
                list_Results.Focused &&
                list_Results.SelectedItems.Count > 0)
            {
                JumpToSelectedItem();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        void PopulateLanguages()
        {
            chklist_Languages.Items.Clear();
            if (mainForm.commentsConfig != null)
            {
                chklist_Languages.Items.Add("comments", true);
            }

            foreach (string lang in mainForm.localizationConfigs.Keys)
            {
                if (mainForm.localizationConfigs[lang] != null)
                {
                    chklist_Languages.Items.Add(lang, true);
                }
            }
        }

        private void rad_TranslationText_CheckedChanged(object sender, EventArgs e)
        {
            lbl_LanguagesToSearch.Visible = rad_TranslationText.Checked;
            chklist_Languages.Visible = rad_TranslationText.Checked;
            chk_MatchCase.Visible = rad_TranslationText.Checked;
        }

        private void btn_FindAll_Click(object sender, EventArgs e)
        {
            if (cmb_SearchExpression.Text != string.Empty)
            {
                if (chk_UseRegex.Checked)
                {
                    try
                    {
                        Regex regex = new Regex(cmb_SearchExpression.Text);
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("The Regular Expression supplied is invalid.", "Regular Expression Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                list_Results.Items.Clear();
                BeginFindAll();
            }
        }

        void BeginFindAll()
        {
            if (cmb_SearchExpression.Items.Count > 0)
            {
                if (cmb_SearchExpression.Items[0] as string != cmb_SearchExpression.Text)
                {
                    cmb_SearchExpression.Items.Insert(0, cmb_SearchExpression.Text);
                }
            }
            else
            {
                cmb_SearchExpression.Items.Insert(0, cmb_SearchExpression.Text);
            }

            if (rad_KeyScopeNames.Checked)
            {
                MainForm.NodeNameSearchQuery searchQuery = new MainForm.NodeNameSearchQuery()
                {
                    Expression = cmb_SearchExpression.Text.ToLower(),
                    MatchWholeExpression = chk_MatchWholeExpression.Checked,
                    IsRegex = chk_UseRegex.Checked
                };

                List<MainForm.NodeNameSearchResult> results = mainForm.BeginNodeNameSearch(searchQuery);
                if (results.Count > 0)
                {
                    PopulateResults(results);
                }
            }
            else
            {
                List<string> langs = new List<string>();

                foreach (string lang in chklist_Languages.CheckedItems)
                {
                    langs.Add(lang);
                }

                MainForm.TranslationSearchQuery searchQuery = new MainForm.TranslationSearchQuery()
                {
                    Expression = cmb_SearchExpression.Text,
                    MatchWholeExpression = chk_MatchWholeExpression.Checked,
                    MatchCase = chk_MatchCase.Checked,
                    IsRegex = chk_UseRegex.Checked,
                    Languages = langs.ToArray()
                };

                List<MainForm.TranslationSearchResult> results = mainForm.BeginTranslationSearch(searchQuery);
                if (results.Count > 0)
                {
                    PopulateResults(results);
                }
            }
            list_Results.Focus();
        }

        void PopulateResults(List<MainForm.NodeNameSearchResult> results)
        {
            list_Results.Columns.Remove(ResultLanguage);
            list_Results.Columns.Remove(ResultText);

            foreach (MainForm.NodeNameSearchResult result in results)
            {
                list_Results.Items.Add(result.NodePath, 0);
            }

            list_Results.Columns[0].Width = -1;
        }

        void PopulateResults(List<MainForm.TranslationSearchResult> results)
        {
            if (list_Results.Columns.Count == 1)
            {
                list_Results.Columns.Insert(0, ResultLanguage);
                list_Results.Columns.Insert(2, ResultText);
            }

            foreach (MainForm.TranslationSearchResult result in results)
            {
                list_Results.Items.Add(new ListViewItem(new string[] { result.Language, result.NodePath, result.Text }));
            }

            list_Results.Columns[0].Width = -1;
            list_Results.Columns[0].Width += 15;
            list_Results.Columns[1].Width = -1;
            list_Results.Columns[1].Width += 15;
            list_Results.Columns[2].Width = -1;

            // todo: make autosizing into a function and add a minimum clamp
        }

        private void list_Results_ItemActivate(object sender, EventArgs e)
        {
            JumpToSelectedItem();
        }

        private void rad_KeyScopeNames_CheckedChanged(object sender, EventArgs e)
        {
            resultLanguageWidth = ResultLanguage.Width;
            resultTextWidth = ResultText.Width;
        }

        void JumpToSelectedItem()
        {
            if (list_Results.Columns.Count == 1)
            {
                mainForm.JumpToTreeViewNode(chk_ChangeFocus.Checked, list_Results.SelectedItems[0].SubItems[0].Text);
            }
            else
            {
                mainForm.JumpToTreeViewNode(chk_ChangeFocus.Checked, list_Results.SelectedItems[0].SubItems[1].Text, list_Results.SelectedItems[0].SubItems[0].Text);
            }
        }
    }
}
