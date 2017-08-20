using MeteringDevices.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeteringDevices.Core.Spb
{
    class RetrieverService : IRetrieveService
    {
        private readonly string _DayMeteringDeviceId;
        private readonly string _NightMeteringDeviceId;

        public RetrieverService(string dayMeteringDeviceId, string nightMeteringDeviceId)
        {
            if (dayMeteringDeviceId == null)
            {
                throw new ArgumentNullException(nameof(dayMeteringDeviceId));
            }

            if (nightMeteringDeviceId == null)
            {
                throw new ArgumentNullException(nameof(nightMeteringDeviceId));
            }

            _DayMeteringDeviceId = dayMeteringDeviceId;
            _NightMeteringDeviceId = nightMeteringDeviceId;
        }

        public IDictionary<string, int> GetCurrentValues(ISession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            Data.Spb.CurrentMeteringValue currentValues = currentValues = session.SpbCurrentMeteringValueRepository.Fetch().AsEnumerable().SingleOrDefault();

            if (currentValues == null)
            {
                return null;
            }

            return new Dictionary<string, int>(StringComparer.Ordinal)
            {
                { _DayMeteringDeviceId, currentValues.Day },
                { _NightMeteringDeviceId, currentValues.Night }
            };
        }
    }
}
