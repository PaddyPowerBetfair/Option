# Option

Option Library is a C# representation of the [maybe monad](https://en.wikibooks.org/wiki/Haskell/Understanding_monads/Maybe) which provides a good way to deal with the exceptional cases by reducing the number of  [exception and null checkings](https://en.wikipedia.org/wiki/Exception_handling#Checked_exceptions) as well as the nested blocks in the code which leads to a more compact and more readable code.

### nested if statements : ugly code and hard to follow, known as [arrow anti pattern](http://wiki.c2.com/?ArrowAntiPattern)

```cs
var player = db.Players.FirstOrDefault(p => p.Name == keyword);
if (player != null)
{
    Console.WriteLine(player.Name);
    var team = db.Teams.Find(t => t.Id == player.Id);
    if (team != null)
    {
        Console.WriteLine(team.Name);
    }
}
```
### can be refactored using [Guard Clauses](https://refactoring.com/catalog/replaceNestedConditionalWithGuardClauses.html) , but still with null checking

```cs
var player = db.Players.FirstOrDefault(p => p.Name == keyword);
if (player == null)
    return;
Console.WriteLine(player.Name);
var team = db.Teams.Find(t => t.Id == player.Id);
if (team == null)
    return;
Console.WriteLine(team.Name);
```

### refactored using  Option Library, compact code which is easy to follow with safe chaining of steps 

```cs
Option
    .From(db.Players.FirstOrDefault(p => p.Name == keyword))
    .Try(p => Console.WriteLine(p.Name))
    .Select(p => db.Teams.Find(t => t.Id == p.TeamId))
    .Try(t => Console.WriteLine(t.Name))
```

# Why we need the Option library?

For simplicity you can think of [`Nullable<T>`](https://msdn.microsoft.com/en-us/library/b3h38hb0(v=vs.110).aspx) being closest thing to the maybe monad in C#. Although it doesn't represent all the characteristics of the maybe monad, it's sufficient enough to give the main idea.

At first glance, extending `Nullable<T>` might be a good option, 
but the `struct` constraint for `T` in it makes it impossible to use with the reference types.

# How to use it

Our hardworking teammates are [trying to publish the Option Library](https://github.com/PaddyPowerBetfair/Option/issues/4) to [nuget](https://www.nuget.org/) , but until then you can download/clone/fork this repository and build it locally. 

Option library targets **.NET Starndard 2.0** , so you can use it with **.NET Core 2.0+**, **.NET Framework 4.6.1+** and many other platforms. Please, see the [platform support.](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md)

# Examples

### Basic creation using extension methods :

`Some`

```cs
var number = Option.Some(23);
var str = Option.Some("apple");

Debug.Assert(number.HasValue);
Debug.Assert(str.HasValue);
```

`None` - 

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
`Try` - try possible exception throwing statements safely

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
var alternativeValue = nan.ValueOr(-1); //safe. returns Value or -1 as an alternative value
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