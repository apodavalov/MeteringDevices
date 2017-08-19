using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeteringDevices.Core.Spb
{
    class SecurityToken
    {

        public SecurityToken(string token, IEnumerable<RestResponseCookie> cookies)
        {
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            Token = token;
            Cookies = cookies.ToList().AsReadOnly();
        }

        public string Token
        {
            get;
            private set;
        }
        
        public IReadOnlyList<RestResponseCookie> Cookies
        {
            get;
            private set;
        }
    }
}
