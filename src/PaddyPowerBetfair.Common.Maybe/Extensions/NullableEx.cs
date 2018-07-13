namespace PaddyPowerBetfair.Common.Maybe.Extensions
{
    public static class NullableEx
    {
        public static Option<T> ToOption<T>(this T? nullable)
            where T : struct
        {
            return nullable.HasValue 
                ? Option.Some(nullable.Value) 
                : Option.None<T>();
        }
    }
}
