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

        public async Task<User> GetLoggedInUser(string Id)
        {
            
            var response = await _client.GetAsync(_configuration["GetLoggedInUser"]+"/"+Id);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(apiResponse);
        }
        public async Task<RegisterModel> RegisterNewUser(RegisterModel user) 
        {
           
            var response = await _client.PostAsJsonAsync(_configuration["RegisterNewUser"], user);
            Models.RegisterModel returnValue = await response.Content.ReadFromJsonAsync<RegisterModel>();
            return returnValue;

        }

    }
}
