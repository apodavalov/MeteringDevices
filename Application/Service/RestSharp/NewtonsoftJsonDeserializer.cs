using RestSharp.Deserializers;
using RestSharp;
using System.IO;
using Newtonsoft.Json;

namespace MeteringDevices.Service.RestSharp
{
    class NewtonsoftJsonDeserializer : IDeserializer
    {
        private readonly Newtonsoft.Json.JsonSerializer _Serializer;

        public string DateFormat
        {
            get;
            set;
        }

        public string Namespace
        {
            get;
            set;
        }

        public string RootElement
        {
            get;
            set;
        }

        public NewtonsoftJsonDeserializer()
        {
            _Serializer = new Newtonsoft.Json.JsonSerializer();
        }

        public NewtonsoftJsonDeserializer(Newtonsoft.Json.JsonSerializer serializer)
        {
            _Serializer = serializer;
        }

        public T Deserialize<T>(IRestResponse response)
        {
            using (StringReader stringReader = new StringReader(response.Content))
            {
                using (JsonTextReader jsonTextReader = new JsonTextReader(stringReader))
                {
                    return _Serializer.Deserialize<T>(jsonTextReader);
                }
            }
        }
    }
}
