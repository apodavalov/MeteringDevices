using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace MeteringDevices.Core.RestSharp
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
