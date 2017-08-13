using Newtonsoft.Json;

namespace MeteringDevices.Service.Kzn
{
    public class SetValuesResponse
    {
        [JsonProperty("result")]
        public ResponseResult Result { get; set; }
    }
}
