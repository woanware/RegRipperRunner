using System.Windows.Forms;
using woanware;

namespace RegRipperRunner
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormFolder : Form
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public FormFolder()
        {
            InitializeComponent();
        }
        #endregion

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (txtFolder.Text.Trim().Length == 0)
            {
                UserInterface.DisplayMessageBox(this, "The folder must be supplied", MessageBoxIcon.Exclamation);
                txtFolder.Select();
                return;
            }

            if (System.IO.Directory.Exists(txtFolder.Text) == false)
            {
                UserInterface.DisplayMessageBox(this, "The folder does not exist", MessageBoxIcon.Exclamation);
                txtFolder.Select();
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string Folder
        {
            get
            {
                return txtFolder.Text;
            }
        }
        #endregion
    }
}
