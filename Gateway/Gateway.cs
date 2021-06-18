using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SNACKIS___Webb.Models;
using SNACKIS___Webb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
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
            var response = await _httpClient.GetAsync(_configuration["GetAllCategories"]);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Category>>(apiResponse);
        }

        public async Task<List<Post>> GetAllPosts()
        {
            var response = await _httpClient.GetAsync(_configuration["GetAllPosts"]);
            string apiResponse = await response.Content.ReadAsStringAsync();
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Post>>(apiResponse);
            return obj;
        }
        public async Task<List<Post>> GetPostsByCatId(string catId) 
        {
            var response = await _httpClient.GetAsync(_configuration["GetPostsBycatId"] + "/" + catId);
            string apiResponse = await response.Content.ReadAsStringAsync();
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Post>>(apiResponse);
            return obj;
        }

        public async Task<Post> GetPostById(string postId,HttpContext context) 
        {
            string token = GetSession(context);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");
            var response = await _httpClient.GetAsync(_configuration["GetPostById"] + "/" + postId);
            string apiResponse = await response.Content.ReadAsStringAsync();
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Post>(apiResponse);
            return obj;
        }
        public async Task<PostDiscussion> GetDiscussionById(string discussionId)
        {
            var response = await _httpClient.GetAsync(_configuration["DiscussionById"] + "/" + discussionId);
            string apiResponse = await response.Content.ReadAsStringAsync();
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<PostDiscussion>(apiResponse);
            return obj;
        }

        public async Task<HttpResponseMessage> CreateNewPost(Post post)
        {
            var response = await _httpClient.PostAsJsonAsync(_configuration["CreateNewPost"], post);
            // Post returnValue = await response.Content.ReadFromJsonAsync<Post>();
            return response;
        }
        public async Task<HttpResponseMessage> DeletePostById(string PostId,HttpContext context) 
        {
            string token = GetSession(context);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");
            var response = await _httpClient.DeleteAsync(_configuration["DeletePostById"] + "/" + PostId);
            return response;
        }
        public async Task<HttpResponseMessage> CreateNewCategory(Category NewCategory, HttpContext context) 
        {
            string token = GetSession(context);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");
            var response = await _httpClient.PostAsJsonAsync(_configuration["CreateNewCategory"], NewCategory);
            return response;
        }
        public string GetSession(HttpContext context)
        {
            try
            {
                byte[] tokenByte;
                context.Session.TryGetValue("_Token", out tokenByte);
                string token = Encoding.ASCII.GetString(tokenByte);
                
                return token;
            }
            catch (Exception)
            {
                return null;
            }
        }



    }
}
