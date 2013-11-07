using System;

namespace ArticleComparer.Core.Utils
{
    public static class MathUtils
    {
        public static bool IsAbout(this double toCompare, double comparand, double precision = double.Epsilon)
        {
            return Math.Abs(toCompare - comparand) <= precision;
        }
    }
}