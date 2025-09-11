using hangfire_template.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace hangfire_template.Controllers
{
    [RoutePrefix("api/webhook")]
    public class WebhookController : ApiController
    {
        [HttpPost]
        [Route("openproject")]
        public async Task<HttpResponseMessage> OpenProjectEvents()
        {
            string requestBody = await Request.Content.ReadAsStringAsync();
            try
            {
                JObject payload = JObject.Parse(requestBody);
                var workPackageData = payload["work_package"];

                if (workPackageData != null)
                {
                    string opWorkPackageId = workPackageData["id"].ToString();

                    using (var db = new GSDbContext())
                    {
                        var existingWp = db.TWorkPackages.FirstOrDefault(w => w.OpenProjectWorkPackageId == opWorkPackageId);

                        if (existingWp == null)
                        {
                            existingWp = new TWorkPackage { CreatedAt = DateTime.Now };
                            db.TWorkPackages.Add(existingWp);
                        }

                        // Update properti menggunakan nama baru dari model TWorkPackage.cs
                        existingWp.OpenProjectWorkPackageId = opWorkPackageId;
                        existingWp.Name = workPackageData["subject"]?.ToString();
                        existingWp.Description = workPackageData["description"]?["raw"]?.ToString();
                        existingWp.LastSyncedAt = DateTime.Now;

                        // Di masa depan, Anda akan mengisi relasi di sini
                        // contoh: existingWp.ProjectId = ...
                        // contoh: existingWp.StatusId = ...

                        await db.SaveChangesAsync();
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {
                // ... (Error handling)
                return Request.CreateResponse(HttpStatusCode.InternalServerError, $"Internal Server Error: {ex.InnerException?.Message ?? ex.Message}");
            }
        }
    }
}