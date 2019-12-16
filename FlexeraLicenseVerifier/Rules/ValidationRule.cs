using System.Collections.Generic;
using Flexera.LicenseVerifier.BL.Interface;

namespace Flexera.LicenseVerifier.BL.Rules
{
    public class ValidationRule: IValidationRule
    {
        public int ApplicationId { get; set; }
        public List<DeviceType> AllowedDeviceList { get; set; }
        public List<DeviceType> OptionalDeviceList { get; set; }

        public ValidationRule()
        {
            AllowedDeviceList= new List<DeviceType>();
            OptionalDeviceList = new List<DeviceType>();
        }
    }
}
