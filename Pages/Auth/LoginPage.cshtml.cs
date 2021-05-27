using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SNACKIS___Webb.Models;
using SNACKIS___Webb.Tools;

namespace SNACKIS___Webb.Pages.Auth
{
    public class LoginPageModel : PageModel
    {

        private readonly UserApi _api;
        

        [BindProperty]
        public string UserName { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public string Message { get; set; }
        public string MessageMail { get; set; }
        public string StoredID { get; set; }
        public string SessionInfoToken { get; private set; }


        public LoginPageModel(UserApi api)
        {
            _api = api;
        }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync(string userName, string password)
        {
            userName = UserName;
            password = Password;

            HttpClient client = _api.Initial();
            

            var values = new Dictionary<string, string>()
                 {
                    {"user", $"{userName}"},
                    {"password", $"{password}"}
                 };

            string payload = JsonConvert.SerializeObject(values);

            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("auth/login", content);

            string request = response.Content.ReadAsStringAsync().Result;

            if (request == "No user or password matched, try again.")
            {
                Message = "Fel e-post/användarnamn eller lösenord, försök igen.";
                return Page();
            }
            if (request == "No such user exists")
            {
                Message = "Ingen sådan användare finns registrerad.";
                return Page();
            }
            else
            {
                LoginResponseModel result = JsonConvert.DeserializeObject<LoginResponseModel>(request);
                TokenChecker.UserName = userName;   
                ToolBox.LoggedInUserID = result.UserID;
                ToolBox.ActiveRole = result.Role;

                byte[] tokenInByte = Encoding.ASCII.GetBytes(result.Token);

                HttpContext.Session.Set(ToolBox.TokenName, tokenInByte);
                HttpContext.Session.SetString("Id", result.UserID);

            }
            if (response.IsSuccessStatusCode)
                TokenChecker.UserStatus = true;
            else
                TokenChecker.UserStatus = false;

            return RedirectToPage("/index");
        }
    }
}
