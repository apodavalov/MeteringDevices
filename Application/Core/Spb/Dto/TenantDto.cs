using Newtonsoft.Json;

namespace MeteringDevices.Core.Spb.Dto
{
    class TenantDto
    {
        [JsonProperty("_extra")]
        public ExtraDto Extra
        {
            get;
            set;
        }

        [JsonProperty("area")]
        public AreaDto Area
        {
            get;
            set;
        }
    }
}
