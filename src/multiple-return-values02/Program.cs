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