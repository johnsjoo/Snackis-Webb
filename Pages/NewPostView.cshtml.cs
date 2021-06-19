using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using SNACKIS___Webb.Models;
using SNACKIS___Webb.Services;

namespace SNACKIS___Webb.Pages
{
    public class NewPostViewModel : PageModel
    {
        private readonly IGateway _gateway;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        [BindProperty(SupportsGet = true)]
        public string NewCatId { get; set; }

        [BindProperty(SupportsGet =true)]
        public Post NewPost { get; set; }

        [BindProperty(SupportsGet =true)]
        public Category CurrerntCategory { get; set; }

        public NewPostViewModel(IGateway gateway, HttpClient client, IConfiguration configuration)
        {
            _gateway = gateway;
            _client = client;
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                CurrerntCategory = await _gateway.GetCategoryById(HttpContext, NewCatId);
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("Error");
            }

        }
        public async Task<IActionResult> OnPostAsync() 
        {
           
            try
            {
                NewPost.CategoryId = NewCatId;   
                var newPostResponse = await _gateway.CreateNewPost(HttpContext, NewPost);
                return Page();
               
            }
            catch (Exception)
            {
                return RedirectToPage("Error");
            }

        }

    }
}
