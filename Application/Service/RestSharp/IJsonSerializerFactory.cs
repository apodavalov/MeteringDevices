using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace MeteringDevices.Service.RestSharp
{
    interface IJsonSerializerFactory
    {
        ISerializer CreateSerializer();

        IDeserializer CreateDeserializer();
    }
}
