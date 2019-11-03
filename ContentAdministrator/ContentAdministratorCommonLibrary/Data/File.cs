#nullable disable 

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ContentAdministratorCommonLibrary.Data
{
    public class File : PocoBase
    {
        #region fields
        public Guid                 guid        { get; set; }       
        [Required] public string    name        { get; set; } 

        [Required] public Directory directory   { get; set; }                                         // Directory.id
        #endregion

        #region constructors
        public File() : base()
        {

        }

        /// <summary>
        /// Create new instance for insert new record to database 
        /// </summary>
        /// <param name="guid">uniq identifier of file, associated physicaly to file (if possible)</param>
        /// <param name="name">name of directory's file</param>
        /// <param name="directory">place of storage</param>
        public File(Guid guid, string name, Directory directory)
        {
            this.guid      = guid;
            this.name      = name;
            this.directory = directory;
        }
        #endregion

        #region Validation/Size
        public const int    nameMaxLength       =  256;                              
        #endregion
    }
}
