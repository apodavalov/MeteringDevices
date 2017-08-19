using MeteringDevices.Core.Spb.Dto;

namespace MeteringDevices.Core.Spb
{
    interface IAccountsApiService
    {
        SecurityToken Login(string username, string password);

        AccountsDataDto GetAccounts(SecurityToken securityToken, string accountNumber);
    }
}
