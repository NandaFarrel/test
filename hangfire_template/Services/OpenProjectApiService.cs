using hangfire_template.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace hangfire_template.Services
{
    public class OpenProjectApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiBaseUrl;

        public OpenProjectApiService()
        {
            _apiBaseUrl = ConfigurationManager.AppSettings["OpenProjectUrl"];
            _apiKey = ConfigurationManager.AppSettings["OpenProjectApiKey"];

            if (string.IsNullOrEmpty(_apiBaseUrl))
            {
                throw new Exception("Konfigurasi 'OpenProjectUrl' tidak ditemukan di Web.config.");
            }

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_apiBaseUrl);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes($"apikey:{_apiKey}")));
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // FUNGSI YANG HILANG (DITAMBAHKAN KEMBALI)
        public async Task<List<JObject>> GetAllWorkPackagesAsync(string projectId)
        {
            var allWorkPackages = new List<JObject>();
            var url = $"/api/v3/projects/{projectId}/work_packages";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JObject.Parse(content);
            var elements = result["_embedded"]?["elements"] as JArray;

            if (elements != null)
            {
                foreach (var item in elements)
                {
                    allWorkPackages.Add(item as JObject);
                }
            }
            return allWorkPackages;
        }

        // FUNGSI YANG HILANG (DITAMBAHKAN KEMBALI)
        public async Task<string> CreateWorkPackageAsync(string projectId, TWorkPackage wp)
        {
            var url = $"/api/v3/projects/{projectId}/work_packages";
            var payload = new
            {
                // Menggunakan properti baru "Name" dan "Description"
                subject = wp.Name,
                description = new { raw = wp.Description },
                _links = new
                {
                    type = new { href = "/api/v3/types/1" } // Asumsi tipe "Task"
                }
            };

            var jsonPayload = JsonConvert.SerializeObject(payload);
            var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, httpContent);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdWp = JObject.Parse(responseContent);
            return createdWp["id"].ToString();
        }

        public async Task UpdateWorkPackageAsync(int workPackageId, TWorkPackage wp)
        {
            var existingWpResponse = await _httpClient.GetAsync($"/api/v3/work_packages/{workPackageId}");
            if (!existingWpResponse.IsSuccessStatusCode)
            {
                throw new Exception($"Work package dengan ID {workPackageId} tidak ditemukan di OpenProject.");
            }
            var existingWpJson = await existingWpResponse.Content.ReadAsStringAsync();
            var existingWpData = JObject.Parse(existingWpJson);
            int lockVersion = existingWpData["lockVersion"].Value<int>();

            var url = $"/api/v3/work_packages/{workPackageId}";
            var payload = new JObject
            {
                ["lockVersion"] = lockVersion,
                // Menggunakan properti baru "Name" dan "Description"
                ["subject"] = wp.Name,
                ["description"] = new JObject
                {
                    ["raw"] = wp.Description
                }
            };

            var jsonPayload = payload.ToString();
            var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(new HttpMethod("PATCH"), url) { Content = httpContent };
            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Gagal update OpenProject. Status: {response.StatusCode}. Response: {errorContent}");
            }
        }
    }
}