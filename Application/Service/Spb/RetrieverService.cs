using System;
using System.Collections.Generic;
using System.Linq;
using MeteringDevices.Data;

namespace MeteringDevices.Service.Spb
{
    class RetrieverService : IRetrieveService
    {
        private const string _MeteringDeviceDayIdKey = "Spb.MeteringDevice.DayId";
        private const string _MeteringDeviceNightIdKey = "Spb.MeteringDevice.NightId";

        public IDictionary<string, int> GetCurrentValues(ISession session)
        {
            Data.Spb.CurrentMeteringValue currentValues = currentValues = session.SpbCurrentMeteringValueRepository.Fetch().AsEnumerable().SingleOrDefault();

            if (currentValues == null)
            {
                return null;
            }

            return new Dictionary<string, int>(StringComparer.Ordinal)
            {
                { ConfigUtils.GetStringFromConfig(_MeteringDeviceDayIdKey), currentValues.Day },
                { ConfigUtils.GetStringFromConfig(_MeteringDeviceNightIdKey), currentValues.Night }
            };
        }
    }
}
