using MeteringDevices.Core.Kzn.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MeteringDevices.Core.Kzn
{
    class SendService : ISendService
    {
        private readonly ISendApiService _SendApiService;
        private readonly string _Username;
        private readonly string _Password;
        
        public SendService(ISendApiService sendApiService, string username, string password)
        {
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
            IList<FlatModelDto> flatModels = _SendApiService.GetFlatInfo(securityToken);

            FlatModelDto flatModel = flatModels.Where(m => string.Equals(m.AccountNumber, accountNumber, StringComparison.Ordinal)).Single();

            IList<DeviceInfoDto> devicesInfo = GetDevicesInfo(securityToken, flatModel, values);

            SetValuesResponseDto setValuesResponse = _SendApiService.PutValues(securityToken, flatModel, devicesInfo);

            CheckResult(setValuesResponse.Result);
        }

        private IList<DeviceInfoDto> GetDevicesInfo(SecurityToken securityToken, FlatModelDto flatModel, IReadOnlyDictionary<string, int> values)
        {
            DeviceInfoResponseDto deviceInfoResponse = _SendApiService.GetDevicesInfo(securityToken, flatModel);

            CheckResult(deviceInfoResponse.Result);

            if (!deviceInfoResponse.Settings.InputAllowed)
            {
                throw new InvalidOperationException("Input is not allowed.");
            }

            IDictionary<string, DeviceInfoDto> dictionary = deviceInfoResponse.Counters.Devices.ToDictionary(d => d.UniqueId, StringComparer.Ordinal);

            if (dictionary.Count != values.Count)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The numbers of devices are different: {0} and {1}.", dictionary.Count, values.Count));
            }

            foreach (KeyValuePair<string, DeviceInfoDto> pair in dictionary)
            {
                pair.Value.Value = values[pair.Key];
            }

            return dictionary.Values.ToList();
        }
        
        private void CheckResult(ResponseResultDto result)
        {
            if (result == null)
            {
                throw new InvalidOperationException("No result was received.");
            }

            if (result.Code != 0)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Result code: {0}, Message: {1}, Details: {2}.", result.Code, result.Message, result.Details));
            }
        }
    }
}
