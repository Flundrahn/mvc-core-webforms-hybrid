# mvc-core-webforms-hybrid

An experimental solution to test a practical approach to start migrating legacy apps using the Strangler Fig pattern with YARP:

- **Data Layer**: A .NET Standard 2.0 library the same code is possible to run in both .NET Framework and .NET Core apps
- **Dual apps**: Runs both an ASP.NET Web Forms app (.NET Framework 4.8) and an ASP.NET Core MVC app (.NET 10) in parallel, using YARP to direct users between.
- **Migration Strategy**: Showcases how to gradually migrate functionality from a legacy Web Forms app to a modern ASP.NET Core app while maintaining a seamless user experience

NOTE that since YARP runs in the ASP.NET Core MVC app, only traffic there can be proxied between the two apps, if a user would surf directly to the Web Forms app, then the link in the nav bar to the new Products page won't work.

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

## Update 19/2/2026

I ran the new `Modernization experience` with copilot in VS 2026 with this project, to handle the upgrade the MVC app from .NET 6 to .NET 10. It was interesting and successful. It generated a mostly solid plan, and identified the necessary dependencies that needed handling. My own contribution was to ensure adding some integration tests before the update, make sure they were green both before and after. This was good since it helped identify and quickly fix a breaking change concerning NHibernate and virtual properties, which was solved by overriding MemberwiseClone and updating the packages for NHibernate and FluentNHibernate.