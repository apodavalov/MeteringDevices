using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace MeteringDevices.Core.RestSharp
{
    interface IJsonSerializerFactory
    {
        ISerializer CreateSerializer();

        IDeserializer CreateDeserializer();
    }
}
