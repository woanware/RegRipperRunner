using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using woanware;

namespace RegRipperRunner
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormMain : Form
    {        
        #region Member Variables
        private Settings _settings;
        private string _regRipper;
        private string _regRipperPlugins;
        private Global.Mode _mode = Global.Mode.Single;
        private Logger _logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            listPlugins.BooleanCheckStateGetter = delegate(Object rowObject)
            {
                return ((Plugin)rowObject).Active;
            };

            listPlugins.BooleanCheckStatePutter = delegate(Object rowObject, bool newValue)
            {
                ((Plugin)rowObject).Active = newValue;
                return newValue; 
            };

            SetupLogger();
        }
        #endregion

        #region Form Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            cboMode.SelectedIndex = 0;

            _settings = new Settings();
            if (_settings.FileExists == true)
            {
                string ret = _settings.Load();
                if (ret.Length > 0)
                {
                    UserInterface.DisplayErrorMessageBox(this, ret);
                }
                else
                {
                    this.WindowState = _settings.FormState;

                    if (_settings.FormState != FormWindowState.Maximized)
                    {
                        this.Location = _settings.FormLocation;
                        this.Size = _settings.FormSize;
                    }
                }
            }

            if (_settings.RegRipperDir.Length > 0)
            {
                UpdatePaths();
                LoadPlugins();
                LoadFilters();
            }
            else
            {
                UserInterface.DisplayMessageBox(this, "The regripper directory must be supplied before the plugins can be loaded", MessageBoxIcon.Information);
                menuToolsOptions_Click(this, new EventArgs());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _settings.FormLocation = base.Location;
            _settings.FormSize = base.Size;
            _settings.FormState = base.WindowState;
            string ret = _settings.Save();
            if (ret.Length > 0)
            {
                UserInterface.DisplayErrorMessageBox(this, ret);
            }
        }
        #endregion

        #region Misc Methods
        /// <summary>
        /// 
        /// </summary>
        private void SetupLogger()
        {
            LoggingConfiguration config = new LoggingConfiguration();

            FileTarget fileTarget = new FileTarget();
            fileTarget.Layout = @"${date:format=yyyyMMddHHmmss} ${message}";
            fileTarget.FileName = System.IO.Path.Combine(Misc.GetUserDataDirectory(), @"Log.txt");

            config.AddTarget("file", fileTarget);

            LoggingRule rule = new LoggingRule("*", LogLevel.Debug, fileTarget);
            config.LoggingRules.Add(rule);

            LogManager.Configuration = config;
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdatePaths()
        {
            _regRipper = System.IO.Path.Combine(_settings.RegRipperDir, Global.REGRIPPER_EXE);
            _regRipperPlugins = System.IO.Path.Combine(_settings.RegRipperDir, "Plugins");
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadPlugins()
        {
            if (System.IO.Directory.Exists(_regRipperPlugins) == false)
            {
                return;
            }

            Task.Factory.StartNew(() =>
            {
                using (new HourGlass(this))
                {
                    List<Plugin> plugins = Functions.LoadPlugins(_regRipperPlugins);
                    listPlugins.SetObjects(plugins);

                    MethodInvoker methodInvoker = delegate
                    {
                        olvcHive.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvcName.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvcOs.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvcShortDesc.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvcVersion.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                    };

                    if (this.InvokeRequired == true)
                    {
                        this.BeginInvoke(methodInvoker);
                    }
                    else
                    {
                        methodInvoker.Invoke();
                    }
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadFilters()
        {
            if (System.IO.Directory.Exists(_regRipperPlugins) == false)
            {
                return;
            }

            using (new HourGlass(this))
            {
                cboFilter.Items.Clear();
                List<string> filters = Functions.GetAllFilters(_regRipperPlugins);
                foreach (string filter in filters)
                {
                    cboFilter.Items.Add(filter);
                }

                if (cboFilter.Items.Count > 0)
                {
                    cboFilter.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadFilterPlugins()
        {
            if (cboFilter.SelectedIndex == -1)
            {
                return;
            }

            if (_mode != Global.Mode.Filter)
            {
                return;
            }

            Task.Factory.StartNew(() =>
            {
                using (new HourGlass(this))
                {
                    MethodInvoker methodInvoker = delegate
                    {
                        if (listPlugins.Items.Count == 0)
                        {
                            return;
                        }

                        string filterName = cboFilter.Items[cboFilter.SelectedIndex].ToString();
                        List<string> plugins = Functions.GetFilterPlugins(_regRipperPlugins, filterName);

                        foreach (Plugin plugin in listPlugins.Objects)
                        {
                            plugin.Active = false;
                            listPlugins.RefreshObject(plugin);
                        }

                        foreach (Plugin plugin in listPlugins.Objects)
                        {
                            var temp = (from p in plugins where p.ToLower() == plugin.Name select p).Count();
                            if (temp > 0)
                            {
                                plugin.Active = true;
                            }

                            listPlugins.RefreshObject(plugin);
                        }
                    };

                    if (this.InvokeRequired == true)
                    {
                        this.BeginInvoke(methodInvoker);
                    }
                    else
                    {
                        methodInvoker.Invoke();
                    }
                }
            });
        }
        #endregion

        #region Combobox Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPluginControlStatus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFilterPlugins();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listPlugins_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RunPlugin();
        }

        #region Run Methods
        /// <summary>
        /// 
        /// </summary>
        private void RunPlugin()
        {
            if (listPlugins.SelectedItems.Count != 1)
            {
                UserInterface.DisplayMessageBox(this, "A plugin must be selected", MessageBoxIcon.Exclamation);
                listPlugins.Select();
                return;
            }

            Plugin plugin = (Plugin)listPlugins.SelectedObject;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select the " + plugin.Hive + " registry hive";
            openFileDialog.Filter = "All Files|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.Cancel)
            {
                return;
            }

            Task.Factory.StartNew(() =>
            {
                using (new HourGlass(this))
                {
                    MethodInvoker methodInvoker = delegate
                    {
                        tabMain.SelectedTab = tabPageOutput;
                        txtOutput.Text = string.Empty;
                        ExecutePlugin(openFileDialog.FileName, plugin.Name, false);
                    };

                    if (this.InvokeRequired == true)
                    {
                        this.BeginInvoke(methodInvoker);
                    }
                    else
                    {
                        methodInvoker.Invoke();
                    }
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        private void RunHive()
        {
            List<string> plugins = new List<string>();
            switch (_mode)
            {
                case Global.Mode.All:
                    plugins = Functions.GetAllPlugins(listPlugins);
                    break;
                case Global.Mode.Filter:
                    if (cboFilter.SelectedIndex == -1)
                    {
                        UserInterface.DisplayMessageBox(this, "The filter must be selected", MessageBoxIcon.Exclamation);
                        return;
                    }

                    plugins = Functions.GetFilterPlugins(_regRipperPlugins, cboFilter.Items[cboFilter.SelectedIndex].ToString());
                    break;
                case Global.Mode.Multiple:
                    plugins = Functions.GetCheckedPlugins(listPlugins);
                    break;
                case Global.Mode.Single:
                    plugins = Functions.GetSelectedPlugin(listPlugins);
                    break;
            }

            if (plugins.Count == 0)
            {
                UserInterface.DisplayMessageBox(this, "No plugins to run", MessageBoxIcon.Exclamation);
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select the registry hive";
            openFileDialog.Filter = "All Files|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.Cancel)
            {
                return;
            }

            Task.Factory.StartNew(() =>
            {
                using (new HourGlass(this))
                {
                    MethodInvoker methodInvoker = delegate
                    {
                        try
                        {
                            txtOutput.Text = string.Empty;
                            Registry.Registry registry = new Registry.Registry(openFileDialog.FileName);
                            if (registry.HiveType == Registry.HiveType.Unknown)
                            {
                                _logger.Error("Unknown hive type: " + openFileDialog.FileName);
                                UserInterface.DisplayErrorMessageBox(this, "Unable to determined registry hive type");
                                return;
                            }

                            tabMain.SelectedTab = tabPageOutput;

                            foreach (string plugin in plugins)
                            {
                                ExecutePlugin(openFileDialog.FileName, plugin, true);
                            }
                        }
                        catch (Exception ex)
                        {
                            UserInterface.DisplayErrorMessageBox(this, "An error occurred: " + ex.Message);
                            return; 
                        }
                    };

                    if (this.InvokeRequired == true)
                    {
                        this.BeginInvoke(methodInvoker);
                    }
                    else
                    {
                        methodInvoker.Invoke();
                    }
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        private void RunFolder()
        {
            List<string> plugins = new List<string>();
            switch (_mode)
            {
                case Global.Mode.All:
                    plugins = Functions.GetAllPlugins(listPlugins);
                    break;
                case Global.Mode.Filter:
                    if (cboFilter.SelectedIndex == -1)
                    {
                        UserInterface.DisplayMessageBox(this, "The filter must be selected", MessageBoxIcon.Exclamation);
                        return;
                    }

                    plugins = Functions.GetFilterPlugins(_regRipperPlugins, cboFilter.Items[cboFilter.SelectedIndex].ToString());
                    break;
                case Global.Mode.Multiple:
                    plugins = Functions.GetCheckedPlugins(listPlugins);
                    break;
                case Global.Mode.Single:
                    plugins = Functions.GetSelectedPlugin(listPlugins);
                    break;
            }

            if (plugins.Count == 0)
            {
                UserInterface.DisplayMessageBox(this, "No plugins to run", MessageBoxIcon.Exclamation);
                return;
            }

            string folder = string.Empty;
            using (FormFolder form = new FormFolder())
            {
                if (form.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                folder = form.Folder;
            }

            txtOutput.Text = string.Empty;

            Task.Factory.StartNew(() =>
            {
                using (new HourGlass(this))
                {
                    MethodInvoker methodInvoker = delegate
                    {
                        tabMain.SelectedTab = tabPageOutput;

                        List<Plugin> tempPlugins = listPlugins.Objects.Cast<Plugin>().ToList();

                        foreach (string file in System.IO.Directory.EnumerateFiles(folder, "*"))
                        {
                            try
                            {
                                Registry.Registry registry = new Registry.Registry(file);
                                if (registry.HiveType == Registry.HiveType.Unknown)
                                {
                                    _logger.Error("Unknown hive type: " + file);
                                    continue;
                                }

                                foreach (string plugin in plugins)
                                {
                                    // Ensure that the plugin hive type is correct for the file hive type
                                    if (Functions.IsPluginForHive(tempPlugins, plugin, registry.HiveType) == false)
                                    {
                                        continue;
                                    }

                                    ExecutePlugin(file, plugin, true);
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.Error("Error occurred whilst running plugin: (" + file + ") " + ex.Message);
                            }
                        }
                    };

                    if (this.InvokeRequired == true)
                    {
                        this.BeginInvoke(methodInvoker);
                    }
                    else
                    {
                        methodInvoker.Invoke();
                    }
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="plugin"></param>
        /// <param name="outputSeparators"></param>
        private void ExecutePlugin(string file, string plugin, bool outputSeparators)
        {
            try
            {
                string ret = Misc.ShellProcessWithOutput(_regRipper, "-r \"" + file + "\" -p \"" + plugin + "\"");
                txtOutput.AppendText(ret);

                if (outputSeparators == true)
                {
                    txtOutput.AppendText(Environment.NewLine);
                    txtOutput.AppendText("--------------------------------------------");
                    txtOutput.AppendText(Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error occurred whilst running plugin: (Plugin:" + plugin + "|File:" + file + ") " + ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void SetPluginControlStatus()
        {
            switch (cboMode.Items[cboMode.SelectedIndex].ToString().ToLower())
            {
                case "all plugins":
                    listPlugins.CheckBoxes = true;
                    listPlugins.Enabled = false;
                    cboFilter.Enabled = false;

                    _mode = Global.Mode.All;

                    foreach (Plugin plugin in listPlugins.Objects)
                    {
                        plugin.Active = true;
                        listPlugins.RefreshObject(plugin);
                    }
                    break;
                case "filter":
                    listPlugins.CheckBoxes = true;
                    listPlugins.Enabled = true;
                    cboFilter.Enabled = true;
                    _mode = Global.Mode.Filter;

                    LoadFilterPlugins();
                    break;
                case "multiple plugins":
                    listPlugins.CheckBoxes = true;
                    listPlugins.Enabled = true;
                    cboFilter.Enabled = false;
                    _mode = Global.Mode.Multiple;
                    break;
                case "single plugin":
                    listPlugins.CheckBoxes = false;
                    listPlugins.Enabled = true;
                    cboFilter.Enabled = false;
                    _mode = Global.Mode.Single;
                    break;
            }
        }

        #region Menu Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuHelpAbout_Click(object sender, EventArgs e)
        {
            using (FormAbout form = new FormAbout())
            {
                form.ShowDialog(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuHelpHelp_Click(object sender, EventArgs e)
        {
            Misc.ShellExecuteFile(System.IO.Path.Combine(Misc.GetApplicationDirectory(), "Help.pdf"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuToolsOptions_Click(object sender, EventArgs e)
        {
            using (var formOptions = new FormOptions(_settings))
            {
                if (formOptions.ShowDialog(this) == DialogResult.Cancel)
                {
                    return;
                }

                // The user path to regripper has changed so lets reload the plugins & filters
                if (_settings.RegRipperDir != formOptions.RegRipperDir)
                {
                    _settings.RegRipperDir = formOptions.RegRipperDir;
                    UpdatePaths();
                    LoadPlugins();
                    LoadFilters();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuToolsRunPlugin_Click(object sender, EventArgs e)
        {
            RunPlugin();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuToolsRunHive_Click(object sender, EventArgs e)
        {
            RunHive();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuToolsRunFolder_Click(object sender, EventArgs e)
        {
            RunFolder();
        }
        #endregion

        #region Context Menu Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void context_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_mode == Global.Mode.Filter)
            {
                contextFilter.Enabled = true;
            }
            else
            {
                contextFilter.Enabled = false;
            }

            if (listPlugins.SelectedObjects.Count == 1)
            {
                contextPlugin.Enabled = true;
            }
            else
            {
                contextPlugin.Enabled = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextFilterAdd_Click(object sender, EventArgs e)
        {
            if (cboFilter.SelectedIndex == -1)
            {
                UserInterface.DisplayMessageBox(this, "The filter must be selected", MessageBoxIcon.Exclamation);
                return;
            }

            // Get the current plugins for filter
            List<string> plugins = Functions.GetFilterPlugins(_regRipperPlugins, cboFilter.Items[cboFilter.SelectedIndex].ToString());

            // Check not already in list of plugins for filter else add to list
            foreach (Plugin plugin in listPlugins.SelectedObjects)
            {
                var count = (from p in plugins where p.ToLower() == plugin.Name select p).Count();
                if (count > 0)
                {
                    continue;
                }

                plugins.Add(plugin.Name);
            }
            
            plugins.Sort();

            woanware.IO.WriteTextToFile(string.Join(Environment.NewLine, plugins),
                                        System.IO.Path.Combine(_regRipperPlugins,
                                                               cboFilter.Items[cboFilter.SelectedIndex].ToString()),
                                        false);

            LoadFilterPlugins();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextFilterDelete_Click(object sender, EventArgs e)
        {
            if (cboFilter.SelectedIndex == -1)
            {
                UserInterface.DisplayMessageBox(this, "The filter must be selected", MessageBoxIcon.Exclamation);
                return;
            }

            // Get the current plugins for filter
            List<string> plugins = Functions.GetFilterPlugins(_regRipperPlugins, cboFilter.Items[cboFilter.SelectedIndex].ToString());

            // Check plugin is in list of plugins for filter then remove
            foreach (Plugin plugin in listPlugins.SelectedObjects)
            {
                var count = (from p in plugins where p.ToLower() == plugin.Name select p).Count();
                if (count == 0)
                {
                    continue;
                }

                plugins.Remove(plugin.Name);
            }

            plugins.Sort();

            woanware.IO.WriteTextToFile(string.Join(Environment.NewLine, plugins),
                                        System.IO.Path.Combine(_regRipperPlugins,
                                                               cboFilter.Items[cboFilter.SelectedIndex].ToString()),
                                        false);

            LoadFilterPlugins();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextFilterNew_Click(object sender, EventArgs e)
        {
            List<string> plugins = new List<string>();
            // Check plugin is in list of plugins for filter then remove
            foreach (Plugin plugin in listPlugins.SelectedObjects)
            {
                plugins.Add(plugin.Name);
            }

            plugins.Sort();

            using (FormNewFilter form = new FormNewFilter(_regRipperPlugins, plugins))
            {
                if (form.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                LoadFilters();
                UserInterface.LocateAndSelectComboBoxValue(form.Filter, cboFilter);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextFilterRefresh_Click(object sender, EventArgs e)
        {
            LoadFilters();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextPlugin_Click(object sender, EventArgs e)
        {
            if (listPlugins.SelectedObjects.Count != 1)
            {
                return;
            }

            Plugin plugin = (Plugin)listPlugins.SelectedObject;

            using (FormPlugin form = new FormPlugin(_regRipperPlugins, plugin.File))
            {
                form.ShowDialog(this);
            }
        }
        #endregion 
    }
}
