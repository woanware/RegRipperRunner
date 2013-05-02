using System.Collections.Generic;
using System.IO;
using System.Linq;
using BrightIdeasSoftware;
using woanware;

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
    }
}
