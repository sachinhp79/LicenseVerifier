using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flexera.LicenseVerifier.BL.Interface
{
    interface IRuleFactory
    {
        IValidationRuleCollection GenerateRuleCollection(List<string> dataList);
    }
}
