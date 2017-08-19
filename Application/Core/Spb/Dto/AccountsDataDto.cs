using Newtonsoft.Json;
using System.Collections.Generic;

namespace MeteringDevices.Core.Spb.Dto
{
    class AccountsDataDto
    {
        [JsonProperty("tenants")]
        public IReadOnlyList<TenantDto> Tenants
        {
            get;
            set;
        }
    }
}
