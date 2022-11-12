using System;

namespace SuperSudoku.Utliity.Extentions
{
    public static class ObjectExtentions
    {
        public static T ThrowArgIfNull<T>(this T obj, string argName)
            where T : class
        {
            if (obj is null) {
                throw new ArgumentNullException(argName);
            }

            return obj;
        }
    }
}
