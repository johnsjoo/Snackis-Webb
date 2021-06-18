﻿using Microsoft.AspNetCore.Http;
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

        Task<Post> GetPostById(string PostId, HttpContext context);

        Task<HttpResponseMessage> CreateNewPost(Post post);

        Task<HttpResponseMessage> DeletePostById(string PostId, HttpContext context);

        Task<PostDiscussion> GetDiscussionById(string discussionId);

        Task<HttpResponseMessage> CreateNewCategory(Category NewCategory, HttpContext context);

        public string GetSession(HttpContext context);

    }
}
