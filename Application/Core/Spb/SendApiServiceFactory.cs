using MeteringDevices.Core.RestSharp;
using System;

namespace MeteringDevices.Core.Spb
{
    class SendApiServiceFactory : ISendApiServiceFactory
    {
        public readonly IRestSharpFactory _RestSharpFactory;

        public SendApiServiceFactory(IRestSharpFactory restSharpFactory)
        {
            if (restSharpFactory == null)
            {
                throw new ArgumentNullException(nameof(restSharpFactory));
            }

            _RestSharpFactory = restSharpFactory;
        }

        public ISendApiService GetService(Uri baseUri)
        {
            return new SendApiService(_RestSharpFactory, baseUri);
        }
    }
}
