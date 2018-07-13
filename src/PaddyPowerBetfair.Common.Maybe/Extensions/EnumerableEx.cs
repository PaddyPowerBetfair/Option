using System;
using System.Collections.Generic;
using System.Linq;

namespace PaddyPowerBetfair.Common.Maybe.Extensions
{
    public static class EnumerableEx
    {
        /// <summary>
        /// Applies the given function to each element of the list and returns the list comprised of the results
        /// for each element where the function returns Some with some value.
        /// </summary>
        public static IEnumerable<TDestination> Choose<TSource, TDestination>(this IEnumerable<TSource> input, Func<TSource, Option<TDestination>> chooser)
        {
            CheckArgumentIsNotNull(input);

            return input.Select(chooser)
                               .Where(o => o.HasValue)
                               .Select(o => o.Value);
        }

        /// <summary>
        /// Returns all the values in a sequence of Options whose Option is Some.
        /// </summary>
        public static IEnumerable<TSource> CollectSome<TSource>(this IEnumerable<Option<TSource>> input)
        {
            CheckArgumentIsNotNull(input);

            return input.Where(o => o.HasValue)
                               .Select(o => o.Value);
        }

        /// <summary>
        /// Forces the evaluation of a sequence if required, this is detected by the run time type of the sequence.
        /// Hot IEnumerables will always bypass evaluation.
        /// </summary>
        public static IEnumerable<T> Cache<T>(this IEnumerable<T> input)
        {
            return TypeIsHotIEnumerable<T>(input.GetType()) 
                ? input 
                : input.ToList();
        }

        private static bool TypeIsHotIEnumerable<T>(Type type)
        {
            return type == typeof(List<T>) 
                || type == typeof(T[]) 
                || type.IsGenericType 
                && type.GetGenericTypeDefinition() == typeof(Dictionary<,>);
        }

        /// <summary>
        /// Lazily generate sequences from a generator function and a seed state
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TItem> Generate<TItem, TState>(Func<TState, Option<Tuple<TState, TItem>>> generate, TState seed)
        {
            for (var item = generate(seed); item.HasValue; item = generate(item.Value.Item1))
                yield return item.Value.Item2;
        }

        /// <summary>
        /// Some of first if input has any, None otherwise
        /// </summary>
        public static Option<T> ToOption<T>(this IEnumerable<T> input)
        {
            return input.TryFirst();
        }

        /// <summary>
        /// Some of first if input has any, None otherwise
        /// </summary>
        public static Option<T> TryFirst<T>(this IEnumerable<T> input)
        {
            return input.TryFirst(_ => true);
        }

        /// <summary>
        /// Some of first if input has any, None otherwise
        /// </summary>
        public static Option<T> TryFirst<T>(this IEnumerable<T> input, Func<T, bool> predicate)
        {
            CheckArgumentIsNotNull(input);

            return input
                .FirstOrDefault(predicate)
                .OptionFromValueOrDefault();
        }

        /// <summary>
        /// Some of last if input has any, None otherwise
        /// </summary>
        public static Option<T> TryLast<T>(this IEnumerable<T> input)
        {
            return input
                .LastOrDefault()
                .OptionFromValueOrDefault();
        }

        /// <summary>
        /// Some of last if input has any, None otherwise
        /// </summary>
        public static Option<T> TryLast<T>(this IEnumerable<T> xs, Func<T, bool> predicate)
        {
            return xs
                .LastOrDefault(predicate)
                .OptionFromValueOrDefault();
        }

        /// <summary>
        /// Some of element at index if input has index, None otherwise
        /// </summary>
        public static Option<T> TryElementAt<T>(this IEnumerable<T> input, int index)
        {
            return input
                .ElementAtOrDefault(index)
                .OptionFromValueOrDefault();
        }

        /// <summary>
        /// Some of element at index if input has index, None otherwise
        /// </summary>
        public static Option<T> TryElementAt<T>(this IEnumerable<T> input, int index, Func<T, bool> predicate)
        {
            return input
                .Where(predicate)
                .ElementAtOrDefault(index)
                .OptionFromValueOrDefault();
        }

        /// <summary>
        /// Some of single if input has any, None otherwise
        /// </summary>
        public static Option<T> TrySingle<T>(this IEnumerable<T> input)
        {
            return input.TrySingle(_ => true);
        }

        /// <summary>
        /// Some of single if input has any, None otherwise
        /// </summary>
        public static Option<T> TrySingle<T>(this IEnumerable<T> input, Func<T, bool> predicate)
        {
            CheckArgumentIsNotNull(input);

            return input
                .SingleOrDefault(predicate)
                .OptionFromValueOrDefault();
        }
        
        private static void CheckArgumentIsNotNull<TX>(IEnumerable<TX> input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
        }
    }
}
