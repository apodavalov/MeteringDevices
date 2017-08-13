using Newtonsoft.Json;
using System.Collections.Generic;

namespace MeteringDevices.Service.Kzn
{
    public class DeviceCounters
    {        
        [JsonProperty("CounterCurrentValueType")]
        public IList<DeviceInfo> Devices { get; set; }
    }
}
