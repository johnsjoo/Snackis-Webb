using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SNACKIS___Webb.Models
{
    public class LoginResponseModel
    {
        public string Token { get; set; }
        public string Expires { get; set; }
        public string UserID { get; set; }
        public string Role { get; set; }
    }
}
