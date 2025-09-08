using Hangfire;
using Hangfire.Common;
using Hangfire.SqlServer;
using hangfire_template.Controllers;
using System;
using System.Configuration;
using System.Web.Hosting;

namespace hangfire_template.Utils
{
    public class HangfireBootstrapper : IRegisteredObject
    {
        public static readonly HangfireBootstrapper Instance = new HangfireBootstrapper();
        private readonly object _lockObject = new object();
        public BackgroundJobServer _backgroundJobServer;

        private HangfireBootstrapper() { }

        public void Start()
        {
            lock (_lockObject)
            {
                HostingEnvironment.RegisterObject(this);
                string connectionString = ConfigurationManager.ConnectionStrings["HangfireDb"].ConnectionString;
                GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);
                var sqlStorage = new SqlServerStorage(connectionString);
                startHangfireServer(sqlStorage);
            }
        }

        public void startHangfireServer(SqlServerStorage sqlStorage)
        {
            JobStorage.Current = sqlStorage;

            var optionServer = new BackgroundJobServerOptions
            {
                ServerName = "ServerUtama",
                Queues = new[] { "1_primary_job", "2_openproject_sync" },
                WorkerCount = 2
            };

            _backgroundJobServer = new BackgroundJobServer(optionServer, sqlStorage);

            var recurJobM = new RecurringJobManager(sqlStorage);

            RecurringJobOptions recurJobOpt1 = new RecurringJobOptions()
            {
                QueueName = "1_primary_job",
                TimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            };

            // DIUBAH: Memanggil method-method yang benar dari SyncTableINFORControllerV2
            recurJobM.AddOrUpdate("sync-cluster-1",
                Job.FromExpression<SyncTableINFORController>(x => x.ProsesSync_Cluster_1()),
                Cron.Daily(1), // Berjalan setiap hari jam 1 pagi
                recurJobOpt1);

            recurJobM.AddOrUpdate("sync-cluster-4",
                Job.FromExpression<SyncTableINFORController>(x => x.ProsesSync_Cluster_4()),
                Cron.Daily(2), // Berjalan setiap hari jam 2 pagi
                recurJobOpt1);

            recurJobM.AddOrUpdate("sync-cluster-6",
                Job.FromExpression<SyncTableINFORController>(x => x.ProsesSync_Cluster_6()),
                Cron.Daily(3), // Berjalan setiap hari jam 3 pagi
                recurJobOpt1);

            recurJobM.AddOrUpdate("upload-images-semarang",
                Job.FromExpression<SyncTableINFORController>(x => x.ProsesUploadImagesPlantSemarang()),
                Cron.Hourly(), // Berjalan setiap jam
                recurJobOpt1);


            // --- Job untuk OpenProject (tidak diubah) ---
            RecurringJobOptions recurJobOptOpenProject = new RecurringJobOptions()
            {
                QueueName = "2_openproject_sync",
                TimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            };

            var openProjectSyncJobId = "sync-db-to-openproject";
            recurJobM.AddOrUpdate(openProjectSyncJobId,
                Job.FromExpression<OpenProjectSyncController>(x => x.SyncNewWorkPackages()),
                Cron.MinuteInterval(15),
                recurJobOptOpenProject);
        }

        public void Stop()
        {
            lock (_lockObject)
            {
                if (_backgroundJobServer != null)
                {
                    _backgroundJobServer.Dispose();
                }
                HostingEnvironment.UnregisterObject(this);
            }
        }

        void IRegisteredObject.Stop(bool immediate)
        {
            Stop();
        }
    }
}