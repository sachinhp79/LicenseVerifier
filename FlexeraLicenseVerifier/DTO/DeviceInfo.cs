using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flexera.LicenseVerifier.BL.Interface;

namespace Flexera.LicenseVerifier.BL.DTO
{
    /// <summary>
    /// Class for Device information
    /// </summary>
    public class DeviceInfo: IDeviceInfo
    {
        public int DeviceId { get; set; }
        public DeviceType TypeOfDevice { get; set; }

        public DeviceInfo(int deviceId, DeviceType deviceType)
        {
            DeviceId = deviceId;
            TypeOfDevice = deviceType;
        }

        public bool IsExistingDevice(int deviceId)
        {
            return deviceId == DeviceId;
        }
    }
}
