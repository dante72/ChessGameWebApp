using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthWebAPI.AuthWebAPI
{
    public class AuthHttpClient : HttpClient
    {
        public AuthHttpClient()
        {
            BaseAddress = new Uri("https://localhost:7256/");
        }
    }
}
