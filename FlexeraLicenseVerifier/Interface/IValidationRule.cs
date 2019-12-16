using System.Collections.Generic;

namespace Flexera.LicenseVerifier.BL.Interface
{
    public interface IValidationRule
    {
        int ApplicationId { get; set; }
        List<DeviceType> AllowedDeviceList { get; set; }
        List<DeviceType> OptionalDeviceList { get; set; }
    }
}
