using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject.DO;
using IDAL;
using IDAL.DO;

namespace DalObject
{
    public partial class DalObject 
    {
        /// <summary>
        /// add Drone to the drons list
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public void AddDrone(Drone d)
        {
            if (DataSource.Drones.Exists(drone => drone.Id == d.Id))
            {
                throw new DroneException($"ID {d.Id} already exists!!");
            }
            else
                DataSource.Drones.Add(d);
        }
        /// <summary>
        /// Update name of drone
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public void UpdateNameOfDrone(int id, string model)
        {
            Drone drone = DataSource.Drones.Find(x => x.Id == id);
            drone.Model = model;
        }
        /// <summary>
        /// view function for Drone
        /// </summary>
        /// <param name="id"></param>
        public Drone GetDrone(int id)
        {
            if (!DataSource.Drones.Exists(item => item.Id == id))
            {
                throw new DroneException($"ID: {id} does not exist!!");
            }
            return DataSource.Drones.First(c => c.Id == id);
        }
        /// <summary>
        /// view lists functions for Drone
        /// </summary>
        public IEnumerable<Drone> ShowDroneList()
        {
            List<Drone> DroneList = new List<Drone>();
            foreach (Drone element in DataSource.Drones)
            {
                DroneList.Add(element);
            }
            return DroneList;
        }
        /// <summary>
        /// find drone by ID
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        public Drone FindDrone(int droneId)
        {
            return DataSource.Drones.Find(x => x.Id == droneId);
        }
        /// <summary>
        /// Send a drone to charge
        /// </summary>
        /// <param name="droneId">the drone to send to charge</param>
        /// <param name="stationId">the station to send it to charge</param>
        public void SendDroneToCharge(int droneId, int stationId)
        {
            Drone drone = GetDrone(droneId);
            Station station = GetStation(stationId);
            DataSource.Stations.Remove(station);

            DataSource.DroneCharges.Add(new DroneCharge
            {
                DroneId = drone.Id,
                StationId = station.Id
            });
            station.ChargeSpots--;

            DataSource.Stations.Add(station);
        }
        /// <summary>
        /// release a drone from charge
        /// </summary>
        /// <param name="droneId">the id of the drone to release</param>
        public void ReleaseDroneFromCharging(int droneId)
        {
            Drone drone = GetDrone(droneId);
            DataSource.Drones.Remove(drone);

            DroneCharge droneCharge = DataSource.DroneCharges.Find(x => x.DroneId == droneId);
            //if (droneCharge.DroneId != droneId)
            //    throw new IdNotFoundException(droneId, "Station charge");

            int stationId = droneCharge.StationId;  //
            Station station = GetStation(stationId);
            DataSource.Stations.Remove(station);

            station.ChargeSpots++;
            
            DataSource.Stations.Add(station);
            DataSource.Drones.Add(drone);
            DataSource.DroneCharges.Remove(droneCharge);

        }
        /// <summary>
        /// discharge drone
        /// </summary>
        /// <param name="droneID"></param>
        /// <param name="droneLatitude"></param>
        /// <param name="droneLongitude"></param>
        /// <exception cref="Exception"></exception>
        public void DischargeDroneByLocation(int droneID, double droneLatitude, double droneLongitude)
        {
            bool flag = false;
            Drone d = DataSource.Drones.Find(x => x.Id == droneID);
            Station s;
            foreach (Station item in DataSource.Stations) //finds the station
            {
                if (item.Latitude == droneLatitude && item.Longitude == droneLongitude)
                {
                    flag = true;
                    s = item;
                    s.ChargeSpots++;
                    break;
                }
            }
            if (flag == false)
            {
                throw new Exception("couldn't find station by drones location");//לעשות חריגה שלא קיים מיקום תחנה לרחפן
            }
        }
       
        /// <summary>
        /// Put the drone in the station for the initial charging- uodate only the station, the drone update is in the BL
        /// </summary>
        /// <param name="StationId"></param>
        /// <param name="drone"></param>
        public void UpdateDroneToStation(int StationId, Drone drone)
        {
            Station station = DataSource.Stations.Find(x => x.Id == StationId);
            station.ChargeSpots = drone.Id;
        }
        /// <summary>
        /// Method of applying drone power
        /// </summary>
        /// <returns>An array of the amount of power consumption of a drone for each situation</returns>
        public double[] PowerConsumptionRequest()
        {
            double[] result = {DataSource.Config.Light, DataSource.Config.Heavy,
                DataSource.Config.Medium, DataSource.Config.Heavy,
                DataSource.Config.ChargingRate };
            return result;
        }
    }
}
