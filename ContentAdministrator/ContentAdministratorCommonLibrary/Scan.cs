using System;
using System.Collections.Generic;
using System.Text;

namespace ContentAdministratorCommonLibrary
{
    using System.Diagnostics;
    using System.Linq;
    using ContentAdministratorCommonLibrary.Data;
    using eMeL_Common;
    using static eMeL_Common.UncouplePathParts;

    public class Scan
    {
        ScanParameters parameters;
        public Scan(ScanParameters parameters)
        {
            this.parameters = parameters;
        }

        public void DoIt()
        {
            using var context = new CADB_Context();

            try 
            {
                var         filenames = GetSelectedFilenames();
                Directory   directory = null;

                foreach (var file in filenames)
                {
                    if (directory == null)
                    {   // TODO: directory info write to database
                        var pathParts = new UncouplePathParts(file, UncouplePathParts.SubdirectoryGuid.Create);

                        foreach (var sdi in pathParts.subdirectories)
                        {
                            var dirName = sdi.name;

                            if ((sdi.guid != null) && (sdi.guid != Guid.Empty))
                            {
                                directory = context.directories.Where(d => d.guid == sdi.guid).FirstOrDefault();

                                if (directory == null)
                                {
                                    VolumeInfo volumeInfo = pathParts.volume;
                                    Volume     volume = null;

                                    if ((volumeInfo.guid != null) && (volumeInfo.guid != Guid.Empty))
                                    {
                                        volume = context.volumes.Where(v => v.guid == volumeInfo.guid).FirstOrDefault();
                                    }
                                    else if (volumeInfo.serialNumber != 0)  
                                    {
                                        volume = context.volumes.Where(d => d.serialNumber == volumeInfo.serialNumber).FirstOrDefault();
                                    }

                                    if (volume == null)
                                    {
                                        volume = new Volume(volumeInfo.guid, volumeInfo.name, volumeInfo.serialNumber);
                                        Trace.Fail("Not implemented! [volume]");    // TODO: ...!!!....
                                    }

                                    

                                    directory = new Directory((Guid)sdi.guid, sdi.name, volume);

                                    context.directories.Add(directory);
                                }
                            }
                            else 
                            {
                                var a = sdi.fullName;
                                Trace.Fail("Not implemented!");
                                throw new NotImplementedException();
                            }
                        }
                    }

                    // TODO !!!
                }

            }
            finally
            {
                context.SaveChanges();
            }

            throw new NotImplementedException();
        }

        private HashSet<string> GetSelectedFilenames()
        {
            var files = new HashSet<string>();

            foreach (var wildcard in parameters.includes)
            {
                var includeNames = System.IO.Directory.GetFiles(parameters.directory, wildcard);

                foreach (var tempName in includeNames)
                {
                    files.Add(tempName);                                                            // This will filter duplicate filenames
                }
            }

            //

            foreach (var wildcard in parameters.excludes)
            {
                var excludeNames = System.IO.Directory.GetFiles(parameters.directory, wildcard);

                foreach (var tempName in excludeNames)
                {
                    files.Remove(tempName);                                                         
                }
            }

            return files;
        }
    }
}
