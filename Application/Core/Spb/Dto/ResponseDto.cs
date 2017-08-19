using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace MeteringDevices.Core.Spb.Dto
{
    class ResponseDto<T> where T : class
    {
        [JsonProperty("data")]
        public T Data
        {
            get;
            set;
        }

        [JsonProperty("messages")]
        public IReadOnlyList<MessageDto> Messages
        {
            get;
            set;
        }

        [JsonProperty("status")]
        public int Status
        {
            get;
            set;
        }
    }
}
