using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace MeteringDevices.Service.RestSharp
{
    class JsonSerializerFactory : IJsonSerializerFactory
    {
        public IDeserializer CreateDeserializer()
        {
            return new NewtonsoftJsonDeserializer();
        }

        public ISerializer CreateSerializer()
        {
            return new NewtonsoftJsonSerializer();
        }
    }
}
