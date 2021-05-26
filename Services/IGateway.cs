using SNACKIS___Webb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNACKIS___Webb.Services
{
    public interface IGateway
    {
        Task<List<Categories>> GetAllCategories();
        Task<List<Post>> GetAllPosts();
       
    }
}
