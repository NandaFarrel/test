using hangfire_template.Controllers;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


[assembly: OwinStartup(typeof(hangfire_template.App_Start.Startup))]

namespace hangfire_template.App_Start
{
    public class Startup
    {
        [Obsolete]
        public void Configuration(IAppBuilder app)
        {
            //app.MapSignalR();       
            //app.UseHangfireServer();
            app.UseHangfireDashboard();
        }
    }
}
