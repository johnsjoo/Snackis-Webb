using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SNACKIS___Webb.Models;
using SNACKIS___Webb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SNACKIS___Webb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Services.IGateway _gateway;
       
        public List<Categories> Categories;
        [BindProperty(SupportsGet =true)]
        public List<Post> Posts { get; set; }

        public IndexModel(ILogger<IndexModel> logger, Services.IGateway gateway)
        {
            _logger = logger;
            _gateway = gateway;

        }

        public async Task OnGetAsync()
        {
            Categories = await _gateway.GetAllCategories();
            Posts = await _gateway.GetAllPosts();
        }
    }
}
