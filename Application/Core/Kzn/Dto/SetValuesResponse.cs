using Newtonsoft.Json;

namespace MeteringDevices.Core.Kzn.Dto
{
    class SetValuesResponse
    {
        [JsonProperty("result")]
        public ResponseResult Result { get; set; }
    }
}
