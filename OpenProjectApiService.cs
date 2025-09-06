// Controllers/OpenProjectSyncController.cs
using hangfire_template.Models;
using hangfire_template.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace hangfire_template.Controllers
{
    public class OpenProjectSyncController : Controller
    {
        /// <summary>
        /// Metode ini akan dipanggil oleh Hangfire untuk memulai proses sinkronisasi.
        /// </summary>
        [HttpPost] // Menambahkan ini agar tidak bisa diakses langsung dari browser via GET
        public async Task<string> SyncNewWorkPackages()
        {
            var db = new GSDbContext();
            var apiService = new OpenProjectApiService();
            int successCount = 0;
            int errorCount = 0;

            // 1. Ambil semua work package dari database yang belum disinkronkan (is_synced = false)
            var workPackagesToSync = db.TWorkPackages.Where(wp => !wp.is_synced).ToList();

            if (!workPackagesToSync.Any())
            {
                return "Tidak ada data baru untuk disinkronkan.";
            }

            // 2. Looping setiap work package
            foreach (var wp in workPackagesToSync)
            {
                try
                {
                    // 3. Panggil API Service untuk membuat work package di OpenProject
                    // PENTING: "gsbproject" adalah ID proyek Anda di OpenProject. Mohon verifikasi ini.
                    var newOpenProjectId = await apiService.CreateWorkPackageAsync("gsbproject", wp);

                    // 4. Jika berhasil, update data di database lokal
                    wp.work_package_id = newOpenProjectId;
                    wp.is_synced = true;
                    wp.last_synced_at = DateTime.Now;

                    await db.SaveChangesAsync();
                    successCount++;
                }
                catch (Exception ex)
                {
                    // Jika terjadi error, catat di log (untuk sekarang kita tampilkan di debug output)
                    System.Diagnostics.Debug.WriteLine($"Error syncing work package ID {wp.id}: {ex.Message}");
                    errorCount++;
                }
            }

            return $"Sinkronisasi selesai. Berhasil: {successCount}, Gagal: {errorCount}.";
        }
    }
}