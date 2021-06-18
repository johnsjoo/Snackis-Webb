using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using SNACKIS___Webb.Models;
using SNACKIS___Webb.Services;

namespace SNACKIS___Webb.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly IGateway _gateway;
       
        [BindProperty]
        public List<Post> Posts { get; set; }


        [BindProperty]
        public int PostCounter { get; set; }

        [BindProperty]
        public List<PostDiscussion> ReportedPostDiscussions { get; set; }

        public IndexModel(HttpClient client, IConfiguration configuration,IGateway gateway)
        {
            _client = client;
            _configuration = configuration;
            _gateway = gateway;

        }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Posts = await _gateway.GetReportedPosts(HttpContext);
                ReportedPostDiscussions = await _gateway.GetReportedPostDiscussions(HttpContext);
                PostCounter = Posts.Count + ReportedPostDiscussions.Count;
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
