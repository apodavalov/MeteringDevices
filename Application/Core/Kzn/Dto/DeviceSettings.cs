using Newtonsoft.Json;

namespace MeteringDevices.Core.Kzn.Dto
{
    class DeviceSettings
    {
        [JsonProperty("inputAllowed")]
        public bool InputAllowed { get; set; }
    }
}
