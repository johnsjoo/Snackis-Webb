using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using SNACKIS___Webb.Models;
using SNACKIS___Webb.Services;

namespace SNACKIS___Webb.Pages.User
{
    public class NewMessageModel : PageModel
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        [BindProperty(SupportsGet = true)]
        public PrivateMessage NewMessage { get; set; }

        [BindProperty(SupportsGet =true)]
        public string UserId { get; set; }

        [BindProperty]
        public Models.User User { get; set; }

        [BindProperty]
        public List<Models.User> Users { get; set; }

        public NewMessageModel(HttpClient client, IConfiguration configuration)
        {
          
            _client = client;
            _configuration = configuration;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            byte[] tokenByte;
            HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
            if (tokenByte != null)
            {
                string token = Encoding.ASCII.GetString(tokenByte);


                if (!String.IsNullOrEmpty(token))
                {
                    _client.DefaultRequestHeaders.Accept.Clear();
                    _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");

                    var response = await _client.GetAsync(_configuration["GetAllUsers"]);
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    Users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.User>>(apiResponse);

                    if (!string.IsNullOrEmpty(UserId))
                    {
                        var userResponse = await _client.GetAsync(_configuration["GetLoggedInUser"] + "/" + UserId);
                        var userApiResponse = await userResponse.Content.ReadAsStringAsync();
                        User = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.User>(userApiResponse);

                    }

                    return Page();
                }

            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            byte[] tokenByte;

            HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
            if (tokenByte != null)
            {
                string token = Encoding.ASCII.GetString(tokenByte);


                if (!String.IsNullOrEmpty(token))
                {
                    _client.DefaultRequestHeaders.Accept.Clear();
                    _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");

                    var response = await _client.PostAsJsonAsync(_configuration["CreateMessage"], NewMessage);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        IActionResult resultPage = await OnGetAsync();
                        return resultPage;

                    }
                    else
                    {
                        return RedirectToPage("/Error");
                    }
                }
                return RedirectToPage("chatt");
            }
            
            return Page();
        }
    }
}
