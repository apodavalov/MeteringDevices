using Newtonsoft.Json;

namespace MeteringDevices.Core.Kzn.Dto
{
    class ResponseResultDto
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
