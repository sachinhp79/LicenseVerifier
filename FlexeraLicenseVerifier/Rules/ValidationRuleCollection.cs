using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flexera.LicenseVerifier.BL.Interface;

namespace Flexera.LicenseVerifier.BL.Rules
{
    public class ValidationRuleCollection: IValidationRuleCollection
    {
        public List<IValidationRule> ValidationRules { get; }

        public ValidationRuleCollection()
        {
            ValidationRules = new List<IValidationRule>();
        }

        public void AddToCollection(IValidationRule newValue)
        {
            ValidationRules.Add(newValue);
        }
    }
}
