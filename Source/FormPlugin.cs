using System.IO;
using System.Windows.Forms;

namespace RegRipperRunner
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormPlugin : Form
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pluginFile"></param>
        public FormPlugin(string pluginDir, string pluginFile)
        {
            InitializeComponent();

            txtPlugin.Text = File.ReadAllText(Path.Combine(pluginDir, pluginFile));
        }
        #endregion

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        #endregion
    }
}
