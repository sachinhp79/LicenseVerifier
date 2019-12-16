using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flexera.LicenseVerifier.BL.Interface
{
    public interface IValidationRuleCollection
    {
        List<IValidationRule> ValidationRules { get; }
        void AddToCollection(IValidationRule newValue);

    }
}
