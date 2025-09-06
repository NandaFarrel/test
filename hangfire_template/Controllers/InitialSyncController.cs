using hangfire_template.Models;
using hangfire_template.Services;
using System;
using System.Linq; // DIUBAH: Menambahkan using yang hilang untuk .Select()
using System.Threading.Tasks;
using System.Web.Mvc;

namespace hangfire_template.Controllers
{
    public class InitialSyncController : Controller
    {
        /// <summary>
        /// Method ini akan dijalankan satu kali untuk menarik semua data
        /// work package yang sudah ada dari OpenProject ke database lokal.
        /// </summary>
        public async Task<string> SyncExistingWorkPackages()
        {
            var apiService = new OpenProjectApiService();
            int newItemsSynced = 0;

            using (var db = new GSDbContext())
            {
                // 1. Ambil semua work package dari API OpenProject untuk proyek "gsbproject".
                var openProjectWorkPackages = await apiService.GetAllWorkPackagesAsync("gsbproject");

                // 2. Ambil semua ID work package yang sudah ada di database kita untuk efisiensi.
                var existingIds = db.TWorkPackages.Select(p => p.work_package_id).ToList();

                // 3. Filter hanya work package dari OpenProject yang belum ada di database kita.
                var workPackagesToSync = openProjectWorkPackages
                    .Where(opWp => !existingIds.Contains(opWp["id"].ToString()))
                    .ToList();

                if (!workPackagesToSync.Any())
                {
                    return "Database sudah sinkron. Tidak ada data baru yang ditambahkan.";
                }

                // 4. Looping data yang belum ada dan tambahkan ke database.
                foreach (var wpData in workPackagesToSync)
                {
                    var newWp = new TWorkPackage
                    {
                        work_package_id = wpData["id"].ToString(),
                        work_package_name = wpData["subject"].ToString(),
                        description = wpData["description"]?["raw"]?.ToString() ?? "",
                        is_synced = true, // Langsung tandai sudah sinkron
                        created_at = DateTime.Now,
                        last_synced_at = DateTime.Now
                    };
                    db.TWorkPackages.Add(newWp);
                    newItemsSynced++;
                }

                // 5. Simpan semua perubahan ke database dalam satu kali panggilan.
                await db.SaveChangesAsync();
            }

            return $"Sinkronisasi awal selesai. {newItemsSynced} item baru ditambahkan ke database.";
        }
    }
}