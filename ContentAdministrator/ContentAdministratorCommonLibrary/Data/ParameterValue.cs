#nullable enable 

using System;
using System.Collections.Generic;
using System.Text;

namespace ContentAdministratorCommonLibrary.Data
{
    public class ParameterValue : PocoBase
    {
        #region fields
        public string   group           { get; set; } = String.Empty;
        public string   name            { get; set; } = String.Empty;
        public string?  stringValue     { get; set; }
        public long?    longValue       { get; set; }  
        public bool?    boolValue       { get; set; }  
        public string?  description     { get; set; }
        #endregion

        #region constructors
        public ParameterValue(string group, string name, string? stringValue = null) : base(true)
        {
            this.group       = group;
            this.name        = name;
            this.stringValue = stringValue;
        }

        public ParameterValue(string group, string name, bool boolValue) : base(true)
        {
            this.group       = group;
            this.name        = name;
            this.boolValue   = boolValue;
        }

        public ParameterValue(string group, string name, string? stringValue, long? longValue, bool? boolValue) : base(true)
        {
            this.group       = group;
            this.name        = name;
            this.stringValue = stringValue;
            this.longValue   = longValue;
            this.boolValue   = boolValue;
        }

        public ParameterValue(string group, string name, long longValue) : base(true)
        {
            this.group      = group;
            this.name       = name;
            this.longValue  = longValue;
        }
        #endregion

        #region Validation/size
        public const int    groupMaxLength   = 32;                              
        public const int    nameMaxLength    = 64;                              
        #endregion
    }
}
