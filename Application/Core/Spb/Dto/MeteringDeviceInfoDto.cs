using Newtonsoft.Json;

namespace MeteringDevices.Core.Spb.Dto
{
    class MeteringDeviceInfoDto
    {
        [JsonProperty("_extra")]
        public MeteringDeviceInfoExtraDto Extra
        {
            get;
            set;
        }
    }
}
