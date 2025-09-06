using hangfire_template.Models;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Hangfire; // Pastikan using statement ini ada

namespace hangfire_template.Controllers
{
    public class WebhookController : Controller
    {
        // Endpoint ini akan menerima notifikasi dari OpenProject.
        // Alamat lengkapnya adalah: http://localhost:51000/Webhook/HandleEvent
        [HttpPost]
        public async Task<ActionResult> HandleEvent()
        {
            // 1. Baca data JSON mentah dari body request yang dikirim oleh OpenProject.
            string requestBody;
            using (var reader = new StreamReader(Request.InputStream))
            {
                requestBody = await reader.ReadToEndAsync();
            }

            // 2. Lemparkan pemrosesan ke Hangfire agar berjalan di latar belakang.
            //    Ini membuat endpoint bisa merespon dengan sangat cepat.
            BackgroundJob.Enqueue<OpenProjectWebhookHandler>(x => x.ProcessWorkPackageEvent(requestBody));

            // 3. Kirim respon "HTTP 200 OK" kembali ke OpenProject.
            //    Ini memberitahu OpenProject bahwa notifikasinya sudah berhasil diterima.
            return new HttpStatusCodeResult(200);
        }
    }

    /// <summary>
    /// Class ini berisi semua logika untuk memproses data dari webhook.
    /// Hangfire akan memanggil method di dalam class ini.
    /// </summary>
    public class OpenProjectWebhookHandler
    {
        public void ProcessWorkPackageEvent(string jsonPayload)
        {
            var data = JObject.Parse(jsonPayload);

            // Pastikan data yang masuk adalah tentang "work_package"
            if (data["action"] != null && data["work_package"] != null)
            {
                var workPackageData = data["work_package"];

                var openProjectId = workPackageData["id"].ToString();
                var subject = workPackageData["subject"].ToString();
                var description = workPackageData["description"]?["raw"]?.ToString() ?? "";

                // Gunakan 'using' untuk memastikan koneksi database ditutup dengan benar
                using (var db = new GSDbContext())
                {
                    // Cek apakah work package ini sudah ada di database kita
                    var existingWp = db.TWorkPackages.FirstOrDefault(wp => wp.work_package_id == openProjectId);

                    if (existingWp == null) // Jika belum ada, buat data baru
                    {
                        var newWp = new TWorkPackage
                        {
                            work_package_id = openProjectId,
                            work_package_name = subject,
                            description = description,
                            is_synced = true, // Langsung ditandai sudah sinkron
                            created_at = System.DateTime.Now,
                            last_synced_at = System.DateTime.Now
                        };
                        db.TWorkPackages.Add(newWp);
                    }
                    else // Jika sudah ada, perbarui datanya
                    {
                        existingWp.work_package_name = subject;
                        existingWp.description = description;
                        existingWp.last_synced_at = System.DateTime.Now;
                    }

                    db.SaveChanges();
                }
            }
        }
    }
}