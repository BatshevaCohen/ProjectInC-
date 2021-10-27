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
        class DataSource
        {
            internal static List<Drone> drones = new List<Drone>(10);
            internal static List<BaseStation> baseStations = new List<BaseStation>(5);
            internal static List<Customer> customer = new List<Customer>(100);
            internal static List<Package> packages = new List<Package>(1000);
            static Random r = new Random();

            internal class config
            {
                internal static int IndexDrone = 0;
                internal static int IndexBasestation = 0;
                internal static int IndexClient = 0;
                internal static int IndexPackage = 0;
                internal static int OrdinalNumber;
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
                static void Initialize()
                {

                    Drone d = new Drone();
                    for (int i = 1; i <= 5; i++)
                    {
                        d.ID = r.Next(100000000, 999999999);
                        d.Model = r.Next(1, 100);
                        d.QWeight = RandomEnumValue<Enums.Weight>();
                        d.Battery = r.Next(0, 100);
                        d.State = RandomEnumValue<Enums.DroneState>();
                    }

                    BaseStation b = new BaseStation();
                    string[] arrStationName = new string[2] { "station1", "station2" };
                    for (int i = 1; i <= 2; i++)
                    {
                        b.ID = i;
                        b.Name = arrStationName[i];
                        b.NumColumns = r.Next(1, 100);
                        b.Longitude = r.Next(-180, 179) + r.NextDouble();
                        b.Latitude = r.Next(-90, 89) + r.NextDouble();
                    }
                    Customer c = new Customer();
                    string[] arrClientFirstName = new string[10] { "Michael", "Christopher", "Jessica", "Matthew", "Ashley", "Jennifer", "Joshua", "Yoni", "Daniel", "David" };
                    for (int i = 0; i < 10; i++)
                    {
                        c.ID = i;
                        c.Name = arrClientFirstName[i];
                        c.Telephone = "05" + r.Next(0, 8) + "-" + r.Next(1000000, 9999999);
                        c.Longitude = r.Next(-180, 179) + r.NextDouble();
                        c.Latitude = r.Next(-90, 89) + r.NextDouble();


                    }
                    Package p = new Package();
                    for (int i = 1; i <= 10; i++)
                    {
                        p.ID = i;
                        p.SendingCustomer = r.Next(1, 10000);
                        p.GettingCustomer = r.Next(1, 10000);
                        p.PWeight = RandomEnumValue<Enums.Weight>();
                        p.PPriority = RandomEnumValue<Enums.Priority>();
                        p.DroneID = r.Next(1, 1000);
                        p.Create = myDateTime();
                        p.Affiliation = myDateTime();
                        p.Collection = myDateTime();
                        p.Supply = myDateTime();
                        OrdinalNumber = i + 1;
                        i++;
                    }
                }

            }
        }
    }
}
