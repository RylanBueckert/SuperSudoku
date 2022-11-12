using System;

namespace SuperSudoku.Utliity
{
    public static class Monads
    {
        public static R Map<T, R>(this T t, Func<T, R> func) =>
            func(t);
    }
}
