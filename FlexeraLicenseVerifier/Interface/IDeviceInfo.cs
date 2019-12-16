using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flexera.LicenseVerifier.BL.Interface
{
    public interface IDeviceInfo
    {
        bool IsExistingDevice(int deviceId);
        DeviceType TypeOfDevice { get; set; }
    }
}
