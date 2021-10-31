using System;
using IDAL;
using DalObject;

namespace ConsoleUI
{
     class Program
    {
        enum MenuOptions { Add, Update, Show_One, Show_List , Exit}
        enum EntityOptions { BaseStation, Drone, Custumer, Parcel ,Exit }
        enum UpdateOptions { Assignement, Pickedup, Delivery, Recharge, Discharge, Exit }
       // enum ListOptions { BaseStation, Drone, Custumer, Parcel, UnAsignementParcel, AvailbleChagingStation, Exit }

        private static void ShowMenu()
        {
            
            MenuOptions menuOptions;
            EntityOptions entityOptions;
            UpdateOptions updateOptions;
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
                                Console.WriteLine(" please enter id, model, maxweight, the status and batery");
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
                                Console.WriteLine(" please enter ID, name, phone, longitude, latitude ");
                                int id_C = int.Parse(Console.ReadLine());
                                string name_C = Console.ReadLine();
                                //נצטרך לבדוק תקינות של מס טלפון
                                Console.WriteLine("enter phone number in format ###-#######");
                                string phone_C = Console.ReadLine();
                                Console.WriteLine("enter longitude and latitude");
                                double longitude_C = double.Parse(Console.ReadLine());
                                double latitude_C = double.Parse(Console.ReadLine());
                                dalobject.AddCustomer(id_C, name_C, phone_C, longitude_C, latitude_C);
                                break;

                            case EntityOptions.Parcel:
                                Console.WriteLine("Please enter parcel ID");
                                int id_P = int.Parse(Console.ReadLine());
                                Console.WriteLine("Please enter the sender's ID");
                                int id_Psender = int.Parse(Console.ReadLine());
                                Console.WriteLine("Please enter target ID");
                                int id_Ptarget = int.Parse(Console.ReadLine());
                                Console.WriteLine("Please enter parcel weight: 1-Light, 2-Medium, 3-Heavy");
                                int weight_P = int.Parse(Console.ReadLine());
                                Console.WriteLine("Please enter parcel priority: 1-Regular, 2-Fast, 3-Emergency");
                                int priority_P = int.Parse(Console.ReadLine());
                                Console.WriteLine("Please enter drone ID");
                                int id_Pdrone = int.Parse(Console.ReadLine());
                                Console.WriteLine("Please enter time to prepare a package for delivery");
                                DateTime requested_P = DateTime.Parse(Console.ReadLine());

                                //מה עושים עם שאר הזמנים? הם מוכנסים רק בהמשך


                                break;

                            case EntityOptions.Exit:
                                DalObject.DalObject.Exit();
                                break;
                        }
                        break;

                    case MenuOptions.Update:
                        {
                            Console.WriteLine("Updating option:\n 1-Parcel to drone,\n 2-Parcel pickedup by drone,\n 3-Supply parcel to customer,\n 4-Send drone to charge,\n 5-Discharge drone, \n 6- Exit");
                            updateOptions = (UpdateOptions)int.Parse(Console.ReadLine());
                            switch (updateOptions)
                            {
                                case UpdateOptions.Assignement:
                                    Console.WriteLine("Please enter Drone ID");
                                    int drone_id = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Please enter Parcel ID");
                                    int parcel_id = int.Parse(Console.ReadLine());


                                    break;

                                case UpdateOptions.Pickedup:


                                    break;

                                case UpdateOptions.Delivery:


                                    break;

                                case UpdateOptions.Recharge:


                                    break;

                                case UpdateOptions.Discharge:


                                    break;

                                case UpdateOptions.Exit:
                                    DalObject.DalObject.Exit();
                                    break;
                            }
                            break;
                        }
                }






            }
            while (true);
            {

            }
}
        }
    
    }