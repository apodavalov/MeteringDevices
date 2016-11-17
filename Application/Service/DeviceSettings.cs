using Newtonsoft.Json;

namespace MeteringDevices.Service
{
    public class DeviceSettings
    {
        [JsonProperty("inputAllowed")]
        public bool InputAllowed { get; set; }
    }
}
