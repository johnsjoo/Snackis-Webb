using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SNACKIS___Webb.Models
{
    public partial class UserLoginResponseModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("fullname")]
        public string Fullname { get; set; }
        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }

    }

    public partial class UserLoginResponseModel
    {
        public static UserLoginResponseModel[] FromJson(string json) => JsonConvert.DeserializeObject<UserLoginResponseModel[]>(json, Converter.Settings);
        public static UserLoginResponseModel FromJsonSingle(string json) => JsonConvert.DeserializeObject<UserLoginResponseModel>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this UserLoginResponseModel[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
