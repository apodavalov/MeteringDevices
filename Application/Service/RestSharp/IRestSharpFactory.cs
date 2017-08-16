using RestSharp;

namespace MeteringDevices.Service.RestSharp
{
    interface IRestSharpFactory
    {
        IRestClient CreateRestClient();

        IRestRequest CreateRestRequest(string resource, Method method);
    }
}
