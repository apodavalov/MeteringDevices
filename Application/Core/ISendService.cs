using System.Collections.Generic;

namespace MeteringDevices.Core
{
    interface ISendService
    {
        void PutValues(string accountNumber, IDictionary<string, int> values);
    }
}
