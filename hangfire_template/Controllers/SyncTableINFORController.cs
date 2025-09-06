using System;
using System.Linq;
using System.Web.Mvc;
using System.Diagnostics;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using hangfire_template.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hangfire.Common;
using Hangfire;
using System.Globalization;
using System.Net;
using System.IO;

namespace hangfire_template.Controllers
{
    public class SyncTableINFORController : Controller
    {
        public GSDbContext GSDbContext { get; set; }
        public TargetDbContext TargetDbContext { get; set; }

        public class NotifyOnFailed : JobFilterAttribute
        {
            private string _deskripsi;

            public NotifyOnFailed()
            {
            }

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
            GSDbContext.Dispose();
            TargetDbContext.Dispose();
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
            var exitCode = process.ExitCode;
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
                execute = ExecuteCommand(cmd, 1);
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
                execute = ExecuteCommand(cmd, 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(execute);
                throw (ex);
            }
            return execute;
        }



        [AutomaticRetry(Attempts = 0)]
        [NotifyOnFailed("Process Upload Images SPCT PAD Plant Semarang Failed. Info: {obj}")]
        public void ProsesUploadImagesPlantSemarang()
        {
            try
            {
                TargetDbContext = new TargetDbContext(@"servername_here", "db_here", "user", "pass"); 
                TargetDbContext.Database.CommandTimeout = 0;

                var dtahun = DateTime.UtcNow.AddHours(7).ToString("yyyy");
                var dbulan = DateTime.UtcNow.AddHours(7).ToString("MM");
                var dtNow = DateTime.UtcNow.AddHours(7);

                var direktori_from = @"\\svr-data-lake\TEMP_IMAGE_SPCT";
                var direktori_completed = @"\\svr-data-lake\TEMP_IMAGE_SPCT\IMAGES_UPLOAD_COMPLETED";
                var direktori_failed = @"\\svr-data-lake\TEMP_IMAGE_SPCT\IMAGES_UPLOAD_FAILED";
                //var username = "hangfire";
                //var pass = "G5admin2021!";

                var username1 = @"SVR-DATA-LAKE\hangfire";
                var pass1 = "G5admin2021!";

                var username2 = @"GSPORTAL-PRD01\hangfire";
                var pass2 = "G5admin2021!";

                var direktori_destination = @"\\gsportal-prd01\wwwroot\samina-pad\Images";
                var direktori_destination_IMG_DB = @"Images/";

                using (new NetworkConnection(direktori_from.ToLower(), new NetworkCredential(username1, pass1)))
                {
                    if (!Directory.Exists(direktori_destination))
                    {
                        Directory.CreateDirectory(direktori_destination);
                    }

                    //var targetfile = Directory.EnumerateFiles(direktori_from.ToLower(), "*.jpg").Where(file => new FileInfo(file).LastWriteTime > dtNow.AddDays(-1)).ToList();
                    var targetfile = Directory.EnumerateFiles(direktori_from.ToLower(), "*.jpg").ToList().OrderBy(p => new FileInfo(p).Name);
                    
                    using (new NetworkConnection(direktori_destination.ToLower(), new NetworkCredential(username2, pass2)))
                    {
                        foreach (string file in targetfile)
                        {
                            var filename = Path.GetFileName(file.ToString()).ToString().Trim();
                            var filenameWithoutExtention = Path.GetFileNameWithoutExtension(file.ToString()).ToString().Trim();                           
                            if(filenameWithoutExtention.Contains("(") && filenameWithoutExtention.Contains(")")) filenameWithoutExtention = filenameWithoutExtention.Split('(')[0].ToString().Trim();

                            if (filename.Contains("C-2ISO-BLUEXXX-00-0000"))
                            {
                                var tes = "";
                            }
                            
                            var vTargetBackup = direktori_destination + @"\" + filename.Replace(" ", "");
                            var vdirektori_completed = direktori_completed + @"\" + filename.Replace(" ", "");
                            var vdirektori_failed = direktori_failed + @"\" + filename.Replace(" ", "");

                            var vTargetDB = direktori_destination_IMG_DB + filename.Replace(" ", "");

                            var getItemFromDB = TargetDbContext.Table_SPT_STOCK.Where(p => p.spt_itemnew == filenameWithoutExtention && p.spt_whs == "MWSPT" && p.spt_status == 1).SingleOrDefault();
                            if(getItemFromDB != null)
                            {
                                // PINDAHKAN KE SERVER IMAGE PORTAL SPCT PAD
                                if (System.IO.File.Exists(vTargetBackup))
                                {
                                    System.IO.File.SetAttributes(vTargetBackup, FileAttributes.Normal);
                                    System.IO.File.Delete(vTargetBackup);
                                }
                                if (!System.IO.File.Exists(vTargetBackup)) System.IO.File.Copy(file.ToString(), vTargetBackup);

                                // PINDAHKAN KE FOLDER COMPLETED
                                if (System.IO.File.Exists(vdirektori_completed))
                                {
                                    System.IO.File.SetAttributes(vdirektori_completed, FileAttributes.Normal);
                                    System.IO.File.Delete(vdirektori_completed);
                                }
                                if (!System.IO.File.Exists(vdirektori_completed)) System.IO.File.Move(file.ToString(), vdirektori_completed);

                               

                                if (System.IO.File.Exists(vTargetBackup))
                                {
                                    var target_column_number = 0;
                                    var target_column_name = "spt_picture";
                                    if (filename.ToString().Contains("(") && filename.ToString().Contains(")"))
                                    {
                                        target_column_number = Convert.ToInt32(filename.Split('(')[1].ToString().Split(')')[0].ToString());
                                        switch (target_column_number)
                                        {
                                            case 2:
                                                target_column_name = "spt_picture2";
                                                break;
                                            case 3:
                                                target_column_name = "spt_picture3";
                                                break;
                                            case 4:
                                                target_column_name = "spt_picture4";
                                                break;
                                            case 5:
                                                target_column_name = "spt_picture5";
                                                break;
                                            default:
                                                target_column_name = "spt_picture";
                                                break;
                                        }
                                    }

                                    if (!string.IsNullOrEmpty(target_column_name))
                                    {
                                        TargetDbContext.Database.ExecuteSqlCommand("UPDATE [db_pad].dbo.t_sptstock SET " + target_column_name + " = '" + vTargetDB + "' WHERE spt_status = 1 AND spt_whs = 'MWSPT'  AND spt_itemnew = '" + getItemFromDB.spt_itemnew.Trim() + "'");
                                    }

                                }
                            }
                            else
                            {
                                // PINDAHKAN KE FOLDER FAILED
                                if (System.IO.File.Exists(vdirektori_failed)) System.IO.File.Delete(vdirektori_failed);
                                System.IO.File.Move(file.ToString(), vdirektori_failed);
                            }                         
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                throw ex;
            }
        }

        [AutomaticRetry(Attempts = 0)]
        [NotifyOnFailed("Sync Data Part1 Failed. Info: {obj}")]
        public async Task ProsesAllSync_Part1()
        {
            GSDbContext = new GSDbContext(@"servername_here", "db_here", "user", "pass");
            // CLEAR JOB FOR QUEUES MORE;
            GSDbContext.Database.ExecuteSqlCommand("DELETE FROM HangFire.Job WHERE InvocationData LIKE '%ProsesAllSync_Part1%' AND StateName LIKE '%Enqueued%'");
            var checkRunningJob = GSDbContext.Database.SqlQuery<int>("SELECT COUNT(*) AS RUNNING_JOB FROM HangFire.Job WHERE InvocationData LIKE '%ProsesAllSync_Part1%' AND StateName LIKE '%Processing%'").First();
            if (checkRunningJob == 1)
            {
               // PROSESNYA DISINI
            }               
        }       

        [AutomaticRetry(Attempts = 0)]
        [NotifyOnFailed("Sync Data Part2 Failed. Info: {obj}")]
        public async Task ProsesAllSync_Part2()
        {
            GSDbContext = new GSDbContext(@"servername_here", "db_here", "user", "pass");
            // CLEAR JOB FOR QUEUES MORE;
            GSDbContext.Database.ExecuteSqlCommand("DELETE FROM HangFire.Job WHERE InvocationData LIKE '%ProsesAllSync_Part2%' AND StateName LIKE '%Enqueued%'");

             var checkRunningJob = GSDbContext.Database.SqlQuery<int>("SELECT COUNT(*) AS RUNNING_JOB FROM HangFire.Job WHERE InvocationData LIKE '%ProsesAllSync_Part2%' AND StateName LIKE '%Processing%'").First();

            if (checkRunningJob == 1)
            {
                // PROSESNYA DISINI
            }
        }

        [AutomaticRetry(Attempts = 0)]
        [NotifyOnFailed("Sync Data Part3 Failed. Info: {obj}")]
        public void ProsesAllSync_Part3()
        {
            GSDbContext = new GSDbContext(@"servername_here", "db_here", "user", "pass");
            // CLEAR JOB FOR QUEUES MORE;
           GSDbContext.Database.ExecuteSqlCommand("DELETE FROM HangFire.Job WHERE InvocationData LIKE '%ProsesAllSync_Part3%' AND StateName LIKE '%Enqueued%'");

            var checkRunningJob = GSDbContext.Database.SqlQuery<int>("SELECT COUNT(*) AS RUNNING_JOB FROM HangFire.Job WHERE InvocationData LIKE '%ProsesAllSync_Part3%' AND StateName LIKE '%Processing%'").First();

            if(checkRunningJob == 1)
            {
                // PROSESNYA DISINI
            }
        }
    }

}