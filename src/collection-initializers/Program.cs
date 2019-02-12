using System;
using System.Collections.Generic;

namespace collection_initializers
{
    class Program
    {
        static void Main(string[] args)
        {
            // standard collection initializer syntax
            var names = new List<string>()
            {
                "John", "Susan", "Maria", "Sonya"
            };

            foreach (var name in names)
            {
                Console.WriteLine("Hello " + name);
            }

            // dictionary collection initializer syntax
            var codeLookup = new Dictionary<int, string>()
            {
                { 400, "Bad Request" },
                { 404, "Not Found" }
            };

            foreach (var code in codeLookup)
            {
                Console.WriteLine($"{code.Key} : {code.Value}");
            }

            // alternative dictionary collection initializer syntax
            codeLookup = new Dictionary<int, string>()
            {
                [200] = "OK",
                [201] = "Created"
            };

            foreach (var code in codeLookup)
            {
                Console.WriteLine($"{code.Key} : {code.Value}");
            }
        }
    }
}
