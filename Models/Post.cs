using SNACKIS___Webb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SNACKIS___Webb.Models
{
    public class Post
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("userid")]
        public string UserId { get; set; }
        [JsonPropertyName("categoryid")]
        public string CategoryId { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("isreported")]
        public bool IsReported { get; set; }
        [JsonPropertyName("category")]
        public virtual Category Categories { get; set; }
        [JsonPropertyName("user")]
        public virtual User User { get; set; }
        [JsonPropertyName("postdiscussion")]
        public ICollection<PostDiscussion> PostDiscussions { get; set; }
    }
}
