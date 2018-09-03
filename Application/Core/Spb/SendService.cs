using System;
using System.Collections.Generic;

namespace MeteringDevices.Core.Spb
{
    class SendService : ISendService
    {
        public void PutValues(string accountNumber, IReadOnlyDictionary<string, int> values)
        {
            throw new NotImplementedException();
        }
    }
}
