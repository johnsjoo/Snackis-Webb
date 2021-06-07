using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SNACKIS___Webb.Models
{
    public class RegisterModel
    {
        
        public string Id { get; set; }

        public string Image { get; set; }

        public string Username { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public string FullName { get; set; }
      
        public string Phone { get; set; }
    }
}
