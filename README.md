# cohort_analysis
cohort analysis on a set of customers to help identify changes in ordering behavior based on their signup date.

## How to run the Cohort Analysis Application?
This application is using dotnet core 2.1 and has been containerized using Docker. Navigate to `Invitae.CohortAnalysis` and run the following command

```sh
docker-compose build
...
    Successfully built 9370f27171e0
    Successfully tagged invitaecohortanalysis_cohort-analysis-console-application:latest
...
docker-compose run cohort-analysis-console-application -t
...
    Invitae.CohortAnalysis Initiated...
    Please provide customer data filename within the DataFiles folder.
    Note: Empty name will use default customer.csv
...
```
- ./DataFiles - Folder to consume customer and order data
- ./OutputResults - All Cohort Analysis are saved within this folder

### Preview
![preview](https://cl.ly/7e7df49ff1e2/Screen%252520Recording%2525202018-08-26%252520at%25252011.48%252520PM.gif)


### Output Result in .csv format
![preview](https://cl.ly/a499f1/Screen%252520Shot%2525202018-08-28%252520at%2525208.20.50%252520PM.png)

## How to run the test cases?
Navigate to `./Invitae.CohortAnalysis` and run the following command
```sh
docker build -f Dockerfile-tests .
```

```sh
Test run for /src/Invitae.CohortAnalysis.Business.Test/bin/Debug/netcoreapp2.1/Invitae.CohortAnalysis.Business.Test.dll(.NETCoreApp,Version=v2.1)
Microsoft (R) Test Execution Command Line Tool Version 15.8.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...

Total tests: 9. Passed: 9. Failed: 0. Skipped: 0.
Test Run Successful.
Test execution time: 1.7156 Seconds
...
Test run for /src/Invitae.CohortAnalysis.Helpers.Test/bin/Debug/netcoreapp2.1/Invitae.CohortAnalysis.Helpers.Test.dll(.NETCoreApp,Version=v2.1)
Microsoft (R) Test Execution Command Line Tool Version 15.8.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...

Total tests: 11. Passed: 11. Failed: 0. Skipped: 0.
Test Run Successful.
...

Test run for /src/Invitae.CohortAnalysis.Services.Test/bin/Debug/netcoreapp2.1/Invitae.CohortAnalysis.Services.Test.dll(.NETCoreApp,Version=v2.1)
Microsoft (R) Test Execution Command Line Tool Version 15.8.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...

Total tests: 16. Passed: 16. Failed: 0. Skipped: 0.
Test Run Successful.
Test execution time: 2.0726 Seconds
...
```
