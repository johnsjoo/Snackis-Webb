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

        public RegisterModel User { get; set; }
        public string RegestrationMessage { get; set; }

        public async Task<User> GetLoggedInUser(string Id)
        {
            
            var response = await _client.GetAsync(_configuration["GetLoggedInUser"]+"/"+Id);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(apiResponse);
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
