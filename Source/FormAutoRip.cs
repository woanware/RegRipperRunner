using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using woanware;

namespace RegRipperRunner
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormAutoRip : Form
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public FormAutoRip()
        {
            InitializeComponent();
        }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormAutoRip_Load(object sender, EventArgs e)
        {
            AddListCategory("os", "gets general operating system information");
            AddListCategory("users", "gets user account information");
            AddListCategory("software", "gets installed software information");
            AddListCategory("network", "gets networking configuration information");
            AddListCategory("storage", "gets storage information");
            AddListCategory("execution", "gets program execution information");
            AddListCategory("autoruns", "gets autostart locations information");
            AddListCategory("log", "gets logging information");
            AddListCategory("web", "gets web browsing information");
            AddListCategory("user_config", "gets user account configuration information");
            AddListCategory("user_act", "gets user account general activity");
            AddListCategory("user_network", "gets user account network activity");
            AddListCategory("user_file", "gets user account file/folder access activity");
            AddListCategory("user_virtual", "gets user account virtualization access activity");
            AddListCategory("comm", "gets communication software information");

            UserInterface.AutoSizeListViewColumns(listCategories);
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        private void AddListCategory(string name, string desc)
        {
            ListViewItem listViewItem = new ListViewItem(new string[]{name, desc});
            listCategories.Items.Add(listViewItem);
        }
        #endregion

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtOutput.Text.Length == 0)
            {
                UserInterface.DisplayMessageBox(this, "The output directory must be supplied", MessageBoxIcon.Exclamation);
                btnOutput.Select();
                return;
            }

            if (System.IO.Directory.Exists(txtOutput.Text) == false)
            {
                UserInterface.DisplayMessageBox(this, "The output directory does not exist", MessageBoxIcon.Exclamation);
                btnOutput.Select();
                return;
            }

            if (chkAll.Checked == false)
            {
                int count = UserInterface.GetNumberOfCheckedListviewItems(listCategories);
                if (count == 0)
                {
                    UserInterface.DisplayMessageBox(this, "At least one category must be checked", MessageBoxIcon.Exclamation);
                    listCategories.Select();
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOutput_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select the output folder";
            if (fbd.ShowDialog(this) == DialogResult.Cancel)
            {
                return;
            }

            txtOutput.Text = fbd.SelectedPath;
        }
        #endregion

        #region Checkbox Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            listCategories.Enabled = ! chkAll.Checked;
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string OutputDir
        {
            get { return txtOutput.Text; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string InputDir
        {
            get { return txtInput.Text; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Categories
        {
            get
            {
                if (chkAll.Checked == true)
                {
                    return new List<string> { "all" };
                }

                List<string> categories = new List<string>();
                foreach (ListViewItem listViewItem in listCategories.CheckedItems)
                {
                    categories.Add(listViewItem.Text);
                }

                return categories;
            }
        }
        #endregion

        private void btnInput_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select the input folder";
            if (fbd.ShowDialog(this) == DialogResult.Cancel)
            {
                return;
            }

            txtInput.Text = fbd.SelectedPath;
        }

        
    }
}
