using Hangfire;
using Hangfire.SqlServer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace hangfire_template
{

    public class MvcApplication : HttpApplication
    {

        //fokud ke pendaftaran
        protected void Application_Start()
        {


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DevExtremeBundleConfig.RegisterBundles(BundleTable.Bundles);

            Utils.HangfireBootstrapper.Instance.Stop();

            Utils.HangfireBootstrapper.Instance.Start();
            //HangfireAspNet.Use(GetHangfireServers);

        }

        //setiap http ambil request
        protected void Application_BeginRequest()
        {
            CultureInfo info = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.ToString());
            info.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            System.Threading.Thread.CurrentThread.CurrentCulture = info;

        }

        protected void Application_End(object sender, EventArgs e)
        {
            Utils.HangfireBootstrapper.Instance.Stop();
        }
    }
}
