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
    public partial class DalObject : IDal
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
        /// discharge drone
        /// </summary>
        /// <param name="droneID"></param>
        /// <param name="droneLatitude"></param>
        /// <param name="droneLongitude"></param>
        /// <exception cref="Exception"></exception>
        public void DischargeDrone(int droneID, double droneLatitude, double droneLongitude)
        {
            bool flag = false;
            Drone d = DataSource.Drones.Find(x => x.Id == droneID);
            Station s;
            foreach (Station item in Stations) //finds the station
            {
                if (item.Latitude == droneLatitude && item.Longitude == droneLongitude)
                {
                    flag = true;
                    s = item;
                    s.ChargeSlots++;
                    break;
                }
            }
            if (flag == false)
            {
                throw new Exception("couldn't find station by drones location");//לעשות חריגה שלא קיים מיקום תחנה לרחפן
            }
        }
        /// <summary>
        ///  charge drone
        /// </summary>
        /// <param name="drone_id"></param>
        /// <param name="station_id"></param>
        public void UpdateDroneToCharge(int drone_id, int station_id)
        {
            Drone d = DataSource.Drones.Find(x => x.Id == drone_id);
            DroneCharge droneCharge = new DroneCharge();
            droneCharge.DroneId = drone_id;
            Station s = DataSource.Stations.Find(x => x.Id == station_id);
            droneCharge.StationId = station_id;
            s.ChargeSlots--;
        }
        /// <summary>
        /// Put the drone in the station for the initial charging- uodate only the station, the drone update is in the BL
        /// </summary>
        /// <param name="StationId"></param>
        /// <param name="drone"></param>
        public void UpdateDroneToStation(int StationId, Drone drone)
        {
            Station station = DataSource.Stations.Find(x => x.Id == StationId);
            station.ChargeSlots = drone.Id;
        }
        

        //public Customer updateBatteryAndLocationDrone(int droneId,int senderId,double longitude,double latitude)
        //{
        //    //and return custumer to update the dtone location like the custumer

        //    //finds the drone by its ID
        //    Drone drone = DataSource.Drones.Find(x => x.Id == droneId);
        //    //finds the customer by its ID
        //    Customer customer = DataSource.Customer.Find(x => x.Id == senderId);
        //    //calculate the distance from the sender to the drone
        //    double distance= CalculateDistance(customer.Longitude, customer.Latitude, longitude, latitude);
        //    // for each KM - 1% of the battery
        //    drone.Battery -= distance * 0.01;
        //    return customer;
        //}        
    }
}
