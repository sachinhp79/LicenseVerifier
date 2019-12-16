using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Flexera.LicenseVerifier.BL.Enum;
using Flexera.LicenseVerifier.BL.Factory;
using Flexera.LicenseVerifier.BL.Interface;
using Flexera.LicenseVerifier.BL.Logger;

namespace Flexera.LicenseVerifier.BL
{
    /// <summary>
    /// Helper class to provide the functionality of reading information from files and generating data
    /// </summary>
    public static class LicenseVerificationHelper
    {
        private static IDataFactory _dataFactory;
        private static IRuleFactory _ruleFactory;
        static LicenseVerificationHelper()
        {
            _dataFactory = new DataFactory();
            _ruleFactory = new RuleFactory();
        }

        /// <summary>
        /// Reads the file and creates collection
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<string> ReadFile(string fileName)
        {
            var reader = File.ReadAllLines(fileName);

            if (!reader.Any())
            {
                LogWriter.Instance.DoLogging($"{fileName}, Data file has no valid data.");
                return null;
            }

            var dataList = reader.ToList();
            dataList.RemoveAt(0);
            return dataList;
        }

        /// <summary>
        /// Generates the Installation details collection
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static IInstallationDetails GenerateInstallationDetailsCollection(string fileName)
        { 
            return _dataFactory.GenerateData(ReadFile(fileName));
        }

        /// <summary>
        /// Generates the Validation Rule collection
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static IValidationRuleCollection GenerateValidationRuleCollection(string fileName)
        {
            return _ruleFactory.GenerateRuleCollection(ReadFile(fileName));
        }

        /// <summary>
        /// Generates the Result file on the processed data
        /// </summary>
        /// <param name="responseInfoList"></param>
        public static void GenerateResults(List<IResponseInfo> responseInfoList)
        {
            if(responseInfoList == null)
            {
                throw new Exception("No valid response data received to generate Results!");
            }

            LogWriter.Instance.GenerateResultFile(responseInfoList);

        }
    }
}
