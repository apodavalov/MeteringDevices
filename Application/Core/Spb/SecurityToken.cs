using System;
using System.Collections.Generic;
using System.Linq;

namespace MeteringDevices.Core.Spb
{
    class SecurityToken
    {
        public SecurityToken(string token, IEnumerable<Cookie> cookies)
        {
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            if (cookies == null)
            {
                throw new ArgumentNullException(nameof(cookies));
            }

            Token = token;
            Cookies = cookies.ToList().AsReadOnly();
        }

        public string Token
        {
            get;
            private set;
        }
        
        public IReadOnlyList<Cookie> Cookies
        {
            get;
            private set;
        }
    }
}
