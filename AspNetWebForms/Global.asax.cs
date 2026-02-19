using System;
using System.Configuration;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Data.Configuration;
using Data.Repositories;

namespace WebForms
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            // Read ShowSql setting from configuration
            var showSqlSetting = ConfigurationManager.AppSettings["NHibernate.ShowSql"];
            var showSql = bool.TryParse(showSqlSetting, out var result) ? result : false;

            NHibernateHelper.InitSessionFactory(connectionString, showSql);

            AddInitialData();
        }

        private void AddInitialData()
        {
            var productRepository = new ProductRepository();
            var initialDataCreator = new InitialDataCreator(productRepository);

            initialDataCreator.AddProducts();
        }
    }
}
