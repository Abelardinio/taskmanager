# Task manager


## Required software for installation:

* [.NET Core 2.1 or higher](https://dotnet.microsoft.com/download)
* [RabbitMQ](https://www.rabbitmq.com/download.html)
* [NodeJS 10 or higher](https://nodejs.org/en/)
* [URL Rewrite plugin for IIS](https://www.iis.net/downloads/microsoft/url-rewrite)
* [spublisher](https://github.com/suzdorf/spublisher) 
* npm and Angular cli installed globally
* MS SQL server

## installation


* Make sure you rabbit mq instance is running under **localhost:5672** and has a default *guest*  user
* Create an empty database in MS SQL server and add its connection string value to *"DbConnectionSettings"* connection string of **appsettings.json**  file of  TaskManager.WebApi project
* Run cmd in TaskManager.Deployment folder
* Execute command:
```
spublisher
```
* After spublisher completes add to the hosts file the following line:
```
127.0.0.1 taskmanager
```
## To run developer server:

* Go to TaskManager.Client
* Run from cmd
```
ng serve
```
* Disable web security in browser 
https://stackoverflow.com/questions/3102819/disable-same-origin-policy-in-chrome
