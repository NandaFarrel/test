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
            // PERBAIKAN: Menggunakan nama connection string yang benar dari Web.config
            GlobalConfiguration.Configuration.UseSqlServerStorage("TargetDbContext");

            // Aktifkan Dashboard Hangfire
            app.UseHangfireDashboard();

            // Aktifkan Server Hangfire untuk memproses background jobs
            app.UseHangfireServer();

            // Daftarkan dan jadwalkan Recurring Job Anda
            RecurringJob.AddOrUpdate<OpenProjectSyncJob>(
                "sync-updates-to-openproject",
                job => job.SyncUpdatesToOpenProject(),
                Cron.Minutely());
        }
    }
}