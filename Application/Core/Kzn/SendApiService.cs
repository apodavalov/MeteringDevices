using MeteringDevices.Core.Kzn.Dto;
using MeteringDevices.Core.RestSharp;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MeteringDevices.Core.Kzn
{
    class SendApiService : ISendApiService
    {
        private readonly IRestSharpFactory _RestSharpFactory;
        private readonly IRestClient _RestClient;
        private readonly Random _Random;

        public SendApiService(IRestSharpFactory restSharpFactory, string baseUrl)
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
            _RestClient = _RestSharpFactory.CreateRestClient();
            _RestClient.BaseUrl = new Uri(baseUrl);
            _Random = new Random();
        }

        public SecurityToken Login(string username, string password)
        {
            if (username == null)
            {
                throw new ArgumentNullException(nameof(username));
            }

            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            IRestRequest restRequest = _RestSharpFactory.CreateRestRequest("api/users/sessions.json", Method.POST);

            restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
            restRequest.AddParameter("password", password);
            restRequest.AddParameter("username", username);

            IRestResponse<LoginResponseDto> restResponse = _RestClient.Execute<LoginResponseDto>(restRequest);
            CheckSuccess(restResponse);

            if (restResponse.Data.SessionToken == null)
            {
                throw new InvalidOperationException("Session token is null.");
            }

            if (restResponse.Data.UserId == null)
            {
                throw new InvalidOperationException("UserId is null.");
            }

            return new SecurityToken(restResponse.Data.UserId, restResponse.Data.SessionToken);
        }

        public IList<FlatModelDto> GetFlatInfo(SecurityToken securityToken)
        {
            if (securityToken == null)
            {
                throw new ArgumentNullException(nameof(securityToken));
            }

            IRestRequest restRequest = _RestSharpFactory.CreateRestRequest(
                string.Format(CultureInfo.InvariantCulture, "/api/v1/users/{0}/informers/tsj.json", securityToken.UserId), 
                Method.GET);

            restRequest.AddQueryParameter("session_token", securityToken.SessionToken);
            
            IRestResponse<List<FlatModelDto>> restResponse = _RestClient.Execute<List<FlatModelDto>>(restRequest);

            CheckSuccess(restResponse);

            return restResponse.Data;
        }

        public DeviceInfoResponseDto GetDevicesInfo(SecurityToken securityToken, FlatModelDto flatModel)
        {
            if (securityToken == null)
            {
                throw new ArgumentNullException(nameof(securityToken));
            }

            if (flatModel == null)
            {
                throw new ArgumentNullException(nameof(flatModel));
            }

            IRestRequest restRequest = _RestSharpFactory.CreateRestRequest(
                "api/v1/services/hcs/tsj/counters/get.json",
                Method.POST);

            restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
            restRequest.AddParameter("session_token", securityToken.SessionToken);
            restRequest.AddParameter("data[accountNumber]", flatModel.AccountNumber);
            restRequest.AddParameter("data[flatNumber]", flatModel.FlatNumber);
            restRequest.AddParameter("data[householderSurname]", flatModel.HouseHolderSurname);
            restRequest.AddParameter("data[accessSessionID]", _Random.Next(1, 10001).ToString(CultureInfo.InvariantCulture));

            IRestResponse<DeviceInfoResponseDto> restResponse = _RestClient.Execute<DeviceInfoResponseDto>(restRequest);

            CheckSuccess(restResponse);

            return restResponse.Data;
        }

        public SetValuesResponseDto PutValues(SecurityToken securityToken, FlatModelDto flatModel, IList<DeviceInfoDto> devicesInfo)
        {
            if (securityToken == null)
            {
                throw new ArgumentNullException(nameof(securityToken));
            }

            if (flatModel == null)
            {
                throw new ArgumentNullException(nameof(flatModel));
            }

            if (devicesInfo == null)
            {
                throw new ArgumentNullException(nameof(devicesInfo));
            }

            IRestRequest restRequest = _RestSharpFactory.CreateRestRequest(
                 "api/v1/services/hcs/tsj/counters/set.json",
                Method.POST);

            restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
            restRequest.AddParameter("session_token", securityToken.SessionToken);

            restRequest.AddParameter("infomat_number", "1007");
            restRequest.AddParameter("data[personalAccountID][accountNumber]", flatModel.AccountNumber);
            restRequest.AddParameter("data[personalAccountID][flatNumber]", flatModel.FlatNumber);
            restRequest.AddParameter("data[personalAccountID][householderSurname]", flatModel.HouseHolderSurname);
            restRequest.AddParameter("data[personalAccountID][accessSessionID]", _Random.Next(1, 10001).ToString(CultureInfo.InvariantCulture));

            foreach (Tuple<DeviceInfoDto, int> deviceInfo in devicesInfo.Select((deviceInfo, i) => new Tuple<DeviceInfoDto, int>(deviceInfo, i)))
            {
                restRequest.AddParameter(
                    string.Format(CultureInfo.InvariantCulture, "data[inputCurrentValues][CounterInputCurrentValueType][{0}][valueID]", deviceInfo.Item2), 
                    deviceInfo.Item1.ValueId
                );
                restRequest.AddParameter(
                    string.Format(CultureInfo.InvariantCulture, "data[inputCurrentValues][CounterInputCurrentValueType][{0}][entryValue]", deviceInfo.Item2), 
                    deviceInfo.Item1.Value.ToString(CultureInfo.InvariantCulture)
                );
            }

            IRestResponse<SetValuesResponseDto> restResponse = _RestClient.Execute<SetValuesResponseDto>(restRequest);

            CheckSuccess(restResponse);

            return restResponse.Data;
        }

        private static void CheckSuccess<T>(IRestResponse<T> restResponse) where T : class
        {
            if (restResponse.ErrorException != null)
            {
                throw restResponse.ErrorException;
            }

            if (restResponse.Data == null)
            {
                throw new InvalidOperationException("No data was read.");
            }
        }
    }
}
