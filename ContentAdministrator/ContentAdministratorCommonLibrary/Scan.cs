using System;
using System.Collections.Generic;
using System.Text;

namespace ContentAdministratorCommonLibrary
{
    using ContentAdministratorCommonLibrary.Data;

    public class Scan
    {
        ScanParameters parameters;
        public Scan(ScanParameters parameters)
        {
            this.parameters = parameters;
        }

        public void DoIt()
        {
            using (var context = new CADB_Context())
            {
                string drive = UncoupleDriveAndDirectoryAndFilePart(parameters.directory).volume;

                string[] directoryElements = GetElementaryDirectories(parameters.directory);
            }

            throw new NotImplementedException();
        }

        private (string volume, string directory, string file) UncoupleDriveAndDirectoryAndFilePart(string path)
        {   // WARNING: only Windows code !!!
            path             = System.IO.Path.GetFullPath(path);
            
            string volume    = null; 
            string directory = null; 
            string file      = null;     

            string root      = System.IO.Path.GetPathRoot(path);    // https://docs.microsoft.com/en-us/dotnet/api/system.io.path.getpathroot?view=netcore-3.0

            if (root.StartsWith(@"\\.\") || root.StartsWith(@"//"))
            {

            }
            else if (root.StartsWith(@"\\") || root.StartsWith(@"//"))
            { // UNC format

            }

            // TODO
            
            directory = System.IO.Path.GetDirectoryName(path);
            file      = System.IO.Path.GetFileName(path);

            string[] pathParts = directory.Split( System.IO.Path.VolumeSeparatorChar );

            if (pathParts.Length > 1)
            {
                volume     = pathParts[0];
                directory = pathParts[1];
            }
            else
            {
                directory = pathParts[0];
            }

            



            return (volume, directory, file);

            // LINUX: https://unix.stackexchange.com/questions/41100/mapping-between-logical-and-physical-block-device-names
            // LINUX: https://unix.stackexchange.com/questions/125522/path-syntax-rules
            // LINUX: https://unix.stackexchange.com/questions/256497/on-what-systems-is-foo-bar-different-from-foo-bar
            // WinEx: https://docs.microsoft.com/en-us/dotnet/api/system.io.path.getpathroot?view=netcore-3.0
        }
        
        private string[] GetElementaryDirectories(string path)
        {
            var parts = UncoupleDriveAndDirectoryAndFilePart(path);

            string[] directories = parts.directory.Split(new[] { System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar });

            return directories;
        }
    }
}
