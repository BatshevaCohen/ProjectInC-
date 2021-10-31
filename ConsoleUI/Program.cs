using System;
using IDAL;
using DalObject;
using IDAL.DO;

namespace ConsoleUI
{
    class Program
    {
        private static Enums.WeightCategories maxWeight;
        private static Enums.WeightCategories wheight;
        private static Enums.Priorities priority;

        enum MenuOptions { Add = 1, Update, Show_One, Show_List, Exit }
        enum EntityOptions { BaseStation = 1, Drone, Custumer, Parcel, Exit }
        enum UpdateOptions { Assignement = 1, Pickedup, Delivery, Recharge, Discharge, Exit }
        enum ListOptions { BaseStation = 1, Drone, Custumer, Parcel, UnAsignementParcel, AvailbleChagingStation, Exit }

        private static void ShowMenu()
        {

            MenuOptions menuOptions;
            EntityOptions entityOptions;
            UpdateOptions updateOptions;
            DalObject.DalObject dalobject = new DalObject.DalObject();



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
                                Station s = new Station();
                                Console.WriteLine("Please insert ID, StationName, longitude latitude and Position ");
                                int id_S;
                                int.TryParse(Console.ReadLine(), out id_S);
                                string StationName = Console.ReadLine();
                                double longitude;
                                double.TryParse(Console.ReadLine(), out longitude);
                                double latitude;
                                double.TryParse(Console.ReadLine(), out latitude);
                                int Position;
                                int.TryParse(Console.ReadLine(), out Position);

                                s.Id = id_S;
                                s.Latitude = latitude;
                                s.Longitude = longitude;
                                s.ChargeSlots = Position;
                                dalobject.AddBaseStation(s);
                                break;

                            case EntityOptions.Drone:
                                Console.WriteLine(" please enter id, model, maxweight, the status and batery");
                                int id_D;
                                int.TryParse(Console.ReadLine(), out id_D);
                                string model = Console.ReadLine();
                                Console.WriteLine("enter DroneStatuses 1-Available 2-Maintenance, 3-Shipping");
                                int status;
                                int.TryParse(Console.ReadLine(), out status);

                                Console.WriteLine("enter WeightCategories 1-Light 2-Medium, 3-Heavy");
                                int Weight;
                                int.TryParse(Console.ReadLine(), out Weight);
                                double battery;
                                double.TryParse(Console.ReadLine(), out battery);

                                Drone d = new Drone();

                                d.Id = id_D;
                                d.Model = model;
                                d.Battery = battery;
                                d.Staus = (Enums.DroneStatuses)status;
                                d.MaxWeight = (Enums.WeightCategories)maxWeight;
                                dalobject.AddDrone(d);
                                break;

                            case EntityOptions.Custumer:
                                Console.WriteLine(" please enter ID, name, phone, longitude, latitude ");
                                int id_C;
                                int.TryParse(Console.ReadLine(), out id_C);
                                string name_C = Console.ReadLine();
                                //נצטרך לבדוק תקינות של מס טלפון
                                Console.WriteLine("enter phone number in format ###-#######");
                                string phone_C = Console.ReadLine();
                                Console.WriteLine("enter longitude and latitude");
                                double longitude_C;
                                double.TryParse(Console.ReadLine(), out longitude_C);
                                double latitude_C;
                                double.TryParse(Console.ReadLine(), out latitude_C);

                                Customer c = new Customer();
                                c.Id = id_C;
                                c.Name = name_C;
                                c.Latitude = latitude_C;
                                c.Longitude = longitude_C;
                                dalobject.AddCustomer(c);
                                break;

                            case EntityOptions.Parcel:
                                Console.WriteLine("Please enter parcel ID");
                                int id_P;
                                int.TryParse(Console.ReadLine(), out id_P);
                                Console.WriteLine("Please enter the sender's ID");
                                int id_Psender;
                                int.TryParse(Console.ReadLine(), out id_Psender);
                                Console.WriteLine("Please enter target ID");
                                int id_Ptarget;
                                int.TryParse(Console.ReadLine(), out id_Ptarget);
                                Console.WriteLine("Please enter parcel weight: 1-Light, 2-Medium, 3-Heavy");
                                int weight_P;
                                int.TryParse(Console.ReadLine(), out weight_P);
                                Console.WriteLine("Please enter parcel priority: 1-Regular, 2-Fast, 3-Emergency");
                                int priority_P;
                                int.TryParse(Console.ReadLine(), out priority_P);
                                Console.WriteLine("Please enter drone ID");
                                int id_Pdrone;
                                int.TryParse(Console.ReadLine(), out id_Pdrone);
                                Console.WriteLine("Please enter time to prepare a package for delivery");
                                DateTime requested_P;
                                DateTime.TryParse(Console.ReadLine(), out requested_P);
                                DateTime steduled_P;
                                DateTime.TryParse(Console.ReadLine(), out steduled_P);
                                DateTime pickedUp_P;
                                DateTime.TryParse(Console.ReadLine(), out pickedUp_P);
                                DateTime delivary_P;
                                DateTime.TryParse(Console.ReadLine(), out delivary_P);
                                Parcel p = new Parcel();
                                p.Id = id_P;
                                p.SenderId = id_Psender;
                                p.TargetId = id_Ptarget;
                                p.Weight = (Enums.WeightCategories)weight_P;
                                p.Priority = (Enums.Priorities)priority_P;
                                p.Requested = requested_P;
                                p.DroneID = id_Pdrone;
                                p.Scheduled = steduled_P;
                                p.PickedUp = pickedUp_P;
                                p.Delivered = delivary_P;

                                dalobject.AddParcel(p);

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
                                    dalobject.UpdateParcelToDrone(parcel_id, drone_id);
                                    break;

                                case UpdateOptions.Pickedup:
                                    Console.WriteLine("Please enter Drone ID");
                                    int drone_id2 = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Please enter Parcel ID");
                                    int parcel_id2 = int.Parse(Console.ReadLine());
                                    dalobject.UpdateParcelPickedupByDrone(parcel_id2, drone_id2);
                                    break;

                                case UpdateOptions.Delivery:
                                    Console.WriteLine("Please enter Customer ID");
                                    int customer_id = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Please enter Parcel ID");
                                    int parcel_id3 = int.Parse(Console.ReadLine());
                                    dalobject.UpdateDeliveryToCustomer(parcel_id3, customer_id);
                                    break;

                                case UpdateOptions.Recharge:
                                    Console.WriteLine("Please enter Drone ID");
                                    int drone_id4 = int.Parse(Console.ReadLine());
                                    Console.WriteLine("choose a station for charging");
                                    //פה צריך להציג רשימת תחנות פלוס איי די שלהן
                                    int station_id = int.Parse(Console.ReadLine());
                                    dalobject.UpdateDroneToCharge(drone_id4, station_id);
                                    break;

                                case UpdateOptions.Discharge:
                                    Console.WriteLine("Please enter Drone ID");
                                    int drone_id5 = int.Parse(Console.ReadLine());
                                    Console.WriteLine("choose a station for charging");
                                    int station_id_discharge = int.Parse(Console.ReadLine());
                                    dalobject.DischargeDrone(drone_id5, station_id_discharge);
                                    break;

                                case UpdateOptions.Exit:
                                    DalObject.DalObject.Exit();
                                    break;
                            }
                            break;
                        }

