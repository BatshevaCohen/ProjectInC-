using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math;
using DalObject.DO;
using IDAL;
using IDAL.DO;

namespace DalObject
{
    public partial class DalObject : IDal
    {
        /// <summary>
        /// add Station to the stations list
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public void AddStation(Station s)
        {
            if (DataSource.Stations.Exists(station => station.Id == s.Id))
            {
                throw new StationException($"ID {s.Id} already exists!!");
            }
            else
                DataSource.Stations.Add(s);
        }
        /// <summary>
        /// Update station data
        /// </summary>
        /// <param name="StationId"></param>
        /// <param name="name"></param>
        /// <param name="charging_spots"></param>
        public void UpdateStetion(int StationId, string name, int charging_spots)
        {
            Station station = DataSource.Stations.Find(x => x.Id == StationId);
            station.ChargeSlots = charging_spots;
            station.Name = name;
            station.Id = StationId;
        }
        /// <summary>
        /// View Station
        /// </summary>
        /// <param name="id"></param>
        public Station GetStation(int id)
        {
            if (!DataSource.Stations.Exists(item => item.Id == id))
            {
                throw new StationException($"ID: {id} does not exist!!");
            }
            return DataSource.Stations.First(c => c.Id == id);
        }
        /// <summary>
        /// View Station List
        /// </summary>
        /// <returns></returns>
        public List<Station> ShowStationList()
        {
            List<Station> stationList = new();
            foreach (Station element in DataSource.Stations)
            {
                stationList.Add(element);
            }
            return stationList;
        }
        /// <summary>
        ///  shows stations with available charging spots
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> ShowChargeableBaseStationList()
        {
            List<Station> ChargeableBaseStationList = new ();
            foreach (Station element in DataSource.Stations)
            {
                if (element.ChargeSlots > 0)
                    ChargeableBaseStationList.Add(element);
            }
            return ChargeableBaseStationList;
        }
        /// <summary>
        /// updates the number of available charging spots
        /// </summary>
        /// <param name="stationId"></param>
        public void UpdateChargeSpots(int stationId)
        {
            Station station = DataSource.Stations.Find(x => x.Id == stationId);
            station.ChargeSlots--;
        }
        /// <summary>
        /// Charge drone and update the station
        /// </summary>
        /// <param name="dronId"></param>
        /// <param name="stationId"></param>
        public void UpdateAddDroneToCharge(int dronId, int stationId)
        {
            DroneCharge droneCharge = new()
            {
                DroneId = dronId,
                StationId = stationId
            };
            //Add new drone to the list
            DataSource.DroneCharges.Add(droneCharge);
        }
        /// <summary>
        /// Uncharge drone and update the station
        /// </summary>
        /// <param name="dronId"></param>
        /// <param name="stationId"></param>
        public void UpdateRemoveDroneToCharge(int dronId, int stationId)
        {
            DroneCharge droneCharge = new()
            {
                DroneId = dronId,
                StationId = stationId
            };
            //מחיקת מופע לרשימה
            DataSource.DroneCharges.Remove(droneCharge);
        }

        /// <summary>
        /// Find station by its ID
        /// </summary>
        /// <param name="stationID"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Station FindStetion(int stationID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// The function recives station ID and returns all of the drones that are charging in that station
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        public List<DroneCharge> GetListOfDronInCharge(int stationId)
        {
            List<DroneCharge> newDroneCharges = new ();
            foreach (DroneCharge item in DataSource.DroneCharges)
            {
                if(item.StationId==stationId)
                {
                    newDroneCharges.Add(item);
                }
            }
            return newDroneCharges;
        }

        /// <summary>
        /// A function that returns a minimum distance between a point and a station
        /// </summary>
        /// <param name="senderLattitude">Lattitude of sender</param>
        /// <param name="senderLongitude">longitude of sender</param>
        /// <param name="flag">Optional field for selecting a nearby station flag = false or available nearby station flag = true</param>
        /// <returns>Station closest to the point</returns>
        public Station GetClosestStation(double senderLattitude, double senderLongitude)
        {
            bool flag = false;
            double minDistance = 1000000000000;
            Station station = new();
            if (!flag)
            {
                foreach (var s in DataSource.Stations)
                {
                    double dictance = Math.Sqrt(Math.Pow(s.Latitude - senderLattitude, 2) + Math.Pow(s.Longitude - senderLongitude, 2));
                    if (minDistance > dictance)
                    {
                        minDistance = dictance;
                        station = s;
                    }
                }
            }
            else
            {
                foreach (var s in DataSource.Stations.Where(s => s.ChargeSlots > 0))
                {
                    double dictance = Math.Sqrt(Math.Pow(s.Latitude - senderLattitude, 2) + Math.Pow(s.Longitude - senderLongitude, 2));
                    if (minDistance > dictance)
                    {
                        minDistance = dictance;
                        station = s;
                    }
                }
            }
            return station;
        }

        /// <summary>
        /// A function that calculates the distance between a customer's location and a station for charging
        /// </summary>
        /// <param name="targetId">target Id</param>
        /// <returns>Minimum distance to the nearest station</returns>
        public double GetDistanceBetweenLocationAndClosestBaseStation(int Reciverid)
        {
            double minDistance = 1000000000000;
            Customer target = GetCustomer(Reciverid);
            foreach (var s in DataSource.Stations)
            {
                double dictance = Math.Sqrt(Math.Pow(s.Latitude - target.Latitude, 2) + Math.Pow(s.Longitude - target.Longitude, 2));
                if (minDistance > dictance)
                {
                    minDistance = dictance;
                }
            }
            return minDistance;
        }
    }
}
