using DemoAccessApiWebApp.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DemoAccessApiWebApp.Services
{
    public class APIService : IAPIService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IJsonService _jsonService;
             
        public APIService(IHttpClientFactory httpClientFactory, IJsonService jsonService)
        {
            _httpClientFactory = httpClientFactory;
            _jsonService = jsonService;
                    
        }

        /// <summary>
        /// Generic Method to perform an HTTP GET Request
        /// </summary>       
        public async Task<ApiHttpResponse<T>> GetDataAsync<T>(string Url, string jwt = null)
        {
            // Get a new http client object from the factory
            var httpClient = _httpClientFactory.CreateClient();

            // Add Token to Header if present            
            if (!string.IsNullOrWhiteSpace(jwt))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            }

            // Call the API           
            var response = await httpClient.GetAsync(new Uri(Url));
            string jsonString = await response.Content.ReadAsStringAsync();

            // Create the return object
            var apiResponse = new ApiHttpResponse<T>() { StatusCode = response.StatusCode, ReasonPhrase = response.ReasonPhrase };
            if (response.IsSuccessStatusCode) { apiResponse.ApiData = _jsonService.FromJson<T>(jsonString); }
            else { apiResponse.ErrorApiResponse = _jsonService.FromJson<ApiResponse>(jsonString); }

            return apiResponse;
        }
      

        /// <summary>
        /// Generic Method to perform an HTTP POST Request with return content
        /// </summary>
        /// <typeparam name="T">The Return Type Model definition</typeparam>
        /// <param name="Url">The API Url</param>
        /// <param name="content">The body content to post</param>     
        public async Task<ApiHttpResponse<T>> PostDataAsync<T>(string Url, object content, string jwt = null)
        {
            // Get a new http client object from the factory
            var httpClient = _httpClientFactory.CreateClient();

            // Add Token to Header if present            
            if (!string.IsNullOrWhiteSpace(jwt))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            }
  
            // Make the Post request
            var postContent = _jsonService.ToJsonStringContent(content);
            var response = await httpClient.PostAsync(new Uri(Url), postContent);
            string jsonString = await response.Content.ReadAsStringAsync();


            // Create the return object
            var apiResponse = new ApiHttpResponse<T>() { StatusCode = response.StatusCode, ReasonPhrase = response.ReasonPhrase, ApiData = default };
            if (response.IsSuccessStatusCode)
            {
                if (!string.IsNullOrWhiteSpace(jsonString)) apiResponse.ApiData = _jsonService.FromJson<T>(jsonString);
            }
            else { apiResponse.ErrorApiResponse = _jsonService.FromJson<ApiResponse>(jsonString); }

            return apiResponse;
        }


        /// <summary>
        /// Generic Method to perform an HTTP POST Request with no return content
        /// </summary>
        public async Task<ApiHttpResponse> PostDataAsync(string Url, object content, string jwt = null)
        {
            // Get a new http client object from the factory
            var httpClient = _httpClientFactory.CreateClient();

            // Add Token to Header if present            
            if (!string.IsNullOrWhiteSpace(jwt))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            }

            // Make the Post Request
            var postContent = _jsonService.ToJsonStringContent(content);
            var response = await httpClient.PostAsync(new Uri(Url), postContent);
            string jsonString = await response.Content.ReadAsStringAsync();

            // Create the return object
            var apiResponse = new ApiHttpResponse() { StatusCode = response.StatusCode, ReasonPhrase = response.ReasonPhrase };
            if (!response.IsSuccessStatusCode) { apiResponse.ErrorApiResponse = _jsonService.FromJson<ApiResponse>(jsonString); }

            return apiResponse;
        }
       
    }
}
