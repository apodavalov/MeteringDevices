using Newtonsoft.Json;

namespace MeteringDevices.Core.Spb.Dto
{
    class MeteringDeviceInfoExtraDto
    {
        [JsonProperty("_id")]
        public string Id
        {
            get;
            set;
        }

        [JsonProperty("serial_number")]
        public string SerialNumber
        {
            get;
            set;
        }
    }
}
