using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    namespace DO
    {
        class DataSource
        {
            internal static List<Drone> drones = new List<Drone>();
            internal static List<BaseStation> baseStations = new List<BaseStation>();
            internal static List<Client> clients = new List<Client>();
            internal static List<Package> packages = new List<Package>();
            static Random r = new Random();



        }
       
    }

}
