using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using DalObject.DO;
using IBL.BO;
using IDAL.DO;

namespace IBL.BO
{
    public partial class BLObject : IBL
    {
        /// <summary>
        /// Add station
        /// </summary>
        /// <param name="station"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddStation(Station station)
        {
            IDAL.DO.Station s = new IDAL.DO.Station();
            s.Name = station.Name;
            s.Id = station.Id;
            s.Longitude = station.Location.Longitude;
            s.Latitude = station.Location.Latitude;
           
            dalo.AddStation(s);//send the new station to DAL 

            
        }


        /// <summary>
        /// Update station
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="charging_spots"></param>
        public void UpdateStetion(int id, string name, int charging_spots)
        {
            dalo.UpdateStetion(id, name, charging_spots);
        }
        /// <summary>
        /// Get station by ID
        /// </summary>
        /// <param name="requestedId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
       public Station GetStation(int stationID)
        {
           IDAL.DO.Station s= dalo.GetStation(stationID);
            Station station = new Station();
            station.Name = s.Name;
            station.Id = s.Id;
            station.Location.Latitude = s.Latitude;
            station.Location.Longitude = s.Longitude;
            station.AvailableChargingSpots = s.ChargeSlots;
            //list of drone and 
            List<DroneCharge> droneCharges= dalo.GetListOfDronInCharge(stationID);
            foreach (DroneCharge item in droneCharges)
            {
                DroneInCharging droneInCharging = new DroneInCharging();
                droneInCharging.Id = item.DroneId;
                DroneToList droneToList = drones.Find(x => x.Id == item.DroneId);
                droneInCharging.Battery = droneToList.Battery;
                station.droneInChargings.Add(droneInCharging);
            }
            
            return station;
        }
        /// <summary>
        /// Show LIST of stations
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Station> ShowStationList()
        {
            List<Station> stationList = new List<Station>();
            var stations = dalo.ShowStationList();
            foreach (IDAL.DO.Station item in stations)
            {
                Station station = new Station();
                station.Id = item.Id;
                station.Location.Latitude = item.Latitude;
                station.Location.Longitude = item.Longitude;
                station.AvailableChargingSpots = item.ChargeSlots;
                //list of drone and 
                List<DroneCharge> droneCharges = dalo.GetListOfDronInCharge(station.Id);
                foreach (DroneCharge item1 in droneCharges)
                {
                    DroneInCharging droneInCharging = new DroneInCharging();
                    droneInCharging.Id = item1.DroneId;
                    DroneToList droneToList = drones.Find(x => x.Id == item1.DroneId);
                    droneInCharging.Battery = droneToList.Battery;
                    station.droneInChargings.Add(droneInCharging);
                }

                stationList.Add(station);
            }
            return stationList;
            
        }
        /// <summary>
        /// Show LIST of chargeable stations
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Station> ShowChargeableStationList()
        {
            List<Station> stationListWithAvailableChargingSpots = new List<Station>();
            var stations = dalo.ShowStationList();
            foreach (IDAL.DO.Station item in stations)
            {
                if(item.ChargeSlots>0)
                {

                    Station station = new Station();
                    station.Id = item.Id;
                    station.Location.Latitude = item.Latitude;
                    station.Location.Longitude = item.Longitude;
                    station.AvailableChargingSpots = item.ChargeSlots;
                    //list of drone and 
                    List<DroneCharge> droneCharges = dalo.GetListOfDronInCharge(station.Id);
                    foreach (DroneCharge item1 in droneCharges)
                    {
                        DroneInCharging droneInCharging = new DroneInCharging();
                        droneInCharging.Id = item1.DroneId;
                        DroneToList droneToList = drones.Find(x => x.Id == item1.DroneId);
                        droneInCharging.Battery = droneToList.Battery;
                        station.droneInChargings.Add(droneInCharging);
                    }

                    stationListWithAvailableChargingSpots.Add(station);
                }
               


            }
            return stationListWithAvailableChargingSpots;
        

        
                throw new NotImplementedException();
        }

        public Drone GetDroneBL(int droneId)
        {
            throw new NotImplementedException();
        }
    }
}
