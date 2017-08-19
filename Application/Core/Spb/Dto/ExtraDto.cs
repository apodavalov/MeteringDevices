using Newtonsoft.Json;

namespace MeteringDevices.Core.Spb.Dto
{
    class ExtraDto
    {
        [JsonProperty("provider")]
        public ProviderDto Provider
        {
            get;
            set;
        }
    }
}
