using MeteringDevices.Core.RestSharp;
using MeteringDevices.Core.Spb.Dto;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MeteringDevices.Core.Spb
{
    class SendApiService : ISendApiService
    {
        private IRestSharpFactory _RestSharpFactory;
        private IRestClient _RestClient;

        public SendApiService(IRestSharpFactory restSharpFactory, Uri baseUri)
        {
            if (restSharpFactory == null)
            {
                throw new ArgumentNullException(nameof(restSharpFactory));
            }

            if (baseUri == null)
            {
                throw new ArgumentNullException(nameof(baseUri));
            }

            _RestSharpFactory = restSharpFactory;
            _RestClient = _RestSharpFactory.CreateRestClient();
            _RestClient.BaseUrl = baseUri;
        }

        public void SetValues(string token, string meteringDeviceId, IReadOnlyList<int> values)
        {
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            if (meteringDeviceId == null)
            {
                throw new ArgumentNullException(nameof(meteringDeviceId));
            }

            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            IRestRequest restRequest = _RestSharpFactory.CreateRestRequest(
                string.Format(CultureInfo.InvariantCulture, "api/v2/meters/green_table_readings/{0}/set_values", meteringDeviceId), 
                Method.POST
            );

            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddQueryParameter("_method", "PATCH");

            SetValuesDto setValuesDto = new SetValuesDto { CreatedBy = "tenant", Values = values, Token = token };
            restRequest.AddJsonBody(setValuesDto);

            IRestResponse<ResponseDto<object>> restResponse = _RestClient.Execute<ResponseDto<object>>(restRequest);

            restResponse.CheckSuccess();
        }
    }
}
