using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flexera.LicenseVerifier.BL.Interface
{
    public interface IResponseInfo
    {
        int ApplicationId { get; set; }
        int UserId { get; set; }
        int NoOfLicenseRequired { get; set; }
    }
}
