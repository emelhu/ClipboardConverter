using System;

namespace ContentAdministratorCommonLibrary_OS_dependent
{
    public class VolumeGuid
    {
        public string GetVolumeByGuid (Guid guid)
        {
            throw new NotImplementedException("VolumeGuid.GetVolumeByGuid");
        }

        public Guid GetGuidByVolume(string volume)
        {
            throw new NotImplementedException("VolumeGuid.GetGuidByVolume");
        }
    }
}

/*
In addition to identifying a drive by its drive letter, you can identify a volume by using its volume GUID. 
This takes the form:
\\.\Volume{b75e2c83-0000-0000-0000-602f00000000}\Test\Foo.txt 
\\?\Volume{b75e2c83-0000-0000-0000-602f00000000}\Test\Foo.txt
*/

/*
// Get drive letter by volume guid:
ManagementObjectSearcher ms = new ManagementObjectSearcher("Select * from Win32_Volume");    
foreach(ManagementObject mo in ms.Get())   
{
    var guid = mo["DeviceID"].ToString();

    if(guid == myGuid)
        return mo["DriveLetter"];
}

//

static string GetVolumeGuidPath(string mountPoint)
{
    StringBuilder sb = new StringBuilder(50);
    GetVolumeNameForVolumeMountPoint(mountPoint, sb, 50);
    return sb.ToString();
}

[DllImport("kernel32.dll", SetLastError = true)]
static extern bool GetVolumeNameForVolumeMountPoint(
    string lpszVolumeMountPoint,
    [Out] StringBuilder lpszVolumeName,
    int cchBufferLength);

*/
