using RestSharp;
using System;

namespace MeteringDevices.Core.RestSharp
{
    interface IRestSharpFactory
    {
        IRestClient CreateRestClient(Uri proxyUri = null);

        IRestRequest CreateRestRequest(string resource, Method method);
    }
}
