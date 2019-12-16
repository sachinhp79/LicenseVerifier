using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Flexera.LicenseVerifier.BL
{
    public class Constants
    {
        public const char CommaSeparator = ',';

        public const string ResultsDirectory = @"\Results\";
        private static readonly string ResultsFileName = @"Results_" + DateTime.Now.ToString("ddMMyyyyhhmmsstt") + ".csv";
        public const string LogDirectoryName = @"\Logs\";
        public static readonly string LogsFilePath = LogDirectoryName + @"Flexera_License_Verifier_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".txt";
        public static readonly string ResultFilePath = ResultsDirectory + ResultsFileName;
        public static readonly string ExecutingAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}
