using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LauncherServer
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Configure DB updates to occur if the web.config says it is ok
            if (bool.Parse(ConfigurationManager.AppSettings["MigrateDatabaseToLatestVersion"]))
            {
                var configuration = new LauncherServer.Migrations.LauncherDbContext.Configuration();
                var migrator = new DbMigrator(configuration);
                migrator.Update();

                var configuration_application = new LauncherServer.Migrations.ApplicationDbContext.Configuration();
                migrator = new DbMigrator(configuration_application);
                migrator.Update();


            }
        }
    }
}
