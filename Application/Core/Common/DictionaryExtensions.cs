using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MeteringDevices.Core.Common
{
    public static class DictionaryExtensions
    { 
        public static ReadOnlyDictionary<K, V> AsReadOnly<K, V>(this IDictionary<K,V> dictionary)
        {
            return new ReadOnlyDictionary<K, V>(dictionary);
        }

        public static void ModifyValue<K,V>(this IDictionary<K,V> dictionary, K key, Func<K,V,V> modifier, V defaultValue = default(V), bool removeKeyIfBecomeToDefault = false)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (modifier == null)
            {
                throw new ArgumentNullException(nameof(modifier));
            }

            V value;

            if (!dictionary.TryGetValue(key, out value))
            {
                value = defaultValue;
            }

            value = modifier(key, value);

            if (removeKeyIfBecomeToDefault && Equals(value, defaultValue))
            {
                dictionary.Remove(key);
            }
            else
            {
                dictionary[key] = value;
            }
        }
    }
}
