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