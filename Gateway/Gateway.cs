using Microsoft.Extensions.Configuration;
using SNACKIS___Webb.Models;
using SNACKIS___Webb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace SNACKIS___Webb.Gateway
{
    public class Gateway : IGateway
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        
        public Gateway(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            
        }
        public async Task<List<Category>> GetAllCategories()
        {
            var response = await _httpClient.GetAsync(_configuration["SnackisApi"]);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Category>>(apiResponse);
        }

        public async Task<List<Post>> GetAllPosts()
        {
            var response = await _httpClient.GetAsync(_configuration["SnackisApi1"]);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Post>>(apiResponse);
        }
        public async Task<List<Post>> GetPostsByCatId(string catId) 
        {
            var response = await _httpClient.GetAsync(_configuration["GetPostsBycatId"] + "/" + catId);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Post>>(apiResponse);
        }

        public async Task<Post> GetPostById(string postId) 
        {
            var response = await _httpClient.GetAsync(_configuration["GetPostById"] + "/" + postId);
            string apiResponse = await response.Content.ReadAsStringAsync();
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Post>(apiResponse);
            return obj;
        }

        public async Task<HttpResponseMessage> CreateNewPost(Post post)
        {
            var response = await _httpClient.PostAsJsonAsync(_configuration["CreateNewPost"], post);
            // Post returnValue = await response.Content.ReadFromJsonAsync<Post>();
            return response;
        }

    }
}
