using Newtonsoft.Json;

namespace MeteringDevices.Service.Kzn
{
    public class DeviceInfoResponse
    {
        [JsonProperty("settings")]
        public DeviceSettings Settings { get; set; }

        [JsonProperty("counters")]
        public DeviceCounters Counters { get; set; }

        [JsonProperty("result")]
        public ResponseResult Result { get; set; }
    }
}
