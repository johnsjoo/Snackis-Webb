using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using SNACKIS___Webb.Models;
using SNACKIS___Webb.Services;

namespace SNACKIS___Webb.Pages.User
{
    public class chattModel : PageModel
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly IGateway _gateway;

        [BindProperty]
        public string ErrorMessage { get; set; }

        [BindProperty(SupportsGet =true)]
        public string UserId { get; set; }

        [BindProperty(SupportsGet =true)]
        public PrivateMessage NewMessage { get; set; }

        [BindProperty(SupportsGet =true)]
        public List<PrivateMessage> Messages { get; set; }

        [BindProperty]
        public List<Models.User> MessageUsers { get; set; }

        public chattModel(HttpClient client, IConfiguration configuration, IGateway gateway)
        {
            _client = client;
            _configuration = configuration;
            _gateway = gateway;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Messages = await _gateway.GetMessagesByUser(HttpContext);
                MessageUsers = await _gateway.GetMessageUsers(HttpContext);
                if (UserId == null)
                {
                    UserId = NewMessage.MessageReceiver;

                    return Page();
                }
            }
            catch (Exception)
            {

                return RedirectToPage("Error");
            }     
            return Page();
        }
        public async Task<IActionResult> OnPostAsync() 
        {
            try
            {
                if (string.IsNullOrEmpty(NewMessage.Message))
                {
                    ErrorMessage = "Du kan inte skicka tomma meddelanden";
                    IActionResult resultPage1 = await OnGetAsync();
                    return Page();
                }
                else
                {
                    var response = await _gateway.CreateMessage(HttpContext, NewMessage);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {

                        IActionResult resultPage = await OnGetAsync();
                        ModelState.Clear();
                        NewMessage.Message = null;

                        return Page();
                    }
                    else
                    {
                        return RedirectToPage("/Error");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
