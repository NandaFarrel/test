using hangfire_template;
using hangfire_template.Models;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace hangfire_template.Controllers
{
    public class WebhookController : Controller
    {
        // ===================================================================
        // ENDPOINT UNTUK MENERIMA WEBHOOK DARI OPENPROJECT
        // ===================================================================
        [HttpPost]
        [ActionName("openproject")] // URL akan menjadi /webhook/openproject
        public async Task<ActionResult> OpenProjectEvents()
        {
            string requestBody;
            using (var reader = new StreamReader(Request.InputStream))
            {
                requestBody = await reader.ReadToEndAsync();
            }

            try
            {
                JObject payload = JObject.Parse(requestBody);
                var workPackage = payload["work_package"];
                var action = payload["action"]?.ToString();

                if (workPackage != null && !string.IsNullOrEmpty(action))
                {
                    string wpId = workPackage["id"].ToString();
                    string subject = workPackage["subject"]?.ToString();
                    string description = workPackage["description"]?["raw"]?.ToString();

                    using (var db = new GSDbContext())
                    {
                        // DIUBAH: FirstOrDefault
                        var existingWp = db.TWorkPackages.FirstOrDefault(w => w.work_package_id == wpId);

                        if (existingWp == null) // Jika belum ada, buat baru
                        {
                            var newWp = new TWorkPackage
                            {
                                work_package_id = wpId,
                                work_package_name = subject,
                                description = description,
                                is_synced = true,
                                last_synced_at = DateTime.Now
                            };
                            db.TWorkPackages.Add(newWp);
                        }
                        else // Jika sudah ada, update
                        {
                            existingWp.work_package_name = subject;
                            existingWp.description = description;
                            existingWp.last_synced_at = DateTime.Now;
                        }

                        // Simpan log sinkronisasi
                        db.TSyncLogs.Add(new TSyncLog
                        {
                            source_platform = "OpenProject",
                            event_type = $"work_package:{action}",
                            source_item_id = wpId,
                            synced_at = DateTime.Now,
                            details = "Successfully synced from webhook."
                        });

                        await db.SaveChangesAsync();
                    }
                }

                return new HttpStatusCodeResult(200, "OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error processing OpenProject webhook: {ex.Message}");
                return new HttpStatusCodeResult(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // ===================================================================
        // ENDPOINT UNTUK MENERIMA WEBHOOK DARI TRELLO
        // ===================================================================

        [AcceptVerbs(HttpVerbs.Head)]
        [ActionName("trello")]
        public ActionResult TrelloEventsHead()
        {
            return new HttpStatusCodeResult(200, "OK");
        }

        [HttpPost]
        [ActionName("trello")] // URL akan menjadi /webhook/trello
        public async Task<ActionResult> TrelloEvents()
        {
            string requestBody;
            using (var reader = new StreamReader(Request.InputStream))
            {
                requestBody = await reader.ReadToEndAsync();
            }

            try
            {
                JObject payload = JObject.Parse(requestBody);
                var action = payload["action"];
                var eventType = action?["type"]?.ToString();
                var card = action?["data"]?["card"];

                if (card != null && !string.IsNullOrEmpty(eventType))
                {
                    string cardId = card["id"]?.ToString();
                    string cardName = card["name"]?.ToString();
                    string cardDesc = card["desc"]?.ToString();

                    using (var db = new GSDbContext())
                    {
                        // DIUBAH: FirstOrDefault
                        var existingWp = db.TWorkPackages.FirstOrDefault(w => w.trello_card_id == cardId);

                        if (eventType == "createCard" && existingWp == null)
                        {
                            var newWp = new TWorkPackage
                            {
                                trello_card_id = cardId,
                                work_package_name = cardName,
                                description = cardDesc,
                                is_synced = true,
                                last_synced_at = DateTime.Now
                            };
                            db.TWorkPackages.Add(newWp);
                        }
                        else if (eventType == "updateCard" && existingWp != null)
                        {
                            existingWp.work_package_name = cardName;
                            existingWp.description = cardDesc;
                            existingWp.last_synced_at = DateTime.Now;
                        }

                        // Simpan log sinkronisasi
                        db.TSyncLogs.Add(new TSyncLog
                        {
                            source_platform = "Trello",
                            event_type = eventType,
                            source_item_id = cardId,
                            synced_at = DateTime.Now,
                            details = "Successfully synced from webhook."
                        });

                        await db.SaveChangesAsync();
                    }
                }

                return new HttpStatusCodeResult(200, "OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error processing Trello webhook: {ex.Message}");
                return new HttpStatusCodeResult(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}