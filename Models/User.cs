using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SNACKIS___Webb.Models
{
    public class User
    {
        [JsonPropertyName("fullname")]
        public string FullName { get; set; }
    }
}
