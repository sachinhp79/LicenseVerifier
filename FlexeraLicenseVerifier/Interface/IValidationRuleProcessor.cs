using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flexera.LicenseVerifier.BL.Interface
{
    interface IValidationRuleProcessor
    {
        List<IResponseInfo> ProcessValidationRules(IInstallationDetails installationDetails);
    }
}
