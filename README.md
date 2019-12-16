1)	Flexera License Verifier:
This project contains the business logic of the application. It consists of the Factory classes which generates the Rules and Data which has been provided in the csv files. The Validation Rule Processor class is the one which processed the validation rules against the data and generates the results. The Logger is a singleton class which is responsible to create the logs of the application.

2)	Flexera License Verifier Unit Test:
This is the X-unit test project created to test the different routines provided in the utility.
“TestResultGeneration” is the test case which does the complete processing of the application and generates the results in the directory named “Results” created under the Executing assemblies Debug folder.


Files and Folders location:

1)	DataFiles:
The data file to be processed should be placed under this folder.
2)	Logs:
The logs generated will processing the file will be generated under this folder.
3)	ValidationRules:
The rules which are to be executed against the data are stored under this folder. The rules are to be defined in a Rules.csv file.

 
4)	Results:
The results generated after the data is processed successfully will be stored under this folder. The Results_<DateTime>.csv file will be generated which will contain the userwise information which will mention total no of License required for an application (mentioned in rule file).

Notes:
Assumptions are made that the data and rules defined in the file is a valid data.
The file name are not read from the configuration file but are provided in the test utility.
