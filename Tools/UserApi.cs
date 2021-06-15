using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SNACKIS___Webb.Tools
{
    public class UserApi
    {
        private readonly HttpClient _client;


        public UserApi(HttpClient client)
        {
            _client = client;
        }
        public HttpClient Initial()
        {
            //_client.BaseAddress = new Uri("http://localhost:50249");
            _client.BaseAddress = new Uri("https://codecaveapi.azurewebsites.net");
            return _client;
        }
    }
}
