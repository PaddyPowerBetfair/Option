using System;

namespace PaddyPowerBetfair.Common.Maybe.Core
{
    /// <summary>
    /// Provides a computation expression where option operations can be easily chained.
    /// </summary>
    public class OptionComputationExpressionBuilder
    {
        public Option<TY> Bind<TX, TY>(Option<TX> input, Func<TX, Option<TY>> func)
        {
            return input.IsNone
                ? Option.None<TY>() 
                : func(input.Value);
        }

        public TX Delay<TX>(Func<TX> func)
        {
            return func();
        }

        public Option<TX> Return<TX>(TX input)
        {
            return Option.Some(input);
        }
    }
}
