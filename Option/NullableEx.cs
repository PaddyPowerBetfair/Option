
namespace Option
{
    /// <summary>
    /// Nullable to Option converter
    /// </summary>
    public static class NullableEx
    {
        /// <summary>
        /// Cast a nullable parameter to an Option<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nullable"></param>
        /// <returns></returns>
        public static Option<T> ToOption<T>(this T? nullable)
            where T : struct
        {
            return nullable.HasValue 
                ? Option.Some(nullable.Value) 
                : Option.None<T>();
        }
    }
}
