using System;
using IBL.BO;

namespace ConsoleUI_BL
{
    class Program
    {
        static IBl bl = new BL.BLObject();
        static void Main(string[] args)
        {

            //FROM ELIEZER
            Customer customer;
            Console.WriteLine("Please enter customer ID");
            int id;
            int.TryParse(Console.ReadLine(), out id);
            try
            {
                customer = bl.GetCustomer(id);
            }
            catch (CustomerException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
