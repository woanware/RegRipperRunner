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
                MethodInvoker methodInvoker = delegate
                {
                    using (new HourGlass(this))
                    {
                        List<Tuple<string, Regex>> regexes = new List<Tuple<string, Regex>>();
                        regexes.Add(new Tuple<string, Regex>("hive", new Regex(@"my %config\s+=\s+\(hive\s+=>\s+""(.*)"",", RegexOptions.IgnoreCase | RegexOptions.Compiled)));
                        regexes.Add(new Tuple<string, Regex>("hasshortdesc", new Regex(@"\s+hasShortDescr\s+=>\s+(.*),", RegexOptions.IgnoreCase | RegexOptions.Compiled)));
                        regexes.Add(new Tuple<string, Regex>("hasdesc", new Regex(@"\s+hasDescr\s+=>\s+(.*),", RegexOptions.IgnoreCase | RegexOptions.Compiled)));
                        regexes.Add(new Tuple<string, Regex>("refs", new Regex(@"\s+hasRefs\s+=>\s+(.*),", RegexOptions.IgnoreCase | RegexOptions.Compiled)));
                        regexes.Add(new Tuple<string, Regex>("os", new Regex(@"\s+osmask\s+=>\s+(.*),", RegexOptions.IgnoreCase | RegexOptions.Compiled)));
                        regexes.Add(new Tuple<string, Regex>("version", new Regex(@"\s+version\s+=>\s+(.*)\)", RegexOptions.IgnoreCase | RegexOptions.Compiled)));
                        regexes.Add(new Tuple<string, Regex>("shortdesc", new Regex(@"sub getShortDescr {\s*return ""(.*)"";\s*}", RegexOptions.IgnoreCase | RegexOptions.Compiled)));

                        List<Plugin> plugins = new List<Plugin>();
                        foreach (string file in System.IO.Directory.EnumerateFiles(_regRipperPlugins, "*.pl"))
                        {
                            string perl = File.ReadAllText(file);

                            Plugin plugin = new Plugin();
                            plugin.Active = true;
                            plugin.Name = System.IO.Path.GetFileNameWithoutExtension(file);
                            plugin.File = System.IO.Path.GetFileName(file);
                            foreach (Tuple<string, Regex> regex in regexes)
                            {
                                Match match = regex.Item2.Match(perl);
                                if (match.Success == true)
                                {
                                    switch (regex.Item1)
                                    {
                                        case "hive":
                                            plugin.Hive = match.Groups[1].Value.Replace(@"\.", ".");
                                            break;
                                        case "hasshortdesc":
                                            plugin.HasShortDesc = Convert.ToBoolean(Convert.ToInt16(match.Groups[1].Value));
                                            break;
                                        case "shortdesc":
                                            plugin.ShortDesc = match.Groups[1].Value;
                                            break;
                                        case "hasdesc":
                                            plugin.HasDesc = Convert.ToBoolean(Convert.ToInt16(match.Groups[1].Value));
                                            break;
                                        case "refs":
                                            plugin.HasRefs = Convert.ToBoolean(Convert.ToInt16(match.Groups[1].Value));
                                            break;
                                        case "os":
                                            int osMask = 0;
                                            if (int.TryParse(match.Groups[1].Value, out osMask) == true)
                                            {
                                                plugin.OsMask = osMask;
                                                plugin.UpdateOs();
                                            }
                                            break;
                                        case "version":
                                            plugin.Version = match.Groups[1].Value;
                                            break;

                                    }
                                }
                                else
                                {
                                    Console.WriteLine("FAIL: " + file);
                                }
                            }

                            plugins.Add(plugin);
                        }

                        listPlugins.SetObjects(plugins);

                        olvcHive.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvcName.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvcOs.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvcShortDesc.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvcVersion.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
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

            using (new HourGlass(this))
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
                    if (temp> 0)
                    {
                        plugin.Active = true;
                    }

                    listPlugins.RefreshObject(plugin);
                }
            }
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

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select the " + listPlugins.SelectedItems[0].SubItems[1].Text + " registry hive";
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
                        Plugin plugin = (Plugin)listPlugins.SelectedObject;

                        string ret = Misc.ShellProcessWithOutput(_regRipper, "-r \"" + openFileDialog.FileName + "\" -p \"" + plugin.Name + "\"");
                        txtOutput.Text = ret;
                        tabMain.SelectedTab = tabPageOutput;
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
                                UserInterface.DisplayErrorMessageBox(this, "Unable to determined registry hive type");
                                return;
                            }

                            tabMain.SelectedTab = tabPageOutput;

                            foreach (string plugin in plugins)
                            {
                                string ret = Misc.ShellProcessWithOutput(_regRipper, "-r \"" + openFileDialog.FileName + "\" -p \"" + plugin + "\"");
                                txtOutput.AppendText(ret);
                                txtOutput.AppendText(Environment.NewLine);
                                txtOutput.AppendText("--------------------------------------------");
                                txtOutput.AppendText(Environment.NewLine);
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
                                    continue;
                                }

                                foreach (string plugin in plugins)
                                {
                                    // Ensure that the plugin hive type is correct for the file hive type
                                    if (Functions.IsPluginForHive(tempPlugins, plugin, registry.HiveType) == false)
                                    {
                                        continue;
                                    }

                                    string ret = Misc.ShellProcessWithOutput(_regRipper, "-r \"" + file + "\" -p \"" + plugin + "\"");
                                    txtOutput.AppendText(ret);
                                    txtOutput.AppendText(Environment.NewLine);
                                    txtOutput.AppendText("--------------------------------------------");
                                    txtOutput.AppendText(Environment.NewLine);
                                }
                            }
                            catch (Exception ex)
                            {
                                //UserInterface.DisplayErrorMessageBox(this, "An error occurred: " + ex.Message);
                                //return; 
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

                    for (int index = 0; index < listPlugins.Items.Count; index++)
                    {
                        listPlugins.Items[index].Checked = true;
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
        #endregion 
    }
}
