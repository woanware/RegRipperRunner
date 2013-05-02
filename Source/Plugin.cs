using System.Collections.Generic;
using woanware;

namespace RegRipperRunner
{
    /// <summary>
    /// 
    /// </summary>
    public class Plugin
    {
        #region Member Variables
        public string Name { get; set; }
        public string File { get; set; }
        public string Hive { get; set; }
        public string Version { get; set; }
        public int OsMask { get; set; }
        public string Os { get; set; }
        public string ShortDesc { get; set; }
        public bool HasShortDesc { get; set; }
        public bool HasDesc { get; set; }
        public bool HasRefs { get; set; }
        public bool Active { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public Plugin()
        {
            Name = string.Empty;
            File = string.Empty;
            Hive = string.Empty;
            Version = string.Empty;
            Os = string.Empty;
            ShortDesc = string.Empty;
        }
        #endregion

        #region Misc Methods
        /// <summary>
        /// 
        /// </summary>
        public void UpdateOs()
        {
            List<string> ret = new List<string>();
            Global.Os temp = (Global.Os)OsMask;
            foreach (Global.Os tempOs in Misc.EnumToList<Global.Os>())
            {
                if (tempOs == Global.Os.None)
                {
                    continue;
                }

                if (temp.Has(tempOs) == true)
                {
                    ret.Add(tempOs.GetEnumDescription());
                }
            }

            Os = string.Join(",", ret);
        }
        #endregion
    }
}
