#nullable enable 

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ContentAdministratorCommonLibrary.Data
{
    using eMeL_Common;

    public class PocoBase
    {
        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int64    id              { get; set; }               // identifier, unique key
        public DateTime lastModTime     { get; set; }
        public Int64    lastModLogin    { get; set; }
        #endregion

        #region Constructors
        protected PocoBase()
        {
            
        }

        /// <summary>
        /// Create new instance with insert new record to database option
        /// </summary>
        /// <param name="forInsert">true for new instance (insert new record to database)</param>
        protected PocoBase(bool forInsert) : base()
        {
            if (forInsert)
            {
                id           = CADB_Context.nextID;
                lastModTime  = DateTime.Now;
                lastModLogin = 0;                                               //TODO: not implemented
            }
        }
        #endregion
    }
}
