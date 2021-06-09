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

namespace SNACKIS___Webb.Pages.Admin
{
    public class CreateModel : PageModel
    {

        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        [BindProperty]
        public Category NewCategory{ get; set; }

        [BindProperty]
        public string Messasge { get; set; }

        public CreateModel(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;

        }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync() 
        {
            byte[] tokenByte;
            HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
            string token = Encoding.ASCII.GetString(tokenByte);

            if (!String.IsNullOrEmpty(token))
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");
                var response = await _client.PostAsJsonAsync(_configuration["CreateNewCategory"], NewCategory);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Messasge = "Ny kategori skapades!";
                    return Page();
                }
                else
                {
                    return RedirectToPage("Error");
                }
            }

            return Page();
        }
    }
}
