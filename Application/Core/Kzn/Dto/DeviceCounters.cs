using Newtonsoft.Json;
using System.Collections.Generic;

namespace MeteringDevices.Core.Kzn.Dto
{
    class DeviceCounters
    {        
        [JsonProperty("CounterCurrentValueType")]
        public IList<DeviceInfo> Devices { get; set; }
    }
}
