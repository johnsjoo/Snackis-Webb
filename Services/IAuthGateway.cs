using SNACKIS___Webb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNACKIS___Webb.Services
{
    public interface IAuthGateway
    {
        Task<User> GetLoggedInUser(string Id);
        Task<RegisterModel> RegisterNewUser(RegisterModel user);


    }
}
