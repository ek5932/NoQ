using System.Collections.Generic;
using System.Linq;

namespace NoQ.Framework.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> value) => value == null || !value.Any();
    }
}
