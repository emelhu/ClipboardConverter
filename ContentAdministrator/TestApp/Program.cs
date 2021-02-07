using System;
using System.IO;
using eMeL_Common;

// Install-Package Trinet.Core.IO.Ntfs -Version 4.1.1

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Console.WriteLine("*** Alternate NTFS stream test ***\n");
            
            var path1 = @"C:\_work_\proba.txt:aaa";
            var temp1 = File.ReadAllText(path1);    // return --> OK

            var path2 = @"C:\_work_\Casad:aaa";
            var temp2 = File.ReadAllText(path2);    // return --> OK

            //var path3 = @"C:\_work_\Casad\:aaa";
            //var temp3 = File.ReadAllText(path3);    // return --> null / exception

            File.WriteAllText(path1, "AAAA1");
            File.WriteAllText(path2, "AAAA2");
            */

            Console.WriteLine("*** UncouplePathParts test ***\n");

            var up1 = new UncouplePathParts(@"c:\_work_\eMeL_CoreCommonLibrary\bin\Debug\netstandard2.0\Proba.txt");
            var up2 = new UncouplePathParts(@"c:\_work_\eMeL_CoreCommonLibrary\bin\Debug\netstandard2.0\Proba.txt", UncouplePathParts.SubdirectoryGuid.Retrieve);
            var up3 = new UncouplePathParts(@"c:\_work_\eMeL_CoreCommonLibrary\bin\Debug\netstandard2.0\Proba.txt", UncouplePathParts.SubdirectoryGuid.Create);
        }
    }
}
