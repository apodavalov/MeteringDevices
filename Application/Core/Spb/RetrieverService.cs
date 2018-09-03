using MeteringDevices.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeteringDevices.Core.Spb
{
    class RetrieverService : IRetrieveService
    {
        private readonly string _DayMeteringDeviceLabel;
        private readonly string _NightMeteringDeviceLabel;
        private readonly string _KitchenColdMeteringDeviceLabel;
        private readonly string _KitchenHotMeteringDeviceLabel;
        private readonly string _BathroomColdMeteringDeviceLabel;
        private readonly string _BathroomHotMeteringDeviceLabel;

        public RetrieverService(string dayMeteringDeviceLabel, string nightMeteringDeviceLabel,
            string kitchenColdMeteringDeviceLabel, string kitchenHotMeteringDeviceLabel,
            string bathroomColdMeteringDeviceLabel, string bathroomHotMeteringDeviceLabel)
        {
            if (dayMeteringDeviceLabel == null)
            {
                throw new ArgumentNullException(nameof(dayMeteringDeviceLabel));
            }

            if (nightMeteringDeviceLabel == null)
            {
                throw new ArgumentNullException(nameof(nightMeteringDeviceLabel));
            }

            if (kitchenColdMeteringDeviceLabel == null)
            {
                throw new ArgumentNullException(nameof(kitchenColdMeteringDeviceLabel));
            }

            if (kitchenHotMeteringDeviceLabel == null)
            {
                throw new ArgumentNullException(nameof(kitchenHotMeteringDeviceLabel));
            }

            if (bathroomColdMeteringDeviceLabel == null)
            {
                throw new ArgumentNullException(nameof(bathroomColdMeteringDeviceLabel));
            }

            if (bathroomHotMeteringDeviceLabel == null)
            {
                throw new ArgumentNullException(nameof(bathroomHotMeteringDeviceLabel));
            }

            _DayMeteringDeviceLabel = dayMeteringDeviceLabel;
            _NightMeteringDeviceLabel = nightMeteringDeviceLabel;
            _KitchenColdMeteringDeviceLabel = kitchenColdMeteringDeviceLabel;
            _KitchenHotMeteringDeviceLabel = kitchenHotMeteringDeviceLabel;
            _BathroomColdMeteringDeviceLabel = bathroomColdMeteringDeviceLabel;
            _BathroomHotMeteringDeviceLabel = bathroomHotMeteringDeviceLabel;
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
                { _DayMeteringDeviceLabel, currentValues.Day },
                { _NightMeteringDeviceLabel, currentValues.Night },
                { _KitchenColdMeteringDeviceLabel, currentValues.KitchenCold },
                { _KitchenHotMeteringDeviceLabel, currentValues.KitchenHot },
                { _BathroomColdMeteringDeviceLabel, currentValues.BathroomCold },
                { _BathroomHotMeteringDeviceLabel, currentValues.BathroomHot }
            };
        }
    }
}
