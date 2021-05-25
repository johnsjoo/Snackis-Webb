using System.Text.Json.Serialization;

namespace SNACKIS___Webb.Services
{
    public class Categories
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}