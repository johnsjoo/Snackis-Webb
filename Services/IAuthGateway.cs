using Microsoft.AspNetCore.Http;
using SNACKIS___Webb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SNACKIS___Webb.Services
{
    public interface IAuthGateway
    {
        Task<User> GetLoggedInUser(string Id);
        Task<HttpResponseMessage> RegisterNewUser(RegisterModel user);
        public string GetSession(HttpContext context);

    }
}
