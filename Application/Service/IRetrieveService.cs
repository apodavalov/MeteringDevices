using MeteringDevices.Data;
using System.Collections.Generic;

namespace MeteringDevices.Service
{
    interface IRetrieveService
    {
        IDictionary<string, int> GetCurrentValues(ISession session);
    }
}
