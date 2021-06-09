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
                byte[] tokenByte;
                HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
                var token = Encoding.ASCII.GetString(tokenByte);

                if (!String.IsNullOrEmpty(token))
                {
                    _client.DefaultRequestHeaders.Accept.Clear();
                    _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");

                    var response = await _client.GetAsync(_configuration["GetCategoryById"] + "/" + NewCatId);
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    CurrerntCategory = Newtonsoft.Json.JsonConvert.DeserializeObject<Category>(apiResponse);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return Page();
                    }
                    else
                    {
                        return RedirectToPage("/Error");
                    }

                }
                return Page();
            }
            catch (Exception)
            {
                return Page();
            }

        }
        public async Task<IActionResult> OnPostAsync() 
        {
            string token = null;
            try
            {
                byte[] tokenByte;
                HttpContext.Session.TryGetValue(ToolBox.TokenName, out tokenByte);
                token = Encoding.ASCII.GetString(tokenByte);
                
                if (!String.IsNullOrEmpty(token))
                {
                    _client.DefaultRequestHeaders.Accept.Clear();
                    _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{token}");
                    NewPost.CategoryId = NewCatId;
                    var response = await _client.PostAsJsonAsync(_configuration["CreateNewPost"], NewPost);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return RedirectToPage("/index");
                    }
                    else
                    {
                        return RedirectToPage("/Error");
                    }
                    
                }
                return Page();
            }
            catch (Exception)
            {
                return Page();
            }

        }

    }
}
