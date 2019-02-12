using System;

namespace string_interpolation
{
    class Program
    {
        static void Main(string[] args)
        {
            string greeting = "Hello";

            // The leading $ tells C# to interpolate the string
            Console.WriteLine($"{greeting} {GetTarget()}!");

            // No leading $ sign, so everything is printed exactly as it appears
            Console.WriteLine("{greeting} {GetTarget()}!");
        }

        static string GetTarget()
        {
            return "World";
        }
    }
}
