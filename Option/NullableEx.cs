
namespace Option
{
    public static class NullableEx
    {
        public static Option<T> ToOption<T>(this T? nullable)
            where T : struct
        {
            var option = nullable.HasValue ? Option.Some(nullable.Value) : Option.None<T>();

            return option;
        }
    }
}
