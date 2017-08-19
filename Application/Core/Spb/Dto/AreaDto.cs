using Newtonsoft.Json;

namespace MeteringDevices.Core.Spb.Dto
{
    class AreaDto
    {
        [JsonProperty("_id")]
        public string Id
        {
            get;
            set;
        }
    }
}
