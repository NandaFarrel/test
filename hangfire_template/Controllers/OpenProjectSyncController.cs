using hangfire_template.Models;
using hangfire_template.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity; // Pastikan using ini ada

namespace hangfire_template.Controllers
{
    public class OpenProjectSyncController : Controller
    {
        [HttpPost]
        public async Task<string> SyncNewWorkPackages()
        {
            // Menggunakan 'using' adalah praktik terbaik untuk memastikan koneksi ditutup
            using (var db = new GSDbContext())
            {
                var apiService = new OpenProjectApiService();
                int successCount = 0;
                int errorCount = 0;

                // Ini adalah baris yang menyebabkan error (line 25)
                // Kita akan mencoba query yang lebih eksplisit
                var workPackagesToSync = db.TWorkPackages
                                           .Where(wp => wp.is_synced == false)
                                           .ToList();

                if (!workPackagesToSync.Any())
                {
                    return "Tidak ada data baru untuk disinkronkan.";
                }

                foreach (var wp in workPackagesToSync)
                {
                    try
                    {
                        // Pastikan "gsbproject" adalah Project Identifier yang benar
                        var newOpenProjectId = await apiService.CreateWorkPackageAsync("gsbproject", wp);

                        wp.work_package_id = newOpenProjectId;
                        wp.is_synced = true;
                        wp.last_synced_at = DateTime.Now;

                        db.Entry(wp).State = EntityState.Modified;
                        successCount++;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error syncing work package ID {wp.id}: {ex.Message}");
                        errorCount++;
                    }
                }

                await db.SaveChangesAsync();

                return $"Sinkronisasi selesai. Berhasil: {successCount}, Gagal: {errorCount}.";
            }
        }
    }
}