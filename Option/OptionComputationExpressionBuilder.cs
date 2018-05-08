using System;

namespace Option
{
    /// <summary>
    /// Provides a computation expression where option operations can be easily chained.
    /// </summary>
    public class OptionComputationExpressionBuilder
    {
        public Option<TY> Bind<TX, TY>(Option<TX> x, Func<TX, Option<TY>> f)
        {
            if (!x.HasValue) return Option.None<TY>();

            var y = f(x.Value);

            return y;
        }

        public TX Delay<TX>(Func<TX> f)
        {
            return f();
        }

        public Option<TX> Return<TX>(TX x)
        {
            return Option.Some(x);
        }
    }
}
