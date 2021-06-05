using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SNACKIS___Webb.Models;
using SNACKIS___Webb.Services;

namespace SNACKIS___Webb.Pages
{
    public class NewPostViewModel : PageModel
    {


        private readonly IGateway _gateway;
        private readonly HttpClient _client;

        [BindProperty(SupportsGet = true)]
        public string NewCatId { get; set; }

        [BindProperty(SupportsGet =true)]
        public Post NewPost { get; set; }



        public NewPostViewModel(IGateway gateway, HttpClient client)
        {
            _gateway = gateway;
            _client = client;
        }

        public async Task OnGetAsync()
        {

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

                    var response = await _gateway.CreateNewPost(NewPost);
                    //string apiResponse = await response.Content.ReadAsStringAsync();
                    //var model = UserLoginResponseModel.FromJsonSingle(apiResponse);


                    return RedirectToPage("/index");
                }
                return Page();
            }
            catch (Exception)
            {
                //Message = "Du måste logga in för att kunna se dina valda varor.";
                return Page();
            }




           
           
        }
    }
}
