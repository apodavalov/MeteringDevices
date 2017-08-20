﻿namespace MeteringDevices.Core.Spb
{
    class Cookie
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

        public Cookie(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
