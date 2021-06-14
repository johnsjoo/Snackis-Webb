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
        public string message { get; set; }
        [BindProperty]
        public List<Post> Posts { get; set; }


        [BindProperty]
        public int PostCounter { get; set; }

        [BindProperty]
        public List<PostDiscussion> ReportedPostDiscussions { get; set; }

        public IndexModel(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;

        }
        public async Task<IActionResult> OnGetAsync()
        {
           
            byte[] tokenByte;
            HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
            string token = Encoding.ASCII.GetString(tokenByte);
           


            if (!String.IsNullOrEmpty(token))
            {
                try
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
                catch (Exception)
                {
                    message = "Get fucked";
                    return Page();
                }


               
            }
            return Page();
        }
    }
}
