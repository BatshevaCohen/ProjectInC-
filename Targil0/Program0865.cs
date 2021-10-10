using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome0865();
            Welcome5626();
            Console.ReadKey();
        }

        static partial void Welcome5626();
        private static void Welcome0865()
        {
            Console.WriteLine("Enter your name: ");
            string name;
            name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my firt console application", name);
        }
    }
}
