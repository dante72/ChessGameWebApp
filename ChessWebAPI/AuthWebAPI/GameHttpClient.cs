using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthWebAPI.AuthWebAPI
{
    public class GameHttpClient : HttpClient
    {
        public GameHttpClient()
        {
            BaseAddress = new Uri("https://localhost:7084/");
        }
    }
}
