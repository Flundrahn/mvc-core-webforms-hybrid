﻿using System;
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
