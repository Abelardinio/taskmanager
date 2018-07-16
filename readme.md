# Task manager


## MAchine Requirements:

* .Net 4.7.1 SDK
* URL Rewrite plugin for IIS
* MS SQL server
* NodeJS 10 or higher
* npm and Angular cli installed globally

## Installing

* Build solution in visual studio
* Go to TaskManager.Deployment folder
* Run build-client.bat
* Create empty database in MS SQL server and add its connection string value to "DBConnection" connection string of Web.config file of  TaskManager.WebApi project
* Run IISMAnage tool with admin rights.
* Add to hosts file  "127.0.0.1 taskmanager"

## To run developer server:

* Go to TaskMAnager.Client
* Run from cmd
```
ng serve
```
* Disable web security in browser 
https://stackoverflow.com/questions/3102819/disable-same-origin-policy-in-chrome
