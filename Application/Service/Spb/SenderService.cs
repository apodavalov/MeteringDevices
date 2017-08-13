using System;
using System.Collections.Generic;

namespace MeteringDevices.Service.Spb
{
    class SenderService : ISendService
    {
        private const string _UsernameKey = "Spb.Service.Username";
        private const string _PasswordKey = "Spb.Service.Password";

        public void PutValues(IDictionary<string, int> values, string accountNumber)
        {
            throw new NotImplementedException();
        }
    }
}
