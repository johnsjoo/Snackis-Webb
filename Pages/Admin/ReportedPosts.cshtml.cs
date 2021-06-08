using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        [BindProperty]
        public List<Post> Posts { get; set; }
        public ReportedPostsModel( HttpClient client, IConfiguration configuration)
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
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");

                var response = await _client.GetAsync(_configuration["getReportedPosts"]);
                string apiResponse = await response.Content.ReadAsStringAsync();
                Posts = JsonSerializer.Deserialize<List<Post>>(apiResponse);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Page();
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
