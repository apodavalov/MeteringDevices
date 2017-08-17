using Newtonsoft.Json;

namespace MeteringDevices.Core.Kzn.Dto
{
    class DeviceInfoDto
    {
        [JsonProperty("valueID")]
        public string ValueId { get; set; }

        [JsonProperty("worksNumber")]
        public string UniqueId { get; set; }

        [JsonIgnore]
        public int Value { get; set; }
    }
}
