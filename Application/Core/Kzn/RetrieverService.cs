using System;
using System.Collections.Generic;
using System.Linq;
using MeteringDevices.Data;

namespace MeteringDevices.Core.Kzn
{
    class RetrieverService : IRetrieveService
    {
        private readonly string _DayMeteringDeviceId;
        private readonly string _NightMeteringDeviceId;
        private readonly string _ColdMeteringDeviceId;
        private readonly string _HotMeteringDeviceId;

        public RetrieverService(string dayMeteringDeviceId, string nightMeteringDeviceId,
                                string coldMeteringDeviceId, string hotMeteringDeviceId)
        {
            if (dayMeteringDeviceId == null)
            {
                throw new ArgumentNullException(nameof(dayMeteringDeviceId));
            }

            if (nightMeteringDeviceId == null)
            {
                throw new ArgumentNullException(nameof(nightMeteringDeviceId));
            }

            if (coldMeteringDeviceId == null)
            {
                throw new ArgumentNullException(nameof(coldMeteringDeviceId));
            }

            if (hotMeteringDeviceId == null)
            {
                throw new ArgumentNullException(nameof(hotMeteringDeviceId));
            }

            _DayMeteringDeviceId = dayMeteringDeviceId;
            _NightMeteringDeviceId = nightMeteringDeviceId;
            _ColdMeteringDeviceId = coldMeteringDeviceId;
            _HotMeteringDeviceId = hotMeteringDeviceId;
        }

        public IDictionary<string, int> GetCurrentValues(ISession session)
        {
            Data.Kzn.CurrentMeteringValue currentValues = currentValues = session.KznCurrentMeteringValueRepository.Fetch().AsEnumerable().SingleOrDefault();

            if (currentValues == null)
            {
                return null;
            }

            return new Dictionary<string, int>(StringComparer.Ordinal)
            {
                { _DayMeteringDeviceId, currentValues.Day },
                { _NightMeteringDeviceId, currentValues.Night },
                { _ColdMeteringDeviceId, currentValues.Cold },
                { _HotMeteringDeviceId, currentValues.Hot }
            };
        }
    }
}
