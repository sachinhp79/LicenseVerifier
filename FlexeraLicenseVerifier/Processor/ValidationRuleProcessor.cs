using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flexera.LicenseVerifier.BL.DTO;
using Flexera.LicenseVerifier.BL.Enum;
using Flexera.LicenseVerifier.BL.Interface;
using Flexera.LicenseVerifier.BL.Logger;

namespace Flexera.LicenseVerifier.BL.Processor
{
    /// <summary>
    /// Processor class to process the validation rules against the data
    /// </summary>
    public class ValidationRuleProcessor: IValidationRuleProcessor
    {
        private readonly IValidationRuleCollection _validationRuleCollection;
        public ValidationRuleProcessor(IValidationRuleCollection validationRuleCollection)
        {
            _validationRuleCollection = validationRuleCollection;
        }

        /// <summary>
        /// Processes the validation rules and generates the userwise installation details
        /// </summary>
        /// <param name="installationDetails"></param>
        /// <returns></returns>
        public List<IResponseInfo> ProcessValidationRules(IInstallationDetails installationDetails)
        {
            try
            {
                if(installationDetails == null)
                {
                    LogWriter.Instance.DoLogging("No valid data found to process", LogLevel.Error);
                    return null;
                }

                var userWiseCollection = installationDetails.GetUsers();

                //foreach (var u in userWiseCollection)
                    Parallel.ForEach(userWiseCollection, u =>
                    {
                        //foreach (var v in _validationRuleCollection.ValidationRules)
                            Parallel.ForEach(_validationRuleCollection.ValidationRules, v =>
                            {
                                if (u.InstalledApplications.Values.Any(x => x.ApplicationId == v.ApplicationId))
                                {
                                    u.InstalledApplications.TryGetValue(v.ApplicationId, out var installedApplication);

                                    if (installedApplication != null && installedApplication.InstalledDevices.Any())
                                    {
                                        var allowedDevicesInstalled =
                                            installedApplication.InstalledDevices.Count(x =>
                                                v.AllowedDeviceList.Any(d => d == x.TypeOfDevice));

                                        var optionalDevicesInstalled =
                                            installedApplication.InstalledDevices.Count(x =>
                                                v.OptionalDeviceList.Any(d => d == x.TypeOfDevice));

                                        installedApplication.LicenseCountRequired =
                                            allowedDevicesInstalled >= optionalDevicesInstalled
                                                ? allowedDevicesInstalled
                                                : optionalDevicesInstalled;
                                    }
                                }
                            });
                    });

                return GenerateResponse(userWiseCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<IResponseInfo> GenerateResponse(Dictionary<int, IUserInfo>.ValueCollection userWiseList)
        {
            if (userWiseList == null || !userWiseList.Any())
            {
                throw new Exception("No valid data received to generate Results!");
            }

            var response = new List<IResponseInfo>();

            foreach (var userInfo in userWiseList)
            {
                userInfo.InstalledApplications.Values.ToList().ForEach(i =>
                    {
                        if (i.LicenseCountRequired > 0)
                        {
                            response.Add(new ResponseInfo()
                            {
                                UserId = userInfo.UserId,
                                ApplicationId = i.ApplicationId,
                                NoOfLicenseRequired = i.LicenseCountRequired
                            });
                        }
                    }
                );
            }

            return response;
        }
    }
}
