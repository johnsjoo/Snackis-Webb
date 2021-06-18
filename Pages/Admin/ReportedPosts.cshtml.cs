using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using SNACKIS___Webb.Models;
using SNACKIS___Webb.Services;

namespace SNACKIS___Webb.Pages.Admin
{
    public class ReportedPostsModel : PageModel
    {

        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly IGateway _gateway;
        public Post toggledPost { get; set; }

        [BindProperty]
        public int PostCounter { get; set; }

        
        public PostDiscussion ClickedPostDiscussion { get; set; }


        [BindProperty]
        public string DiscussionId { get; set; }
        [BindProperty]
        public string postId { get; set; }
        [BindProperty]
        public List<Post> Posts { get; set; }

        [BindProperty]
        public List<PostDiscussion> ReportedPostDiscussions { get; set; }

        public ReportedPostsModel(IGateway gateway, HttpClient client, IConfiguration configuration)
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

                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            id = postId;
            toggledPost = await _gateway.GetPostById(id, HttpContext);
            var response = await _gateway.ToggleReportedPost(HttpContext, id, toggledPost);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                IActionResult result = await OnGetAsync();
                return Page();
            }
            else
            {
                return NotFound();
            }
        }
        public async Task<IActionResult> OnPostReportDiscussion(string id)
        {
            
            try
            {
                id = DiscussionId;
                ClickedPostDiscussion = await _gateway.GetDiscussionById(id, HttpContext);
                var response = await _gateway.ReportDiscussionById(id, HttpContext, ClickedPostDiscussion);
                IActionResult resultPage = await OnGetAsync();
                return resultPage;
            }
            catch (Exception)
            {
                return NotFound();
            }
               
        }

    }
}
