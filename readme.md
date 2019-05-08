# Task manager


## Required software for installation:

* [.NET Core 2.1 or higher](https://dotnet.microsoft.com/download)
* [RabbitMQ](https://www.rabbitmq.com/download.html)
* [NodeJS 10 or higher](https://nodejs.org/en/)
* [URL Rewrite plugin for IIS](https://www.iis.net/downloads/microsoft/url-rewrite)
* npm and Angular cli installed globally
* MS SQL server
* Mongo db

## Installation

* Ensure you have default installation of mongo db and it is available by "mongodb://localhost:27017" connection string
* Ensure your MS SQL Server supports both SQL Server And Windows Authentication. Ensure it is available using connection string *"Server=(local);Integrated Security=SSPI;"*.
* Ensure your RabbitMQ Server is available by default connection:

```

{
    "Username": "guest",
    "Password": "guest",
    "VirtualHost": "/",
    "HostName": "localhost",
    "Port": "5672"
}

```
* Run cmd under administrator permissions within TaskManager.Deployment folder 
* Execute command:

```
install

```

* The appliaction now is available at http://taskmanager

## Docker

* Execute cmd within TaskManager.Deployment folder
* Execute command:

```
package

```

* Execute command:

```
cd ..\TaskManager.Deployment
docker-compose up -d

```
* Ensure all containers are _up_
* The site is availbale by the url http://localhost:5000

## To run developer server:

* Go to TaskManager.Client
* Run from cmd
```
ng serve
```
* Disable web security in browser 
https://stackoverflow.com/questions/3102819/disable-same-origin-policy-in-chrome
