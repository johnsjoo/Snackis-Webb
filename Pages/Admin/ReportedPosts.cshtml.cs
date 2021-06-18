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
            byte[] tokenByte;
            HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
            string token = Encoding.ASCII.GetString(tokenByte);

            if (!String.IsNullOrEmpty(token))
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");

                var response = await _client.GetAsync(_configuration["getReportedPosts"]);
                string apiResponse = await response.Content.ReadAsStringAsync();
                Posts = JsonSerializer.Deserialize<List<Post>>(apiResponse);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var discussionResponse = await _client.GetAsync(_configuration["GetreportedDiscussions"]);
                    string discussionApiResponse = await discussionResponse.Content.ReadAsStringAsync();
                    ReportedPostDiscussions = JsonSerializer.Deserialize<List<PostDiscussion>>(discussionApiResponse);
                    PostCounter = Posts.Count + ReportedPostDiscussions.Count;
                    return Page();
                }
                else
                {
                    return NotFound();
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            id = postId;
            byte[] tokenByte;
            HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
            string token = Encoding.ASCII.GetString(tokenByte);

            toggledPost = await _gateway.GetPostById(postId, HttpContext);

            if (!String.IsNullOrEmpty(token))
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");

                var response = await _client.PutAsJsonAsync(_configuration["toggleReportedPost"] + id, toggledPost);
                string apiResponse = await response.Content.ReadAsStringAsync();
                //Posts = JsonSerializer.Deserialize<List<Post>>(apiResponse);

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
            return Page();

        }
        public async Task<IActionResult> OnPostReportDiscussion(string id)
        {
            id = DiscussionId;
            
            byte[] tokenByte;
            HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
            string token = Encoding.ASCII.GetString(tokenByte);

            ClickedPostDiscussion = await _gateway.GetDiscussionById(id);

            if (!String.IsNullOrEmpty(token))
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");

                var response = await _client.PutAsJsonAsync(_configuration["ReportDiscussionById"] + id, ClickedPostDiscussion);
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    IActionResult resultPage = await OnGetAsync();
                    return resultPage;
                    //return Page();
                }
                else
                {
                    return NotFound();
                }
            }
            return Page();

        }

    }
}
