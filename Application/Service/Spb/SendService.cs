using System;
using System.Collections.Generic;

namespace MeteringDevices.Service.Spb
{
    class SendService : ISendService
    {
        private const string _UsernameKey = "Spb.Service.Username";
        private const string _PasswordKey = "Spb.Service.Password";

        public void PutValues(string accountNumber, IDictionary<string, int> values)
        {
            throw new NotImplementedException();
        }
    }
}
