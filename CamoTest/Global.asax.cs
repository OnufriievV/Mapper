using CamoTest.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Hangfire;

namespace CamoTest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private BackgroundJobServer _backgroundJobServer;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //WebApiConfig.Register(System.Web.Http.GlobalConfiguration.Configuration);
            Bootstrapper.Run();

            Hangfire.GlobalConfiguration.Configuration.UseSqlServerStorage("CamoTestEntities");
            _backgroundJobServer = new BackgroundJobServer();
        }

        protected void Application_End()
        {
           _backgroundJobServer.Dispose();
        }

    }
}
