using Newtonsoft.Json;

namespace MeteringDevices.Core.Spb.Dto
{
    class LoginDataDto
    {
        [JsonProperty("_token")]
        public string Token
        {
            get;
            set;
        }
    }
}
