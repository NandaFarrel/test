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
                List<JObject> openProjectWorkPackages = await apiService.GetAllWorkPackagesAsync(projectId);
                var localWorkPackages = db.TWorkPackages.ToList();

                foreach (var item in openProjectWorkPackages)
                {
                    string opWorkPackageId = item["id"].ToString();
                    var existingWp = localWorkPackages.FirstOrDefault(w => w.OpenProjectWorkPackageId == opWorkPackageId);

                    if (existingWp == null)
                    {
                        var newWp = new TWorkPackage
                        {
                            OpenProjectWorkPackageId = opWorkPackageId,
                            Name = item["subject"]?.ToString(),
                            Description = item["description"]?["raw"]?.ToString(),
                            LastSyncedAt = DateTime.Now,
                            CreatedAt = DateTime.Now
                        };
                        db.TWorkPackages.Add(newWp);
                        newCount++;
                    }
                    else
                    {
                        existingWp.Name = item["subject"]?.ToString();
                        existingWp.Description = item["description"]?["raw"]?.ToString();
                        existingWp.LastSyncedAt = DateTime.Now;
                        updateCount++;
                    }
                }

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