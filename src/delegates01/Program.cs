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