using System.Collections.Generic;

namespace MeteringDevices.Service
{
    interface ISendService
    {
        void PutValues(string accountNumber, IDictionary<string, int> values);
    }
}
