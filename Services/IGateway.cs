using Microsoft.AspNetCore.Http;
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

        Task<HttpResponseMessage> ReportPostDiscussion(HttpContext context, string Id, PostDiscussion ClickedPostDiscussion);
        Task<HttpResponseMessage> ReportPostById(HttpContext context, string Id, Post ClickedPost);
        Task<HttpResponseMessage> DeletePostById(string PostId, HttpContext context);
        Task<HttpResponseMessage> CreateNewPostDiscussion(HttpContext context, PostDiscussion NewDiscussion);
        Task<PostDiscussion> GetDiscussionById(string discussionId, HttpContext context);

        Task<HttpResponseMessage> CreateNewCategory(Category NewCategory, HttpContext context);
        Task<HttpResponseMessage> DeleteDiscussionById(string deleteId, HttpContext context);
        Task<List<Post>> GetReportedPosts(HttpContext context);
        Task<List<PostDiscussion>> GetReportedPostDiscussions(HttpContext context);
        Task<HttpResponseMessage> ToggleReportedPost(HttpContext context, string id, Post toggledPost);
        Task<HttpResponseMessage> ReportDiscussionById(string id, HttpContext context, PostDiscussion ClickedPostDiscussion);
        Task<List<PrivateMessage>> GetMessagesByUser(HttpContext context);
        Task<List<Models.User>> GetMessageUsers(HttpContext context);
        Task<HttpResponseMessage> CreateMessage(HttpContext context, PrivateMessage NewMessage);
        Task<Category> GetCategoryById(HttpContext context, string NewCatId);
        Task<HttpResponseMessage> CreateNewPost(HttpContext context, Post NewPost);

        public string GetSession(HttpContext context);

    }
}
