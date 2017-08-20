using Newtonsoft.Json;
using System.Collections.Generic;

namespace MeteringDevices.Core.Kzn.Dto
{
    class DeviceCountersDto
    {        
        [JsonProperty("CounterCurrentValueType")]
        public IReadOnlyList<DeviceInfoDto> Devices { get; set; }
    }
}
