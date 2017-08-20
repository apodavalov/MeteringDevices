using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.IO;

namespace MeteringDevices.Core.RestSharp
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

        public T Deserialize<T>(IRestResponse response)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

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
