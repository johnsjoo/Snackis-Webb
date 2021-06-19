using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using SNACKIS___Webb.Gateway;
using SNACKIS___Webb.Models;
using SNACKIS___Webb.Services;

namespace SNACKIS___Webb.Pages.User
{
    public class NewMessageModel : PageModel
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly IGateway _gateway;
        private readonly IAuthGateway _authGateway;

        [BindProperty(SupportsGet = true)]
        public PrivateMessage NewMessage { get; set; }

        [BindProperty(SupportsGet =true)]
        public string UserId { get; set; }

        [BindProperty]
        public Models.User User { get; set; }

        [BindProperty]
        public List<Models.User> Users { get; set; }

        public NewMessageModel(HttpClient client, IConfiguration configuration, IGateway gateway,IAuthGateway authGateway)
        {
          
            _client = client;
            _configuration = configuration;
            _gateway = gateway;
            _authGateway = authGateway;
        }
        public async Task<IActionResult> OnGetAsync()
        {

            try
            {
                Users = await _authGateway.GetAllUsers(HttpContext);

                if (!string.IsNullOrEmpty(UserId))
                {
                   
                    User = await _authGateway.GetLoggedInUser(HttpContext, UserId);

                }
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
                var response = await _gateway.CreateMessage(HttpContext, NewMessage);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    IActionResult resultPage = await OnGetAsync();
                    return resultPage;
                }
                    
                
            }
            catch (Exception)
            {

                return RedirectToPage("/Error");
            }
            return RedirectToPage("chatt");
        }
    }
}
