using MeteringDevices.Data;
using System.Collections.Generic;

namespace MeteringDevices.Core
{
    interface IRetrieveService
    {
        IDictionary<string, int> GetCurrentValues(ISession session);
    }
}
