using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaddyPowerBetfair.Common.Maybe.Extensions
{
    public static class OptionEx
    {
        /// <summary>
        /// Converts an IOption into a Nullable using the wrapped value when Some and null when None
        /// </summary>
        public static T? ToNullable<T>(this Option<T> x)
            where T : struct
        {
            return x.HasValue
                ? (T?)x.Value
                : null;
        }

        /// <summary>
        /// Projects the element inside the option if present.
        /// More succinct in C# than using a desugared computation expression builder.
        /// </summary>
        public static Option<TY> Select<TX, TY>(this Option<TX> x, Func<TX, TY> f)
        {
            return x.HasValue
                ? Option.Some(f(x.Value))
                : Option.None<TY>();
        }

        /// <summary>
        /// Sequentially compose two actions, passing any value produced by the first as an argument to the second.
        /// Also known as >>= or bind
        /// </summary>
        public static Option<TY> SelectMany<TX, TY>(this Option<TX> x, Func<TX, Option<TY>> f)
        {
            return x.HasValue
                ? f(x.Value)
                : Option.None<TY>();
        }

        /// <summary>
        /// Attempts an asyncronous computation.
        /// </summary>
        /// <returns></returns>
        public static async Task<Option<T>> TryAsync<T>(Func<Task<T>> attemptUnsafely, Action<Exception> handleError)
        {
            try
            {
                var value = await attemptUnsafely();
                return Option.Some(value);
            }
            catch (Exception e)
            {
                handleError(e);
                return Option.None<T>();
            }
        }

        /// <summary>
        /// When all the options in the input contain a value it returns Some of all the values, None otherwise
        /// please note it will evaluated the input enumerable once if not evaluated already.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xs"></param>
        /// <returns></returns>
        public static Option<IEnumerable<T>> Flatten<T>(this IEnumerable<Option<T>> xs)
        {
            var xsTilde = xs.Cache();
            return xsTilde.All(x => x.HasValue)
                ? Option.Some(xsTilde.Select(x => x.Value))
                : Option.None<IEnumerable<T>>();
        }

        /// <summary>
        /// Creates an option instance
        /// </summary>
        public static Option<T> OptionFromValueOrDefault<T>(this T valueOrDefault)
        {
            return EqualityComparer<T>.Default.Equals(valueOrDefault, default(T))
                ? Option.None<T>()
                : Option.Some(valueOrDefault);
        }

        /// <summary>
        /// Tries an action and returns None if fails
        /// </summary>
        public static Option<T> Try<T>(this Option<T> option, Action<T> action)
        {
            try
            {
                action(option.Value);

                return option;
            }
            catch
            {
                return Option<T>.None();
            }
        }
    }
}
