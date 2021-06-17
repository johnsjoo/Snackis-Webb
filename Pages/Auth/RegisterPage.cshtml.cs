using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        
        private readonly HttpClient _client;
        private readonly IAuthGateway _authGateway;
      

        public RegisterPageModel(IAuthGateway ag, HttpClient client)
        {
            _authGateway = ag;
            _client = client;
            
            
            
        }
        
        [BindProperty (SupportsGet =true)]
        public Models.RegisterModel NewUser { get; set; }

        [BindProperty (SupportsGet =true)]
        public IFormFile UploadFile { get; set; }
        
        [BindProperty]
        public string UserNameMessage { get; set; }
        [BindProperty]
        public string MessageMail { get; set; }

        [BindProperty]
        public string IncompleteUnserInfo { get; set; }
        public void OnGet() 
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            if (UploadFile != null)
            {
                var file = "./wwwroot/img/" + UploadFile.FileName;
                using (var filestream = new FileStream(file, FileMode.Create)) 
                {
                    NewUser.Image = UploadFile.FileName;
                    await UploadFile.CopyToAsync(filestream);
                }
                
            }
            var response = await _authGateway.RegisterNewUser(NewUser);
            string request = response.Content.ReadAsStringAsync().Result;

            if (request == $"wrong username")
            {
                UserNameMessage = "V�nligen v�lj ett anv�ndarnamn som inte inneh�ller mellanslag";
                return Page();
            }
            if (request == "Username in use")
            {
                UserNameMessage = "Anv�ndarnamnet anv�nds redan, testa ett annat";
                return Page();
            }
            if (request == $"E-mail in use")
            {
                MessageMail = "Mailen anv�nds redan, testa en annan";
            }
            if (request == $"")
            {
                IncompleteUnserInfo = "Var v�nlig fyll i samtliga f�llt";
                return Page();
            }
            return RedirectToPage("/Index");  
        }
    }
}
