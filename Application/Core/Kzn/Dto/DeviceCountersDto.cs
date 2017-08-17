using Newtonsoft.Json;
using System.Collections.Generic;

namespace MeteringDevices.Core.Kzn.Dto
{
    class DeviceCountersDto
    {        
        [JsonProperty("CounterCurrentValueType")]
        public IList<DeviceInfoDto> Devices { get; set; }
    }
}
