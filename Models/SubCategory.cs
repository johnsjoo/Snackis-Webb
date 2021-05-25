using SNACKIS___Webb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SNACKIS___Webb.Models
{
    public class SubCategory
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("categories")]
        public virtual Categories Categories { get; set; }
        [JsonPropertyName("categoryid")]
        public string CategoryId { get; set; }
        [JsonPropertyName("posts")]
        public virtual IList<Post> Posts { get; set; }
    }
}
