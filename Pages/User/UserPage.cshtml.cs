using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using SNACKIS___Webb.Models;
using SNACKIS___Webb.Services;
using SNACKIS___Webb.Tools;

namespace SNACKIS___Webb.Pages.User
{
    public class UserPageModel : PageModel
    {
        private readonly IAuthGateway _authgateway;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        
        public UserPageModel(IAuthGateway authgateway, HttpClient client, IConfiguration configuration)
        {
            _authgateway = authgateway;
            _client = client;
            _configuration = configuration;
           
        }

        public UserLoginResponseModel User { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                string Id = HttpContext.Session.GetString("Id");
                User = await _authgateway.GetLoggedInUserByModel(HttpContext, Id);
                return Page();
            }
            catch (Exception)
            {

                return NotFound();
            }   
     
        }
    }
}
