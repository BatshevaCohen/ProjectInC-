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
        enum MenuOptions { Add = 1, Update, Show_One, Show_List, Exit }
        enum EntityOptions { BaseStation = 1, Drone, Custumer, Parcel, Exit }
        enum UpdateOptions { Drone_Name = 1, Stetion_Details, Customer_Details, Charge, Discharge, AssignParcelToDrone, Pickedup, Parcel_Supply_By_Drone, Exit }
        enum ListOptions { BaseStation = 1, Drone, Custumer, Parcel, UnAsignementParcel, AvailbleChagingStation, Exit }

        private static void ShowMenu()
        {
            MenuOptions menuOptions;
            EntityOptions entityOptions;
            UpdateOptions updateOptions;
            IBL.BO.BLObject bLObject = new();//constractor BLObject
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
                                IBL.BO.Station s = new ();
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
                                List<Drone> droneList = new() { };
                                s.AvailableChargingSpots = Position;
                                s.Location.Longitude = longitude;
                                s.Location.Latitude = latitude;
                                try
                                {
                                    bLObject.AddStation(s);
                                    
                                }
                                catch(Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
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

                                Drone d = new()
                                {
                                    Id = id_D,
                                    Model = model,
                                    Weight = (Weight)weight
                                };

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


                                Customer c = new()
                                {
                                    Id = id_C,
                                    Name = name_C,
                                    Phone = phone_C
                                };
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

                                Parcel p = new() { };
                                p.Sender.Id = id_Psender;
                                p.Resiver.Id = id_Reciver;
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
                                int  drone_id5;
                                    Console.WriteLine("Please enter Drone ID");
                                    int.TryParse(Console.ReadLine(), out drone_id5);
                                    Console.WriteLine("Please enter new name for the drone:");
                                   string name = Console.ReadLine();
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
                                    int Customer_id;
                                    string nameCustumer, phonCustumer;
                                    Console.WriteLine("Please enter custumer ID");
                                    int.TryParse(Console.ReadLine(), out Customer_id);
                                    Console.WriteLine("Enter new name custumer:");
                                    nameCustumer = Console.ReadLine();
                                    Console.WriteLine("Enter Phon's custumer:");
                                    phonCustumer= Console.ReadLine();
                                    bLObject.UpdateCustomer(Customer_id, nameCustumer, phonCustumer);
                                    break;
                                //send drone to Charge
                                case UpdateOptions.Charge:
                                    Console.WriteLine("Please enter Drone ID");
                                    int drone_id4;
                                    int.TryParse(Console.ReadLine(), out drone_id4);
                                    try
                                    {
                                        bLObject.UpdateChargeDrone(drone_id4);
                                    }
                                    catch(Exception ex)
                                    {
                                        Console.WriteLine(ex);
                                    }
                                    Console.WriteLine("\nDrone updated to- charge status successfully!\n");
                                    break;

                                case UpdateOptions.Discharge://release
                                    Console.WriteLine("Please enter Drone ID");
                                    int.TryParse(Console.ReadLine(), out drone_id5);
                                    Console.WriteLine("enter the time of charging");
                                    TimeSpan TimeOfCharging;
                                    TimeSpan.TryParse(Console.ReadLine(), out TimeOfCharging);
                                    try
                                    {
                                        bLObject.DischargeDrone(drone_id5, TimeOfCharging);
                                    }

                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex);
                                    }

                                    Console.WriteLine("\nDrone updated to- discharge successfully!\n");
                                    break;


                                case UpdateOptions.AssignParcelToDrone:
                                    Console.WriteLine("Please enter Drone ID");
                                    int drone_id;
                                    int.TryParse(Console.ReadLine(), out drone_id);
                                    try
                                    {
                                        bLObject.UpdateParcelToDrone(drone_id);
                                    }

                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex);
                                    }
                                    Console.WriteLine("\nParcel updated to drone successfully!\n");
                                    break;

                                case UpdateOptions.Pickedup:
                                    Console.WriteLine("Please enter Drone ID");
                                    int drone_id2;
                                    int.TryParse(Console.ReadLine(), out drone_id2);
                                    try
                                    {


                                        bLObject.UpdateParcelPickUpByDrone(drone_id2);
                                    }

                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex);
                                    }
                                    Console.WriteLine("\nParcel pick up updated successfully!\n");
                                    break;

                                case UpdateOptions.Parcel_Supply_By_Drone:
                                    Console.WriteLine("Please enter Drone ID");
                                    int drone_id6;
                                    int.TryParse(Console.ReadLine(), out drone_id6);
                                    try
                                    {
                                        bLObject.UpdateParcelSuppliedByDrone(drone_id6);
                                    }

                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex);
                                    }
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
                                Console.WriteLine("enter dron id");
                                //  Station GetStation(int requestedId);
                                bLObject.GetStation(Id_S).ToString();
                                break;
                            case EntityOptions.Drone:
                                int Id_D;//hh
                                int.TryParse(Console.ReadLine(), out Id_D);
                                Console.WriteLine(bLObject.GetDrone(Id_D).ToString());
                                break;
                            case EntityOptions.Custumer:
                                int Id_C;
                                int.TryParse(Console.ReadLine(), out Id_C);
                                Console.WriteLine(bLObject.GetCustomer(Id_C).ToString());
                                break;
                            case EntityOptions.Parcel:
                                int Id_P;
                                int.TryParse(Console.ReadLine(), out Id_P);
                                Console.WriteLine(bLObject.GetParcel(Id_P).ToString());
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
                                IEnumerable<Station> BaseStationList;
                                BaseStationList = bLObject.ShowStationList();
                                foreach (Station element in BaseStationList) //prints the elements in the list
                                    Console.WriteLine(element);
                                break;
                            // prints the list of the drones
                            case ListOptions.Drone:
                                IEnumerable<Drone> DroneList;
                                DroneList = bLObject.ShowDroneList();
                                foreach (Drone element in DroneList) //prints the elements in the list
                                    Console.WriteLine(element);
                                break;
                            // prints the list of the customers
                            case ListOptions.Custumer:
                                IEnumerable<Customer> CustomerList;
                                CustomerList = bLObject.ShowCustomerList();
                                foreach (Customer element in CustomerList) //prints the elements in the list
                                    Console.WriteLine(element);
                                break;
                            // prints the list of the parcels
                            case ListOptions.Parcel:
                                IEnumerable<Parcel> ParcelList;
                                ParcelList = bLObject.ShowParcelList();
                                foreach (Parcel element in ParcelList) //prints the elements in the list
                                    Console.WriteLine(element);
                                break;
                            // prints the list of the stations that available for charging
                            case ListOptions.AvailbleChagingStation:
                                IEnumerable<Station> ChargeableBaseStationList;
                                ChargeableBaseStationList =bLObject.ShowChargeableStationList();
                                foreach (Station element in ChargeableBaseStationList) //prints the elements in the list
                                    Console.WriteLine(element);
                                break;
                            // prints the list of the non associated parcel
                            case ListOptions.UnAsignementParcel:
                                bLObject.ShowNonAssociatedParcelList();
                                List<Parcel> NonAssociatedParcelList;
                                NonAssociatedParcelList = bLObject.ShowNonAssociatedParcelList();
                                foreach (Parcel element in NonAssociatedParcelList) //prints the elements in the list
                                    Console.WriteLine(element);
                                break;
                           
                        }
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
