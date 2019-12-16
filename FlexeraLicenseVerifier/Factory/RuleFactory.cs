using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Flexera.LicenseVerifier.BL.DTO;
using Flexera.LicenseVerifier.BL.Enum;
using Flexera.LicenseVerifier.BL.Interface;
using Flexera.LicenseVerifier.BL.Logger;
using Flexera.LicenseVerifier.BL.Rules;

namespace Flexera.LicenseVerifier.BL.Factory
{
    /// <summary>
    /// Factory class to create the Validation Rules
    /// </summary>
    public sealed class RuleFactory: IRuleFactory
    {
        public IValidationRuleCollection GenerateRuleCollection(List<string> dataList)
        {
            try
            {
                var validationRuleCollection = new ValidationRuleCollection();
                foreach (var columnValues in dataList.Select(dataLine => dataLine.Split(Constants.CommaSeparator)))
                {
                    try
                    {
                        validationRuleCollection.AddToCollection(CreateValidationRule(columnValues));
                    }
                    catch (Exception ex)
                    {
                        LogWriter.Instance.DoLogging("Invalid data : " + string.Join(Constants.CommaSeparator.ToString(), columnValues));
                        LogWriter.Instance.DoLogging(ex.Message, LogLevel.Error);
                        continue;
                    }
                }

                return validationRuleCollection;
            }
            catch (Exception ex)
            {
                LogWriter.Instance.DoLogging("Invalid data : " + ex.StackTrace, LogLevel.Error);
                throw;
            }
        }

        /// <summary>
        /// Factory method to create the instance of Validation Rule
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private IValidationRule CreateValidationRule(string[] values)
        {
            var validationRule = new ValidationRule()
            {
                ApplicationId = int.Parse(values[0]),
                AllowedDeviceList = new List<DeviceType>(){ (DeviceType)System.Enum.Parse(typeof(DeviceType), values[1].ToUpper())},
                OptionalDeviceList = new List<DeviceType>() { (DeviceType)System.Enum.Parse(typeof(DeviceType), values[2].ToUpper()) }
            };

            return validationRule;
        }
    }
}
