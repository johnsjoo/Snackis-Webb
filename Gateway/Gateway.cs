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
        public async Task<HttpResponseMessage> CreateNewPostDiscussion(HttpContext context, PostDiscussion NewDiscussion) 
        {
            
            string token = GetSession(context);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");
            var response = await _httpClient.PostAsJsonAsync(_configuration["CreateDiscussion"], NewDiscussion);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return response;
        }
        public async Task<HttpResponseMessage> ReportPostDiscussion(HttpContext context, string Id, PostDiscussion ClickedPostDiscussion) 
        {
            string token = GetSession(context);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");
            var response = await _httpClient.PutAsJsonAsync(_configuration["reportPostDiscussion"] + Id, ClickedPostDiscussion);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return response;
        }
        public async Task<HttpResponseMessage> ReportPostById(HttpContext context, string Id, Post ClickedPost) 
        {
            string token = GetSession(context);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");
            var response = await _httpClient.PutAsJsonAsync(_configuration["ReportPostById"] + "/" + Id, ClickedPost);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return response;
        }
        public async Task<PostDiscussion> GetDiscussionById(string discussionId, HttpContext context)
        {
            string token = GetSession(context);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");
            var response = await _httpClient.GetAsync(_configuration["DiscussionById"] + "/" + discussionId);
            string apiResponse = await response.Content.ReadAsStringAsync();
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<PostDiscussion>(apiResponse);
            return obj;
        }

        public async Task<HttpResponseMessage> CreateNewPost(HttpContext context, Post NewPost)
        {
            string token = GetSession(context);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");
            var response = await _httpClient.PostAsJsonAsync(_configuration["CreateNewPost"], NewPost);
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
        public async Task<HttpResponseMessage> DeleteDiscussionById(string deleteId, HttpContext context) 
        {
            string token = GetSession(context);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");
            var response = await _httpClient.DeleteAsync(_configuration["DeleteDiscussionById"] + "/" + deleteId);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return response;
        }
        public async Task<List<Post>> GetReportedPosts(HttpContext context) 
        {
            string token = GetSession(context);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");
            var response = await _httpClient.GetAsync(_configuration["getReportedPosts"]);
            string apiResponse = await response.Content.ReadAsStringAsync();
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Post>>(apiResponse);
            return obj;
        }
        public async Task<List<PostDiscussion>> GetReportedPostDiscussions(HttpContext context) 
        {
            string token = GetSession(context);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");
            var discussionResponse = await _httpClient.GetAsync(_configuration["GetreportedDiscussions"]);
            string discussionApiResponse = await discussionResponse.Content.ReadAsStringAsync();
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PostDiscussion>>(discussionApiResponse);
            return obj;
        }
        public async Task<HttpResponseMessage> ToggleReportedPost(HttpContext context, string id, Post toggledPost) 
        {
            string token = GetSession(context);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");
            var response = await _httpClient.PutAsJsonAsync(_configuration["toggleReportedPost"] + id, toggledPost);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return response;
        }
        public async Task<HttpResponseMessage> ReportDiscussionById(string id, HttpContext context, PostDiscussion ClickedPostDiscussion)
        {
            string token = GetSession(context);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");
            var response = await _httpClient.PutAsJsonAsync(_configuration["ReportDiscussionById"] + id, ClickedPostDiscussion);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return response;

        }
        public async Task<List<PrivateMessage>> GetMessagesByUser(HttpContext context)
        {
            string token = GetSession(context);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");
            var response = await _httpClient.GetAsync(_configuration["GetMessagesByUser"]);
            var apiResponse = await response.Content.ReadAsStringAsync();
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PrivateMessage>>(apiResponse);
            return obj;
        }
        public async Task<List<Models.User>> GetMessageUsers(HttpContext context) 
        {
            string token = GetSession(context);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");
            var userResponse = await _httpClient.GetAsync(_configuration["GetMessageUsers"]);
            var userApiResponse = await userResponse.Content.ReadAsStringAsync();
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.User>>(userApiResponse);
            return obj;
        }
        public async Task<HttpResponseMessage> CreateMessage(HttpContext context, PrivateMessage NewMessage) 
        {
            string token = GetSession(context);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");
            var response = await _httpClient.PostAsJsonAsync(_configuration["CreateMessage"], NewMessage);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return response;
        }
        public async Task<Category> GetCategoryById(HttpContext context,string NewCatId) 
        {
            string token = GetSession(context);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");
            var response = await _httpClient.GetAsync(_configuration["GetCategoryById"] + "/" + NewCatId);
            string apiResponse = await response.Content.ReadAsStringAsync();
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Category>(apiResponse);
            return obj;

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