                    // show options
                    case MenuOptions.Show_One:
                        Console.WriteLine("View item options: \n 1- base station \n 2-Drone\n 3- Custumer\n 4- Parcel\n 5- Exit\n");
                        entityOptions = (EntityOptions)int.Parse(Console.ReadLine());
                        Console.WriteLine($"Enter a requested {entityOptions} id");
                        int requestion;
                        int.TryParse(Console.ReadLine(), out requestion);

                        break;
                        switch (entityOptions)
                        {
                            case EntityOptions.BaseStation:
                                Console.WriteLine("Please enter station id");
                                int stationID = int.Parse(Console.ReadLine());
                                dalobject.ShowBaseStation(stationID);
                                break;

                            case EntityOptions.Drone:
                                Console.WriteLine("Please enter drone id");
                                int droneID = int.Parse(Console.ReadLine());
                                dalobject.ShowDrone(droneID);
                                break;

                            case EntityOptions.Custumer:
                                Console.WriteLine("Please enter customer id");
                                int customerID = int.Parse(Console.ReadLine());
                                dalobject.ShowBaseStation(customerID);
                                break;

                            case EntityOptions.Parcel:
                                Console.WriteLine("Please enter parcel id");
                                int parcelID = int.Parse(Console.ReadLine());
                                dalobject.ShowBaseStation(parcelID);
                                break;

                            case EntityOptions.Exit:
                                DalObject.DalObject.Exit();
                                break;
                        }
                        break;
                    case MenuOptions.Show_List:
                        Console.WriteLine(" List options:\n 1-BaseStation  \n 2- Drone \n 3- Custumer\n 4- Parcel\n 5- UnAsignementParcel\n 6- AvailbleChagingStation\n 7- Exit \n");
                        ListOptions listOptions = (ListOptions)int.Parse(Console.ReadLine());
                        switch (listOptions)
                        {
                            case ListOptions.BaseStation:
                                dalobject.ShowBaseStationList();
                                break;
                            case ListOptions.Drone:
                                dalobject.ShowDroneList();
                                break;
                            case ListOptions.Custumer:
                                dalobject.ShowCustomerList();
                                break;
                            case ListOptions.Parcel:
                                dalobject.ShowParcelList();
                                break;
                            case ListOptions.AvailbleChagingStation:
                                dalobject.ShowNonAssociatedParcelList();
                                break;
                            case ListOptions.UnAsignementParcel:
                                dalobject.ShowChargeableBaseStationList();
                                break;
                        }
                        break;
                    case MenuOptions.Exit:
                        break;
                }

            }
            while (menuOptions != MenuOptions.Exit);
        }
        static void Main(string[] arg)
        {
            new DalObject.DalObject();
            ShowMenu();
        }
    
    } 
}
         