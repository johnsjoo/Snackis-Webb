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

namespace SNACKIS___Webb.Pages.User
{
    public class UserPageModel : PageModel
    {
        private readonly IAuthGateway _authgateway;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        public UserPageModel(IAuthGateway authgateway, HttpClient client, IConfiguration configuration)
        {
            _authgateway = authgateway;
            _client = client;
            _configuration = configuration;
        }


        public string Id { get; set; }
        public UserLoginResponseModel User { get; set; }
        public async Task OnGetAsync()
        {

            byte[] tokenByte;
            HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
            string token = Encoding.ASCII.GetString(tokenByte);
            string Id = HttpContext.Session.GetString("Id");

            if (!String.IsNullOrEmpty(token))
            {
                //User = await _authgateway.GetLoggedInUser(Id);

                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", $"bearer {token}");
                var response = await _client.GetAsync(_configuration["GetLoggedInUser"] + "/" + Id);
                string apiResponse = await response.Content.ReadAsStringAsync();

                
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //var model = UserLoginResponseModel.FromJsonSingle();
                    //User = model;
                    
                }
                else
                {
                   
                }
            }
           // return Page();




            
        }


    }
}
