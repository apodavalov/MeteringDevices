using Newtonsoft.Json;
using System;

namespace MeteringDevices.Core.Spb.Dto
{
    class LoginRequestDto
    {
        [JsonProperty("login")]
        public string Login
        {
            get;
            set;
        }

        [JsonProperty("password")]
        public string Password
        {
            get;
            set;
        }
    }
}
