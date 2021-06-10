using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SNACKIS___Webb.Models
{
    public class Message
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("userid")]
        public string UserId { get; set; }
        [JsonPropertyName("message")]
        public string message { get; set; }
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("messagereceiver")]
        public string MessageReceiver { get; set; }
        [JsonPropertyName("user")]
        public virtual User User { get; set; }
    }
}
