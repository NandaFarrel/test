using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using hangfire_template.Models;
using Newtonsoft.Json;

namespace hangfire_template.Services
{
    public class OpenProjectApiService
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly string _openProjectUrl;
        private readonly string _apiKey;

        public OpenProjectApiService()
        {
            _openProjectUrl = ConfigurationManager.AppSettings["OpenProjectUrl"];
            _apiKey = ConfigurationManager.AppSettings["OpenProjectApiKey"];

            _httpClient.BaseAddress = new Uri(_openProjectUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var encodedKey = Convert.ToBase64String(Encoding.ASCII.GetBytes($"apikey:{_apiKey}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedKey);
        }

        public async Task<List<JObject>> GetAllWorkPackagesAsync(string projectId)
        {
            var requestUrl = $"/api/v3/projects/{projectId}/work_packages";
            var response = await _httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JObject.Parse(responseBody);
                var elements = result["_embedded"]["elements"].ToObject<List<JObject>>();
                return elements;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Gagal mengambil work package. Status: {response.StatusCode}. Error: {errorContent}");
            }
        }

        /// <summary>
        /// Membuat work package baru di OpenProject.
        /// </summary>
        /// <param name="projectId">ID Project di OpenProject (contoh: "demo-project").</param>
        /// <param name="workPackage">Objek TWorkPackage yang berisi data untuk dikirim.</param>
        /// <param name="typeId">ID tipe work package (contoh: 1 untuk "Task"). Cek /api/v3/types di OpenProject Anda.</param>
        /// <returns>ID dari work package yang baru dibuat di OpenProject.</returns>
        public async Task<string> CreateWorkPackageAsync(string projectId, TWorkPackage workPackage, int typeId = 1)
        {
            // Endpoint untuk membuat work package. 
            // Perhatikan bahwa projectId tidak lagi diperlukan di URL ini karena sudah ditentukan di dalam payload.
            var requestUrl = "/api/v3/work_packages";

            // Membuat payload JSON sesuai dengan format yang dibutuhkan oleh OpenProject API v3.
            var payload = new JObject
            {
                ["subject"] = workPackage.work_package_name,
                ["description"] = new JObject
                {
                    ["format"] = "markdown",
                    ["raw"] = workPackage.description
                },
                ["_links"] = new JObject
                {
                    // Link ke project tempat work package akan dibuat.
                    ["project"] = new JObject
                    {
                        ["href"] = $"/api/v3/projects/{projectId}"
                    },
                    // Link ke tipe work package (misalnya, Task, Bug, Feature).
                    // ID "1" biasanya adalah "Task" pada instalasi default.
                    // Anda mungkin perlu menyesuaikan ini sesuai konfigurasi OpenProject Anda.
                    ["type"] = new JObject
                    {
                        ["href"] = $"/api/v3/types/{typeId}"
                    }
                }
            };

            var content = new StringContent(payload.ToString(Formatting.None), Encoding.UTF8, "application/json");

            // Mengirim permintaan POST untuk membuat work package.
            var response = await _httpClient.PostAsync(requestUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var createdWorkPackage = JObject.Parse(responseBody);

                // Mengambil dan mengembalikan ID dari work package yang baru dibuat.
                return createdWorkPackage["id"].ToString();
            }
            else
            {
                // Jika gagal, lemparkan exception dengan detail error.
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Gagal membuat work package. Status: {response.StatusCode}. Error: {errorContent}");
            }
        }
    }
}