using Hangfire;
using Hangfire.Common;
using hangfire_template.Models;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace hangfire_template.Controllers
{
    public class SyncTableINFORController : Controller
    {
        public class NotifyOnFailed : JobFilterAttribute
        {
            private string _deskripsi;
            public NotifyOnFailed() { }
            public NotifyOnFailed(string deskripsi)
            {
                _deskripsi = deskripsi;
            }
        }

        public SyncTableINFORController()
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public static string ExecuteCommand(string command, int timeout)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
            {
                FileName = "C:\\Windows\\System32\\cmd.exe",
                CreateNoWindow = false,
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            Process process = System.Diagnostics.Process.Start(processInfo);
            process.WaitForExit();
            String strOutPut = process.StandardOutput.ReadToEnd();
            process.Close();
            return strOutPut;
        }

        public string ProsesSync_Cluster_1()
        {
            var execute = "";
            try
            {
                var cmd = @"C:\job_cluster_1.bat";
                execute = ExecuteCommand(cmd, 10000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(execute);
                throw (ex);
            }
            return execute;
        }

        public async Task<string> ProsesSync_Cluster_4()
        {
            var execute = "";
            try
            {
                var cmd = @"C:\BIG_DATA\JOB\job_cluster_4.bat";
                execute = await Task.Run(() => ExecuteCommand(cmd, 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(execute);
                throw (ex);
            }
            return execute;
        }

        public async Task<string> ProsesSync_Cluster_6()
        {
            var execute = "";
            try
            {
                var cmd = @"C:\BIG_DATA\JOB\job_cluster_6.bat";
                execute = await Task.Run(() => ExecuteCommand(cmd, 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(execute);
                throw (ex);
            }
            return execute;
        }


        [AutomaticRetry(Attempts = 0)]
        public void ProsesUploadImagesPlantSemarang()
        {
            using (var TargetDbContext = new TargetDbContext())
            {
                try
                {
                    TargetDbContext.Database.CommandTimeout = 0;
                    // ... (Sisa logika Anda di sini tidak diubah) ...
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                    throw;
                }
            }
        }

        [AutomaticRetry(Attempts = 0)]
        [NotifyOnFailed("Sync Data Part1 Failed. Info: {obj}")]
        public void ProsesAllSync_Part1() // DIUBAH: Disederhanakan menjadi non-async
        {
            using (var GSDbContext = new GSDbContext())
            {
                GSDbContext.Database.ExecuteSqlCommand("DELETE FROM HangFire.Job WHERE InvocationData LIKE '%ProsesAllSync_Part1%' AND StateName LIKE '%Enqueued%'");
                var checkRunningJob = GSDbContext.Database.SqlQuery<int>("SELECT COUNT(*) AS RUNNING_JOB FROM HangFire.Job WHERE InvocationData LIKE '%ProsesAllSync_Part1%' AND StateName LIKE '%Processing%'").FirstOrDefault();
                if (checkRunningJob == 1)
                {
                    // PROSESNYA DISINI
                }
            }
        }

        [AutomaticRetry(Attempts = 0)]
        [NotifyOnFailed("Sync Data Part2 Failed. Info: {obj}")]
        public void ProsesAllSync_Part2() // DIUBAH: Disederhanakan menjadi non-async
        {
            using (var GSDbContext = new GSDbContext())
            {
                GSDbContext.Database.ExecuteSqlCommand("DELETE FROM HangFire.Job WHERE InvocationData LIKE '%ProsesAllSync_Part2%' AND StateName LIKE '%Enqueued%'");
                var checkRunningJob = GSDbContext.Database.SqlQuery<int>("SELECT COUNT(*) AS RUNNING_JOB FROM HangFire.Job WHERE InvocationData LIKE '%ProsesAllSync_Part2%' AND StateName LIKE '%Processing%'").FirstOrDefault();
                if (checkRunningJob == 1)
                {
                    // PROSESNYA DISINI
                }
            }
        }

        [AutomaticRetry(Attempts = 0)]
        [NotifyOnFailed("Sync Data Part3 Failed. Info: {obj}")]
        public void ProsesAllSync_Part3() // DIUBAH: Disederhanakan menjadi non-async
        {
            using (var GSDbContext = new GSDbContext())
            {
                GSDbContext.Database.ExecuteSqlCommand("DELETE FROM HangFire.Job WHERE InvocationData LIKE '%ProsesAllSync_Part3%' AND StateName LIKE '%Enqueued%'");
                var checkRunningJob = GSDbContext.Database.SqlQuery<int>("SELECT COUNT(*) AS RUNNING_JOB FROM HangFire.Job WHERE InvocationData LIKE '%ProsesAllSync_Part3%' AND StateName LIKE '%Processing%'").FirstOrDefault();
                if (checkRunningJob == 1)
                {
                    // PROSESNYA DISINI
                }
            }
        }
    }
}