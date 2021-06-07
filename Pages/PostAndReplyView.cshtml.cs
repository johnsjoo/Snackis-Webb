using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SNACKIS___Webb.Models;
using SNACKIS___Webb.Services;

namespace SNACKIS___Webb.Pages
{
    public class PostAndReplyViewModel : PageModel
    {
        private readonly IGateway _gateway;
        public PostAndReplyViewModel(IGateway gateway)
        {
            _gateway = gateway;
        }
        [BindProperty(SupportsGet = true)]
        public string PostId { get; set; }

        [BindProperty(SupportsGet =true)]
        public Post ClickedPost { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!string.IsNullOrEmpty(PostId))
            {
                
                ClickedPost = await _gateway.GetPostById(PostId);

                
                return Page();
            }
            
            return Page();
        }
    }
}
