using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace SNACKIS___Webb.Pages.User
{
    public class NewMessageModel : PageModel
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;


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

                    var response = await _client.GetAsync(_configuration["GetMessagesByUser"]);
                    var apiResponse = await response.Content.ReadAsStringAsync();
                }

            }

            return Page();
        }
    }
}
