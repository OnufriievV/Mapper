using System;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CamoTest.Startup))]

namespace CamoTest
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("CamoTestEntities");
            app.UseHangfireDashboard(); // comment this in production
            app.UseHangfireServer();
        }
    }
}
