using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using SNACKIS___Webb.Models;
using SNACKIS___Webb.Services;

namespace SNACKIS___Webb.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        private readonly IGateway _gateway;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        

        [BindProperty(SupportsGet =true)]
        public Post DeletedPost { get; set; }

        [BindProperty(SupportsGet =true)]
        public string DeletedId { get; set; }

        public DeleteModel(HttpClient client,IGateway gateway, IConfiguration configuration)
        {
            _configuration = configuration;
            _client = client;
            _gateway = gateway;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                DeletedPost = await _gateway.GetPostById(DeletedId, HttpContext);
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }

        }
        public async Task<IActionResult> OnPostAsync() 
        {

            try
            {
                var response = await _gateway.DeletePostById(DeletedId, HttpContext); 
                string apiResponse = await response.Content.ReadAsStringAsync();
                return RedirectToPage("ReportedPosts");
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
