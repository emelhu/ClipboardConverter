#nullable enable 

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ContentAdministratorCommonLibrary
{
    public class ScanParameters
    {
        #region parameter values
        public string       directory;
        public List<string> includes    = new List<string>();                                           
        public List<string> excludes    = new List<string>();
        #endregion

        public const string  includeFileWildcardDefault = "*.*";

        #region constructors
        public ScanParameters(string path, List<string>? includes = null, List<string>? excludes = null)
        {
            int     dirPartLength;
            string? dirPart = Path.GetDirectoryName(path);                                      // null --> root

            if (dirPart == null)
            {   // null --> root
                dirPartLength = 0;
                dirPart = @"\";                               
            }
            else
            {
                if (dirPart == String.Empty)
                {
                    dirPartLength = 0;
                    dirPart = @".\";                                                                // current directory
                }
                else
                {
                    System.Diagnostics.Debug.Assert(! String.IsNullOrWhiteSpace(dirPart));

                    dirPartLength = dirPart.Length;                     
                }
            }

            this.directory = Path.GetFullPath(dirPart);

            if (includes != null)
            {
                this.includes = new List<string>(includes);
            }

            if (excludes != null)
            {
                this.excludes = new List<string>(excludes);
            }

            string? wildcard = null;

            if (dirPartLength < path.Length)
            {
                wildcard = path.Substring(dirPartLength);

                if (wildcard.StartsWith('\\') || wildcard.StartsWith('/'))
                {
                    wildcard = wildcard.Substring(1);
                }
            }

            if (string.IsNullOrWhiteSpace(wildcard))
            {
                wildcard = includeFileWildcardDefault;
            }

            this.includes.Add(wildcard);
        }        

        public ScanParameters(ScanParameters parameters, string subdir)
        {   // Half-Copy constructor
            this.directory = subdir;                              
            this.includes  = parameters.includes;                               // same List instance, no problem
            this.excludes  = parameters.excludes;                               // same List instance, no problem
        }
        #endregion
    }
}
