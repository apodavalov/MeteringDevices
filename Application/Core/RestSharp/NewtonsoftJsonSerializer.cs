using Newtonsoft.Json;
using RestSharp.Serializers;
using System.IO;

namespace MeteringDevices.Core.RestSharp
{
    class NewtonsoftJsonSerializer : ISerializer
    {
        private readonly Newtonsoft.Json.JsonSerializer _Serializer;

        public string ContentType
        {
            get;
            set;
        }

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

        public NewtonsoftJsonSerializer()
        {
            ContentType = "application/json";
            _Serializer = new Newtonsoft.Json.JsonSerializer
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Include,
                DefaultValueHandling = DefaultValueHandling.Include
            };
        }

        public NewtonsoftJsonSerializer(Newtonsoft.Json.JsonSerializer serializer)
        {
            ContentType = "application/json";
            _Serializer = serializer;
        }

        public string Serialize(object obj)
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    jsonTextWriter.Formatting = Formatting.None;
                    jsonTextWriter.QuoteChar = '"';

                    _Serializer.Serialize(jsonTextWriter, obj);

                    var result = stringWriter.ToString();
                    return result;
                }
            }
        }
    }
}
