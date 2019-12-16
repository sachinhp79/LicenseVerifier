using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flexera.LicenseVerifier.BL.DTO;
using Flexera.LicenseVerifier.BL.Enum;
using Flexera.LicenseVerifier.BL.Interface;
using Flexera.LicenseVerifier.BL.Logger;

namespace Flexera.LicenseVerifier.BL.Factory
{
    /// <summary>
    /// Factory class to generate the Installation details of user from file
    /// </summary>
    public sealed class DataFactory: IDataFactory
    {
        private object lockObj = new object();

        /// <summary>
        /// Generates the instance of Installation Details from Raw data
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public IInstallationDetails GenerateData(List<string> dataList)
        {
            try
            {
                var rawDataCollection = GenerateRawData(dataList);
                var installationDetailsInfo = new InstallationDetailsInfo();
                foreach (var rawData in rawDataCollection)
                {
                    try
                    {
                        var userInfo = PopulateUserInfo(rawData.UserId, installationDetailsInfo);
                        var applicationInfo = PopulateApplicationInfo(userInfo, rawData.ApplicationId, rawData.Description);
                        var deviceInfo = PopulateDeviceInfo(applicationInfo, rawData.ComputerId, rawData.Device);
                    }
                    catch (Exception ex)
                    {
                        LogWriter.Instance.DoLogging("Invalid data : ");
                        LogWriter.Instance.DoLogging(ex.Message, LogLevel.Error);
                    }
                }
                return installationDetailsInfo;
            }
            catch (Exception ex)
            {
                LogWriter.Instance.DoLogging("Invalid data : " + ex.StackTrace, LogLevel.Error);
                throw;
            }
        }

        /// <summary>
        /// Creates the instance of UserInfo and loads in the collection
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="installationDetails"></param>
        /// <returns></returns>
        private IUserInfo PopulateUserInfo(int userId, IInstallationDetails installationDetails)
        {
            if(!installationDetails.IsExistingUser(userId))
            {
                var userInfo = new UserInfo(userId);
                installationDetails.AddToCollection(userInfo);
                return userInfo;
            }

            return installationDetails.FindUser(userId);
        }

        /// <summary>
        /// Creates the instance of ApplicationInfo and loads in the collection
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="applicationId"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        private IApplicationInfo PopulateApplicationInfo(IUserInfo userInfo, int applicationId, string description)
        {
            if(!userInfo.IsExistingApplication(applicationId))
            {
                var applicationInfo = new ApplicationInfo(applicationId, description);
                userInfo.AddToCollection(applicationInfo);
                return applicationInfo;
            }

            return userInfo.FindApplication(applicationId);
        }

        /// <summary>
        /// Creates the instance of DeviceInfo and loads in the collection
        /// </summary>
        /// <param name="applicationInfo"></param>
        /// <param name="deviceId"></param>
        /// <param name="deviceType"></param>
        /// <returns></returns>
        private IDeviceInfo PopulateDeviceInfo(IApplicationInfo applicationInfo, int deviceId, DeviceType deviceType)
        {
            if (!applicationInfo.IsExistingDevice(deviceId))
            {
                var deviceInfo = new DeviceInfo(deviceId, deviceType);
                applicationInfo.AddToCollection(deviceInfo);
                return deviceInfo;
            }

            return applicationInfo.FindDevice(deviceId);
        }

        /// <summary>
        /// Generates the collection of Raw Data from file
        /// </summary>
        /// <param name="rawDataList"></param>
        /// <returns></returns>
        public static List<RawData> GenerateRawData(List<string> rawDataList)
        {
            var userId = 0;
            try
            {
                var rawDataCollection = new List<RawData>();
                foreach (var rawDataString in rawDataList)
                {
                    var columnValues = rawDataString.Split(',');
                    userId = int.Parse(columnValues[1]);
                    var rawData = new RawData()
                    {
                        ComputerId = int.Parse(columnValues[0]),
                        UserId = int.Parse(columnValues[1]),
                        ApplicationId = int.Parse(columnValues[2]),
                        Device = (DeviceType)System.Enum.Parse(typeof(DeviceType), columnValues[3].ToUpper()),
                        Description = columnValues[4]
                    };
                    rawDataCollection.Add(rawData);
                }

                return rawDataCollection;
            }
            catch (Exception e)
            {
                Console.WriteLine(userId);
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
