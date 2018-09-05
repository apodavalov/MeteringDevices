﻿using MeteringDevices.Core.RestSharp;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace MeteringDevices.Core
{
    class TelegramSender : IIMSender
    {
        private readonly IRestClient _RestClient;
        private readonly long _ChatId;
        private readonly IRestSharpFactory _RestSharpFactory;

        public TelegramSender(string baseUrl, string token, long chatId, IRestSharpFactory restSharpFactory, Uri proxyUri)
        {
            if (baseUrl == null)
            {
                throw new ArgumentNullException(nameof(baseUrl));
            }

            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            if (restSharpFactory == null)
            {
                throw new ArgumentNullException(nameof(restSharpFactory));
            }

            _RestClient = restSharpFactory.CreateRestClient();

            UriBuilder uriBuilder = new UriBuilder(baseUrl);
            uriBuilder.Path = token;

            _RestClient.BaseUrl = uriBuilder.Uri;
            _ChatId = chatId;
            _RestSharpFactory = restSharpFactory;
        }

        public void Send(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            IRestRequest request = _RestSharpFactory.CreateRestRequest("sendMessage", Method.POST);
            
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
            request.AddParameter("chat_id", _ChatId);
            request.AddParameter("text", message);
            
            IRestResponse<TelegramStatus> response = _RestClient.Execute<TelegramStatus>(request);
           
            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            if (!response.Data.Success)
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
        [JsonProperty("ok")]
        public bool Success
        {
            get;
            set;
        }

        [JsonProperty("error_code")]
        public int? ErrorCode
        {
            get;
            set;
        }

        [JsonProperty("description")]
        public string Description
        {
            get;
            set;
        }
    }
}