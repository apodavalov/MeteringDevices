using System;
using System.Collections.Generic;

namespace MeteringDevices.Core.Common
{
    public static class EnumerableUtils
    {
        public static bool EquivalentOfEquatables<T>(IEnumerable<T> enumerableA, IEnumerable<T> enumerableB) where T : IEquatable<T>
        {
            if (enumerableA == null && enumerableB == null)
            {
                return true;
            }

            if (enumerableA == null || enumerableB == null)
            {
                return false;
            }

            IDictionary<T, long> dictionary = new Dictionary<T, long>();

            foreach (T item in enumerableA)
            {
                dictionary.ModifyValue(item, (k, v) => v + 1, 0);
            }

            foreach (T item in enumerableB)
            {
                dictionary.ModifyValue(item, (k, v) => v - 1, 0, true);
            }

            return dictionary.Count == 0;
        }
    }
}
