using hangfire_template.Models;
using hangfire_template.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;

namespace hangfire_template.Controllers
{
    public class OpenProjectSyncController : Controller
    {
        [HttpPost]
        public async Task<string> SyncNewWorkPackages()
        {
            using (var db = new GSDbContext())
            {
                var apiService = new OpenProjectApiService();
                int successCount = 0;
                int errorCount = 0;

                // Properti is_synced sudah dihapus, logika ini perlu diubah atau dihapus
                // Untuk sementara, kita komentari agar tidak error
                /* var workPackagesToSync = db.TWorkPackages
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
                        var newOpenProjectId = await apiService.CreateWorkPackageAsync("gsbproject", wp);
                        
                        wp.OpenProjectWorkPackageId = newOpenProjectId;
                        wp.is_synced = true;
                        wp.LastSyncedAt = DateTime.Now;

                        db.Entry(wp).State = EntityState.Modified;
                        successCount++;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error syncing work package ID {wp.id}: {ex.Message}");
                        errorCount++;
                    }
                }
                */
                await db.SaveChangesAsync();
                return $"Sinkronisasi selesai. Berhasil: {successCount}, Gagal: {errorCount}.";
            }
        }
    }
}