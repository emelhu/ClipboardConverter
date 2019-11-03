#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace ContentAdministratorCommonLibrary.Data
{
    public class Directory : PocoBase
    {
        #region fields
        public Guid                 guid                { get; set; }
        [Required] public string    name                { get; set; } = String.Empty;

        public Directory            parentDirectory     { get; set; }                                          // Directory.pk
        #endregion

        #region other interface
        public bool                 isRoot { get => parentDirectory == null; }
        #endregion

        #region constructors
        public Directory() : base()
        {

        }
        /// <summary>
        /// Create new instance for insert new record to database 
        /// </summary>
        /// <param name="guid">uniq identifier of directory, associated physicaly to directory</param>
        /// <param name="name">name of volume's directory</param>
        /// <param name="parentDirectory">to build directory tree, null if root of volume</param>
        public Directory(Guid guid, string name, Directory parentDirectory) : base(true)
        {            
            this.guid            = guid;
            this.name            = name;
            this.parentDirectory = parentDirectory;
        }
        #endregion

        #region Validation/Size
        public const int    nameMaxLength  =  256;                              
        #endregion
    }

    
}
