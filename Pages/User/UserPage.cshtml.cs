using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using SNACKIS___Webb.Models;
using SNACKIS___Webb.Services;
using SNACKIS___Webb.Tools;

namespace SNACKIS___Webb.Pages.User
{
    public class UserPageModel : PageModel
    {
        private readonly IAuthGateway _authgateway;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly UserApi _api;
        public UserPageModel(IAuthGateway authgateway, HttpClient client, IConfiguration configuration, UserApi api)
        {
            _authgateway = authgateway;
            _client = client;
            _configuration = configuration;
            _api = api;
        }

        public string Id { get; set; }
        public UserLoginResponseModel User { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            
            byte[] tokenByte;
            HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
            string token = Encoding.ASCII.GetString(tokenByte);
            string Id = HttpContext.Session.GetString("Id");

            if (!String.IsNullOrEmpty(token))
            {
                HttpClient client = _api.Initial();
              
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");
               
                var response = await _client.GetAsync(_configuration["GetLoggedInUser"] + "/" + Id);
                string apiResponse = await response.Content.ReadAsStringAsync();

                var model = UserLoginResponseModel.FromJsonSingle(apiResponse);
                User = model;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    
                    return Page();
                }
                else
                {
                    return NotFound();
                }
            }
            return Page();
        }
    }
}
