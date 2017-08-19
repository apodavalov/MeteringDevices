using System.Collections.Generic;

namespace MeteringDevices.Core.Spb
{
    interface ISendApiService
    {
        void SetValues(string token, string meteringDeviceId, IReadOnlyList<int> values);
    }
}
