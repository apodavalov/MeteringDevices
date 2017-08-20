using MeteringDevices.Core.Spb.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeteringDevices.Core.Spb
{
    class SendService : ISendService
    {
        private readonly IAccountsApiService _SendApiService;
        private readonly ISendApiServiceFactory _SendApiServiceFactory;
        private readonly string _Username;
        private readonly string _Password;
        
        public SendService(ISendApiServiceFactory sendApiServiceFactory, IAccountsApiService sendApiService, 
            string username, string password)
        {
            if (sendApiServiceFactory == null)
            {
                throw new ArgumentNullException(nameof(sendApiServiceFactory));
            }

            if (sendApiService == null)
            {
                throw new ArgumentNullException(nameof(sendApiService));
            }

            if (username == null)
            {
                throw new ArgumentNullException(nameof(username));
            }

            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            _SendApiServiceFactory = sendApiServiceFactory;
            _SendApiService = sendApiService;
            _Username = username;
            _Password = password; 
        }

        public void PutValues(string accountNumber, IReadOnlyDictionary<string, int> values)
        {
            if (accountNumber == null)
            {
                throw new ArgumentNullException(nameof(accountNumber));
            }

            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            SecurityToken securityToken = _SendApiService.Login(_Username, _Password);
            AccountsDataDto data = _SendApiService.GetAccounts(securityToken, accountNumber);

            string url = GetUrl(data, accountNumber);

            if (url == null)
            {
                throw new InvalidOperationException("Url is null.");
            }

            Uri baseUri = new UriBuilder("https", url).Uri;

            ISendApiService sendApiService = _SendApiServiceFactory.GetService(baseUri);

            IDictionary<string, IReadOnlyList<int>> convertedValues = ConvertedValues(values);

            foreach (KeyValuePair<string, IReadOnlyList<int>> value in convertedValues)
            {
                sendApiService.SetValues(securityToken.Token, value.Key, value.Value);
            }
        }

        private IDictionary<string, IReadOnlyList<int>> ConvertedValues(IReadOnlyDictionary<string, int> values)
        {
            IDictionary<string, List<Tuple<string,int>>> dictionary = new Dictionary<string, List<Tuple<string, int>>>();

            foreach (KeyValuePair<string, int> keyValue in values)
            {
                string[] tokens = keyValue.Key.Split(new char[] { '_' }, 2, StringSplitOptions.None);
                string meteringDeviceId = tokens[0];

                List<Tuple<string, int>> meteringDeviceValues;  

                if (dictionary.ContainsKey(meteringDeviceId))
                {
                    meteringDeviceValues = dictionary[meteringDeviceId];
                }
                else
                {
                    meteringDeviceValues = dictionary[meteringDeviceId] = new List<Tuple<string, int>>();
                }

                meteringDeviceValues.Add(new Tuple<string, int>(tokens.Length >= 2 ? tokens[1] : null, keyValue.Value));
            }

            IDictionary<string, IReadOnlyList<int>> result = new Dictionary<string, IReadOnlyList<int>>();

            foreach (KeyValuePair<string, List<Tuple<string,int>>> keyValue in dictionary)
            {
                IReadOnlyList<int> sortedValues = keyValue.Value.OrderBy(value => value.Item1).Select(value => value.Item2).ToList().AsReadOnly();
                result.Add(keyValue.Key, sortedValues);
            }

            return result;
        }

        private string GetUrl(AccountsDataDto data, string accountNumber)
        {
            if (data?.Tenants == null)
            {
                return null;
            }

            foreach (TenantDto tenantDto in data.Tenants)
            {
                if (string.Equals(tenantDto?.Area?.Id, accountNumber, StringComparison.Ordinal))
                {
                    return tenantDto?.Extra?.Provider?.Url;
                }
            }

            return null;
        }
    }
}
