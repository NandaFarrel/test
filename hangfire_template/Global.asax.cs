using System;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace hangfire_template
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DevExtremeBundleConfig.RegisterBundles(BundleTable.Bundles);

            // TIDAK ADA LAGI PEMANGGILAN HangfireBootstrapper DI SINI
        }

        // Metode ini tetap sama, berfungsi untuk mengatur format tanggal
        protected void Application_BeginRequest()
        {
            CultureInfo info = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.ToString());
            info.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            System.Threading.Thread.CurrentThread.CurrentCulture = info;
        }

        // Metode ini juga sudah tidak memerlukan pemanggilan HangfireBootstrapper
        protected void Application_End(object sender, EventArgs e)
        {
            // Tidak perlu melakukan apa-apa di sini terkait Hangfire
        }
    }
}