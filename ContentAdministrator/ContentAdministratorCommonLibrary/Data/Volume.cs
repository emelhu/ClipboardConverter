#nullable enable

using System;
using System.Collections.Generic;
using System.Text;

namespace ContentAdministratorCommonLibrary.Data
{
    using eMeL_Common;

    public class Volume : PocoBase
    {
        public Guid         guid            { get; set; }
        public string       name            { get; set; } = String.Empty;
        public UInt32?      serialNumber    { get; set;}

        public bool         readOnly        { get; set; }
        public bool         removeable      { get; set; }

        #region constructors
        /// <summary>
        /// Create new instance for insert new record to database 
        /// </summary>
        /// <param name="guid">uniq identifier of volume, associated physicaly to volume</param>
        /// <param name="name">name of volume </param>
        public Volume(Guid guid, string name, UInt32? serialNumber) : base(true)
        {            
            if (guid == Guid.Empty)
            {
                throw new EMEL_ParameterException("Volume constructor error! 'guid' parameter is empty!", new Guid("233A7FDA-9A66-4B82-AF70-67A17584F433"));
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                throw new EMEL_ParameterException("Volume constructor error! 'name' parameter is empty!", new Guid("233A7FDA-9A66-4B82-AF70-67A17484F344"));
            }

            name = name.Trim();

            if (name.Length > nameMaxLength)
            {
                throw new EMEL_ParameterException($"Volume constructor error! 'name' parameter is too long [{name.Length} > {nameMaxLength}]!", new Guid("233A7FDA-9A66-4B82-AF70-67A17484F366"));
            }

            if (name.Length < 2)
            {
                throw new EMEL_ParameterException($"Volume constructor error! 'name' parameter is too short [< 2]!", new Guid("233A7FDA-9A66-4B82-AF70-67A26484F377"));
            }

            this.guid           = guid;
            this.name           = name;

            if (serialNumber == 0)
            {
                serialNumber = null;
            }

            this.serialNumber   = serialNumber;
        }
        #endregion

        #region Validation/Size
        public const int    nameMaxLength   =  256;                              
        #endregion
    }
}


