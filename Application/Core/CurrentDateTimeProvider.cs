using System;

namespace MeteringDevices.Core
{
    class CurrentDateTimeProvider : ICurrentDateTimeProvider
    {
        public DateTime GetUtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
