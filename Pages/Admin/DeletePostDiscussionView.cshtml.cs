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
    public class DeletePostDiscussionViewModel : PageModel
    {

        private readonly IGateway _gateway;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        public DeletePostDiscussionViewModel(HttpClient client, IGateway gateway, IConfiguration configuration)
        {
            _configuration = configuration;
            _client = client;
            _gateway = gateway;
        }

        [BindProperty(SupportsGet = true)]
        public PostDiscussion DeletedPostDiscussion { get; set; }

        [BindProperty(SupportsGet = true)]
        public string DeletedDiscussionId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                DeletedPostDiscussion = await _gateway.GetDiscussionById(DeletedDiscussionId, HttpContext);
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
                await _gateway.DeleteDiscussionById(DeletedDiscussionId, HttpContext);
                return RedirectToPage("ReportedPosts");
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
