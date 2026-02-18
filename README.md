# mvc-core-webforms-hybrid
To experiment with using YARP to combine a MVC Core app with a .NET Framework Web Forms app that both use the same NHibernate data layer.

## Requirements
- Microsoft SQL Server must be installed and running
- Visual Studio is required for building and running the solution

## Setup
- clone repo
- note that default connection string resides in appsettings.json for the MVC Core app, and in web.config for the Web Forms app
- note that NHibernateHelper will export schema, meaning it will write over any existing data in tables.
- before running ensure db exists
- open in Visual Studio
- build and run