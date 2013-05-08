using System.IO;
using System.Windows.Forms;
using woanware;

namespace RegRipperRunner
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormOptions : Form
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public FormOptions(Settings setting)
        {
            InitializeComponent();

            txtRegRipperDir.Text = setting.RegRipperDir;
        }
        #endregion

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (txtRegRipperDir.Text.Trim().Length == 0)
            {
                UserInterface.DisplayMessageBox(this, "The path to the regripper rip.exe must be supplied", MessageBoxIcon.Exclamation);
                return;
            }

            if (File.Exists(System.IO.Path.Combine(txtRegRipperDir.Text, Global.REGRIPPER_EXE)) == false)
            {
                UserInterface.DisplayMessageBox(this, "The supplied path does not appear to be valid", MessageBoxIcon.Exclamation);
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, System.EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Select the folder containing the rip.exe file";

            if (System.IO.Directory.Exists(txtRegRipperDir.Text) == true)
            {
                folderBrowserDialog.SelectedPath = txtRegRipperDir.Text;
            }

            if (folderBrowserDialog.ShowDialog(this) == DialogResult.Cancel)
            {
                return;
            }

            txtRegRipperDir.Text = folderBrowserDialog.SelectedPath;
        }
        #endregion

        #region
        /// <summary>
        /// 
        /// </summary>
        public string RegRipperDir
        {
            get { return txtRegRipperDir.Text; }
        }
        #endregion
    }
}
