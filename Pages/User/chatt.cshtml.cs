using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using SNACKIS___Webb.Models;

namespace SNACKIS___Webb.Pages.User
{
    public class chattModel : PageModel
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        [BindProperty(SupportsGet =true)]
        public string UserId { get; set; }
        [BindProperty(SupportsGet =true)]
        public PrivateMessage NewMessage { get; set; }

        [BindProperty(SupportsGet =true)]
        public List<PrivateMessage> Messages { get; set; }

        public chattModel(HttpClient client, IConfiguration configuration)
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

                    var response = await _client.GetAsync(_configuration["GetMessagesByUser"]);
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    Messages = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PrivateMessage>>(apiResponse);
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

                
            }
            return Page();
        }
    }
}
