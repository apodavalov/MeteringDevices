using Newtonsoft.Json;

namespace MeteringDevices.Core.Spb.Dto
{
    class MessageDto
    {
        [JsonProperty("text")]
        public string Text
        {
            get;
            set;
        }

        [JsonProperty("type")]
        public string Type
        {
            get;
            set;
        }
    }
}
