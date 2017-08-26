using Newtonsoft.Json;

namespace MeteringDevices.Core.Spb.Dto
{
    class TenantExtraDto
    {
        [JsonProperty("provider")]
        public ProviderDto Provider
        {
            get;
            set;
        }
    }
}
