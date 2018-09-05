using System;

namespace MeteringDevices.Core
{
    interface ICurrentDateTimeProvider
    {
        DateTime GetUtcNow();
    }
}
