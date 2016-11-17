using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteringDevices.Service
{
    public class DeviceInfo
    {
        [JsonProperty("valueID")]
        public string ValueId { get; set; }

        [JsonProperty("worksNumber")]
        public string UniqueId { get; set; }

        [JsonIgnore]
        public int Value { get; set; }

        public DeviceInfo SetValue(int value)
        {
            Value = value;

            return this;
        }
    }
}
