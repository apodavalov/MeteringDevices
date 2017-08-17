using Newtonsoft.Json;

namespace MeteringDevices.Core.Kzn.Dto
{
    class LoginResponseDto
    {
        [JsonProperty("session_token")]
        public string SessionToken { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; } 
    }
}
