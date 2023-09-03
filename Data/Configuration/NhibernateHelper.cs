using Data.Entities;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Data.Configuration
{
    public static class NhibernateHelper
    {
        private const string ConnectionString = @"Data Source=localhost;Initial Catalog=BlazorWebFormsHybrid;Integrated Security=True";
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

        //private FluentConfiguration GetConfiguration()
        //{
        //    return
        //}

        //public void Migrate()
        //{
        //    var cfg = new Configuration();
        //    cfg.Configure()

        //    new SchemaExport(GetConfiguration().BuildConfiguration()).Execute(true, true, false);
        //}
    }
}

//		Message	"The entity 'NhibernateHelper' doesn't have an Id mapped. Use the Id method to map your identity property. For example: Id(x => x.Id)."	string
//+		RelatedEntity	{Name = "NhibernateHelper" FullName = "Data.Configuration.NhibernateHelper"}	System.Type {System.RuntimeType}
//		Resolution	"Use the Id method to map your identity property. For example: Id(x => x.Id)"	string
//		Source	"FluentNHibernate"	string
