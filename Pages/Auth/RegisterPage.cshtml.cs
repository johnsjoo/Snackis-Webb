using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SNACKIS___Webb.Models;
using SNACKIS___Webb.Services;
using SNACKIS___Webb.Tools;

namespace SNACKIS___Webb.Pages.Auth
{
    public class RegisterPageModel : PageModel
    {
        private readonly UserApi _api;
        private readonly HttpClient _client;
        private readonly IAuthGateway _authGateway;


        public RegisterPageModel(IAuthGateway ag, HttpClient client)
        {
            _authGateway = ag;
            _client = client;
        }

        [BindProperty]
        public Models.RegisterModel NewUser { get; set; }
        public void OnGet() 
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _authGateway.RegisterNewUser(NewUser);
            return RedirectToPage("/Pages/Index");
            
        }

        
    }
}
