# mvc-core-webforms-hybrid
To experiment with using YARP to combine an ASP.NET Core MVC app with an ASP.NET Web Forms app that both use the same NHibernate data layer.

## Requirements
- Microsoft SQL Server must be installed and running
- Visual Studio is required for building and running the solution

## Setup
- clone repo
- note that default connection string resides in `appsettings.json` for the MVC app, and in `web.config` for the Web Forms app
- note that NHibernateHelper will export schema, meaning it will write over any existing data in tables.
- before running ensure must be created
- open in Visual Studio
- build and run

# ToDo
- migrate mvc app to .net 10
- make the UI identical to showcase possible approach to make the two apps seamless to users
- make nicer description of project

# Draft project description
NOTE - clean this up after finishing

## About project and stuff
- Data is .NET Standard library that can be used by both .NET Framework and .NET Core
- Runs a WebForms app and an MVC Core app in parallell to demonstrate possible approach to strangle fig pattern way of migrating a legacy app
