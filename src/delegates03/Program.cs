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