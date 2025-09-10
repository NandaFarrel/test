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
                        var newOpenProjectIdString = await apiService.CreateWorkPackageAsync("gsbproject", wp);

                        // PERBAIKAN: Konversi hasil string dari API menjadi integer sebelum disimpan
                        if (int.TryParse(newOpenProjectIdString, out int newOpenProjectId))
                        {
                            // Memperbarui record di database lokal
                            wp.work_package_id = newOpenProjectId;
                            wp.is_synced = true;
                            wp.last_synced_at = DateTime.Now;

                            db.Entry(wp).State = EntityState.Modified;
                            successCount++;
                        }
                        else
                        {
                            // Tangani kasus jika ID yang diterima bukan angka
                            System.Diagnostics.Debug.WriteLine($"Error: API returned a non-integer ID '{newOpenProjectIdString}' for local work package ID {wp.id}.");
                            errorCount++;
                        }
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