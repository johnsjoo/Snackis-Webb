using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SNACKIS___Webb.Models;
using SNACKIS___Webb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SNACKIS___Webb.Gateway
{
    public class AuthGateway : IAuthGateway
    {

        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public AuthGateway(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;

        }

        public async Task<User> GetLoggedInUser(string Id)
        {
            //Använd code behind istället.
            var response = await _httpClient.GetAsync(_configuration["GetLoggedInUser"]+"/"+Id);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(apiResponse);
        }
    }
}
