using Newtonsoft.Json;

namespace MeteringDevices.Service.Kzn
{
    public class DeviceSettings
    {
        [JsonProperty("inputAllowed")]
        public bool InputAllowed { get; set; }
    }
}
