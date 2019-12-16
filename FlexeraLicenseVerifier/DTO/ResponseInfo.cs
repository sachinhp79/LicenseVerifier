using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flexera.LicenseVerifier.BL.Interface;

namespace Flexera.LicenseVerifier.BL.DTO
{
    /// <summary>
    /// Response info class to generate results
    /// </summary>
    public class ResponseInfo: IResponseInfo
    {
        public int ApplicationId { get; set; }
        public int UserId { get; set; }
        public int NoOfLicenseRequired { get; set; }

        public override string ToString()
        {
            return UserId.ToString() + 
                   Constants.CommaSeparator + 
                   ApplicationId + 
                   Constants.CommaSeparator +
                   NoOfLicenseRequired;
        }
    }
}
