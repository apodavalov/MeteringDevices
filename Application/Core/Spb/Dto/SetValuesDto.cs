using Newtonsoft.Json;
using System.Collections.Generic;

namespace MeteringDevices.Core.Spb.Dto
{
    class SetValuesDto
    {
        [JsonProperty("values")]
        public IReadOnlyList<int> Values
        {
            get;
            set;
        }

        [JsonProperty("_token")]
        public string Token
        {
            get;
            set;
        }

        [JsonProperty("created_by")]
        public string CreatedBy
        {
            get;
            set;
        }
    }
}
