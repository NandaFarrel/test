using hangfire_template.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace hangfire_template.Services
{
    public class OpenProjectFetchJob
    {
        // Metode ini akan dipanggil oleh Hangfire
        public async Task FetchAllWorkPackages(string projectId)
        {
            Console.WriteLine($"Memulai job FetchAllWorkPackages untuk proyek: {projectId}");
            var apiService = new OpenProjectApiService();

            // 1. Ambil semua work package dari API OpenProject
            var openProjectWorkPackages = await apiService.GetAllWorkPackagesAsync(projectId);

            using (var db = new GSDbContext())
            {
                foreach (var wpData in openProjectWorkPackages)
                {
                    try
                    {
                        // 2. Ekstrak semua data dari JSON
                        string opWorkPackageId = wpData["id"].ToString();
                        var projectData = wpData["_embedded"]?["project"];
                        var statusData = wpData["_embedded"]?["status"];
                        var authorData = wpData["_embedded"]?["author"];
                        var assigneeData = wpData["_embedded"]?["assignee"];

                        // 3. Cari atau Buat (Get or Create) entitas yang berhubungan
                        var project = await GetOrCreateProject(db, projectData);
                        var status = await GetOrCreateStatus(db, statusData);
                        var author = await GetOrCreateUser(db, authorData);
                        var assignee = await GetOrCreateUser(db, assigneeData);

                        // 4. Cari atau Buat Work Package utama
                        var workPackage = await db.TWorkPackages
                            .FirstOrDefaultAsync(wp => wp.OpenProjectWorkPackageId == opWorkPackageId);

                        if (workPackage == null)
                        {
                            workPackage = new TWorkPackage { CreatedAt = DateTime.Now };
                            db.TWorkPackages.Add(workPackage);
                        }

                        // 5. Update properti dan hubungkan dengan Foreign Keys
                        workPackage.OpenProjectWorkPackageId = opWorkPackageId;
                        workPackage.Name = wpData["subject"]?.ToString();
                        workPackage.Description = wpData["description"]?["raw"]?.ToString();
                        workPackage.LastSyncedAt = DateTime.Now;

                        if (project != null) workPackage.ProjectId = project.Id;
                        if (status != null) workPackage.StatusId = status.Id;
                        if (assignee != null) workPackage.AssigneeId = assignee.Id;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Gagal memproses Work Package ID {wpData["id"]}. Error: {ex.Message}");
                    }
                }

                // 6. Simpan semua perubahan ke database dalam satu transaksi
                await db.SaveChangesAsync();
                Console.WriteLine("Selesai menjalankan job FetchAllWorkPackages.");
            }
        }

        // --- Fungsi Helper (Get or Create) ---

        private async Task<TProject> GetOrCreateProject(GSDbContext db, JToken projectData)
        {
            if (projectData == null) return null;
            string opIdentifier = projectData["identifier"]?.ToString();
            if (string.IsNullOrEmpty(opIdentifier)) return null;

            var project = await db.TProjects.FirstOrDefaultAsync(p => p.OpenProjectIdentifier == opIdentifier);
            if (project == null)
            {
                project = new TProject
                {
                    OpenProjectIdentifier = opIdentifier,
                    Name = projectData["name"]?.ToString()
                };
                db.TProjects.Add(project);
                // Disimpan sekaligus di akhir
            }
            return project;
        }

        private async Task<TStatus> GetOrCreateStatus(GSDbContext db, JToken statusData)
        {
            if (statusData == null) return null;
            string opStatusId = statusData["id"]?.ToString();
            if (string.IsNullOrEmpty(opStatusId)) return null;

            var status = await db.TStatuses.FirstOrDefaultAsync(s => s.OpenProjectStatusId == opStatusId);
            if (status == null)
            {
                status = new TStatus
                {
                    OpenProjectStatusId = opStatusId,
                    Name = statusData["name"]?.ToString()
                };
                db.TStatuses.Add(status);
            }
            return status;
        }

        private async Task<TUser> GetOrCreateUser(GSDbContext db, JToken userData)
        {
            if (userData == null) return null;
            string opUserId = userData["id"]?.ToString();
            if (string.IsNullOrEmpty(opUserId)) return null;

            var user = await db.TUsers.FirstOrDefaultAsync(u => u.OpenProjectUserId == opUserId);
            if (user == null)
            {
                user = new TUser
                {
                    OpenProjectUserId = opUserId,
                    FullName = userData["name"]?.ToString(),
                    Email = userData["email"]?.ToString()
                };
                db.TUsers.Add(user);
            }
            return user;
        }
    }
}