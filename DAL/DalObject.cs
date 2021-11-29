using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject.DO;
using IDAL;
using IDAL.DO;
//
namespace DalObject
{

    /// <summary>
    /// constractor 
    /// </summary>
    public partial class DalObject : IDal
    {
      //  private IEnumerable<Station> Stations;

        public DalObject()
        {
            DataSource.Initialize();
        }
        
        public double[] PowerRequest()
        {
            double[] arr = new double[5];
            arr[0] = DataSource.Config.Available;
            arr[1] = DataSource.Config.Heavy;
            arr[2] = DataSource.Config.Light;
            arr[3] = DataSource.Config.Medium;
            arr[4] = DataSource.Config.ChargingRate;
            return arr;
        }

        /// <summary>
        /// Looking for the closest station with available charging spots
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <returns></returns>
        public IEnumerable<Distanse> MinimumDistance(double longitude, double latitude)
        {
            List<Distanse> listDis = new() { };
            foreach (Station element in DataSource.Stations)
            {
                Distanse distanse = new() { };
                double dis;
                dis = (element.Longitude - longitude) * (element.Longitude - longitude) + (element.Latitude - longitude) * (element.Latitude - longitude);
                dis = Math.Sqrt(dis);
                distanse.Id = element.Id;
                distanse.Distance = dis;

                listDis.Add(distanse);
            }
            return listDis;
        }
        //--BONUS--: another option that recives coordinates and print the distance from it to a station or a customer
        public double CalculateDistance(double longitude1, double latitude1, double longitude2, double latitude2)
        {
            // this function is from https://stackoverflow.com/questions/27928/calculate-distance-between-two-latitude-longitude-points-haversine-formula
            // (with changes for C#)
            var p = 0.017453292519943295;    // Math.PI / 180
            var a = 0.5 - Math.Cos((latitude2 - latitude1) * p) / 2 +
                    Math.Cos(latitude1 * p) * Math.Cos(latitude2 * p) *
                    (1 - Math.Cos((longitude2 - longitude1) * p)) / 2;

            return 12742 * Math.Asin(Math.Sqrt(a)); // 2 * R; R = 6371 km
        }

        /// <summary>
        /// Exit function
        /// </summary>
        /// <returns></returns>
        /// 
        public static int Exit()
        {
            return 0;
        }

        public IEnumerable<Station> ShowChargeableStationList()
        {
            throw new NotImplementedException();
        }

        public void StationException(int id, string errMsg)
        {
            throw new NotImplementedException();
        }

        public void CustomerException(int id, string errMsg, Severity severity)
        {
            throw new NotImplementedException();
        }

        public void ParcelException(int id, string errMsg, Severity severity)
        {
            throw new NotImplementedException();
        }

        public void DroneException(int id, string errMsg)
        {
            throw new NotImplementedException();
        }

        public void DischargeDrone(int drone_id, TimeSpan s)
        {
            throw new NotImplementedException();
        }

        public Customer FindCustomer(int customerID)
        {
            throw new NotImplementedException();
        }

        public Station GetStetion(int stationID)
        {
            throw new NotImplementedException();
        }

        public Customer updateBatteryAndLocationDrone(int droneId, int senderId, double longt, double lanti)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Station> IDal.ShowStationList()
        {
            throw new NotImplementedException();
        }
    }

}
