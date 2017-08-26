using MeteringDevices.Core.Spb.Dto;
using System.Collections.Generic;

namespace MeteringDevices.Core.Spb
{
    interface ISendApiService
    {
        IList<MeteringDeviceInfoDto> GetDevicesInfo(string token, string accountId);

        void SetValues(string token, string meteringDeviceId, IReadOnlyList<int> values);
    }
}
