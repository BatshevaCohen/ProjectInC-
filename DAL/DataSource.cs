using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    namespace DO
    {
        public class DataSource
        {

            /// <summary>
            /// database of DO entities
            /// </summary>
            public static int OrdinalNumber = 1000000;
            internal static List<Drone> drones = new List<Drone>(2);
            internal static List<Station> Stations = new List<Station>(5);
            internal static List<Customer> customer = new List<Customer>(100);
            internal static List<Parcel> parcels = new List<Parcel>(1000);

            static Random r = new Random();

            public static object Station { get; set; }

            internal class config
            {
                

                internal static double Light { get => 10;}
                internal static double Available { get => 0; }
                internal static double Medium { get => 50; }
                internal static double Heavy { get => 150; }
                internal static double ChargingRate { get => 10.25; }
            }
            public static DateTime myDateTime()
            {
                DateTime myDateTime = new DateTime(r.Next(0, 60), 0);
                return myDateTime;
            }
            /// <summary>
            /// Initialize all the lists
            /// </summary>
            public static void Initialize()
            {
                // names of different entities 
                string[] arrDroneModel = new string[5] { "Drone1", "Drone2", "Drone3", "Drone4", "Drone5" };
                string[] arrStation = new string[2] { "station1", "station2" };
                string[] arrClientFirstName = new string[10] { "Michael", "Christopher", "Jessica", "Matthew", "Ashley", "Jennifer", "Joshua", "Yoni", "Daniel", "David" };

                //adding drones
                for (int i = 1; i <= 5; i++)
                {
                    drones.Add(new Drone()
                    {
                        Id = i * 1000,
                        Model = arrDroneModel[i - 1],
                        MaxWeight = RandomEnumValue<WeightCategories>(),
                        //Battery = r.Next(0, 100),
                        //Status = RandomEnumValue<DroneStatuses>()
                    });
                }
                
                //adding stations
                for (int i = 1; i <= 2; i++)
                {
                    Stations.Add(new Station()
                    {
                        Id = r.Next(20000, 100000),
                        Name = arrStation[i - 1],
                        ChargeSlots = r.Next(1, 100),
                        Longitude = r.Next(-180, 179) + r.NextDouble(),
                        Latitude = r.Next(-90, 89) + r.NextDouble()
                    });
                }
                //adding customers
                for (int i = 0; i < 10; i++)
                {
                    customer.Add(new Customer()
                    {
                        Id = i,
                        Name = arrClientFirstName[i],
                        Phone = "05" + r.Next(0, 8) + "-" + r.Next(1000000, 9999999),
                        Longitude = r.Next(-180, 179) + r.NextDouble(),
                        Latitude = r.Next(-90, 89) + r.NextDouble()
                    });
                }
                //adding parcels
                for (int i = 1; i <= 10; i++)
                {
                    //choose two different ids for sender and target from Customer's id
                    int senderId = r.Next(1, 10);
                    int targetId;
                    do
                    {
                        targetId = r.Next(1, 10);
                    } while (targetId == senderId);

                    parcels.Add(new Parcel()
                    {
                        Id = ++OrdinalNumber,    //serial number
                        SenderId = senderId,
                        Resiver = targetId,
                        Weight = RandomEnumValue<WeightCategories>(),
                        Priority = RandomEnumValue<Priorities>(),
                        DroneID = r.Next(1000, 5000),
                        Requested = myDateTime(),
                        Scheduled = myDateTime(),
                        PickedUp = myDateTime(),
                        Delivered = myDateTime()
                    });
                }
            }
            /// <summary>
            /// function for random enums
            /// from https://stackoverflow.com/questions/3132126/how-do-i-select-a-random-value-from-an-enumeration
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            private static T RandomEnumValue<T>()
            {
                var v = Enum.GetValues(typeof(T));
                return (T)v.GetValue(r.Next(v.Length));
            }
        }
    }
}

