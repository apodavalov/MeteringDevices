using RestSharp;

namespace MeteringDevices.Core.RestSharp
{
    interface IRestSharpFactory
    {
        IRestClient CreateRestClient();

        IRestRequest CreateRestRequest(string resource, Method method);
    }
}
