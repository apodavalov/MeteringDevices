using System;

namespace MeteringDevices.Core.Kzn
{
    class SecurityToken
    {
        public SecurityToken(string userId, string sessionToken)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (sessionToken == null)
            {
                throw new ArgumentNullException(nameof(sessionToken));
            }

            UserId = userId;
            SessionToken = sessionToken;
        }

        public string SessionToken
        {
            get;
            private set;
        }

        public string UserId
        {
            get;
            private set;
        }
    }
}
