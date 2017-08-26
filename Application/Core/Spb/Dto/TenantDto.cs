using Newtonsoft.Json;

namespace MeteringDevices.Core.Spb.Dto
{
    class TenantDto
    {
        [JsonProperty("_extra")]
        public TenantExtraDto Extra
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

        [JsonProperty("number")]
        public string AccountNumber
        {
            get;
            set;
        }
    }
}
