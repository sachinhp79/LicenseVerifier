using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Flexera.LicenseVerifier.BL.Interface;

namespace Flexera.LicenseVerifier.BL.DTO
{
    /// <summary>
    /// Class for Application information which holds information of devices on which application is installed
    /// </summary>
    public class ApplicationInfo: IApplicationInfo
    {
        public int LicenseCountRequired { get; set; }
        public int ApplicationId { get; }
        public string Description { get; set; }
        public List<IDeviceInfo> InstalledDevices { get; set; }

        public ApplicationInfo(int applicationId, string description)
        {
            ApplicationId = applicationId;
            Description = description;
            InstalledDevices = new List<IDeviceInfo>();
        }
        public void AddToCollection(IDeviceInfo deviceInfo)
        {
            if (InstalledDevices == null)
            {
                InstalledDevices = new List<IDeviceInfo>();
            }

            InstalledDevices.Add(deviceInfo);
        }

        public void AddRangeToCollection(List<IDeviceInfo> deviceInfoList)
        {
            if (InstalledDevices == null)
            {
                InstalledDevices = new List<IDeviceInfo>();
            }

            InstalledDevices.AddRange(deviceInfoList);
        }

        public bool IsExistingApplication(int applicationId)
        {
            return ApplicationId == applicationId;
        }

        public bool IsExistingDevice(int deviceId)
        {
            return InstalledDevices.Any(a => a.IsExistingDevice(deviceId));
        }

        public IDeviceInfo FindDevice(int deviceId)
        {
            return InstalledDevices.FirstOrDefault(x => x.IsExistingDevice(deviceId));
        }
    }
}
