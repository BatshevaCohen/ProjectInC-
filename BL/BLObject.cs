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
        public IDAL.IDal dalo;
        public List<DroneToList> drones;
        static Random r = new Random();
        DateTime ZeroTime = new DateTime(2000, 1, 1, 00, 00, 00); //default time when nothing inserted 
        public BLObject()
        {
            dalo = new DalObject.DalObject();//Access to the layer DAL
            drones = new List<DroneToList>();
           // dal = new DalObject.DalObject();
        }

        
        
        //UPDATE:
        public void UpdateDroneName(int id, string model)
        {
            dalo.UpdateNameOfDrone(id, model);
        }
        
        //sending drone to charge
        public void UpdateChargeDrone(int droneId)
        {
            IDAL.DO.Station station = new IDAL.DO.Station();
            DroneToList dronel = drones.Find(x => x.Id == droneId); //finds the drone by the recived ID
            if (dronel.droneStatuses == DroneStatuses.Available) //if the drone is available- it can be sent for charging
            {
                List<Distanse> disStationFromDrone = dalo.MinimumDistance(dronel.location.Longitude, dronel.location.Latitude).ToList();//list of the distances from the drone to every station
                double min = 9999999;
                int idS, counter = 0;
                bool flag = false;
                int sized = disStationFromDrone.Count; //number of distances in the list
                while (!flag && counter <= sized) //goes over the list
                {
                    foreach (Distanse item in disStationFromDrone)
                    {
                        if (item.distance <= min) //to find the station with the minimum distance from the drone
                        {
                            min = item.distance;
                            idS = item.id;
                        }
                        station = dalo.GetBaseStation(item.id);
                        if (station.ChargeSlots > 0) //if there is an available charging spot in the station
                        {
                            if (dronel.battery > min * 10 / 100) //only if there is enough battery
                            {
                                flag = true;
                                //function to update Battery, drone mode drone location
                                updateDtoneAndStation(droneId, station.Id, min);
                            }

                        }
                        counter++;
                        disStationFromDrone.Remove(item);
                    }
                }
            }
        }
        public void updateDtoneAndStation(int droneId,int stationId,double minDistance )
        {
            //update for the way to the station
            DroneToList dronel = drones.Find(x => x.Id == droneId); //finds the drone by its ID
            IDAL.DO.Station station = new IDAL.DO.Station();
            station = dalo.GetBaseStation(stationId); //finds the station by its ID
            dronel.droneStatuses = DroneStatuses.Maintenance; //update the drone to charging status
            dronel.location.Latitude = station.Latitude; //update the drone's location to the charging station location - latitude
            dronel.location.Longitude = station.Longitude; //update the drone's location to the charging station location - longitudw
            double droneBattery = minDistance * 10 / 100;
            dronel.battery = droneBattery;
            dalo.UpdateChargeSlots(station.Id);
            DroneInCharging droneInCharging = new DroneInCharging();
            droneInCharging.battery = droneBattery;
            droneInCharging.Id = droneId;
            Station s = new Station();
            s.droneInChargings.Add(droneInCharging);
        }
        public void UpdateDichargeDrone(int droneID, double chargingTime)
        {
            DroneToList dronel = drones.Find(x => x.Id == droneID); 
            Station station = new Station();
            if (dronel.droneStatuses == DroneStatuses.Maintenance) //only a drone that was in charging could be discharge
            {
                double droneLocationLatitude = dronel.location.Latitude;
                double droneLocationLongitude = dronel.location.Longitude;
                dalo.DischargeDrone(droneID, droneLocationLatitude, droneLocationLongitude);
                DroneInCharging droneInCharge = new DroneInCharging();
                droneInCharge = station.droneInChargings.Find(x => x.Id == droneID); //find the drone in charging
                station.droneInChargings.Remove(droneInCharge); //remove the drone frome the list of droneInChargings


            }
            else
            {
                throw new Exception("drone can't be discharged");
            }
            dronel.battery += chargingTime * dalo.PowerRequest()[4];
            dronel.droneStatuses = DroneStatuses.Available;

        }
        public void UpdateParcelToDrone(int droneId, int drone_id)
        {

        }
        public void UpdatePickUpParcelByDrone(int droneId)
        {

        }
        public void UpdateParcelSupplyByDrone(int droneId)
        {

        }
        //SHOW:
        BO.Station GetStation(int requestedId)
        {
            throw new NotImplementedException();
        }
        BO.Drone GetDrone(int droneId)
        {
            throw new NotImplementedException();
        }
        Customer GetCustomer(int customerId)
        {
            throw new NotImplementedException();
        }
        Parcel GetParcel(int parcelId)
        {
            throw new NotImplementedException();
        }
        //SHOW LIST:
        public List<Station> ShowStationList()
        {
            throw new NotImplementedException();
        }
        public List<Drone> ShowDroneList()
        {
            throw new NotImplementedException();
        }
        public List<Customer> ShowCustomerList()
        {
            throw new NotImplementedException();
        }
        public List<Parcel> ShowParcelList()
        {
            throw new NotImplementedException();
        }
        public List<Parcel> ShowNonAssociatedParcelList()
        {
            throw new NotImplementedException();
        }
        public List<Station> ShowChargeableStationList()
        {
            throw new NotImplementedException();
        }

        Station IBL.GetStation(int requestedId)
        {
            throw new NotImplementedException();
        }

        Drone IBL.GetDrone(int droneId)
        {
            throw new NotImplementedException();
        }

        Customer IBL.GetCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        Parcel IBL.GetParcel(int parcelId)
        {
            throw new NotImplementedException();
        }

        

        public void UpdateDeliveryToCustomer(int parcel_id3, int customer_id)
        {
            throw new NotImplementedException();
        }

        
    }
}
