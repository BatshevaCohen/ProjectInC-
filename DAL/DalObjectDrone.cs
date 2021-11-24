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
            if (DataSource.drones.Exists(drone => drone.Id == d.Id))
            {
                throw new DroneException($"ID {d.Id} already exists!!");
            }
            else
                DataSource.drones.Add(d);
        }
        /// <summary>
        /// Update name of drone
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public void UpdateNameOfDrone(int id, string model)
        {
            Drone drone = DataSource.drones.Find(x => x.Id == id);
            drone.Model = model;
        }
        /// <summary>
        /// view function for Drone
        /// </summary>
        /// <param name="id"></param>
        public Drone GetDrone(int id)
        {
            if (!DataSource.drones.Exists(item => item.Id == id))
            {
                throw new DroneException($"ID: {id} does not exist!!");
            }
            return DataSource.drones.First(c => c.Id == id);
        }
        /// <summary>
        /// view lists functions for Drone
        /// </summary>
        public IEnumerable<Drone> ShowDroneList()
        {
            List<Drone> DroneList = new List<Drone>();
            foreach (Drone element in DataSource.drones)
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
            return DataSource.drones.Find(x => x.Id == droneId);
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
            Drone d = DataSource.drones.Find(x => x.Id == droneID);
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
            Drone d = DataSource.drones.Find(x => x.Id == drone_id);
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
       public Customer updateBatteryAndLocationDrone(int droneId,int senderId,double longt,double lang)
        {
            //and return custumer to update the dtone location like the custumer
            Drone drone = DataSource.drones.Find(x => x.Id == droneId);
            Customer customer = DataSource.customer.Find(x => x.Id == senderId);
            double  dis = (customer.Longitude - longt) * (customer.Longitude - longt) + (customer.Latitude - lang) * (customer.Latitude - lang);
            dis = Math.Sqrt(dis);
            //לשאול את שיראל
            drone.Battery -= drone.Battery * 0.1;
            return customer;

        }
    }
}
