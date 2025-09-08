using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using hangfire_template.Models;

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

        // Add this method to match the usage in the controller
        public async Task<string> CreateWorkPackageAsync(string projectId, TWorkPackage workPackage)
        {
            // TODO: Implement the logic to create a work package in OpenProject via API
            // For now, return a dummy ID to allow compilation
            await Task.Delay(10); // Simulate async work
            return "dummy_openproject_id";
        }
    }
}