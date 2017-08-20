namespace MeteringDevices.Core.Spb
{
    class SimpleCookie
    {
        public string Name
        {
            get;
            private set;
        }

        public string Value
        {
            get;
            private set;
        }

        public SimpleCookie(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
