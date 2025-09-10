using hangfire_template.Models;
using hangfire_template.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace hangfire_template.Controllers
{
    public class InitialSyncController : Controller
    {
        [HttpPost]
        public async Task<string> SyncAllFromOpenProject(string projectId = "gsbproject")
        {
            var apiService = new OpenProjectApiService();
            var db = new GSDbContext();
            int newCount = 0;
            int updateCount = 0;

            try
            {
                // 1. Ambil semua work package dari OpenProject
                List<JObject> openProjectWorkPackages = await apiService.GetAllWorkPackagesAsync(projectId);

                // 2. Ambil semua work package yang ada di database lokal
                var localWorkPackages = db.TWorkPackages.ToList();

                // 3. Looping setiap work package dari OpenProject
                foreach (var item in openProjectWorkPackages)
                {
                    // PERBAIKAN: Baca 'id' sebagai integer
                    int wpId = item["id"].Value<int>();

                    // PERBAIKAN: Bandingkan integer dengan integer
                    var existingWp = localWorkPackages.FirstOrDefault(w => w.work_package_id == wpId);

                    if (existingWp == null)
                    {
                        // 4a. Jika tidak ada, buat entri baru
                        var newWp = new TWorkPackage
                        {
                            // PERBAIKAN: Simpan 'id' sebagai integer
                            work_package_id = wpId,
                            work_package_name = item["subject"]?.ToString(),
                            description = item["description"]?["raw"]?.ToString(),
                            is_synced = true,
                            last_synced_at = DateTime.Now
                        };
                        db.TWorkPackages.Add(newWp);
                        newCount++;
                    }
                    else
                    {
                        // 4b. Jika sudah ada, update datanya
                        existingWp.work_package_name = item["subject"]?.ToString();
                        existingWp.description = item["description"]?["raw"]?.ToString();
                        existingWp.last_synced_at = DateTime.Now;
                        updateCount++;
                    }
                }

                // 5. Simpan semua perubahan ke database
                await db.SaveChangesAsync();

                return $"Sinkronisasi awal selesai. Data baru: {newCount}, Data diperbarui: {updateCount}.";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error during initial sync: {ex.Message}");
                return $"Terjadi error: {ex.Message}";
            }
        }
    }
}