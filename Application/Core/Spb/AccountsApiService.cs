using MeteringDevices.Core.RestSharp;
using MeteringDevices.Core.Spb.Dto;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeteringDevices.Core.Spb
{
    class AccountsApiService : IAccountsApiService
    {
        private readonly IRestSharpFactory _RestSharpFactory;
        private readonly IRestClient _RestClient;

        public AccountsApiService(IRestSharpFactory restSharpFactory, string baseUrl)
        {
            if (restSharpFactory == null)
            {
                throw new ArgumentNullException(nameof(restSharpFactory));
            }

            if (baseUrl == null)
            {
                throw new ArgumentNullException(nameof(baseUrl));
            }

            _RestSharpFactory = restSharpFactory;
            _RestClient = restSharpFactory.CreateRestClient();
            _RestClient.BaseUrl = new Uri(baseUrl);
        }

        public SecurityToken Login(string username, string password)
        {
            IRestRequest restRequest = _RestSharpFactory.CreateRestRequest("api/v2/auth/login", Method.POST);

            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddJsonBody(new LoginRequestDto { Login = username, Password = password });

            IRestResponse<ResponseDto<LoginDataDto>> restResponse = _RestClient.Execute<ResponseDto<LoginDataDto>>(restRequest);

            restResponse.CheckSuccess();

            string token = restResponse.Data?.Data?.Token;

            if (token == null)
            {
                throw new InvalidOperationException("Expected to receive a token.");
            }

            return new SecurityToken(token, restResponse.Cookies);
        }

     

        public AccountsDataDto GetAccounts(SecurityToken securityToken, string accountNumber)
        {
            IRestRequest restRequest = _RestSharpFactory.CreateRestRequest("api/v2/auth/accounts", Method.GET);
            restRequest.AddQueryParameter("_token", securityToken.Token);

            foreach (var cookie in securityToken.Cookies)
            {
                restRequest.AddCookie(cookie.Name, cookie.Value);
            }

            IRestResponse<ResponseDto<AccountsDataDto>> restResponse =
                _RestClient.Execute<ResponseDto<AccountsDataDto>>(restRequest);

            restResponse.CheckSuccess();

            return restResponse.Data.Data;
        }
    }
}
