using System.Collections.Generic;

namespace MeteringDevices.Core
{
    interface ISendService
    {
        void PutValues(string accountNumber, IReadOnlyDictionary<string, int> values);
    }
}
