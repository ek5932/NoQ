using System;
using System.Runtime.CompilerServices;

namespace NoQ.Framework.Extensions
{
    public static class GuardExtensions
    {
        public static T VerifyNotNull<T>(this T value, string name)
        {
            return value ?? throw new ArgumentNullException(name);
        }

        public static int VerifyNotZero(this int value, string name = "")
        {
            if (value == 0)
                throw new ArgumentException(name);

            return value;
        }
    }
}
