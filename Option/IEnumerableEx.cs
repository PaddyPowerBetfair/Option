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
        /// Reduces sequence to a comma-separated string representation
        /// </summary>
        public static string ToCommaSeparatedString<T>(this IEnumerable<T> xs)
        {
            return xs.ToStringSeparatedString(",");
        }

        /// <summary>
        /// Reduces sequence to a separator-separated string representation
        /// </summary>
        public static string ToStringSeparatedString<T>(this IEnumerable<T> xs, string separator)
        {
            if (xs == null) throw new ArgumentNullException();

            return string.Join(separator, xs);
        }

        /// <summary>
        /// Reduces sequence to a comma-separated string representation, applying a formatter to each element
        /// </summary>
        public static string ToCommaSeparatedString<T>(this IEnumerable<T> xs, Func<T, string> format)
        {
            return xs.ToStringSeparatedString(format, ",");
        }

        /// <summary>
        /// Reduces sequence to a separator-separated string representation, applying a formatter to each element
        /// </summary>
        public static string ToStringSeparatedString<T>(this IEnumerable<T> xs, Func<T, string> format, string separator)
        {
            if (xs == null) throw new ArgumentNullException();

            return string.Join(separator, xs.Select(format));
        }

        /// <summary>
        /// Applies a specified function to the corresponding elements of three enumerables, producing an enumerable of the results.
        /// </summary>
        /// <typeparam name="TFirst">The type of the elements of the first input enumerable.</typeparam>
        /// <typeparam name="TSecond">The type of the elements of the second input enumerable.</typeparam>
        /// <typeparam name="TThird">The type of the elements of the third input enumerable.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the result enumerable.</typeparam>
        /// <param name="first">The first input enumerable.</param>
        /// <param name="second">The second input enumerable.</param>
        /// <param name="third">The third input enumerable.</param>
        /// <param name="resultSelector">A function that specifies how to combine the corresponding elements of the two enumerables.</param>
        /// <returns>An IEnumerable<T> that contains elements of the two input enumerables, combined by resultSelector.</returns>
        public static IEnumerable<TResult> Zip3<TFirst, TSecond, TThird, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second,
                                                IEnumerable<TThird> third, Func<TFirst, TSecond, TThird, TResult> resultSelector)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (third == null) throw new ArgumentNullException(nameof(third));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            using (var e1 = first.GetEnumerator())
            using (var e2 = second.GetEnumerator())
            using (var e3 = third.GetEnumerator())
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext())
                    yield return resultSelector(e1.Current, e2.Current, e3.Current);
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
        /// Builds a transformation of the cartessian product of two sequences
        /// </summary>
        public static IEnumerable<TResult> CartessianProduct<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            var secondCached = second.Cache();
            return from i in first
                   from j in secondCached
                   select resultSelector(i, j);
        }

        /// <summary>
        /// Applies a specified function to the corresponding elements of four enumerables, producing an enumerable of the results.
        /// </summary>
        /// <typeparam name="TFirst">The type of the elements of the first input enumerable.</typeparam>
        /// <typeparam name="TSecond">The type of the elements of the second input enumerable.</typeparam>
        /// <typeparam name="TThird">The type of the elements of the third input enumerable.</typeparam>
        /// <typeparam name="TFourth">The type of the elements of the fourth input enumerable.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the result enumerable.</typeparam>
        /// <param name="first">The first input enumerable.</param>
        /// <param name="second">The second input enumerable.</param>
        /// <param name="third">The third input enumerable.</param>
        /// <param name="fourth">The fourth input enumerable.</param>
        /// <param name="resultSelector">A function that specifies how to combine the corresponding elements of the four enumerables.</param>
        /// <returns>An IEnumerable<T> that contains elements of the four input enumerables, combined by resultSelector.</returns>
        public static IEnumerable<TResult> Zip4<TFirst, TSecond, TThird, TFourth, TResult>(
            this IEnumerable<TFirst> first, IEnumerable<TSecond> second, IEnumerable<TThird> third,
            IEnumerable<TFourth> fourth, Func<TFirst, TSecond, TThird, TFourth, TResult> resultSelector)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (third == null) throw new ArgumentNullException(nameof(third));
            if (fourth == null) throw new ArgumentNullException(nameof(fourth));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            using (var e1 = first.GetEnumerator())
            using (var e2 = second.GetEnumerator())
            using (var e3 = third.GetEnumerator())
            using (var e4 = fourth.GetEnumerator())
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext() && e4.MoveNext())
                    yield return resultSelector(e1.Current, e2.Current, e3.Current, e4.Current);
        }

        /// <summary>
        /// Computes the product of an enumerable of int values
        /// </summary>
        public static int Product(this IEnumerable<int> xs)
        {
            if (xs == null) throw new ArgumentNullException();

            var y = xs.Aggregate(1, (x0, x1) => x0 * x1);

            return y;
        }

        /// <summary>
        /// Computes the product of an enumerable of long values
        /// </summary>
        public static long Product(this IEnumerable<long> xs)
        {
            if (xs == null) throw new ArgumentNullException();

            var y = xs.Aggregate(1L, (x0, x1) => x0 * x1);

            return y;
        }

        /// <summary>
        /// Computes the product of an enumerable of float values
        /// </summary>
        public static float Product(this IEnumerable<float> xs)
        {
            if (xs == null) throw new ArgumentNullException();

            var y = xs.Aggregate(1.0f, (x0, x1) => x0 * x1);

            return y;
        }

        /// <summary>
        /// Computes the double of an enumerable of double values
        /// </summary>
        public static double Product(this IEnumerable<double> xs)
        {
            if (xs == null) throw new ArgumentNullException();

            var y = xs.Aggregate(1.0, (x0, x1) => x0 * x1);

            return y;
        }

        /// <summary>
        /// Computes the product of an enumerable of decimal values
        /// </summary>
        public static decimal Product(this IEnumerable<decimal> xs)
        {
            if (xs == null) throw new ArgumentNullException();

            var y = xs.Aggregate(1.0M, decimal.Multiply);

            return y;
        }

        /// <summary>
        /// Safe (try-catch'ed) map operation for collections
        /// </summary>
        public static IEnumerable<TDestination> TrySelect<TSource, TDestination, TException>(
            this IEnumerable<TSource> xs, Func<TSource, TDestination> select, Action<TSource, TException> @catch)
            where TException : Exception
        {
            var output = new List<TDestination>();
            foreach (var x in xs)
            {
                try
                {
                    var y = select(x);
                    output.Add(y);
                }
                catch (TException exception)
                {
                    @catch(x, exception);
                }
            }

            return output;
        }

        /// <summary>
        /// Returns a new enumerable resulting from adding x at the end of kvps
        /// </summary>
        public static IEnumerable<T> Append<T>(this IEnumerable<T> xs, T x)
        {
            if (xs == null) throw new ArgumentNullException();

            return xs.Concat(Singleton(x));
        }

        /// <summary>
        /// Returns a new enumerable resulting from removing all the ocurrences of item
        /// Comparison is done through item.Equals
        /// </summary>
        public static IEnumerable<T> Remove<T>(this IEnumerable<T> xs, T item)
        {
            if (xs == null) throw new ArgumentNullException();

            return xs.Remove(x => item.Equals(x));
        }

        /// <summary>
        /// Returns a new enumerable resulting from removing all the ocurrences satifying condition
        /// </summary>
        public static IEnumerable<T> Remove<T>(this IEnumerable<T> xs, Func<T, bool> condition)
        {
            if (xs == null) throw new ArgumentNullException();

            return xs.Where(x => !condition(x));
        }

        /// <summary>
        /// Returns an enumerable with a single element
        /// </summary>
        public static IEnumerable<T> Singleton<T>(T item)
        {
            yield return item;
        }

        /// <summary>
        /// Returns a new enumerable where occurrences of p are replaced with q
        /// Comparison is done through p.Equals
        /// </summary>
        /// <param name="xs">sequence</param>
        /// <param name="p">item to compare to</param>
        /// <param name="q">item to place</param>
        public static IEnumerable<T> Replace<T>(this IEnumerable<T> xs, T p, T q)
        {
            if (xs == null) throw new ArgumentNullException();

            return xs.Replace(q, x => p.Equals(x));
        }

        /// <summary>
        /// Returns a new enumerable where occurrences satisfying condition are replaced with item
        /// </summary>
        /// <param name="xs">sequence</param>
        /// <param name="item">item to place where condition</param>
        /// <param name="condition">condition an item needs to satisfy in order to get replaced by the item parameter</param>
        public static IEnumerable<T> Replace<T>(this IEnumerable<T> xs, T item, Func<T, bool> condition)
        {
            if (xs == null) throw new ArgumentNullException();

            var replaced = xs
                .Select(x => condition(x) ? item : x);

            return replaced;
        }

        /// <summary>
        /// Returns a new hash set with the items inside the input sequence
        /// </summary>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> xs)
        {
            if (xs == null) throw new ArgumentNullException();

            return new HashSet<T>(xs);
        }

        /// <summary>
        /// Merges two enumerables element by element using a merge function
        /// </summary>
        public static IEnumerable<T> Merge<T>(this IEnumerable<T> ps, IEnumerable<T> qs, Func<T, T, T> merge)
        {
            if (ps == null) throw new ArgumentNullException();
            if (qs == null) throw new ArgumentNullException(nameof(qs));
            if (merge == null) throw new ArgumentNullException(nameof(merge));

            var merged = ps.Zip(qs, merge);

            return merged;
        }

        /// <summary>
        /// Converts an enumerable of key value pairs into a dictionary by indexing based on key
        /// </summary>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> kvps)
        {
            if (kvps == null) throw new ArgumentNullException();

            var dictionary = kvps.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return dictionary;
        }

        /// <summary>
        /// Merges the enumerables inside an enumerable.
        /// </summary>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> xss)
        {
            if (xss == null) throw new ArgumentNullException();

            var flat = xss.Aggregate(Enumerable.Empty<T>(), (acc, xs) => acc.Concat(xs));

            return flat;
        }

        /// <summary>
        /// Ensures a enumerable is an array by either casting or storing the evaluation results onto an array.
        /// </summary>
        public static T[] EnsureArray<T>(this IEnumerable<T> xs)
        {
            if (xs == null) throw new ArgumentNullException();

            var array = xs as T[] ?? xs.ToArray();

            return array;
        }

        /// <summary>
        /// Ensures a enumerable is a list by either casting or storing the evaluation results onto a list.
        /// </summary>
        public static List<T> EnsureList<T>(this IEnumerable<T> xs)
        {
            if (xs == null) throw new ArgumentNullException();

            var list = xs as List<T> ?? xs.ToList();

            return list;
        }

        /// <summary>
        /// Creates a new array of type TY[] with the contents of the input enumerable
        /// Forces enumerable evaluation
        /// </summary>
        public static Array ToArrayOfType<TX, TY>(this IEnumerable<TX> xs)
        {
            return ToArrayOfType(xs, typeof(TY));
        }

        /// <summary>
        /// Creates a new array of type type[] with the contents of the input enumerable
        /// Forces enumerable evaluation
        /// </summary>
        public static Array ToArrayOfType<TX>(this IEnumerable<TX> xs, Type type)
        {
            CheckArgumentIsNotNull(xs);

            var eagerXs = xs.EnsureArray();
            var array = Array.CreateInstance(type, eagerXs.Length);
            var index = 0;
            foreach (var x in eagerXs)
            {
                array.SetValue(x, index);
                index++;
            }

            return array;
        }

        /// <summary>
        /// Creates a new enumerable where each element in the input is paired with its previous and
        /// projected through a selector function.
        /// </summary>
        public static IEnumerable<TY> PairWithPrevious<TX, TY>(this IEnumerable<TX> xs, Func<TX, TX, TY> selector)
        {
            CheckArgumentIsNotNull(xs);

            var previouss = xs;
            var currents = xs.Skip(1);
            var pairs = currents.Zip(previouss, selector);

            return pairs;
        }

        /// <summary>
        /// Creates a new enumerable where each element in the input is paired with its previous element.
        /// </summary>
        public static IEnumerable<Tuple<T, T>> PairWithPrevious<T>(this IEnumerable<T> xs)
        {
            return xs.PairWithPrevious(Tuple.Create);
        }

        /// <summary>
        /// Creates a new enumerable where each element in the input is paired with its next element and
        /// projected through a selector function.
        /// </summary>
        public static IEnumerable<TY> PairWithNext<TX, TY>(this IEnumerable<TX> xs, Func<TX, TX, TY> selector)
        {
            CheckArgumentIsNotNull(xs);

            var nexts = xs.Skip(1);
            var pairs = xs.Zip(nexts, selector);

            return pairs;
        }

        /// <summary>
        /// Creates a new enumerable where each element in the input is paired with its next element.
        /// </summary>
        public static IEnumerable<Tuple<T, T>> PairWithNext<T>(this IEnumerable<T> xs)
        {
            return xs.PairWithNext(Tuple.Create);
        }

        /// <summary>
        /// It returns the min element within a collection using a projection for sorting
        /// Will fail on empty inputs
        /// </summary>
        public static T MinBy<T, TKey>(this IEnumerable<T> xs, Func<T, TKey> selector)
        {
            CheckArgumentIsNotNull(xs);

            var y = xs.OrderBy(selector)
                .First();

            return y;
        }

        /// <summary>
        /// It returns the max element within a collection using a projection for sorting
        /// Will fail on empty inputs
        /// </summary>
        public static T MaxBy<T, TKey>(this IEnumerable<T> xs, Func<T, TKey> selector)
        {
            CheckArgumentIsNotNull(xs);

            var y = xs.OrderByDescending(selector)
                .First();

            return y;
        }

        /// <summary>
        /// It returns the min element within a collection using a projection for sorting,
        /// or a default value if the sequence contains no elements
        /// </summary>
        public static T MinByOrDefault<T, TKey>(this IEnumerable<T> xs, Func<T, TKey> selector)
        {
            CheckArgumentIsNotNull(xs);

            var y = xs.OrderBy(selector)
                .FirstOrDefault();

            return y;
        }

        /// <summary>
        /// Returns the max element within a collection using a projection for sorting,
        /// or a default value if the sequence contains no elements
        /// </summary>
        public static T MaxByOrDefault<T, TKey>(this IEnumerable<T> xs, Func<T, TKey> selector)
        {
            CheckArgumentIsNotNull(xs);

            var y = xs.OrderByDescending(selector)
                .FirstOrDefault();

            return y;
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
        /// Lazy batcher
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> xs, int batchSize)
        {
            CheckArgumentIsNotNull(xs);

            using (var enumerator = xs.GetEnumerator())
            {
                var batch = new List<T>(batchSize);

                var i = 0;
                while (enumerator.MoveNext())
                {
                    batch.Add(enumerator.Current);

                    i = (i + 1) % batchSize;

                    if (i == 0)
                    {
                        yield return batch;
                        batch = new List<T>(batchSize);
                    }
                }

                if (i != 0)
                {
                    yield return batch;
                }
            }
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

        /// <summary>
        /// Generalization of string.StartsWith to IEnumerables
        /// Element comparison is done through EqualityComparer of 't
        /// </summary>
        public static bool StartsWith<T>(this IEnumerable<T> @as, IEnumerable<T> bs)
        {
            CheckArgumentIsNotNull(@as);
            CheckArgumentIsNotNull(bs);

            var differences = @as.Zip(bs, (a, b) =>
                !EqualityComparer<T>.Default.Equals(a, b));

            var result = !differences.Any(d => d);

            return result;
        }

        /// <summary>
        /// Applies an accumulator function over a sequence until the accumulator satisfies some criteria
        /// </summary>
        /// <returns></returns>
        public static TResult AggregateUntil<TSource, TAccumulate, TResult>(this IEnumerable<TSource> xs,
            TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> func,
            Func<TAccumulate, bool> shouldStop,
            Func<TAccumulate, TResult> resultSelector)
        {
            CheckArgumentIsNotNull(xs);
            var acc = seed;

            foreach (var x in xs) // in the absence of TCO mutable loop is preferred
            {
                if (shouldStop(acc)) break;
                acc = func(acc, x);
            }

            var result = resultSelector(acc);

            return result;
        }

        /// <summary>
        /// Partitions a sequence in two by applying a predicate.
        /// Elements fulfilling predicate fo the first sequence.
        /// Strict / eager.
        /// Implementation uses mutation for performance reasons.
        /// </summary>
        public static Tuple<IEnumerable<T>, IEnumerable<T>> Partition<T>(this IEnumerable<T> xs, Func<T, bool> predicate)
        {
            CheckArgumentIsNotNull(xs);

            var yes = new List<T>();
            var no = new List<T>();

            foreach (var x in xs)
            {
                if (predicate(x))
                {
                    yes.Add(x);
                }
                else
                {
                    no.Add(x);
                }
            }

            return Tuple.Create<IEnumerable<T>, IEnumerable<T>>(yes, no);
        }

        private static void CheckArgumentIsNotNull<TX>(IEnumerable<TX> xs)
        {
            if (xs == null) throw new ArgumentNullException();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> xs)
        {
            return xs == null || !xs.Any();
        }

        /// <summary>
        /// Immediately executes the given action on each element in the source sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence</typeparam>
        /// <param name="source">The sequence of elements</param>
        /// <param name="action">The action to execute on each element</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        /// <summary>
        /// Returns distinct elements from a sequence by using the supplied selector to retrieve
        /// a property used to compare the object values.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the sequence</typeparam>
        /// <typeparam name="TSelection">The type returned by the selector used for comparison</typeparam>
        /// <param name="source">The sequence of elements</param>
        /// <param name="selector">
        /// The selector used to retrieve a property of the source items to use for comparison
        /// </param>
        /// <returns>An <see cref="IEnumerable{TSource}"/> that contains distinct elements from the source sequence.</returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TSelection>(this IEnumerable<TSource> source, Func<TSource, TSelection> selector)
        {
            return source.Distinct(new DelegateComparer<TSource, TSelection>(selector));
        }

        /// <summary>
        /// Determines whether none of the elements of a sequence satisfy a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> that contains the elements to apply the predicate to.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        /// <value>true</value> if no element of the source sequence passes the test in the specified predicate, or if the sequence is empty; otherwise, <value>false</value>.
        /// </returns>
        public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.All(i => !predicate(i));
        }

        #region Helpers

        private class DelegateComparer<TSource, TSelection> : IEqualityComparer<TSource>
        {
            private readonly Func<TSource, TSelection> _selector;

            public DelegateComparer(Func<TSource, TSelection> selector)
            {
                _selector = selector ?? throw new ArgumentException("selctor is null");
            }

            public bool Equals(TSource x, TSource y)
            {
                if (x == null && y == null) return true;
                if (x == null ^ y == null) return false;

                var xVal = _selector(x);
                var yVal = _selector(y);
                if (xVal == null && yVal == null) return true;
                if (xVal == null ^ yVal == null) return false;

                return xVal.Equals(yVal);
            }

            public int GetHashCode(TSource obj)
            {
                if (obj == null) throw new ArgumentNullException(nameof(obj));

                var objVal = _selector(obj);
                if (objVal == null) throw new ArgumentException("Can not calculate hashcode of a null value");

                return _selector(obj).GetHashCode();
            }
        }

        #endregion
    }
}
