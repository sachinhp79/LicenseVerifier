
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Flexera.LicenseVerifier.BL;
using Flexera.LicenseVerifier.BL.DTO;
using Flexera.LicenseVerifier.BL.Enum;
using Flexera.LicenseVerifier.BL.Factory;
using Flexera.LicenseVerifier.BL.Logger;
using Flexera.LicenseVerifier.BL.Processor;
using Xunit;
using Xunit.Abstractions;

namespace Flexera.LicenseVerifier.UnitTests
{
    /// <summary>
    /// Unit test class to test the License Verifier functionality
    /// </summary>
    public class LicenseVerifierTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public LicenseVerifierTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TestReadDataFile()
        {
            var fileName = Path.GetDirectoryName(path: Assembly.GetExecutingAssembly().Location) + @"\DataFiles\UserInstallationDetails.csv";

            _testOutputHelper.WriteLine(DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));

            var installationDetails = LicenseVerificationHelper.ReadFile(fileName: fileName);

            _testOutputHelper.WriteLine(DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));

            Assert.NotNull(@object: installationDetails);
            Assert.IsType<List<string>>(@object: installationDetails);
            Assert.NotEmpty(installationDetails);
            Assert.Equal(220129, installationDetails.Count);
        }


        [Fact]
        public void TestReadConfigFile()
        {
            var configFileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\ValidationRules\Rules.csv";
            var rules = LicenseVerificationHelper.ReadFile(configFileName);

            Assert.NotNull(@object: rules);
            Assert.IsType<List<string>>(@object: rules);
            Assert.NotEmpty(rules);
            Assert.Equal(1, rules.Count);
        }

        [Fact]
        public void TestGenerateInstallationDetailsCollection()
        {
            var fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataFiles\sample-large.csv";
            var installationDetails = LicenseVerificationHelper.GenerateInstallationDetailsCollection(fileName);
            var i = installationDetails as InstallationDetailsInfo;
            Assert.NotNull(i);
            Assert.Equal(10000, i.UserWiseInstallations.Count);
        }

        [Fact]
        public void TestProcessValidationRules()
        {
            var fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataFiles\UserInstallationDetails.csv";
            var rules = LicenseVerificationHelper.ReadFile(fileName);
            var installationDetails = LicenseVerificationHelper.GenerateInstallationDetailsCollection(fileName);
            var i = installationDetails as InstallationDetailsInfo;
            Assert.NotNull(i);
            Assert.Equal(10000, i.UserWiseInstallations.Count);
            var userList = i.GetUsers().ToList();
            Assert.All(userList, user =>
            {
                Assert.NotEqual(-1, user.UserId);
                Assert.NotNull(user.InstalledApplications);
            } );
        }

        [Fact]
        public void TestGenerateRawData()
        {
            var fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataFiles\sample-large.csv";
            var rules = LicenseVerificationHelper.ReadFile(fileName);
            var collections = DataFactory.GenerateRawData(rules);
            Assert.NotNull(collections);
            Assert.IsType<List<string>>(@object: rules);
            Assert.NotEmpty(rules);
            Assert.All(collections,
                item =>
                {
                    Assert.NotEqual(-1, item.UserId);
                    Assert.NotEqual(-1, item.ApplicationId);
                    Assert.NotEqual(-1, item.ComputerId);
                    Assert.NotNull(item.Description);
                }
            );

            Assert.Equal(21998525, collections.Count);
        }

        [Theory]
        [InlineData(@"\DataFiles\sample-large.csv", @"\ValidationRules\Rules.csv")]
        public void TestResultGeneration(string dataFileName,string ruleFileName)
        {
            try
            {
                LogWriter.Instance.DoLogging("Starting License Verification Process...");

                var dataFilePath = Constants.ExecutingAssemblyPath + dataFileName;
                var ruleFilePath = Constants.ExecutingAssemblyPath + ruleFileName;

                var stopWatch = Stopwatch.StartNew();
                var stopWatch1 = Stopwatch.StartNew();

                LogWriter.Instance.DoLogging("Reading User Information and generating Installation details...");
                var installationDetails = LicenseVerificationHelper.GenerateInstallationDetailsCollection(dataFilePath);
                LogWriter.Instance.DoLogging("Time taken to generate data (secs): " + stopWatch1.Elapsed.TotalSeconds);
                LogWriter.Instance.DoLogging("Reading and generating Validation Rules...");

                stopWatch1.Restart();
                var validationRuleCollection = LicenseVerificationHelper.GenerateValidationRuleCollection(ruleFilePath);
                LogWriter.Instance.DoLogging("Time taken to generate validation rule (secs): " + decimal.Parse(stopWatch1.Elapsed.TotalSeconds.ToString(), NumberStyles.Float));
                LogWriter.Instance.DoLogging("Starting to process the data...");

                stopWatch1.Restart();
                var validationRuleProcessor = new ValidationRuleProcessor(validationRuleCollection);
                LogWriter.Instance.DoLogging("Time taken to Process validation rule (secs): " + decimal.Parse(stopWatch1.Elapsed.TotalSeconds.ToString(), NumberStyles.Float));
                LogWriter.Instance.DoLogging("Generating Results...");

                stopWatch1.Restart();
                LicenseVerificationHelper.GenerateResults(validationRuleProcessor.ProcessValidationRules(installationDetails));
                LogWriter.Instance.DoLogging("Time taken to generate Results: " + stopWatch1.Elapsed.TotalSeconds);
                LogWriter.Instance.DoLogging("Total time taken to process the file (secs): " + stopWatch.Elapsed.TotalSeconds);

                LogWriter.Instance.DoLogging("License Verification Process Complete1d Successfully!");
            }
            catch (Exception ex)
            {
                LogWriter.Instance.DoLogging("License Verification Process failed!");
                LogWriter.Instance.DoLogging(ex.StackTrace, LogLevel.Error);
            }
        }

    }
}
