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
**NOTE:** any data files or outputted results will read/save within `./DataFiles` and `./OutputResults`

### Preview
![preview](https://cl.ly/7e7df49ff1e2/Screen%252520Recording%2525202018-08-26%252520at%25252011.48%252520PM.gif)


### Output Result in .csv format
![preview](https://cl.ly/7ed37767c70e)

## How to run the test cases?
Navigate to `Invitae.CohortAnalysis` and run the following command
```sh
docker build -f Dockerfile-tests .
```

```sh
#TODO add test results here
```
