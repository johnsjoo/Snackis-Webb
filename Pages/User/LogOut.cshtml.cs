using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SNACKIS___Webb.Tools;

namespace SNACKIS___Webb.Pages.User
{
    public class LogOutModel : PageModel
    {
       

        public IActionResult OnGet()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/index");
        }
    }
}
