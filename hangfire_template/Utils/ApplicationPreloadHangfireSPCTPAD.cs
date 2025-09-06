using System;
using System.IO;

namespace hangfire_template.Utils
{
    public class ApplicationPreloadHangfireSPCTPAD : System.Web.Hosting.IProcessHostPreloadClient
    {
        public void Preload(string[] parameters)
        {
            //var dir = System.Web.HttpContext.Current.Server.MapPath("~/Content/logPRELOAD.txt");
            //StreamWriter fs = new StreamWriter(dir, true);
            //fs.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " started ");
            //fs.Dispose();

            HangfireBootstrapper.Instance.Stop();

            HangfireBootstrapper.Instance.Start();

        }
    }
}
