using System;
using System.Collections.Generic;

namespace Option
{
    internal static class EnumerableExHelper
    {
        /// <summary>
        /// Check that input is not null. If null throw ArgumentNullException
        /// </summary>
        /// <typeparam name="TX"></typeparam>
        /// <param name="input"></param>
        public static void CheckArgumentIsNotNull<TX>(this IEnumerable<TX> input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
        }

        /// <summary>
        /// Check if the type is a generic type or not
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool TypeIsHotIEnumerable<T>(this Type type)
        {
            return type == typeof(List<T>)
                || type == typeof(T[])
                || type.IsGenericType
                && type.GetGenericTypeDefinition() == typeof(Dictionary<,>);
        }
    }
}
