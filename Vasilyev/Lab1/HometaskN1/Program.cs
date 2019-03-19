using System;
using BookLibrary;
using System.Threading;

namespace HometaskN1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select what do you want to do with a Book in Repository...");
            Console.WriteLine("0. Get");
            Console.WriteLine("1. Add");
            Console.WriteLine("2. Change");
            Console.WriteLine("3. Delete");
            var BookService = new BookService(new BookRepository());
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.D0:
                        Console.WriteLine("\nEnter Id...");
                        break;
                    case ConsoleKey.D1:
                        Console.WriteLine("\nEnter Title...");
                        break;
                    case ConsoleKey.D2:
                        Console.WriteLine("\nEnter Id & new Title...");
                        break;
                    case ConsoleKey.D3:
                        Console.WriteLine("\nEnter Id...");
                        break;
                    default:
                        break;
                }
            } while (key.Key != ConsoleKey.Escape);
            Console.ReadLine();
        }
    }
}
