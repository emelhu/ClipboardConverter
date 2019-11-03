#nullable enable

using System;
using System.Collections.Generic;
using System.Text;

namespace ContentAdministratorCommonLibrary.Data
{
    public class Volume : PocoBase
    {
        public Guid         guid        { get; set; }
        public string       name        { get; set; } = String.Empty;

        public bool         readOnly    { get; set; }
        public bool         removeable  { get; set; }

        #region constructors
        /// <summary>
        /// Create new instance for insert new record to database 
        /// </summary>
        /// <param name="guid">uniq identifier of volume, associated physicaly to volume</param>
        /// <param name="name">name of volume </param>
        public Volume(Guid guid, string name) : base(true)
        {            
            this.guid            = guid;
            this.name            = name;
        }
        #endregion

        #region Validation/Size
        public const int    nameMaxLength       =  256;                              
        #endregion
    }
}


