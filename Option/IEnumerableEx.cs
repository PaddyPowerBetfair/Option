using System;
using System.Collections.Generic;
using System.Linq;

namespace Option
{
    public static class IEnumerableEx
    {
        /// <summary>
        /// Applies the given function to each element of the list and returns the list comprised of the results
        /// for each element where the function returns Some with some value.
        /// </summary>
        public static IEnumerable<TDestination> Choose<TSource, TDestination>(this IEnumerable<TSource> xs, Func<TSource, Option<TDestination>> chooser)
        {
            if (xs == null) throw new ArgumentNullException();

            var enumerable = xs.Select(chooser)
                               .Where(o => o.HasValue)
                               .Select(o => o.Value);

            return enumerable;
        }

        /// <summary>
        /// Returns all the values in a sequence of Options whose Option is Some.
        /// </summary>
        public static IEnumerable<TSource> CollectSome<TSource>(this IEnumerable<Option<TSource>> xs)
        {
            if (xs == null) throw new ArgumentNullException();

            var enumerable = xs.Where(o => o.HasValue)
                               .Select(o => o.Value);

            return enumerable;
        }

        /// <summary>
        /// Forces the evaluation of a sequence if required, this is detected by the run time type of the sequence.
        /// Hot IEnumerables will always bypass evaluation.
        /// </summary>
        public static IEnumerable<T> Cache<T>(this IEnumerable<T> xs)
        {
            return TypeIsHotIEnumerable<T>(xs.GetType()) ? xs : xs.ToList();
        }

        private static bool TypeIsHotIEnumerable<T>(Type type)
        {
            return type == typeof(List<T>) ||
                type == typeof(T[]) ||
                type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>);
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
        /// Some of first if xs has any, None otherwise
        /// </summary>
        public static Option<T> ToOption<T>(this IEnumerable<T> xs)
        {
            return xs.TryFirst();
        }

        /// <summary>
        /// Some of first if xs has any, None otherwise
        /// </summary>
        public static Option<T> TryFirst<T>(this IEnumerable<T> xs)
        {
            return xs.TryFirst(_ => true);
        }

        /// <summary>
        /// Some of first if xs has any, None otherwise
        /// </summary>
        public static Option<T> TryFirst<T>(this IEnumerable<T> xs, Func<T, bool> predicate)
        {
            CheckArgumentIsNotNull(xs);

            return xs
                .FirstOrDefault(predicate)
                .OptionFromValueOrDefault();
        }

        /// <summary>
        /// Some of last if xs has any, None otherwise
        /// </summary>
        public static Option<T> TryLast<T>(this IEnumerable<T> xs)
        {
            return xs
                .LastOrDefault()
                .OptionFromValueOrDefault();
        }

        /// <summary>
        /// Some of last if xs has any, None otherwise
        /// </summary>
        public static Option<T> TryLast<T>(this IEnumerable<T> xs, Func<T, bool> predicate)
        {
            return xs
                .LastOrDefault(predicate)
                .OptionFromValueOrDefault();
        }

        /// <summary>
        /// Some of element at n if xs has n, None otherwise
        /// </summary>
        public static Option<T> TryElementAt<T>(this IEnumerable<T> xs, int n)
        {
            return xs
                .ElementAtOrDefault(n)
                .OptionFromValueOrDefault();
        }

        /// <summary>
        /// Some of element at n if xs has n, None otherwise
        /// </summary>
        public static Option<T> TryElementAt<T>(this IEnumerable<T> xs, int n, Func<T, bool> predicate)
        {
            return xs
                .Where(predicate)
                .ElementAtOrDefault(n)
                .OptionFromValueOrDefault();
        }

        /// <summary>
        /// Some of single if xs has any, None otherwise
        /// </summary>
        public static Option<T> TrySingle<T>(this IEnumerable<T> xs)
        {
            return xs.TrySingle(_ => true);
        }

        /// <summary>
        /// Some of single if xs has any, None otherwise
        /// </summary>
        public static Option<T> TrySingle<T>(this IEnumerable<T> xs, Func<T, bool> predicate)
        {
            CheckArgumentIsNotNull(xs);

            return xs
                .SingleOrDefault(predicate)
                .OptionFromValueOrDefault();
        }
        
        private static void CheckArgumentIsNotNull<TX>(IEnumerable<TX> xs)
        {
            if (xs == null) throw new ArgumentNullException(nameof(xs));
        }
    }
}
