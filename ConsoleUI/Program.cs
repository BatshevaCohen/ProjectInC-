using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Stam();
            Console.ReadKey();
        }

        private static void Stam()
        {
            IDAL.DO.Client client = new IDAL.DO.Client
            {
                ID = 123,
                Name = "kuku",
                Telephone = "0556789147",
                Latitude = -36.123456,
                Longitude = 29.654321
            };
            Console.WriteLine(client);
        }
    }
}
