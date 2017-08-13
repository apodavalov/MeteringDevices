using System.Collections.Generic;

namespace MeteringDevices.Service
{
    interface ISendService
    {
        void PutValues(IDictionary<string, int> values, string accountNumber);
    }
}
