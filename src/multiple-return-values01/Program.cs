using System;

class Program
{
    static void Main()
    {
        // multiple return types with no names, use 'Value1', 'Value2', etc.
        (string, string) greetings = Greeting("John");

        Console.WriteLine(greetings.Item1);
        Console.WriteLine(greetings.Item2);
    }

    static (string, string) Greeting(string name)
    {
        return ("Hello, " + name, "HEY! " + name);
    }
}