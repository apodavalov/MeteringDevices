using Newtonsoft.Json;

namespace MeteringDevices.Core.Kzn.Dto
{
    class SetValuesResponseDto
    {
        [JsonProperty("result")]
        public ResponseResultDto Result { get; set; }
    }
}
