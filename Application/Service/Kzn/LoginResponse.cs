using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
