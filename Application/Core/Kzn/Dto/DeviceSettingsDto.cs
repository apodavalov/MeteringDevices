using Newtonsoft.Json;

namespace MeteringDevices.Core.Kzn.Dto
{
    class DeviceSettingsDto
    {
        [JsonProperty("inputAllowed")]
        public bool InputAllowed { get; set; }
    }
}
