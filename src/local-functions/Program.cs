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
            int month = rnd.Next(1, 5); 

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
                case 4:
                    greeting = "Hey there";
                    break;
                case 5:
                    greeting = "Sup";
                    break;
                default:
                    greeting = "Hello";
                    break;
            }

            greeting = greeting + ", " + name;
            return greeting;
        }
    }
}
