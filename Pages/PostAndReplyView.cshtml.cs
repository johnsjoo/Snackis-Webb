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
    public class PostAndReplyViewModel : PageModel
    {
        private readonly IGateway _gateway;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        public PostAndReplyViewModel(IGateway gateway, HttpClient client, IConfiguration configuration)
        {
            _gateway = gateway;
            _client = client;
            _configuration = configuration;
        }


        [BindProperty(SupportsGet = true)]
        public string PostId { get; set; }

        [BindProperty(SupportsGet =true)]
        public Post ClickedPost { get; set; }

        public List<Category> Categories;

        public UserLoginResponseModel User { get; set; }


        

        public async Task<IActionResult> OnGetAsync()
        {
            byte[] tokenByte;
            
            HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
            if (tokenByte != null)
            {
                string token = Encoding.ASCII.GetString(tokenByte);
                string Id = HttpContext.Session.GetString("Id");

                if (!String.IsNullOrEmpty(token))
                {
                    _client.DefaultRequestHeaders.Accept.Clear();
                    _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");

                    var response = await _client.GetAsync(_configuration["GetLoggedInUser"] + "/" + Id);
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var model = UserLoginResponseModel.FromJsonSingle(apiResponse);
                    User = model;
                }

            }

                Categories = await _gateway.GetAllCategories();

            if (!string.IsNullOrEmpty(PostId))
            {
                
                ClickedPost = await _gateway.GetPostById(PostId);

                
                return Page();
            }
            
            return Page();
        }
        public async Task<IActionResult> OnPost(string id)
        {
            id = PostId;
            byte[] tokenByte;
            HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
            string token = Encoding.ASCII.GetString(tokenByte);
            

            if (!String.IsNullOrEmpty(token))
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");

                var response = await _client.PutAsJsonAsync(_configuration["ReportPostById"]+"/" + id, ClickedPost);
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    IActionResult resultPage = await OnGetAsync();
                    return resultPage;
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
