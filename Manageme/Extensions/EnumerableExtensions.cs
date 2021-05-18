using System;
using System.Collections.Generic;
using System.Linq;

namespace Manageme.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool None<T>(this IEnumerable<T> source)
        {
            return ! source.Any();
        }
        public static bool None<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return ! source.Any(predicate);
        }
        
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }
    }
}

