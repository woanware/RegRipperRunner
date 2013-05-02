using System;
using System.ComponentModel;

namespace RegRipperRunner
{
    /// <summary>
    /// 
    /// </summary>
    public class Global
    {
        public const string REGRIPPER_EXE = "rip.exe";

        /// <summary>
        /// Plugin mode
        /// </summary>
        public enum Mode
        {
            All,
            Single,
            Multiple,
            Filter
        }

        /// <summary>
        /// 
        /// </summary>
        [Flags]
        public enum Os
        {
            [Description("")]
            None = 0,
            [Description("WinXP")]
            Xp = 1,
            [Description("Win2K3")]
            Win2003 = 2,
            [Description("Vista")]
            Vista = 4,
            [Description("Win2K8")]
            Win2008 = 8,
            [Description("Win7/Win2K8R2")]
            Win7_Win2008R2 = 16,
            [Description("Win8")]
            Win8 = 32,
            [Description("Win2K12")]
            Win2012 = 64
        }
    }
    
}
