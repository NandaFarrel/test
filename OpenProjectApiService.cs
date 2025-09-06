using hangfire_template.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq; // Pastikan using ini ada
using System;
using System.Collections.Generic; // Pastikan using ini ada
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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

            // Otorisasi menggunakan API Key (diperlukan untuk membaca data)
            var encodedKey = Convert.ToBase64String(Encoding.ASCII.GetBytes($"apikey:{_apiKey}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedKey);
        }

        /// <summary>
        /// Mengambil semua work package dari sebuah proyek di OpenProject.
        /// </summary>
        public async Task<List<JObject>> GetAllWorkPackagesAsync(string projectId)
        {
            // Endpoint API untuk mendapatkan work package
            var requestUrl = $"/api/v3/projects/{projectId}/work_packages";
            var response = await _httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JObject.Parse(responseBody);
                // Data work package ada di dalam properti "_embedded" -> "elements"
                var elements = result["_embedded"]["elements"].ToObject<List<JObject>>();
                return elements;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Gagal mengambil work package dari OpenProject. Status: {response.StatusCode}. Error: {errorContent}");
            }
        }

        /// <summary>
        /// Membuat sebuah Work Package baru di OpenProject.
        /// </summary>
        public async Task<string> CreateWorkPackageAsync(string projectId, TWorkPackage workPackage)
        {
            var requestUrl = $"/api/v3/projects/{projectId}/work_packages";

            var payload = new
            {
                subject = workPackage.work_package_name,
                description = new
                {
                    format = "markdown",
                    raw = workPackage.description
                }
            };

            var jsonPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(responseBody);
                return result.id.ToString();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Gagal membuat work package di OpenProject. Status: {response.StatusCode}. Error: {errorContent}");
            }
        }
    }
}