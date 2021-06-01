using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SNACKIS___Webb.Models;
using SNACKIS___Webb.Services;

namespace SNACKIS___Webb.Pages.User
{
    public class UserPageModel : PageModel
    {
        private readonly IAuthGateway _authgateway;
        public UserPageModel(IAuthGateway authgateway)
        {
            _authgateway = authgateway;
        }

        public Models.User User { get; set; }
        public async Task OnGetAsync()
        {
            User = await _authgateway.GetLoggedInUser();
        }


    }
}
