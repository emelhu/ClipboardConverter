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
        public            Guid      guid                { get; set; }
        [Required] public string    name                { get; set; } = String.Empty;

        public Directory            parentDirectory     { get; set; }                                          // Directory.pk
        public Volume               volume              { get; set; }                                          // Volume.pk
        #endregion

        #region other interface
        public bool                 isRoot { get => volume != null; }
        #endregion

        #region constructors
        public Directory() : base()
        {

        }

        #region technical
        /// <summary>
        /// Check consistency of this standalone database record
        /// </summary>
        public void CheckConsistency()
        {
            string errText = null;

            if ((parentDirectory == null) && (volume == null))
            {
                errText = "'parentDirectory' or 'volume' must had set valid reference";
            }
            else if ((parentDirectory != null) && (volume != null))
            {
                errText = "one of the two 'parentDirectory' and 'volume' must had set null reference";
            }

            // TODO: more check rule!

            if (errText != null)
            {
                throw new ValidationException($"Directory db.record error: {errText}! [{guid}][{name}]");
            }
        }
        #endregion

        /// <summary>
        /// Create new instance for insert new record to database for a subdirectory
        /// </summary>
        /// <param name="guid">uniq identifier of directory, associated physicaly to directory</param>
        /// <param name="name">name of volume's directory</param>
        /// <param name="parentDirectory">to build directory tree, null if root of volume</param>
        public Directory(Guid guid, string name, Directory parentDirectory) : base(true)
        {            
            this.guid            = guid;
            this.name            = name;
            this.parentDirectory = parentDirectory;
            this.volume          = null; 
        }

        /// <summary>
        /// Create new instance for insert new record to database for a root directory
        /// </summary>
        /// <param name="guid">uniq identifier of directory, associated physicaly to directory</param>
        /// <param name="name">name of volume's directory</param>
        /// <param name="volume">Volume of root directory</param>
        public Directory(Guid guid, string name, Volume volume) : base(true)
        {            
            this.guid            = guid;
            this.name            = name;
            this.parentDirectory = null;
            this.volume          = volume; 
        }
        #endregion

        #region Validation/Size
        public const int    nameMaxLength  =  256;                              
        #endregion
    }

    
}
