using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Requests
{
    public class AuthenticateResponse
    {
        public AuthenticateResponse(string token)
        {
            Token = token;
        }
        public string Token { get; set; }
    }
}
