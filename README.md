# C# Language Features

**Background**: C# (as of version 5.0) is both an [ECMA](https://ecma-international.org/publications/standards/Ecma-334.htm) and ISO/IEC standard. See [ECMA-334](https://ecma-international.org/publications/files/ECMA-ST/ECMA-334.pdf). C#'s compiler, [Roslyn](https://github.com/dotnet/roslyn), is written in C# and is open source under the Apache 2.0 license.

C# projects can be compiled and run on macOS, Linux, or Windows when targeting [.NET Core](https://github.com/dotnet/core). The .NET Core runtime and SDK are open source on GitHub and available under the MIT license.

See [Pop!\_OS setup guide](https://github.com/erik1066/pop-os-setup) for instructions on installing .NET Core's runtime and SDK on Ubuntu 18.04.

Example "Hello, World!" application:

```cs
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, world!");
    }
}
```

C# version release dates:

| Version | Date |                                                              ECMA                                                              |
| ------- | :--: | :----------------------------------------------------------------------------------------------------------------------------: |
| C# 1.0  | 2002 | [Yes](http://www.ecma-international.org/publications/files/ECMA-ST-WITHDRAWN/ECMA-334,%202nd%20edition,%20December%202002.pdf) |
| C# 2.0  | 2005 |                        [Yes](http://www.ecma-international.org/publications/files/ECMA-ST/Ecma-334.pdf)                        |
| C# 3.0  | 2007 |                                                               No                                                               |
| C# 4.0  | 2010 |                                                               No                                                               |
| C# 5.0  | 2012 |                       [Yes](https://www.ecma-international.org/publications/files/ECMA-ST/ECMA-334.pdf)                        |
| C# 6.0  | 2015 |                                                               No                                                               |
| C# 7.0  | 2017 |                                                               No                                                               |
| C# 8.0  | 2019 |                                                               No                                                               |

## String interpolation

> Available in C# 6.0. [C# string interpolation](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated)

You can use string interpolation instead of string concatenation, provided you prefix the string with a `$`:

```cs
string name = $"My name is {firstname} {GetLastname()}";
```

## Multiple return values via tuples and deconstruction

> Available in C# 7.0. [C# tuples, deconstruction, and multiple return values](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7#tuples)

You can return multiple values from a function as of C# 7.0, either with names or without names, much like you can in Golang. Multiple return values without names:

```cs
using System;

class Program
{
    static void Main()
    {
        // multiple return types with no names, use 'Item1', 'Item2', etc.
        (string, string) greetings = Greeting("John");

        Console.WriteLine(greetings.Item1);
        Console.WriteLine(greetings.Item2);
    }

    static (string, string) Greeting(string name)
    {
        return ("Hello, " + name, "HEY! " + name);
    }
}
```

Multiple return values with names:

```cs
using System;

class Program
{
    static void Main()
    {
        // multiple return types that use the default property names defined in the Greeting() method (implicit when using var)
        var greetings1 = Greeting("John");

        Console.WriteLine(greetings1.Regular);
        Console.WriteLine(greetings1.Excited);

        // multiple return types that use overriden property names
        (string First, string Second) greetings2 = Greeting("Mary");
        Console.WriteLine(greetings2.First);
        Console.WriteLine(greetings2.Second);
    }

    static (string Regular, string Excited) Greeting(string name)
    {
        return ("Hello, " + name, "HEY! " + name);
    }
}
```

## Implicity-typed variables using `var`

> Available in C# 3.0. [C# var reference](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/var)

The `var` keyword can be used for implicity-typed variables:

```cs
var name = "John";
var age = 25;
var birthdate = DateTime.Today;
```

It's not recommended to use `var` when the type can't be inferred by reading the code. For example, the following is legal C# code, but bad practice because it's unclear to the reader what `handler` is.

```cs
// allowed, but a bad practice - it's not clear what 'handler' is
var handler = builder.CreateNew("http://localhost:9090");
```

## Passing functions using delegates

> Available in C# 1.0. [C# delegates](https://docs.microsoft.com/en-us/dotnet/csharp/delegates-overview)

### Basic delegates

Like GoLang and JavaScript, you can pass functions as arguments to other functions. This is done using C# delegates.

```cs
using System;

public delegate string Greeter(string name);

class Program
{
    static void Main(string[] args)
    {
        Greeter g = CreateGreeting;
        string greeting = g("Bob");
        Console.WriteLine(greeting);
    }

    static string CreateGreeting(string name)
    {
        return $"Hello, {name}!";
    }
}
```

### Multicast delegates

Note that the special `delegate` type in C# is _multicast_, meaning you can attach multiple functions to the delegate and they will all be called when the delegate is invoked. Attaching is done using the `+=` operator, and detaching is done using the `-=` operator.

```cs
using System;

public delegate void Greeter(string name);

class Program
{
    static void Main(string[] args)
    {
        Greeter g = DisplayBasicGreeting;
        g += DisplayHappyGreeting;

        g("Bob"); // displays both "Hello, Bob" and "Hey, Bob!" to the console

        g -= DisplayBasicGreeting;
        g -= DisplayHappyGreeting;
    }

    static void DisplayBasicGreeting(string name)
    {
        Console.WriteLine($"Hello, {name}");
    }

    static void DisplayHappyGreeting(string name)
    {
        Console.WriteLine($"Hey, {name}!");
    }
}
```

### Generic delegates

Generic programming works with delegates:

```cs
using System;

public delegate T Greeter<T>(T arg1, T arg2);

class Program
{
    static void Main(string[] args)
    {
        Greeter<string> g = CreateGreeting;
        string greeting = g("Susan", "Dr.");
        Console.WriteLine(greeting);
    }

    static string CreateGreeting(string name, string title)
    {
        return $"Hello, {title} {name}!";
    }
}
```

## Digit separators

> Available in C# 7.0. [C# numeric literal improvements](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7#numeric-literal-syntax-improvements)

C# allows the use of underscores to separate digits in numeric literals. For example:

```csharp
int amount = 1_000_000;
```

## Declaring `out` variables on-the-fly and `out` discards

> Available in C# 7.0.

`out` variables can now be declared on-the-fly:

```cs
// The C# 7 way of handling out variables
bool success = int.TryParse("ABCD", out int result);
Console.WriteLine(result);
```

In older versions of C#, we'd need to instead declare `result` earlier, like this:

```cs
// The old way of handling out variables
int result;
bool success = int.TryParse("ABCD", out result);
Console.WriteLine(result);
```

In addition to delcaring `out` variables on-the-fly, you can now _discard_ `out` variables you don't want using the `_` symbol:

```cs
Transform(out _, out _, out int data, out _);
```

## Patterns

> Available in C# 7.0. [C# pattern matching](https://docs.microsoft.com/en-us/dotnet/csharp/pattern-matching)

You can also declare variables on-the-fly with the `is` operator:

```cs
public void Transform(object data)
{
    if (data is DateTime dt)
    {
        Console.WriteLine("Day of month: " + dt.Day);
    }
    else if (data is string s)
    {
        Console.WriteLine($"String '{s}' has a length of {s.Length}");
    }
    else
    {
        Console.WriteLine("Unsupported data detected");
    }
}
```

Declaring variables with an `is` operator makes those variables "pattern variables." We can also do this with `switch` statements as shown below.

```cs
public void Transform(object data)
{
    switch (data)
    {
        case int i:
            Console.WriteLine("Int detected: " + i);
            break;
        case DateTime dt:
            Console.WriteLine("DateTime detected: " + dt.ToShortDateString());
            break;
        case string s when s.Length >= 5:
            Console.WriteLine("String detected: " + s);
            break;
        case string s when s.Length < 5:
            Console.WriteLine("Short string detected: " + s);
            break;
        case bool b when b == false:
            Console.WriteLine("FALSE");
            break;
    }
}
```

## Dynamic binding

> Available in C# 4.0. [C# dynamic binding](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/using-type-dynamic)

You can defer the binding of property names to objects from _compile time_ to _runtime_ using the `dynamic` keyword.

```cs
dynamic person = new Person();
person.Speak();
```

In this case, we're telling the compiler to avoid checking to see if `Speak()` actually exists on `Person`. Dynamic binding can be useful when interoperating with dynamic languages.

## Named arguments

> Available in C# 4.0. [C# named and optional arguments](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/named-and-optional-arguments)

You can provide arguments to a method in one of two ways. The first way is by providing the arguments in a comma-separated list in the order they are defined in the method definition, as shown below:

```cs
using System;

class Program
{
    static void Main()
    {
        var greeting = Greeting("Sonya", "Hello");

        Console.WriteLine(greeting); // "Hello, Sonya"
    }

    static string Greeting(string name, string greeting)
    {
        return greeting + ", " + name;
    }
}
```

We can be more explicit, however, and use _named arguments_ as of C# 4.0:

```cs
using System;

class Program
{
    static void Main()
    {
        var greeting = Greeting(name: "Sonya", greeting: "Hello");

        Console.WriteLine(greeting); // "Hello, Sonya"
    }

    static string Greeting(string name, string greeting)
    {
        return greeting + ", " + name;
    }
}
```

With named arguments, the order of the arguments no longer matters. We can provide the arguments in any order we want.

## Default values for method parameters

> Available in C# 4.0. [C# named and optional arguments](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/named-and-optional-arguments)

Method parameters can have default values:

```cs
using System;

class Program
{
    static void Main()
    {
        var greeting = Greeting(name: "Sonya", greeting: "Hello");

        Console.WriteLine(greeting); // "Hello, Sonya!"
    }

    static string Greeting(string name, string greeting, string terminator = "!")
    {
        return greeting + ", " + name + terminator;
    }
}
```

## Anonymous types

> Available in C# 3.0. [C# anonymous types reference](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/anonymous-types)

Anonymous types allow the programmer to create read-only objects on-the-fly.

```cs
var person = new { FirstName = "Andy", LastName = "Dwyer", Age = 30 };
```

Behind-the-scenes, the C# compiler creates a class with readonly properties for the `FirstName`, `LastName`, and `Age` fields.

Note that it's illegal to return an anonymous object from a method because you cannot return `var`. If you must do this, you can use the `dynamic` keyword or use an `object` and rely on other aspects of C# and .NET to obtain the values (these features are not discussed here, however).

## Tuples

> Available in C# 7.0. [C# tuple reference](https://docs.microsoft.com/en-us/dotnet/csharp/tuples)

Tuples allow storing a set of values, much like anonymous types. Their primary reason for existence is to allow returning multiple values from a method, something an anonymous type cannot do.

```cs
var person = ("Andy", "Dwyer", 30);

Console.WriteLine(person.Item1); // Andy
Console.WriteLine(person.Item2); // Dwyer
Console.WriteLine(person.Item3); // 30
```

You can specify tuple types explicitly. Doing so is how we can return a tuples from a method:

```cs
(string, string, int) person = ("Andy", "Dwyer", 30);
```

That means this is legal:

```cs
(string, string, int) person = GetPerson();
```

We can name our tuple parameters:

```cs
var person = (Firstname: "Andy", Lastname: "Dwyer", Age: 30);

Console.WriteLine(person.Firstname); // Andy
Console.WriteLine(person.Lastname); // Dwyer
Console.WriteLine(person.Age); // 30
```

And we can specify them from the return values:

```cs
(string Firstname, string Lastname, int Age) person = GetPerson();

Console.WriteLine(person.Firstname);
Console.WriteLine(person.Lastname);
Console.WriteLine(person.Age);
```

C# 7 also added support for _deconstruction_ using tuples. Thus, this is legal:

```cs
var person = ("Andy", "Dwyer", 30);

(string first, string last, int age) = person;

Console.WriteLine(first); // Andy
Console.WriteLine(last); // Dwyer
Console.WriteLine(age); // 30
```

The `Equals` method is overriden such that two tuples are equal if their values are equal. For example:

```cs
var person1 = ("April", "Ludgate", 25);
var person2 = ("April", "Ludgate", 25);

Console.WriteLine(person1.Equals(person2)); // true
```

## Foreach loops

> Available in C# 1.0.

In addition to the `for` loop, C# has a `foreach` loop:

```cs
foreach (string name in names)
{
    Console.WriteLine(name);
}
```

You can use Lambdas in the loop expression, too:

```cs
foreach (var name in names.Where(n => n.StartsWith("J")))
{
    Console.WriteLine(name);
}
```

## Collection initializers

> Available in C# 3.0. Note that the special dictionary initializer is available in C# 6.0.

Previously, collections could only be populated after they were initialized:

```cs
var names = new List<string>();
names.Add("John");
names.Add("Susan");
names.Add("Maria");
names.Add("Sonya");
```

However, in C# 3.0, we can now populate collections at the time they're initialized in a single line of code:

```cs
var names = new List<string>()
{
    "John", "Susan", "Maria", "Sonya"
};
```

Dictionaries can be initialized this way starting in C# 6.0:

```cs
var codeLookup = new Dictionary<int, string>()
{
    { 400, "Bad Request" },
    { 404, "Not Found" }
};
```

An alternative syntax that's equivalent to the one above:

```cs
var codeLookup = new Dictionary<int, string>()
{
    [400] = "Bad Request",
    [404] = "Not Found"
};
```

## Properties

> Available in C# 1.0. [C# properties](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties)

C# provides syntatic sugar for getter and setter methods around private data fields via the `get` and `set` keywords. In the example below, `Hours` is a C# property with both `get` and `set` methods:

```cs
using System;

class TimePeriod
{
   private double _seconds;

   public double Hours
   {
       get { return _seconds / 3600; }
       set {
          if (value < 0 || value > 24)
             throw new ArgumentOutOfRangeException(
                   $"{nameof(value)} must be between 0 and 24.");

          _seconds = value * 3600;
       }
   }
}
```

_Without_ `get` and `set`, the above code snippet would look like the following:

```cs
using System;

class TimePeriod
{
   private double _seconds;

   public GetHours()
   {
       return _seconds / 3600;
   }

   public SetHours(double hours)
   {
        if (hours < 0 || hours > 24)
             throw new ArgumentOutOfRangeException(
                   $"{nameof(value)} must be between 0 and 24.");

          _seconds = value * 3600;
   }
}
```

Using properties is straightforward:

```cs
TimePeriod period = new TimePeriod();
period.Hours = 24;
double hours = period.Hours;
```

What if you don't need to execute logic in the `get` and `set` methods as shown above? C# allows auto-generation of backing fields for properties when they're just pass-throughs:

```cs
using System;

class Book
{
    public string Title { get; set; }
    public int Pages { get; set; }
}
```

C# allows default values to be set using this syntax, too:

```cs
using System;

class Book
{
    public string Title { get; set; } = string.Empty;
    public int Pages { get; set; } = 0;
}
```

Access modifiers can be used with the `get` and `set` keywords. In the example below, `Title` can be retrieved by any caller but is only set-able by class methods. `Pages` is effectively read-only, although C# allows the constructor to set a value.

```cs
using System;

class Book
{
    public string Title { get; private set; }
    public int Pages { get; }

    public Book(string title, int pages)
    {
        Title = title;
        Pages = pages; // allowed, but only in the constructor
    }
}
```

## Local functions

> Available in C# 7.0. [C# local functions reference](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/local-functions)

```cs
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        var names = new List<string>()
        {
            "John",
            "Maria",
            "Ava",
            "Fransico",
        };

        foreach (var name in names)
        {
            string greeting = GenerateGreeting(name); // calls the local function
            Console.WriteLine(greeting);
        }

        // local function declaration
        string GenerateGreeting(string name)
        {
            Random rnd = new Random();
            int month = rnd.Next(1, 3);

            string greeting = "";

            switch (month)
            {
                case 1:
                    greeting = "Greetings";
                    break;
                case 2:
                    greeting = "Salutations";
                    break;
                case 3:
                    greeting = "Hello";
                    break;
            }

            greeting = greeting + ", " + name;
            return greeting;
        }
    }
}

```

## Obsolete methods

You can decorate methods with the `[Obsolete]` attribute to mark them as deprecated. Using obsolete methods generates a compiler warning.

```cs
using System.Attribute;

public class Builder
{
    [Obsolete]
    public string Build()
    {
        ...
    }
}
```

## Null coalesing operator

> Available in C# 2.0. [C# ?? operator](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator)

The old way of checking for a null and then assigning a value "if not null" required several lines of C# code. We can now do the same thing with the null coalesing operator. In the code below, if `newTitle` is not null, then assign `newTitle` to `title`. Otherwise, assign `title` the value "War and Peace".

```cs
string title = newTitle ?? "War and Peace";
```

You can also throw exceptions after the `??` operator:

```cs
string title = newTitle ?? throw new ArgumentNullException(nameof(newTitle));
```

## Safe navigation operator

> Available in C# 6.0. [C# ?. and .[] operator](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-conditional-operators)

The `?` operator assigns `null` if the reference it's checking is null. This eliminates the need for boilerplate null-checking logic. For example:

```cs
string authorName = books?[0]?.author?.name;
```

In older versions of C#, to implement the same logic, we'd have had to check three entities for `null`: The `books` array, the first item in the `books` array, and the `author` property of the book.

## Ranges

> Available in C# 8.0

Starting in C# 8.0, we can use the `[start...end]` syntax on collections and arrays in `foreach` loops:

```cs
using System;

class Program
{
    static void Main(string[] args)
    {
        var names = new string[]
        {
            "Mary",
            "Maria",
            "Mario",
            "Denise",
            "Henry"
        };

        foreach (var name in names[1..4])
        {
            Console.WriteLine(name);
        }
    }
}
```

Ranges can be shortcut by leaving either the `start` or `end` empty. For instance:

```cs
foreach (var name in names[1..])
```

Output:

```
Maria
Mario
Denise
Henry
```

When we leave off the `start` value instead:

```cs
foreach (var name in names[..3])
```

Output:

```
Mary
Maria
Mario
```

We can also use the `^` operator as a prefix to the `end` value to tell C# to treat that value as _n values from the end_. The `^1` in the example below indicates that we should stop "one before the last item":

```cs
foreach (var name in names[1..^1])
```

Output:

```
Maria
Mario
Denise
```

We can also create a `Range` object and use it instead of literal integer values:

```cs
Range range = 1..4;
foreach (var name in names[range])
```

## Verbatim Strings

We can create strings that don't need to be escaped by using the `@` operator:

```cs
string names = @"
                 John
                 Harry
                 Mary";
```

Output:

```

                 John
                 Harry
                 Mary
```

## Nested using statements

If you've been using C# and objects that implement `IDisposable` for long enough, you're familar with the following `using` syntax for disposing of managed resources:

```cs
using (var reader = new System.IO.StreamReader(csv.OpenReadStream()))
{
    ...
}
```

But we often need to nest `using` statements, like such:

```cs
using (var reader = new System.IO.StreamReader(csv.OpenReadStream()))
{
    using (var csvReader = new ChoCSVReader(reader).WithFirstLineHeader())
    {
        foreach (var row in csvReader)
        {
            rows.Add(row.DumpAsJson());
        }
    }
}
```

Thankfully there's a more concise way to represent nested `using` statements:

```cs
using (var reader = new System.IO.StreamReader(csv.OpenReadStream()))
using (var csvReader = new ChoCSVReader(reader).WithFirstLineHeader())
{
    foreach (var row in csvReader)
    {
        rows.Add(row.DumpAsJson());
    }
}
```

## Default interface implemention

> Available in C# 8.0

We can now include a default implementation for interfaces, much like Java:

```cs
public interface IEntity
{
    int Id { get; set; }
    void GenerateId()
    {
        Id = System.Guid.NewGuid();
    }
}
```

Doing so enables developers to expand an interface's surface area without breaking compatibility with existing implementations of that interface.
