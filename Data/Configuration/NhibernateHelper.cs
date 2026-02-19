using Data.Entities;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;

namespace Data.Configuration
{
    public static class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        public static ISessionFactory SessionFactory => _sessionFactory 
            ?? throw new InvalidOperationException("SessionFactory is not initialized. Call InitSessionFactory with a valid connection string before accessing it.");

        public static void InitSessionFactory(string connectionString, bool showSql = true)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string is null or empty. Please provide a valid connection string to initialize the SessionFactory.");
            }
            _sessionFactory = CreateSessionFactory(connectionString, showSql);
        }

        private static ISessionFactory CreateSessionFactory(string connectionString, bool showSql)
        {
            var dbConfig = MsSqlConfiguration.MsSql2012
                .ConnectionString(connectionString);

            if (showSql)
            {
                dbConfig.ShowSql();
            }

            return Fluently.Configure()
               .Database(dbConfig)
               .Mappings(m => m.AutoMappings
                   .Add(AutoMap.AssemblyOf<Product>().Where(a => a.Namespace == "Data.Entities")))
               .ExposeConfiguration(cfg =>
                {
                    var schemaExport = new SchemaExport(cfg);
                    schemaExport.Create(false, true);
                })
               .BuildSessionFactory();
        }
    }
}
