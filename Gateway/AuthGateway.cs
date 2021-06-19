using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SNACKIS___Webb.Models;
using SNACKIS___Webb.Services;
using SNACKIS___Webb.Tools;
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
    public class AuthGateway : IAuthGateway
    {

        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        
        

        public AuthGateway(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _client = httpClient;
            
        }



        public async Task<List<Models.User>> GetAllUsers(HttpContext context) 
        {
            string token = GetSession(context);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");
            var response = await _client.GetAsync(_configuration["GetAllUsers"]);
            var apiResponse = await response.Content.ReadAsStringAsync();
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.User>>(apiResponse);
            return obj;
        }
        public async Task<User> GetLoggedInUser(HttpContext context,string Id)
        {
            string token = GetSession(context);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");
            var response = await _client.GetAsync(_configuration["GetLoggedInUser"]+"/"+Id);
            string apiResponse = await response.Content.ReadAsStringAsync();
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.User>(apiResponse);
            return obj;
        }
        public async Task<UserLoginResponseModel> GetLoggedInUserByModel(HttpContext context, string userId)
        {
            string token = GetSession(context);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");
            var response = await _client.GetAsync(_configuration["GetLoggedInUser"] + "/" + userId);
            string apiResponse = await response.Content.ReadAsStringAsync();
            //var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.User>(apiResponse);
            var model = UserLoginResponseModel.FromJsonSingle(apiResponse);
            return model;

        }
        public async Task<HttpResponseMessage> RegisterNewUser(RegisterModel user) 
        {
           
            var response = await _client.PostAsJsonAsync(_configuration["RegisterNewUser"], user);
            string apiResponse = await response.Content.ReadAsStringAsync();
            
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Models.RegisterModel returnValue = await response.Content.ReadFromJsonAsync<RegisterModel>();
                return response;
            }
            else
            {
                return response;
            }
           
            

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
