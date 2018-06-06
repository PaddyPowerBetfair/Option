using System;

namespace Option
{
    /// <summary>
    /// The option type is used when an actual value might not exist for a named value or variable.
    /// An option has an underlying type and can hold a value of that type, or it might not have a value.
    /// Also known as the Maybe monad.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Option<T>
    {
        private static readonly Option<T> _none = new Option<T>(default(T), false);

        public T Value
        {
            get
            {
                if (!HasValue) throw new InvalidOperationException();

                return ValueOrNull;
            }
        }

        public T ValueOrNull { get; }

        public T ValueOr(T defaultValue)
        {
            return HasValue 
                ? ValueOrNull 
                : defaultValue;
        }

        public bool HasValue { get; }

        public bool IsNone => !HasValue;

        private Option(T value, bool hasValue)
        {
            ValueOrNull = value;
            HasValue = hasValue;
        }

        internal static Option<T> Some(T instance)
        {
            return new Option<T>(instance, true);
        }

        internal static Option<T> None()
        {
            return _none;
        }

        public bool Equals(Option<T> other)
        {
            return !HasValue
                   && !other.HasValue
                   || HasValue
                   && other.HasValue
                   && Value.Equals(other.Value);
        }

        public static bool operator ==(Option<T> a, Option<T> b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Option<T> a, Option<T> b)
        {
            return !(a == b);
        }

        public static explicit operator Option<T>(T valueOrNull)
        {
            return Option.From(valueOrNull);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Option<T> option
                   && Equals(option);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (ValueOrNull.GetHashCode() * 397) ^ HasValue.GetHashCode();
            }
        }

        public override string ToString()
        {
            return HasValue
                ? "Some<" + typeof(T).Name + ">(" + (ValueOrNull == null ? "null" : ValueOrNull.ToString()) + ")"
                : "None<" + typeof(T).Name + ">()";
        }
    }
}