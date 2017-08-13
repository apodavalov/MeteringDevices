using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace MeteringDevices.Service.Kzn
{
    public class SenderService : ISendService
    {
        private const bool _Secured = true;

        private const string _UsernameKey = "Kzn.Service.Username";
        private const string _PasswordKey = "Kzn.Service.Password";
        private const string _HostnameKey = "Kzn.Service.Hostname";
        private const string _LoginPath = "api/users/sessions.json";
        private const string _TsjPathTemplate = "/api/v1/users/{0}/informers/tsj.json";
        private const string _GetCountersPath = "/api/v1/services/hcs/tsj/counters/get.json";
        private const string _SetCountersPath = "/api/v1/services/hcs/tsj/counters/set.json";
        private const string _SessionTokenValue = "session_token";
        private CookieContainer _Cookies = new CookieContainer();
        private UTF8Encoding _UTF8Encoding = new UTF8Encoding(false);
        private JsonSerializer _JsonSerializer = new JsonSerializer();
        private Random _Random = new Random();
        private string _SessionToken;
        private string _UserId;

        private Lazy<string> _Hostname;

        private string GetHostname()
        {
            return ConfigurationManager.AppSettings[_HostnameKey];
        }

        public SenderService()
        {
            Reset();
        }

        private void Reset()
        {
            _Hostname = new Lazy<string>(GetHostname);
            _Cookies.Add(new Cookie("device_view", "tablet", string.Empty, _Hostname.Value));
        }

        private void Login(string userName, string password)
        {
            if (userName == null)
            {
                throw new ArgumentNullException(nameof(userName));
            }

            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            HttpWebRequest httpWebRequest = HttpWebRequest.CreateHttp(BuildUri(_Secured, _Hostname.Value, _LoginPath));
            PopulateHeaders(httpWebRequest, true);

            PopulateLoginParams(userName, password, httpWebRequest);

            LoginResponse message = null;

            using (HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                CheckStatusOk(httpResponse);

                _Cookies.Add(httpResponse.Cookies);

                using (Stream httpStream = httpResponse.GetResponseStream())
                {
                    message = Deserialize<LoginResponse>(httpStream, httpResponse.ContentEncoding);
                }
            }

            _SessionToken = message.SessionToken;
            _UserId = message.UserId;
        }

        public void PutValues(IDictionary<string, int> values, string accountNumber)
        {
            try
            {
                Login(ConfigUtils.GetStringFromConfig(_UsernameKey), ConfigUtils.GetStringFromConfig(_PasswordKey));
                PutValuesInternal(values, accountNumber);
            }
            finally
            {
                _Cookies = new CookieContainer();
                Reset();
            }
        }

        private void PutValuesInternal(IDictionary<string, int> values, string accountNumber)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (accountNumber == null)
            {
                throw new ArgumentNullException(nameof(accountNumber));
            }

            FlatModel flat = GetFlatInfo(accountNumber);
            IList<DeviceInfo> devices = CollectDevicesInfo(values, flat);
            PutValues(flat, devices);
        }

        private void PutValues(FlatModel flat, IList<DeviceInfo> devices)
        {
            HttpWebRequest httpWebRequest = HttpWebRequest.CreateHttp(BuildUri(_Secured, _Hostname.Value, _SetCountersPath));
            PopulateHeaders(httpWebRequest, true);

            using (Stream stream = httpWebRequest.GetRequestStream())
            {
                using (UrlFormEncodedWriter writer = new UrlFormEncodedWriter(stream))
                {
                    writer.WriteParam(_SessionTokenValue, _SessionToken);
                    writer.WriteParam("infomat_number", "1007");
                    writer.WriteParam("data[personalAccountID][accountNumber]", flat.AccountNumber);
                    writer.WriteParam("data[personalAccountID][flatNumber]", flat.FlatNumber);
                    writer.WriteParam("data[personalAccountID][householderSurname]", flat.HouseHolderSurname);
                    writer.WriteParam("data[personalAccountID][accessSessionID]", _Random.Next(1, 10001).ToString(CultureInfo.InvariantCulture));

                    int i = 0;

                    foreach (DeviceInfo deviceInfo in devices)
                    {
                        writer.WriteParam(string.Format(CultureInfo.InvariantCulture, "data[inputCurrentValues][CounterInputCurrentValueType][{0}][valueID]", i), deviceInfo.ValueId);
                        writer.WriteParam(string.Format(CultureInfo.InvariantCulture, "data[inputCurrentValues][CounterInputCurrentValueType][{0}][entryValue]", i), deviceInfo.Value.ToString(CultureInfo.InvariantCulture));

                        i++;
                    }
                }
            }

            SetValuesResponse setValuesResponse = null;

            using (HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                CheckStatusOk(httpResponse);

                _Cookies.Add(httpResponse.Cookies);

                using (Stream httpStream = httpResponse.GetResponseStream())
                {
                    setValuesResponse = Deserialize<SetValuesResponse>(httpStream, httpResponse.ContentEncoding);
                }
            }

            CheckResult(setValuesResponse.Result);
        }

        private IList<DeviceInfo> CollectDevicesInfo(IDictionary<string, int> values, FlatModel flat)
        {
            HttpWebRequest httpWebRequest = HttpWebRequest.CreateHttp(BuildUri(_Secured, _Hostname.Value, _GetCountersPath));
            PopulateHeaders(httpWebRequest, true);

            using (Stream stream = httpWebRequest.GetRequestStream())
            {
                using (UrlFormEncodedWriter writer = new UrlFormEncodedWriter(stream))
                {
                    writer.WriteParam(_SessionTokenValue, _SessionToken);
                    writer.WriteParam("data[accountNumber]", flat.AccountNumber);
                    writer.WriteParam("data[flatNumber]", flat.FlatNumber);
                    writer.WriteParam("data[householderSurname]", flat.HouseHolderSurname);
                    writer.WriteParam("data[accessSessionID]", _Random.Next(1, 10001).ToString(CultureInfo.InvariantCulture));
                }
            }

            DeviceInfoResponse deviceInfoResponse = null;

            using (HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                CheckStatusOk(httpResponse);

                _Cookies.Add(httpResponse.Cookies);

                using (Stream httpStream = httpResponse.GetResponseStream())
                {
                    deviceInfoResponse = Deserialize<DeviceInfoResponse>(httpStream, httpResponse.ContentEncoding);
                }
            }

            CheckResult(deviceInfoResponse.Result);

            if (!deviceInfoResponse.Settings.InputAllowed)
            {
                throw new InvalidOperationException("Input is not allowed.");
            }

            IDictionary<string, DeviceInfo> dictionary = deviceInfoResponse.Counters.Devices.ToDictionary(d => d.UniqueId, StringComparer.Ordinal);

            if (dictionary.Count != values.Count)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture,"The numbers of devices are different: {0} and {1}.", dictionary.Count, values.Count));
            }

            foreach (KeyValuePair<string,DeviceInfo> pair in dictionary)
            {
                pair.Value.Value = values[pair.Key];
            }

            return dictionary.Values.ToList();
        }

        private void CheckResult(ResponseResult result)
        {
            if (result.Code != 0)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture,"Result code: {0}, Message: {1}, Details: {2}.", result.Code, result.Message, result.Details));
            }
        }

        private FlatModel GetFlatInfo(string accountNumber)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.Ordinal)  { { _SessionTokenValue, _SessionToken } };

            HttpWebRequest httpWebRequest = HttpWebRequest.CreateHttp(BuildUri(_Secured, _Hostname.Value, string.Format(CultureInfo.InvariantCulture,_TsjPathTemplate, _UserId), dictionary));
            PopulateHeaders(httpWebRequest, false);

            IList<FlatModel> message = null;

            using (HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                CheckStatusOk(httpResponse);

                _Cookies.Add(httpResponse.Cookies);

                using (Stream httpStream = httpResponse.GetResponseStream())
                {
                    message = Deserialize<IList<FlatModel>>(httpStream, httpResponse.ContentEncoding);
                }
            }

            return message.Where(m => string.CompareOrdinal(m.AccountNumber, accountNumber) == 0).Single();
        }

        private static void CheckStatusOk(HttpWebResponse httpWebResponse)
        {
            int status = (int)httpWebResponse.StatusCode;

            if (status / 100 != 2)
            {
                throw new HttpException(status, httpWebResponse.StatusDescription);
            }
        }

        private T Deserialize<T>(Stream httpStream, string contentEncoding)
        {
            if (string.Equals(contentEncoding, "gzip", StringComparison.OrdinalIgnoreCase))
            {
                httpStream = new GZipStream(httpStream, CompressionMode.Decompress);
            }
            else if (string.Equals(contentEncoding, "deflate", StringComparison.OrdinalIgnoreCase))
            {
                httpStream = new DeflateStream(httpStream, CompressionMode.Decompress);
            }

            using (StreamReader streamReader = new StreamReader(httpStream, _UTF8Encoding))
            {
                using (JsonReader jsonReader = new JsonTextReader(streamReader))
                {
                    return _JsonSerializer.Deserialize<T>(jsonReader);
                }
            }
        }

        private void PopulateLoginParams(string userName, string password, HttpWebRequest httpWebRequest)
        {
            using (Stream stream = httpWebRequest.GetRequestStream())
            {
                using (UrlFormEncodedWriter writer = new UrlFormEncodedWriter(stream))
                {
                    writer.WriteParam("ip", "10.7.4.21");
                    writer.WriteParam("infomat_number", "1007");
                    writer.WriteParam("password", password);
                    writer.WriteParam("username", userName);
                }
            }
        }

        private void PopulateHeaders(HttpWebRequest httpWebRequest, bool isPost)
        {
            httpWebRequest.UserAgent = "Mozilla/5.0 (Linux; Android 5.1.1; GT-P5210 Build/LMY48Z) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.130 Crosswalk/14.43.343.25 Safari/537.36";
            httpWebRequest.Accept = "*/*";
            httpWebRequest.Headers["Accept-Language"] = "en-us,en";
            httpWebRequest.Headers["Accept-Encoding"] = "gzip, deflate";

            if (isPost)
            {
                httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                httpWebRequest.Headers["Origin"] = "file://";
                httpWebRequest.Method = "POST";
            }
            else
            {
                httpWebRequest.Method = "GET";
            }

            httpWebRequest.KeepAlive = true;
            httpWebRequest.CookieContainer = _Cookies;            
            httpWebRequest.Timeout = 30000;
            httpWebRequest.ServicePoint.Expect100Continue = false;
        }

        private static string GetSchema(bool secured)
        {
            return secured ? "https" : "http";
        }

        private static Uri BuildUri(bool secured, string hostName, string loginPath, IDictionary<string,string> query = null)
        {
            UriBuilder builder = new UriBuilder();

            builder.Host = hostName;
            builder.Scheme = GetSchema(secured);
            builder.Path = loginPath;

            if (query != null && query.Count > 0)
            {
                builder.Query = UrlFormEncodedWriter.FormatDictionary(query);
            }

            return builder.Uri;
        }
    }
}
