using System.Collections.Generic;
using System.IO;
using System.Linq;
using BrightIdeasSoftware;
using woanware;
using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using NLog;

namespace RegRipperRunner
{
    /// <summary>
    /// 
    /// </summary>
    public static class Functions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pluginDir"></param>
        /// <returns></returns>
        public static List<Plugin> LoadPlugins(string pluginDir)
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
            foreach (string file in System.IO.Directory.EnumerateFiles(pluginDir, "*.pl"))
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
                        Logger _logger = LogManager.GetCurrentClassLogger();
                        _logger.Error("Could not parse the plugin: " + file + " (" + regex.Item1 + ")");
                    }
                }

                plugins.Add(plugin);
            }

            return plugins;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listView"></param>
        /// <returns></returns>
        public static List<string> GetAllPlugins(ObjectListView listView)
        {
            List<string> plugins = new List<string>();
            foreach (Plugin plugin in listView.Objects)
            {
                plugins.Add(plugin.Name);
            }
            return plugins;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listView"></param>
        /// <returns></returns>
        public static List<string> GetCheckedPlugins(ObjectListView listView)
        {
            List<string> plugins = new List<string>();
            foreach (Plugin plugin in listView.Objects)
            {
                if (plugin.Active == false)
                {
                    continue;
                }

                plugins.Add(plugin.Name);
            }
            return plugins;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pluginsDir"></param>
        /// <param name="filterName"></param>
        /// <returns></returns>
        public static List<string> GetFilterPlugins(string pluginsDir, string filterName)
        {
            if (File.Exists(System.IO.Path.Combine(pluginsDir, filterName)) == false)
            {
                return new List<string>();
            }

            string[] filters = File.ReadAllLines(System.IO.Path.Combine(pluginsDir, filterName));
            List<string> plugins = new List<string>(filters);
            return plugins;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listView"></param>
        /// <returns></returns>
        public static List<string> GetSelectedPlugin(ObjectListView listView)
        {
            if (listView.SelectedItems.Count == 0)
            {
                return new List<string>();
            }
            Plugin plugin = (Plugin)listView.SelectedObject;
            return new List<string>() { plugin.Name };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plugins"></param>
        /// <param name="pluginName"></param>
        /// <param name="hiveType"></param>
        /// <returns></returns>
        public static bool IsPluginForHive(List<Plugin> plugins, 
                                           string pluginName, 
                                           Registry.HiveType hiveType)
        {
            var plugin = (from p in plugins where p.Name == pluginName select p).SingleOrDefault();
            if (plugin == null)
            {
                return false;
            }

            string[] hives = plugin.Hive.Split(',');
            foreach (string hive in hives)
            {
                if (hive.ToLower() == hiveType.GetEnumDescription().ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllFilters(string pluginDir)
        {
            List<string> filters = new List<string>();
            foreach (string file in System.IO.Directory.EnumerateFiles(pluginDir, "*"))
            {
                if (System.IO.Path.GetExtension(file) != string.Empty)
                {
                    continue;
                }

                filters.Add(System.IO.Path.GetFileNameWithoutExtension(file));
            }

            return filters;
        }
    }
}
