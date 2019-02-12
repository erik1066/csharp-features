using System;

namespace anonymous_types
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new 
            { 
                FirstName = "Andy", 
                LastName = "Dwyer", 
                Age = 30 
            };
            
            Console.WriteLine($"Hello, {person.FirstName} {person.LastName}, who is age {person.Age}!");
        }
    }
}
