using Hangfire;
using Microsoft.Owin;
using Owin;
using hangfire_template.Services;

[assembly: OwinStartup(typeof(hangfire_template.Startup))]

namespace hangfire_template
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("TargetDbContext");

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            // JOB LAMA: Mengirim update DARI Database KE OpenProject
            RecurringJob.AddOrUpdate<OpenProjectSyncJob>(
                "sync-updates-to-openproject",
                job => job.SyncUpdatesToOpenProject(),
                Cron.Minutely()); // Berjalan setiap menit

            // JOB BARU: Mengambil data DARI OpenProject KE Database
            RecurringJob.AddOrUpdate<OpenProjectFetchJob>(
                "fetch-data-from-openproject",
                job => job.FetchAllWorkPackages("demo-project"), // Ganti "demo-project" dengan ID proyek Anda
                "*/5 * * * *"); // Berjalan setiap 5 menit
        }
    }
}