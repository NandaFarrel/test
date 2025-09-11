using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hangfire_template.Models; // Pastikan using ini ada

namespace hangfire_template.Services
{
    public class OpenProjectSyncJob
    {
        public async Task SyncUpdatesToOpenProject()
        {
            List<TWorkPackage> recordsToSync;

            using (var db = new GSDbContext())
            {
                // PERBAIKAN: Menggunakan nama properti yang baru: NeedsOpSync dan OpenProjectWorkPackageId
                recordsToSync = db.TWorkPackages
                                  .Where(wp => wp.NeedsOpSync == true && !string.IsNullOrEmpty(wp.OpenProjectWorkPackageId))
                                  .ToList();
            }

            if (!recordsToSync.Any())
            {
                return;
            }

            var apiService = new OpenProjectApiService();

            foreach (var record in recordsToSync)
            {
                try
                {
                    // PERBAIKAN: Menggunakan OpenProjectWorkPackageId
                    if (int.TryParse(record.OpenProjectWorkPackageId, out int wpIdInt))
                    {
                        await apiService.UpdateWorkPackageAsync(wpIdInt, record);

                        using (var updateDb = new GSDbContext())
                        {
                            // PERBAIKAN: Menggunakan NeedsOpSync
                            record.NeedsOpSync = false;
                            updateDb.Entry(record).State = System.Data.Entity.EntityState.Modified;
                            await updateDb.SaveChangesAsync();
                        }

                        // PERBAIKAN: Menggunakan OpenProjectWorkPackageId
                        Console.WriteLine($"Berhasil sinkronisasi update untuk WP ID: {record.OpenProjectWorkPackageId}");
                    }
                }
                catch (Exception ex)
                {
                    // PERBAIKAN: Menggunakan OpenProjectWorkPackageId
                    Console.WriteLine($"Gagal sinkronisasi update untuk WP ID: {record.OpenProjectWorkPackageId}. Error: {ex.Message}");
                    throw;
                }
            }
        }
    }
}