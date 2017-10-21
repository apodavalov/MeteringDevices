using System;
using System.Collections.Generic;

namespace MeteringDevices.Core.Common
{
    public static class EnumerableExtensions
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            return new HashSet<T>(enumerable);
        }
    }
}
