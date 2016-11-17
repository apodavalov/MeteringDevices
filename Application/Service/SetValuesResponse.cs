using Newtonsoft.Json;

namespace MeteringDevices.Service
{
    public class SetValuesResponse
    {
        [JsonProperty("result")]
        public ResponseResult Result { get; set; }
    }
}
