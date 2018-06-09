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

## How can I contribute?
Please see [CONTRIBUTING.md](CONTRIBUTING.md).

## What licence is this released under?
This is released under a modified version of the BSD licence.
Please see [LICENCE.md](https://github.com/PaddyPowerBetfair/standards/blob/master/LICENCE.md).