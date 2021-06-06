using SNACKIS___Webb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SNACKIS___Webb.Services
{
    public interface IGateway
    {
        Task<List<Category>> GetAllCategories();
        Task<List<Post>> GetAllPosts();

        Task<List<Post>> GetPostsByCatId(string catId);

        Task<Post> GetPostById(string PostId);

        Task<HttpResponseMessage> CreateNewPost(Post post);

    }
}
