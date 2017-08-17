using Newtonsoft.Json;

namespace MeteringDevices.Core.Kzn.Dto
{
    class DeviceInfoResponseDto
    {
        [JsonProperty("settings")]
        public DeviceSettingsDto Settings { get; set; }

        [JsonProperty("counters")]
        public DeviceCountersDto Counters { get; set; }

        [JsonProperty("result")]
        public ResponseResultDto Result { get; set; }
    }
}
