﻿using System;
using System.Configuration;

namespace MeteringDevices.Service
{
    public static class ConfigUtils
    {
        public static string GetStringFromConfig(string key, string defaultValue = null)
        {
            return ConfigurationManager.AppSettings[key] ?? defaultValue;
        }

        public static bool GetBoolFromConfig(string key, bool defaultValue = false)
        {
            string stringValue = GetStringFromConfig(key, string.Empty);
            bool result;

            if (!bool.TryParse(stringValue, out result))
            {
                return defaultValue;
            }
            
            return result;
        }
        public static long GetLongFromConfig(string key, long defaultValue = 0)
        {
            string stringValue = GetStringFromConfig(key, string.Empty);
            long result;

            if (!long.TryParse(stringValue, out result))
            {
                return defaultValue;
            }

            return result;
        }
    }
}
