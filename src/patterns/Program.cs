using System;

class Program
{
    static void Main(string[] args)
    {
        Foo(DateTime.Now);
        Foo("C#");

        Bar(DateTime.Now);
        Bar(32);
        Bar("The Red Badge of Courage");
        Bar("Zuul");
        Bar(false);
    }

    public static void Foo(object data)
    {
        if (data is DateTime dt)
        {
            Console.WriteLine("Day of month: " + dt.Day);
        }
        else if (data is string s)
        {
            Console.WriteLine($"String '{s}' has a length of {s.Length}");
        }
        else
        {
            Console.WriteLine("Unsupported data detected");
        }
    }

    public static void Bar(object data)
    {
        switch (data)
        {
            case int i:
                Console.WriteLine("Int detected: " + i);
                break;
            case DateTime dt:
                Console.WriteLine("DateTime detected: " + dt.ToShortDateString());
                break;
            case string s when s.Length >= 5:
                Console.WriteLine("String detected: " + s);
                break;
            case string s when s.Length < 5:
                Console.WriteLine("Short string detected: " + s);
                break;
            case bool b when b == false:
                Console.WriteLine("FALSE");
                break;
        }
    }
}
