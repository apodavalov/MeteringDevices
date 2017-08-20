using MeteringDevices.Core.Kzn.Dto;
using System.Collections.Generic;

namespace MeteringDevices.Core.Kzn
{
    interface ISendApiService
    {
        SecurityToken Login(string username, string password);

        IList<FlatModelDto> GetFlatInfo(SecurityToken securityToken);

        DeviceInfoResponseDto GetDevicesInfo(SecurityToken securityToken, FlatModelDto flatModel);

        SetValuesResponseDto PutValues(SecurityToken securityToken, FlatModelDto flatModel, IList<DeviceInfoDto> devicesInfo);
    }
}
