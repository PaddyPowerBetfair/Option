# Option

Option is a C# representation of the maybe monad

### Monad
Monads are used to encapsulate a value and simplify handling of unusual cases. You can think of monads as generic types.

### Maybe
Maybe is a data type (also known as an option type) which is used to represent an optional value.

### Maybe Monad
Maybe monad is a generic type which contains either some value of it's underlying type or nothing.

If you need more information you can check [wikibooks introduction](https://en.wikibooks.org/wiki/Haskell/Understanding_monads/Maybe) to maybe monad.

# Why we need the Option library?

> For simplicity you can think of `Nullable<T>` being closest thing to the maybe monad in C#. Although it doesn't represent all the characteristics of the maybe monad, it's sufficient enough to give the main idea.

At first glance, extending `Nullable<T>` might be a good option, 
but the `struct` constraint for `T` in it makes it impossible to use with the reference types.

Using maybe monad also makes code more readable by reducing the nested code blocks, as you will see in the following examples.

# How to use it

Our hardworking teammates are trying to publish the Option library to [nuget](https://www.nuget.org/), but until then you can download/clone/fork this repository and build it locally. 

> Option library targets **.NET Starndard 2.0** , so you can use it with **.NET Core 2.0+**, **.NET Framework 4.6.1+** and many other platforms. Please, see the [platform support.](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md)

# Examples

### Basic creation using extension methods :

`Some`

```cs
var number = Option.Some(23);
var str = Option.Some("apple");

Debug.Assert(number.HasValue);
Debug.Assert(str.HasValue);
```

`None`

```cs
var nan = Option.None<int>();
var empty = Option.None<string>();

Debug.Assert(!nan.HasValue);
Debug.Assert(!empty.HasValue);
```

`From` - if you don't have the value in compile time

```cs
var copy = Option.From("apple".Or(null));

Debug.Assert(copy.Value == "apple"); // Option.Some("apple")
// or
Debug.Assert(!copy.HasValue); // Option.None<string>()

```
`Try`

```cs
int zero = 0;
var success = Option.Try(() => 4 / 2);
var fail = Option.Try(() => 1 / zero);

Debug.Assert(success.HasValue); // Option.Some(2)
Debug.Assert(!fail.HasValue); // Option.None<int>()
```

### Getting value

```cs

var defaultValue = number.Value; //not safe. might throw InvalidOperationException
var unsafeValue = nan.Value; //not safe. might throw InvalidOperationException
var safeValue = nan.ValueOrNull; // safe. returns Value or default(T)
var alternativeValue = nan.ValueOr(-1); //safe returns Value or -1 as an alternative value
```

### Basic extension methods

Convert to `Nullable<T>`

```cs
var nullableValue = number.ToNullable();
var nanValue = nan.ToNullable();
```

### Projection

```cs
int power(int n) => n * n;
var anotherNumber = number.Select(power);
var anotherNan = nan.Select(power);

Debug.Assert(anotherNumber.Value == 23 * 23); // Option.Some(23 * 23)
Debug.Assert(!anotherNan.HasValue); // Option.None<int>()
```

### Binding

```cs
Option<int> length(string s) => Option.From(s.Length);
var strLength = str.SelectMany(length);
var emptyLength = empty.SelectMany(length);

Debug.Assert(strLength.Value == 5); // Option.Some(5)
Debug.Assert(!emptyLength.HasValue); // Option.None<int>()
```

## How can I contribute?
Please see [CONTRIBUTING.md](CONTRIBUTING.md).

## What licence is this released under?
This is released under a modified version of the BSD licence.
Please see [LICENCE.md](https://github.com/PaddyPowerBetfair/standards/blob/master/LICENCE.md).