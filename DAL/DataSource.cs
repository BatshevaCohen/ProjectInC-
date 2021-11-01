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

          

            internal static List<Drone> drones = new List<Drone>(2);
            internal static List<Station> Stations = new List<Station>(5);
            internal static List<Customer> customer = new List<Customer>(100);
            internal static List<Parcel> parcels = new List<Parcel>(1000);
            static Random r = new Random();

            internal class config
            {
                public static int OrdinalNumber=0;
            }
            static T RandomEnumValue<T>()
            {
                var v = Enum.GetValues(typeof(T));
                return (T)v.GetValue(r.Next(v.Length));
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

                Drone d = new Drone();
                string[] arrDroneModel = new string[5] { "Drone1", "Drone2", "Drone3", "Drone4", "Drone5" };
                for (int i = 1; i <= 5; i++)
                {
                    
                    d.Id = i;
                    d.Model = arrDroneModel[i-1];
                    d.MaxWeight = RandomEnumValue<Enums.WeightCategories>();
                    d.Battery = r.Next(0, 100);
                    d.Staus = RandomEnumValue<Enums.DroneStatuses>();
                    drones.Add(d);
                    
                  
                }
                string[] arrStation = new string[2] { "station1", "station2" };
                Station b = new Station();
                for (int i = 1; i <= 2; i++)
                {
                    b.Id = i;
                    b.Name = arrStation[i-1];
                    b.ChargeSlots = r.Next(1, 100);
                    b.Longitude = r.Next(-180, 179) + r.NextDouble();
                    b.Latitude = r.Next(-90, 89) + r.NextDouble();
                   Stations.Add(b);
                    //Console.WriteLine(Stations);
                }
                Customer c = new Customer();
                string[] arrClientFirstName = new string[10] { "Michael", "Christopher", "Jessica", "Matthew", "Ashley", "Jennifer", "Joshua", "Yoni", "Daniel", "David" };
                for (int i = 0; i < 10; i++)
                {
                    c.Id = i;
                    c.Name = arrClientFirstName[i];
                    c.Phone = "05" + r.Next(0, 8) + "-" + r.Next(1000000, 9999999);
                    c.Longitude = r.Next(-180, 179) + r.NextDouble();
                    c.Latitude = r.Next(-90, 89) + r.NextDouble();
                    customer.Add(c);
                  
                }
                Parcel p = new Parcel();
                for (int i = 1; i <= 10; i++)
                {
                    p.Id = i;
                    p.SenderId = r.Next(1, 10000);
                    p.TargetId = r.Next(1, 10000);
                    p.Weight = RandomEnumValue<Enums.WeightCategories>();
                    p.Priority = RandomEnumValue<Enums.Priorities>();
                    p.DroneID = r.Next(1, 1000);
                    p.Requested = myDateTime();
                    p.Scheduled = myDateTime();
                    p.PickedUp = myDateTime();
                    p.Delivered = myDateTime();
                    config.OrdinalNumber = i + 1;
                    i++;
                    parcels.Add(p);
                }
            }

           

        }
    }
}

