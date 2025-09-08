using hangfire_template.Models;
using hangfire_template.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace hangfire_template.Controllers
{
    public class InitialSyncController : Controller
    {
        public async Task<string> SyncExistingWorkPackages()
        {
            var apiService = new OpenProjectApiService();
            int newItemsSynced = 0;

            using (var db = new GSDbContext())
            {
                var openProjectWorkPackages = await apiService.GetAllWorkPackagesAsync("gsbproject");
                var existingIds = db.TWorkPackages.Select(p => p.work_package_id).ToList();
                var workPackagesToSync = openProjectWorkPackages
                    .Where(opWp => !existingIds.Contains(opWp["id"].ToString()))
                    .ToList();

                if (!workPackagesToSync.Any())
                {
                    return "Database sudah sinkron.";
                }

                foreach (var wpData in workPackagesToSync)
                {
                    var newWp = new TWorkPackage
                    {
                        work_package_id = wpData["id"].ToString(),
                        work_package_name = wpData["subject"].ToString(),
                        description = wpData["description"]?["raw"]?.ToString() ?? "",
                        is_synced = true,
                        created_at = DateTime.Now,
                        last_synced_at = DateTime.Now
                    };
                    db.TWorkPackages.Add(newWp);
                    newItemsSynced++;
                }
                await db.SaveChangesAsync();
            }
            return $"Sinkronisasi awal selesai. {newItemsSynced} item baru ditambahkan.";
        }
    }
}