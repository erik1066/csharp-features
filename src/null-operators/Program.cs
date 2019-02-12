using System;
using System.Collections.Generic;

namespace null_operators
{
    class Program
    {
        static void Main(string[] args)
        {
            // example of null coalesing operator
            string greeting = null;
            string newGreeting = greeting ?? "Hello";

            Console.WriteLine(newGreeting + " World!");

            // example of the safe navigation operator
            var books = new List<Book>()
            {
                new Book() { Title = "Don Quixote", Author = "Miguel De Cervantes", Pages = 992 },
                new Book() { Title = "The Red Badge of Courage", Author = "Stephen Crane", Pages = 112 },
                new Book() { Title = "The Secret Garden" },
            };

            string title = books?[2]?.Author; // Author doesn't exist on the book at index 2
            Console.WriteLine(title); // prints nothing
        }
    }

    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
    }
}
