using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SNACKIS___Webb.Models
{
    public class PostDiscussion
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("userid")]
        public string UserId { get; set; }
        [JsonPropertyName("postid")]
        public string PostId { get; set; }
        [JsonPropertyName("discussion")]
        public string Discussion { get; set; }
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("isreported")]
        public bool IsReported { get; set; }
        [JsonPropertyName("user")]
        public virtual User User { get; set; }
    }
}
