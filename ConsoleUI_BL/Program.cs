using System;
using IBL;
using IBL.BO;
using IDAL;
using DalObject;
using System.Collections.Generic;



namespace ConsoleUI_BL
{
    class Program
    {
        static IBL bl = new IBL.BO.BLObject();
        enum MenuOptions { Add = 1, Update, Show_One, Show_List, Exit }
        enum EntityOptions { BaseStation = 1, Drone, Custumer, Parcel, Exit }
        enum UpdateOptions { Drone_Name = 1, Stetion_Details, Customer_Details, Charge, Discharge, Assignement, Pickedup, Parcel_Supply_By_Drone, Exit }
        enum ListOptions { BaseStation = 1, Drone, Custumer, Parcel, UnAsignementParcel, AvailbleChagingStation, Exit }

        private static void ShowMenu()
        {
            MenuOptions menuOptions;
            EntityOptions entityOptions;
            UpdateOptions updateOptions;
            IBL.BO.BLObject bLObject = new IBL.BO.BLObject();//constractor BLObject
            do
            {
                Console.WriteLine("WELCOME!");
                Console.WriteLine("option:\n 1- Add,\n 2- Update,\n 3- Show_One,\n 4- Show_List,\n 5- Exit,\n");
                menuOptions = (MenuOptions)int.Parse(Console.ReadLine());
                switch (menuOptions)
                {
                    //adding options
                    case MenuOptions.Add:
                        Console.WriteLine("Adding option:\n 1-BaseStation,\n 2-Drone,\n 3-Custumer,\n 4-Parcel,\n 5-Exit");

                        entityOptions = (EntityOptions)int.Parse(Console.ReadLine());
                        switch (entityOptions)
                        {
                            //add a new station
                            case EntityOptions.BaseStation:
                                IBL.BO.Station s = new Station();
                                Console.WriteLine("Please insert ID, StationName (string), longitude, latitude, and charging level ");
                                int id_S;
                                int.TryParse(Console.ReadLine(), out id_S);
                                s.Id = id_S;
                                string StationName = Console.ReadLine();
                                s.Name = StationName;
                                double longitude;
                                double.TryParse(Console.ReadLine(), out longitude);
                                double latitude;
                                double.TryParse(Console.ReadLine(), out latitude);
                                s.Location.Longitude = longitude;
                                s.Location.Latitude = latitude;
                                int Position;
                                int.TryParse(Console.ReadLine(), out Position);
                                List<Drone> droneList= new List<Drone>();
                                s.AvailableChargingSpots = droneList;
                                bLObject.AddStation(s);
                                Console.WriteLine("\nBase station added successfully! \n");
                                break;
                            //add a new drone
                            case EntityOptions.Drone:
                                Console.WriteLine("please enter ID, Model (string), Weight category and Number of station for initial charging");
                                int id_D, weight, stationId;
                                int.TryParse(Console.ReadLine(), out id_D);
                                string model = Console.ReadLine();
                                Console.WriteLine("enter WeightCategories 1-Light 2-Medium, 3-Heavy");
                                int.TryParse(Console.ReadLine(), out weight);
                                int.TryParse(Console.ReadLine(), out stationId);

                                Drone d = new Drone();
                                d.Id = id_D;
                                d.Model = model;
                                d.Weight = (Weight)weight;

                                bLObject.AddDrone(d, stationId);
                                Console.WriteLine("\nDrone added successfully! \n");
                                break;
                            //add a new customer
                            case EntityOptions.Custumer:
                                Console.WriteLine("please enter Customer ID, Name, Phone number, Longitude and Latitude");
                                int id_C;
                                int.TryParse(Console.ReadLine(), out id_C);
                                string name_C = Console.ReadLine();
                                //נצטרך לבדוק תקינות של מס טלפון
                                Console.WriteLine("enter phone number");
                                string phone_C = Console.ReadLine();
                                Console.WriteLine("enter longitude and latitude");
                                double longitude_C;
                                double.TryParse(Console.ReadLine(), out longitude_C);
                                double latitude_C;
                                double.TryParse(Console.ReadLine(), out latitude_C);

                                Customer c = new Customer();
                                c.Id = id_C;
                                c.Name = name_C;
                                c.Phone = phone_C;
                                c.Location.Latitude = latitude_C;
                                c.Location.Longitude = longitude_C;
                                bLObject.AddCustomer(c);
                                Console.WriteLine("\nCustomer added successfully! \n");
                                break;
                            //add a new parcel
                            case EntityOptions.Parcel:
                                Console.WriteLine("Please enter the sender's ID");
                                int id_Psender, id_Reciver, weight_P, priority_P;
                                int.TryParse(Console.ReadLine(), out id_Psender);
                                Console.WriteLine("Please enter target ID");
                                int.TryParse(Console.ReadLine(), out id_Reciver);
                                Console.WriteLine("Please enter parcel weight: 1-Light, 2-Medium, 3-Heavy");
                                int.TryParse(Console.ReadLine(), out weight_P);
                                Console.WriteLine("Please enter parcel priority: 1-Regular, 2-Fast, 3-Emergency");
                                int.TryParse(Console.ReadLine(), out priority_P);
                                
                                Parcel p = new Parcel();
                                p.Sender.Id = id_Psender;
                                p.Reciver.Id = id_Reciver;
                                p.Weight = (Weight)weight_P;
                                p.Priority = (Priority)priority_P;
                                bLObject.AddParcel(p);
                                Console.WriteLine("\nParcel added successfully! \n");
                                break;

                            // EXIT
                            case EntityOptions.Exit:
                                DalObject.DalObject.Exit();
                                break;
                        }
                        break;

                    //update functions
                    case MenuOptions.Update:
                        {
                            //enum UpdateOptions { Drone_Name = 1, Stetion_Details, Customer_Details, Charge, Discharge, Assignement, Pickedup, Parcel_Supply_By_Drone, Exit }
                            Console.WriteLine("Updating option:\n 1-Drone's name,\n 2-Station details,\n 3-Customer details,\n 4-Send drone to charge,\n 5-Discharge drone,\n 6-Parcel to drone,\n 7-Parcel pickedup by drone,\n 8-Parcel supply by drone,\n 9- Exit");
                            updateOptions = (UpdateOptions)int.Parse(Console.ReadLine());
                            switch (updateOptions)
                            {
                                //update drone's name
                                case UpdateOptions.Drone_Name:
                                    int drone_id5;
                                    String name;
                                    Console.WriteLine("Please enter Drone ID");
                                    int.TryParse(Console.ReadLine(), out drone_id5);
                                    Console.WriteLine("Please enter new name for the drone:");
                                    name = Console.ReadLine();
                                    bLObject.UpdateDroneName(drone_id5, name);
                                    Console.WriteLine("Drone's name updated successfully!");
                                    break;
                                //update station details:
                                case UpdateOptions.Stetion_Details:
                                    int station_id, charging_spots;
                                    string station_name;
                                    Console.WriteLine("Please enter station ID");
                                    int.TryParse(Console.ReadLine(), out station_id);
                                    Console.WriteLine("Please enter station name");
                                    station_name = Console.ReadLine();
                                    Console.WriteLine("Enter number of charging spots:");
                                    int.TryParse(Console.ReadLine(), out charging_spots);
                                    bLObject.UpdateStetion(station_id, station_name, charging_spots);
                                    Console.WriteLine("Station updated successfully!");
                                    break;

                                case UpdateOptions.Customer_Details:

                                    break;

                                case UpdateOptions.Charge:
                                    Console.WriteLine("Please enter Drone ID");
                                    int drone_id4;
                                    int.TryParse(Console.ReadLine(), out drone_id4);
                                    Console.WriteLine("choose a station for charging");
                                    // show the list os stations to choose from
                                    dalobject.ShowStationList();
                                    int station_id;
                                    int.TryParse(Console.ReadLine(), out station_id);
                                    dalobject.UpdateDroneToCharge(drone_id4, station_id);
                                    Console.WriteLine("\nDrone updated to- charge status successfully!\n");
                                    break;

                                case UpdateOptions.Discharge:
                                    Console.WriteLine("Please enter Drone ID");
                                    int drone_id5;
                                    int.TryParse(Console.ReadLine(), out drone_id5);
                                    Console.WriteLine("choose a station for charging");
                                    int station_id_discharge;
                                    int.TryParse(Console.ReadLine(), out station_id_discharge);
                                    dalobject.DischargeDrone(drone_id5, station_id_discharge);
                                    Console.WriteLine("\nDrone updated to- discharge status successfully!\n");
                                    break;


                                case UpdateOptions.Assignement:
                                    Console.WriteLine("Please enter Drone ID");
                                    int drone_id;
                                    int.TryParse(Console.ReadLine(), out drone_id);
                                    Console.WriteLine("Please enter Parcel ID");
                                    int parcel_id;
                                    int.TryParse(Console.ReadLine(), out parcel_id);
                                    dalobject.UpdateParcelToDrone(parcel_id, drone_id);
                                    Console.WriteLine("\nParcel updated to drone successfully!\n");
                                    break;

                                case UpdateOptions.Pickedup:
                                    Console.WriteLine("Please enter Drone ID");
                                    int drone_id2;
                                    int.TryParse(Console.ReadLine(), out drone_id2);
                                    Console.WriteLine("Please enter Parcel ID");
                                    int parcel_id2;
                                    int.TryParse(Console.ReadLine(), out parcel_id2);
                                    dalobject.UpdateParcelPickedupByDrone(parcel_id2, drone_id2);
                                    Console.WriteLine("\nParcel pick up updated successfully!\n");
                                    break;

                                case UpdateOptions.Parcel_Supply_By_Drone:
                                    Console.WriteLine("Please enter Customer ID");
                                    int customer_id;
                                    int.TryParse(Console.ReadLine(), out customer_id);
                                    Console.WriteLine("Please enter Parcel ID");
                                    int parcel_id3;
                                    int.TryParse(Console.ReadLine(), out parcel_id3);
                                    bLObject.UpdateDeliveryToCustomer(parcel_id3, customer_id);
                                    Console.WriteLine("\nParcel updated to customer successfully!\n");
                                    break;

                                case UpdateOptions.Exit:
                                    DalObject.DalObject.Exit();
                                    break;
                            }
                            break;
                        }

                    // show options
                    case MenuOptions.Show_One:
                        Console.WriteLine("View item options: \n 1- base station \n 2- Drone\n 3- Custumer\n 4- Parcel\n 5- Exit\n");
                        entityOptions = (EntityOptions)int.Parse(Console.ReadLine());
                        Console.WriteLine($"Enter a requested {entityOptions} id");
                        switch (entityOptions)
                        {
                            case EntityOptions.BaseStation:

                                int Id_S;
                                int.TryParse(Console.ReadLine(), out Id_S);
                                Console.WriteLine(dalobject.GetBaseStation(Id_S));
                                break;
                            case EntityOptions.Drone:
                                int Id_D;
                                int.TryParse(Console.ReadLine(), out Id_D);
                                Console.WriteLine(dalobject.GetDrone(Id_D));
                                break;
                            case EntityOptions.Custumer:
                                int Id_C;
                                int.TryParse(Console.ReadLine(), out Id_C);
                                Console.WriteLine(dalobject.GetCustomer(Id_C));
                                break;
                            case EntityOptions.Parcel:
                                int Id_P;
                                int.TryParse(Console.ReadLine(), out Id_P);
                                Console.WriteLine(dalobject.GetParcel(Id_P));
                                break;
                            case EntityOptions.Exit:
                                DalObject.DalObject.Exit();
                                break;
                        }
                        int requestion;
                        int.TryParse(Console.ReadLine(), out requestion);
                        break;
                    // show_list options
                    case MenuOptions.Show_List:
                        Console.WriteLine(" List options:\n 1- BaseStation  \n 2- Drone \n 3- Custumer\n 4- Parcel\n 5- UnAsignementParcel\n 6- AvailbleChagingStation\n 7- Exit \n");
                        ListOptions listOptions = (ListOptions)int.Parse(Console.ReadLine());
                        switch (listOptions)
                        {
                            // prints the list of the base stations
                            case ListOptions.BaseStation:
                                List<Station> BaseStationList = new List<Station>();
                                BaseStationList = dalobject.ShowStationList();
                                foreach (Station element in BaseStationList) //prints the elements in the list
                                    Console.WriteLine(element);
                                break;
                            // prints the list of the drones
                            case ListOptions.Drone:
                                List<Drone> DroneList = new List<Drone>();
                                DroneList = dalobject.ShowDroneList();
                                foreach (Drone element in DroneList) //prints the elements in the list
                                    Console.WriteLine(element);
                                break;
                            // prints the list of the customers
                            case ListOptions.Custumer:
                                List<Customer> CustomerList = new List<Customer>();
                                CustomerList = dalobject.ShowCustomerList();
                                foreach (Customer element in CustomerList) //prints the elements in the list
                                    Console.WriteLine(element);
                                break;
                            // prints the list of the parcels
                            case ListOptions.Parcel:
                                List<Parcel> ParcelList = new List<Parcel>();
                                ParcelList = dalobject.ShowParcelList();
                                foreach (Parcel element in ParcelList) //prints the elements in the list
                                    Console.WriteLine(element);
                                break;
                            // prints the list of the stations that available for charging
                            case ListOptions.AvailbleChagingStation:
                                List<Station> ChargeableBaseStationList = new List<Station>();
                                ChargeableBaseStationList = (List<Station>)dalobject.ShowChargeableStationList();
                                foreach (Station element in ChargeableBaseStationList) //prints the elements in the list
                                    Console.WriteLine(element);
                                break;
                            // prints the list of the non associated parcel
                            case ListOptions.UnAsignementParcel:
                                dalobject.ShowNonAssociatedParcelList();
                                List<Parcel> NonAssociatedParcelList = new List<Parcel>();
                                NonAssociatedParcelList = dalobject.ShowNonAssociatedParcelList();
                                foreach (Parcel element in NonAssociatedParcelList) //prints the elements in the list
                                    Console.WriteLine(element);
                                break;
                            case ListOptions.Exit:
                                DalObject.DalObject.Exit();
                                break;
                        }
                        break;
                            // Exit
                            case DistanceOptions.Exit:
                                DalObject.DalObject.Exit();
                                break;
                        break;
                    case MenuOptions.Exit:
                        break;
                }

            }
            while (menuOptions != MenuOptions.Exit);
        }
            static void Main(string[] args)
        {
            ShowMenu();
            ////FROM ELIEZER
            //Customer customer;
            //Console.WriteLine("Please enter customer ID");
            //int id;
            //int.TryParse(Console.ReadLine(), out id);
            //try
            //{
            //    customer = bl.GetCustomer(id);
            //}
            //catch (CustomerException exception)
            //{
            //    Console.WriteLine(exception.Message);
            //}
        }
    }
}
