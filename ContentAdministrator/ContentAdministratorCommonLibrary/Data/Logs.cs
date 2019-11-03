#nullable enable 

using System;
using System.Collections.Generic;
using System.Text;

namespace ContentAdministratorCommonLibrary.Data
{
    public class Logs : PocoBase
    {
        #region fields
        public string   group       { get; set; } = String.Empty;
        public string   logText     { get; set; } = String.Empty;
        public string?  note        { get; set; }
        public DateTime logTime     { get; set; }
        #endregion

        #region constructors
        /// <summary>
        /// Create new instance with insert new record to 'Logs' database table option
        /// </summary>
        /// <param name="group">Group of logs</param>
        /// <param name="logText">Log message</param>
        /// <param name="note">Note text</param>
        public Logs(string group, string logText, string? note = null) : base(true)
        {
            this.group   = group;
            this.logText = logText;
            this.note    = note;

            this.logTime = DateTime.Now;
        }
        #endregion

        #region Validation/size
        public const int    groupMaxLength      =   32;                              
        public const int    logTextMaxLength    = 1024;                              
        #endregion
    }
}
