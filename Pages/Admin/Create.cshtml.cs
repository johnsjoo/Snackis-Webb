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

namespace SNACKIS___Webb.Pages.Admin
{
    public class CreateModel : PageModel
    {
        private readonly IGateway _gateway;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        [BindProperty]
        public Category NewCategory{ get; set; }

        [BindProperty]
        public string Messasge { get; set; }
        [BindProperty]
        public string ErrorMessasge { get; set; }

        public CreateModel(IGateway gateway, HttpClient client, IConfiguration configuration)
        {
            _gateway = gateway;
            _client = client;
            _configuration = configuration;

        }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync() 
        {
            try
            {
                var response = await _gateway.CreateNewCategory(NewCategory, HttpContext);
                string request = response.Content.ReadAsStringAsync().Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Messasge = "Ny kategori skapades!";
                }
                if (request == "no content")
                {
                    ErrorMessasge = $"Du måste fylla i fältet 'Titel'";
                }
                return Page();

            }
            catch (Exception)
            {
                return RedirectToPage("Error");
            }

        }
    }
}
