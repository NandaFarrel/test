using Hangfire;
using Hangfire.SqlServer;
using hangfire_template.Controllers;
using System;
using System.Configuration; // DIUBAH: Ditambahkan untuk membaca Web.config
using System.Data.SqlClient; // DIUBAH: Ditambahkan untuk membaca connection string
using System.Web.Hosting;

namespace hangfire_template.Utils
{
    public class HangfireBootstrapper : IRegisteredObject
    {
        public static readonly HangfireBootstrapper Instance = new HangfireBootstrapper();
        private readonly object _lockObject = new object();
        public BackgroundJobServer _backgroundJobServer;
        public GSDbContext GSDbContext { get; set; }

        private HangfireBootstrapper()
        {
            // DIUBAH: Mengambil detail koneksi dari Web.config secara dinamis
            var connStr = ConfigurationManager.ConnectionStrings["HangfireDb"].ConnectionString;
            var builder = new SqlConnectionStringBuilder(connStr);
            GSDbContext = new GSDbContext(builder.DataSource, builder.InitialCatalog, builder.UserID, builder.Password);
        }

        public void Start()
        {
            lock (_lockObject)
            {
#if DEBUG
                // Kode untuk membersihkan tabel Hangfire saat debug, biarkan seperti ini.
#endif

                HostingEnvironment.RegisterObject(this);

                // DIUBAH: Mengambil connection string langsung dari Web.config
                string connectionString = ConfigurationManager.ConnectionStrings["HangfireDb"].ConnectionString;

                GlobalConfiguration.Configuration
                    .UseSqlServerStorage(connectionString);

                var sqlStorage = new SqlServerStorage(connectionString);

                startHangfireServer(sqlStorage);
            }
        }

        // DIUBAH: Seluruh method ini diperbarui untuk menambahkan job OpenProject
        public void startHangfireServer(SqlServerStorage sqlStorage)
        {
            JobStorage.Current = sqlStorage;

            var optionServer = new BackgroundJobServerOptions
            {
                ServerName = "ServerUtama",
                // Menambahkan queue baru untuk pekerjaan OpenProject agar terpisah
                Queues = new[] { "1_primary_job", "2_openproject_sync" },
                WorkerCount = 2 // Menaikkan worker agar bisa menjalankan 2 job bersamaan
            };

            _backgroundJobServer = new BackgroundJobServer(optionServer, sqlStorage);

            var monitoringApi = sqlStorage.GetMonitoringApi();
            var serverList = monitoringApi.Servers();

            // --- Konfigurasi Recurring Job yang sudah ada ---
            RecurringJobManager recurJobM = new RecurringJobManager(sqlStorage);
            RecurringJobOptions recurJobOpt1 = new RecurringJobOptions()
            {
                QueueName = "1_primary_job",
            };

            var conid_job = "proses_sync_data_part1";
            recurJobM.RemoveIfExists(conid_job);
            recurJobM.AddOrUpdate(conid_job, Hangfire.Common.Job.FromExpression<SyncTableINFORController>(x => x.ProsesAllSync_Part1()), Cron.MinuteInterval(50), recurJobOpt1);

            var conid_job2 = "proses_sync_data_part2";
            recurJobM.RemoveIfExists(conid_job2);
            recurJobM.AddOrUpdate(conid_job2, Hangfire.Common.Job.FromExpression<SyncTableINFORController>(x => x.ProsesAllSync_Part2()), Cron.MinuteInterval(50), recurJobOpt1);

            var conid_job3 = "proses_sync_data_part3";
            recurJobM.RemoveIfExists(conid_job3);
            recurJobM.AddOrUpdate(conid_job3, Hangfire.Common.Job.FromExpression<SyncTableINFORController>(x => x.ProsesAllSync_Part3()), Cron.MinuteInterval(50), recurJobOpt1);

            // --- TAMBAHAN: JOB BARU UNTUK OPENPROJECT DI SINI ---
            RecurringJobOptions recurJobOptOpenProject = new RecurringJobOptions()
            {
                QueueName = "2_openproject_sync", // Menggunakan queue khusus
                TimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time") // Zona Waktu Jakarta (WIB)
            };

            var openProjectSyncJobId = "sync-db-to-openproject";
            recurJobM.RemoveIfExists(openProjectSyncJobId);
            // Menjadwalkan job untuk berjalan setiap 15 menit
            recurJobM.AddOrUpdate(
                openProjectSyncJobId,
                Hangfire.Common.Job.FromExpression<hangfire_template.Controllers.OpenProjectSyncController>(
                    x => x.SyncNewWorkPackages()
                ),
                Cron.MinuteInterval(15),
                recurJobOptOpenProject
            );
            // --------------------------------------------------------
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