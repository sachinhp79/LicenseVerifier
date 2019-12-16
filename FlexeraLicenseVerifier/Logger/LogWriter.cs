using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Flexera.LicenseVerifier.BL.Enum;
using Flexera.LicenseVerifier.BL.Interface;

namespace Flexera.LicenseVerifier.BL.Logger
{
    /// <summary>
    /// Singleton log class to log the information
    /// </summary>
    public sealed class LogWriter
    {
        private LogWriter()
        {
        }

        public static LogWriter Instance = new LogWriter();

        public void DoLogging(string message, LogLevel logLevel = LogLevel.Info)
        {
            try
            {
                CreateDirectoryInNotExists(Constants.LogDirectoryName);

                using (var outputFile = new StreamWriter(Constants.ExecutingAssemblyPath + Constants.LogsFilePath, true))
                {
                    outputFile.WriteLine(System.Enum.GetName(typeof(LogLevel), logLevel) + " : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt ") + message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void CreateDirectoryInNotExists(string directoryName)
        {
            var directoryPath = Constants.ExecutingAssemblyPath;
            if (!Directory.Exists(directoryPath + directoryName))
            {
                Directory.CreateDirectory(directoryPath + directoryName);
            }
        }

        public void GenerateResultFile(List<IResponseInfo> responseInfoList)
        {
            try
            {
                if(responseInfoList == null || !responseInfoList.Any())
                {
                    DoLogging("No valid response data to save.", LogLevel.Info);
                    return;
                }

                CreateDirectoryInNotExists(Constants.ResultsDirectory);

                using (var outputFile = new StreamWriter(Constants.ExecutingAssemblyPath + Constants.ResultFilePath))
                {
                    outputFile.WriteLine("UserId" + Constants.CommaSeparator + "ApplicationId" + Constants.CommaSeparator + "No of License Required");
                    foreach (var r in responseInfoList)
                    {
                        outputFile.WriteLine(r.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
