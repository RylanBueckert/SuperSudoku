using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperSudoku.Utliity.Extentions
{
    public static class EumerableExtentions
    {
        public static void ForEach<T>(this IEnumerable<T> @this, Action<T> func)
        {
            foreach (var t in @this) {
                func(t);
            }
        }

        public static bool HasDuplicates<T>(this IEnumerable<T> @this)
        {
            var set = new HashSet<T>();
            foreach (var t in @this) {
                if (!set.Add(t)) {
                    return true;
                }
            }

            return false;
        }

        public static bool IsConsecutive(this IEnumerable<int> @this) =>
            !@this.Select((v, i) => v - i).Distinct().Skip(1).Any();
    }
}
