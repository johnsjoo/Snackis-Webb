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
            byte[] tokenByte;
            HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
            string token = Encoding.ASCII.GetString(tokenByte);

            if (!String.IsNullOrEmpty(token))
            {
                try
                {
                    _client.DefaultRequestHeaders.Accept.Clear();
                    _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");
                    DeletedPostDiscussion = await _gateway.GetDiscussionById(DeletedDiscussionId);

                    return Page();
                }
                catch (Exception)
                {

                    return RedirectToPage("/Error");
                }
            }
            return Page();

        }
        public async Task<IActionResult> OnPostAsync()
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
                    //var response = await _gateway.DeletePostById(DeletedId);
                    var response = await _client.DeleteAsync(_configuration["DeleteDiscussionById"] + "/" + DeletedDiscussionId);
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    return RedirectToPage("ReportedPosts");


                }
                catch (Exception)
                {

                    return RedirectToPage("/Error");
                }
            }
            return Page();
        }
    }
}
