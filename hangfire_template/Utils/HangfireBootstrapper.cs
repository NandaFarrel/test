// Utils/HangfireBootstrapper.cs
using Hangfire; // DIUBAH: Menambahkan using yang hilang
using Hangfire.Common;
using Hangfire.SqlServer;
using hangfire_template.Controllers; // DIUBAH: Menambahkan using yang hilang
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

                GlobalConfiguration.Configuration
                    .UseSqlServerStorage(connectionString);

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

            // Memanggil kembali method-method asli Anda dari SyncTableINFORController
            var conid_job = "proses_sync_data_part1";
            recurJobM.RemoveIfExists(conid_job);
                    recurJobM.AddOrUpdate(conid_job, Job.FromExpression<SyncTableINFORController>(x => x.ProsesAllSync_Part1()), "*/50 * * * *", recurJobOpt1);

            var conid_job2 = "proses_sync_data_part2";
            recurJobM.RemoveIfExists(conid_job2);
            recurJobM.AddOrUpdate(conid_job2, Job.FromExpression<SyncTableINFORController>(x => x.ProsesAllSync_Part2()), "*/50 * * * *", recurJobOpt1);

            var conid_job3 = "proses_sync_data_part3";
            recurJobM.RemoveIfExists(conid_job3);
            recurJobM.AddOrUpdate(conid_job3, Job.FromExpression<SyncTableINFORController>(x => x.ProsesAllSync_Part3()), "*/50 * * * *", recurJobOpt1);


            // --- Job Baru untuk OpenProject ---
            RecurringJobOptions recurJobOptOpenProject = new RecurringJobOptions()
            {
                QueueName = "2_openproject_sync",
                TimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")
            };

            var openProjectSyncJobId = "sync-db-to-openproject";
            recurJobM.RemoveIfExists(openProjectSyncJobId);
            recurJobM.AddOrUpdate(
                openProjectSyncJobId,
                Job.FromExpression<OpenProjectSyncController>(x => x.SyncNewWorkPackages()),
                "*/15 * * * *",
                recurJobOptOpenProject
            );
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