using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;

namespace MeteringDevices.Service
{
    public class UrlFormEncodedWriter : IDisposable
    {
        private const string _Format = "{0}={1}";
        private const string _Separator = "&";
        private const string _FormatWithSeparator = _Separator + _Format;
        private Stream _BaseStream;
        private bool _Disposed = false;
        private bool _First = true;
        private Encoding _Encoding;

        public UrlFormEncodedWriter(Stream baseStream) : this(baseStream, new UTF8Encoding(false))
        {

        }

        public UrlFormEncodedWriter(Stream baseStream, Encoding encoding)
        {
            if (baseStream == null)
            {
                throw new ArgumentNullException(nameof(baseStream));
            }

            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            _BaseStream = baseStream;
            _Encoding = encoding;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_Disposed)
            {
                return;
            }

            _Disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UrlFormEncodedWriter()
        {
            Dispose(false);
        }

        public void WriteParam(string name, string value)
        {
            if (_Disposed)
            {
                throw new ObjectDisposedException(nameof(UrlFormEncodedWriter));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            string format;

            if (_First)
            {
                format = _Format;
                _First = false;
            }
            else
            {
                format = _FormatWithSeparator;
            }

            byte[] buffer = _Encoding.GetBytes(string.Format(CultureInfo.InvariantCulture, format, HttpUtility.UrlEncode(name), HttpUtility.UrlEncode(value)));

            _BaseStream.Write(buffer, 0, buffer.Length);
        }

        public static string FormatDictionary(IDictionary<string, string> nameValueCollection)
        {
            StringBuilder stringBuilder = new StringBuilder();

            bool first = true;

            foreach (KeyValuePair<string,string> pair in nameValueCollection)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    stringBuilder.Append(_Separator);
                }

                stringBuilder.AppendFormat(CultureInfo.InvariantCulture, _Format, HttpUtility.UrlEncode(pair.Key), HttpUtility.UrlEncode(pair.Value));
            }

            return stringBuilder.ToString();
        }
    }
}
