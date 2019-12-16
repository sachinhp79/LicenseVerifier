using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flexera.LicenseVerifier.BL.DTO;

namespace Flexera.LicenseVerifier.BL.Interface
{
    public interface IApplicationInfo
    {
        int ApplicationId { get; }

        List<IDeviceInfo> InstalledDevices{ get; set; }

        void AddToCollection(IDeviceInfo deviceInfo);
        void AddRangeToCollection(List<IDeviceInfo> deviceInfoList);
        bool IsExistingApplication(int applicationId);
        bool IsExistingDevice(int deviceId);
        IDeviceInfo FindDevice(int deviceId);
        int LicenseCountRequired { get; set; }
    }
}
