using System;
using System.Collections.Generic;
using System.Linq;
using MeteringDevices.Data;

namespace MeteringDevices.Service.Kzn
{
    class RetrieverService : IRetrieveService
    {
        private const string _KznMeteringDeviceDayIdKey = "Kzn.MeteringDevice.DayId";
        private const string _KznMeteringDeviceNightIdKey = "Kzn.MeteringDevice.NightId";
        private const string _KznMeteringDeviceColdIdKey = "Kzn.MeteringDevice.ColdId";
        private const string _KznMeteringDeviceHotIdKey = "Kzn.MeteringDevice.HotId";

        public IDictionary<string, int> GetCurrentValues(ISession session)
        {
            Data.Kzn.CurrentMeteringValue currentValues = currentValues = session.KznCurrentMeteringValueRepository.Fetch().AsEnumerable().SingleOrDefault();

            if (currentValues == null)
            {
                return null;
            }

            return new Dictionary<string, int>(StringComparer.Ordinal)
            {
                { ConfigUtils.GetStringFromConfig(_KznMeteringDeviceDayIdKey), currentValues.Day },
                { ConfigUtils.GetStringFromConfig(_KznMeteringDeviceNightIdKey), currentValues.Night },
                { ConfigUtils.GetStringFromConfig(_KznMeteringDeviceColdIdKey), currentValues.Cold },
                { ConfigUtils.GetStringFromConfig(_KznMeteringDeviceHotIdKey), currentValues.Hot }
            };
        }
    }
}
