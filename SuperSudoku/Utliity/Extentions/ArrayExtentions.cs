using System;
using System.Collections.Generic;

namespace SuperSudoku.Utliity.Extentions
{
    public static class ArrayExtentions
    {
        public static IEnumerable<T> ToEnumerable<T>(this T[,] target)
        {
            foreach (var item in target)
                yield return item;
        }
    }
}
