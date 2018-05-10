using System;

namespace Option
{
    public static class EnumEx
    {
        /// <summary>
        /// Maps anything to a Some of a enum value if a correspondence exists, None otherwise.
        /// </summary>
        /// <returns></returns>
        public static Option<TOut> TryMapByStringValue<TIn, TOut>(TIn input, bool ignoreCase = false)
            where TOut : struct
        {
            var stringValue = input.ToString();
            return Enum.TryParse(stringValue, ignoreCase, out TOut enumValue)
                       ? Option.Some(enumValue)
                       : Option.None<TOut>();
        }

        /// <summary>
        /// Tries to parse in a strongly-typed way.
        /// </summary>
        public static Option<TEnum> TryParse<TEnum>(string value, bool ignoreCase = false)
            where TEnum : struct
        {
            return Enum.TryParse(value, ignoreCase, out TEnum outValue) ?
                Option.Some(outValue) :
                Option.None<TEnum>();
        }
    }
}
