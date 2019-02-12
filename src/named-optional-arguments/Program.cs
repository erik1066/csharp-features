using System;

class Program
{
    static void Main()
    {
        var greeting = GetGreeting(
            title: "Dr.", 
            name: "Sonya",
            terminator: "?",
            greeting: "Hello");

        Console.WriteLine(greeting); // "Hello, Dr. Sonya?"

        // Note the 'terminator' argument is not provided - it is optional because the method declares a default value
        greeting = GetGreeting(
            title: "the Honorable", 
            name: "Andy Dwyer",
            greeting: "Salutations");

        Console.WriteLine(greeting); // "Salutations, the Honorable Andy Dwyer!"

        // We can still call the method without named arguments as long as we provide the arguments in-order
        greeting = GetGreeting(
            "Greetings",
            "Agent",
            "Smith",
            "");

        Console.WriteLine(greeting); // "Greetings, Agent Smith"
    }

    static string GetGreeting(string greeting, string title, string name, string terminator = "!")
    {
        return greeting + ", " + title + " " + name + terminator;
    }
}