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
    }
}
