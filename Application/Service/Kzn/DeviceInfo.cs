using Newtonsoft.Json;

namespace MeteringDevices.Service.Kzn
{
    public class DeviceInfo
    {
        [JsonProperty("valueID")]
        public string ValueId { get; set; }

        [JsonProperty("worksNumber")]
        public string UniqueId { get; set; }

        [JsonIgnore]
        public int Value { get; set; }
    }
}
