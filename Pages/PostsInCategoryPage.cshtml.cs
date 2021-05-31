using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SNACKIS___Webb.Models;

namespace SNACKIS___Webb.Pages
{
    public class PostsInCategoryPageModel : PageModel
    {
        
        private readonly Services.IGateway _gateway;

        public PostsInCategoryPageModel( Services.IGateway gateway)
        {           
            _gateway = gateway;
        }


        [BindProperty(SupportsGet =true)]
        public string CatId { get; set; }
        public List<Post> Posts { get; set; }
        public List<Category> Categories { get; set; }
        public async Task OnGet()
        {
            Posts = await _gateway.GetPostsByCatId(CatId);
            Categories = await _gateway.GetAllCategories();
        }
    }
}
