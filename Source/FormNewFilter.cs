using System.Windows.Forms;
using System.Linq;
using woanware;
using System.Collections.Generic;
using System;

namespace RegRipperRunner
{
    public partial class FormNewFilter : Form
    {
        #region Member Variables
        private string _pluginDir;
        private List<string> _filters;
        private List<string> _plugins;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pluginDir"></param>
        /// <param name="plugins"></param>
        public FormNewFilter(string pluginDir, List<string> plugins)
        {
            InitializeComponent();

            _pluginDir = pluginDir;
            _plugins = plugins;

            using (new HourGlass(this))
            {
                _filters = Functions.GetAllFilters(pluginDir);
            }
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
            if (txtFilter.Text.Trim().Length == 0)
            {
                UserInterface.DisplayMessageBox(this, "The filter name must be entered", MessageBoxIcon.Exclamation);
                txtFilter.Select();
                return;
            }

            var count = (from f in _filters where f.ToLower() == txtFilter.Text.ToLower() select f).Count();
            if (count > 0)
            {
                UserInterface.DisplayMessageBox(this, "The filter already exists", MessageBoxIcon.Exclamation);
                txtFilter.Select();
                return;
            }

            using (new HourGlass(this))
            {
                string temp = string.Join(Environment.NewLine, _plugins);
                string ret = IO.WriteTextToFile(temp, System.IO.Path.Combine(_pluginDir, txtFilter.Text), false);
                if (ret.Length > 0)
                {
                    UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst saving the filter: " + ret);
                    txtFilter.Select();
                    return;
                }
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
        public string Filter
        {
            get
            {
                return txtFilter.Text;
            }
        }
        #endregion
    }
}
