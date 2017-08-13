using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteringDevices.Service
{
    class TelegramNotifier
    {
        private static readonly Uri _BaseUrl = new Uri("https://api.telegram.org/");

        private readonly RestClient _RestClient;

        public TelegramNotifier(string token)
        {
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            _RestClient = new RestClient();

            UriBuilder uriBuilder = new UriBuilder(_BaseUrl);
            uriBuilder.Path = token;

            _RestClient.BaseUrl = uriBuilder.Uri;   
        }

        public void SendMessage(long chatId, string message)
        {
            IRestRequest request = new RestRequest("sendMessage", Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("chat_id", chatId);
            request.AddParameter("text", message);

            IRestResponse<TelegramStatus> response = _RestClient.Execute<TelegramStatus>(request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            if (!response.Data.Ok)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "Failed to send message (telegram): error code {0}, description: {1} ", 
                        response.Data.ErrorCode, response.Data.Description
                    )
                );
            }

        }
    }

    class TelegramStatus
    {
        public bool Ok
        {
            get;
            set;
        }

        public int? ErrorCode
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public object Result
        {
            get;
            set;
        }
    }
}
