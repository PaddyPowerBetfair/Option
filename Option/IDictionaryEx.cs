using System;
using System.Collections.Generic;
using System.Linq;

namespace Option
{
    public static class IDictionaryEx
    {
        public static TV TryGetValueOrDefault<TK, TV>(this IDictionary<TK, TV> source, TK key,
            TV defaultValue = default(TV))
        {
            return source.TryGetValue(key, out var value)
                ? value
                : defaultValue;
        }

        /// <summary>
        /// TryGetValue wrapper with option types.
        /// It returns Some of the value when a value for the give key is present
        /// or None otherside
        /// </summary>
        public static Option<TValue> TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key)
        {
            return source.TryGetValue(key, out var value)
                ? Option.Some(value)
                : Option.None<TValue>();
        }

        /// <summary>
        /// Projects each kvp value by applying a selector on the kvp contents
        /// </summary>
        public static IDictionary<TKey, TValueOut> SelectValues<TKey, TValueIn, TValueOut>(
            this IEnumerable<KeyValuePair<TKey, TValueIn>> source, Func<TKey, TValueIn, TValueOut> selector)
        {
            var destination = source.ToDictionary(kvp => kvp.Key, kvp => selector(kvp.Key, kvp.Value));

            return destination;

        }

        /// <summary>
        /// Merges multiple dictionaries. Does not tolerate duplicate keys.
        /// </summary>
        /// <exception cref="ArgumentException">Cannot add to merged dictionary as key already exists: {key}</exception>
        public static IDictionary<TKey, TVal> Merge<TKey, TVal>(this IDictionary<TKey, TVal> sourceDictionary,
                                                                params IDictionary<TKey, TVal>[] otherDictionaries)
        {
            if (otherDictionaries == null
                || otherDictionaries.Length == 0)
                return sourceDictionary;

            var mergedDictionary = new Dictionary<TKey, TVal>();

            foreach (var src in new List<IDictionary<TKey, TVal>> { sourceDictionary }.Concat(otherDictionaries))
            {
                foreach (var kvp in src)
                {
                    if (mergedDictionary.ContainsKey(kvp.Key))
                        throw new ArgumentException($"Cannot add to merged dictionary as key already exists: {kvp.Key}");

                    mergedDictionary[kvp.Key] = kvp.Value;
                }
            }

            return mergedDictionary;
        }
    }
}
