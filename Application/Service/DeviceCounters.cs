using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteringDevices.Service
{
    public class DeviceCounters
    {        
        [JsonProperty("CounterCurrentValueType")]
        public IList<DeviceInfo> Devices { get; set; }
    }
}
