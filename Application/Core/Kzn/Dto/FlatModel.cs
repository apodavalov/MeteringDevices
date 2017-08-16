using Newtonsoft.Json;

namespace MeteringDevices.Core.Kzn.Dto
{
    class FlatModel
    {
        [JsonProperty("accountNumber")]
        public string AccountNumber { get; set; }

        [JsonProperty("flatNumber")]
        public string FlatNumber { get; set; }

        [JsonProperty("houseHolderSurname")]
        public string HouseHolderSurname { get; set; }
    }
}
