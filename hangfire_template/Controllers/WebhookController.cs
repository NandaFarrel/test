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
                var workPackage = payload["work_package"];
                var action = payload["action"]?.ToString();

                if (workPackage != null && !string.IsNullOrEmpty(action))
                {
                    int wpId = workPackage["id"].Value<int>();
                    string subject = workPackage["subject"]?.ToString();
                    string description = workPackage["description"]?["raw"]?.ToString();

                    using (var db = new GSDbContext())
                    {
                        var existingWp = db.TWorkPackages.FirstOrDefault(w => w.work_package_id == wpId);

                        if (existingWp == null)
                        {
                            var newWp = new TWorkPackage
                            {
                                work_package_id = wpId,
                                work_package_name = subject,
                                description = description,
                                is_synced = true,
                                last_synced_at = DateTime.Now,
                                // === PERBAIKAN FINAL DI SINI ===
                                created_at = DateTime.Now
                            };
                            db.TWorkPackages.Add(newWp);
                        }
                        else
                        {
                            existingWp.work_package_name = subject;
                            existingWp.description = description;
                            existingWp.last_synced_at = DateTime.Now;
                        }

                        db.TSyncLogs.Add(new TSyncLog
                        {
                            source_platform = "OpenProject",
                            event_type = action,
                            source_item_id = wpId.ToString(),
                            synced_at = DateTime.Now,
                            details = "Successfully synced from webhook."
                        });

                        await db.SaveChangesAsync();
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error processing OpenProject webhook: {ex.Message}");
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, $"Internal Server Error: {ex.InnerException.Message}");
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}