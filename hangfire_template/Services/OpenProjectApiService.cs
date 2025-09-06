// Services/OpenProjectApiService.cs
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
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly string _openProjectUrl;
        private readonly string _apiKey;

        public OpenProjectApiService()
        {
            _openProjectUrl = ConfigurationManager.AppSettings["OpenProjectUrl"];
            _apiKey = ConfigurationManager.AppSettings["OpenProjectApiKey"];

            // Konfigurasi dasar untuk HttpClient
            _httpClient.BaseAddress = new Uri(_openProjectUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Otorisasi menggunakan API Key
            var encodedKey = Convert.ToBase64String(Encoding.ASCII.GetBytes($"apikey:{_apiKey}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedKey);
        }

        /// <summary>
        /// Membuat sebuah Work Package baru di OpenProject.
        /// </summary>
        /// <param name="projectId">ID dari proyek di OpenProject (contoh: "my-project-identifier").</param>
        /// <param name="workPackage">Data work package yang diambil dari database Anda.</param>
        /// <returns>ID dari work package yang baru dibuat di OpenProject.</returns>
        public async Task<string> CreateWorkPackageAsync(string projectId, TWorkPackage workPackage)
        {
            // Endpoint API untuk membuat work package
            var requestUrl = $"/api/v3/projects/{projectId}/work_packages";

            // Membuat objek payload yang akan dikirim sebagai JSON
            var payload = new
            {
                subject = workPackage.work_package_name,
                description = new
                {
                    format = "markdown",
                    raw = workPackage.description
                }
                // Anda bisa menambahkan properti lain di sini sesuai kebutuhan
                // seperti assignee, status, priority, dll.
            };

            var jsonPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                // Kita perlu parse response JSON untuk mendapatkan ID work package baru
                dynamic result = JsonConvert.DeserializeObject(responseBody);
                return result.id.ToString();
            }
            else
            {
                // Jika gagal, lemparkan exception dengan pesan error dari API
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Gagal membuat work package di OpenProject. Status: {response.StatusCode}. Error: {errorContent}");
            }
        }

        /// <summary>
        /// Mengambil semua Work Packages dari sebuah proyek di OpenProject.
        /// </summary>
        /// <param name="projectId">ID dari proyek di OpenProject (contoh: "my-project-identifier").</param>
        /// <returns>List dari semua work packages yang ada di proyek tersebut.</returns>
        public async Task<List<JObject>> GetAllWorkPackagesAsync(string projectId)
        {
            // Endpoint API untuk mengambil semua work packages
            var requestUrl = $"/api/v3/projects/{projectId}/work_packages";

            var response = await _httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(responseBody);

                // Mengambil daftar work packages dari response JSON
                var workPackages = json["_embedded"]?["elements"]?.ToObject<List<JObject>>() ?? new List<JObject>();
                return workPackages;
            }
            else
            {
                // Jika gagal, lemparkan exception dengan pesan error dari API
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Gagal mengambil work packages dari OpenProject. Status: {response.StatusCode}. Error: {errorContent}");
            }
        }
    }
}