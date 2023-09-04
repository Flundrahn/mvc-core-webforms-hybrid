using Data.Entities;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace Data.Configuration
{
    public static class NhibernateHelper
    {
        private const string ConnectionString = @"Data Source=localhost;Initial Catalog=MvcCoreWebFormsHybrid;Integrated Security=True";
        private static ISessionFactory _sessionFactory;

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory is null)
                {
                    _sessionFactory = CreateSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static void InitSessionFactory()
        {
            _sessionFactory = CreateSessionFactory();
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
               .Database(MsSqlConfiguration.MsSql2012
                   .ConnectionString(ConnectionString)
                   .ShowSql())
               .Mappings(m => m.AutoMappings
                   .Add(AutoMap.AssemblyOf<Product>().Where(a => a.Namespace == "Data.Entities")))
               //.ExposeConfiguration(cfg =>
               // {
               //     var schemaExport = new SchemaExport(cfg);
               //     schemaExport.Create(false, true);
               // })
               .BuildSessionFactory();
        }
    }
}
