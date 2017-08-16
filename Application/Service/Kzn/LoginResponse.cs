using Newtonsoft.Json;

namespace MeteringDevices.Service.Kzn
{
    public class LoginResponse
    {
        [JsonProperty("session_token")]
        public string SessionToken { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; } 
    }
}
