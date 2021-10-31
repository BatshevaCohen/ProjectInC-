using System;
using IDAL;
using DalObject;

namespace ConsoleUI
{
     class Program
    {
        enum MenuOptions { Add, Update, Show_One, Show_List , Exit}
        enum EntityOptions { BaseStation, Drone, Custumer, Parcel ,Exit }
     //   enum UpdateOptions { Assignement, Pickedup, Delivery, Recharge, Exit }
       // enum ListOptions { BaseStation, Drone, Custumer, Parcel, UnAsignementParcel, AvailbleChagingStation, Exit }

        private static void ShowMenu()
        {
            
            MenuOptions menuOptions;
            EntityOptions entityOptions;
           DalObject.DalObject dalobject=new DalObject.DalObject();
            
            
            do
            {
                Console.WriteLine("WELCOME!");
                Console.WriteLine("option:\n 1-Add,\n 2-Update,\n 3-Show_One,\n 4-Show_List,\n 5- Exit,\n");
                menuOptions = (MenuOptions)int.Parse(Console.ReadLine());
                switch (menuOptions)
                {
                    case MenuOptions.Add:
                        Console.WriteLine("Adding option:\n 1-BaseStation,\n 2-Drone,\n 3-Custumer,\n 4-Parcel,\n 5-Exit");

                        entityOptions = (EntityOptions)int.Parse(Console.ReadLine());
                        switch (entityOptions)
                        {
                            case EntityOptions.BaseStation:
                                
                                Console.WriteLine("Please insert ID, StationName, longitude latitude and Position ");
                                int id_S = int.Parse(Console.ReadLine());
                                string StationName = Console.ReadLine();
                                double longitude = double.Parse(Console.ReadLine());
                                double latitude = double.Parse(Console.ReadLine());
                                int Position;
                                int.TryParse(Console.ReadLine(), out Position);
                                dalobject.AddBaseStation(id_S, StationName, longitude, latitude, Position);

                                break;

                            case EntityOptions.Drone:
                                Console.WriteLine(" plese enter id, model, maxweight, the status and batery");
                                int id_D = int.Parse(Console.ReadLine());
                                string model = Console.ReadLine();
                                Console.WriteLine("enter DroneStatuses 1-Available 2-Maintenance, 3-Shipping");
                               int status= int.Parse(Console.ReadLine());
                                Console.WriteLine("enter WeightCategories 1-Light 2-Medium, 3-Heavy");
                                int Weight= int.Parse(Console.ReadLine());
                                double battery = double.Parse(Console.ReadLine());
                                dalobject.AddDrone(id_D, model, battery, Weight, status);
                                break;
                            case EntityOptions.Custumer:
                                Console.WriteLine(" plese enter id, name, pone ");

                                break;

                        }
                        break;
                }






            }
            while (true);
            {

            }
}
        }
    
    }