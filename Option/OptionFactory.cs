using System;

namespace Option
{
    /// <summary>
    /// Option factory, contains convenience constructors for Option types.
    /// </summary>
    public static class Option
    {
        public static Option<T> From<T>(T valueOrNull)
        {
            return valueOrNull == null 
                ? None<T>() 
                : Some(valueOrNull);
        }

        /// <summary>
        /// Creates an option instance holding a value
        /// </summary>
        public static Option<T> Some<T>(T instance)
        {
            return Option<T>.Some(instance);
        }

        /// <summary>
        /// Creates an option instance holding no value
        /// </summary>
        public static Option<T> None<T>()
        {
            return Option<T>.None();
        }

        /// <summary>
        /// Attempts func and returns Some on success and None on Exception
        /// </summary>
        public static Option<T> Try<T>(Func<T> func)
        {
            try
            {
                return Some(func());
            }
            catch
            {
                return None<T>();
            }
        }
    }
}
