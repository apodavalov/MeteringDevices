using Newtonsoft.Json;

namespace MeteringDevices.Core.Spb.Dto
{
    class ProviderDto
    {
        [JsonProperty("url")]
        public string Url
        {
            get;
            set;
        }
    }
}
