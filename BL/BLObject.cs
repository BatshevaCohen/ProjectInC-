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
           
        }



        //UPDATE
        //public void UpdateDroneName(int id, string model)
        //{
        //    dalo.UpdateNameOfDrone(id, model);
        //}

      
        public void updateDtoneAndStation(int droneId,int stationId,double minDistance )
        {
            //update for the way to the station
            DroneToList dronel = drones.Find(x => x.Id == droneId); //finds the drone by its ID
            IDAL.DO.Station station = new IDAL.DO.Station();
            station = dalo.GetStation(stationId); //finds the station by its ID
            dronel.droneStatuses = DroneStatuses.Maintenance; //update the drone to charging status
            dronel.location.Latitude = station.Latitude; //update the drone's location to the charging station location - latitude
            dronel.location.Longitude = station.Longitude; //update the drone's location to the charging station location - longitudw
            double droneBattery = minDistance * 10 / 100;
            dronel.battery = droneBattery;
            dalo.UpdateChargeSpots(station.Id);
            DroneInCharging droneInCharging = new DroneInCharging();
            droneInCharging.battery = droneBattery;
            droneInCharging.Id = droneId;
            Station s = new Station();
            s.droneInChargings.Add(droneInCharging);
        }
        //public void UpdateDichargeDrone(int droneID, double chargingTime)
        //{
        //    DroneToList dronel = drones.Find(x => x.Id == droneID); 
        //    Station station = new Station();
        //    if (dronel.droneStatuses == DroneStatuses.Maintenance) //only a drone that was in charging could be discharge
        //    {
        //        double droneLocationLatitude = dronel.location.Latitude;
        //        double droneLocationLongitude = dronel.location.Longitude;
        //        dalo.DischargeDrone(droneID, droneLocationLatitude, droneLocationLongitude);
        //        DroneInCharging droneInCharge = new DroneInCharging();
        //        droneInCharge = station.droneInChargings.Find(x => x.Id == droneID); //find the drone in charging
        //        station.droneInChargings.Remove(droneInCharge); //remove the drone frome the list of droneInChargings


        //    }
        //    else
        //    {
        //        throw new Exception("drone can't be discharged");
        //    }
        //    dronel.battery += chargingTime * dalo.PowerRequest()[4];
        //    dronel.droneStatuses = DroneStatuses.Available;

        //}
        //public void UpdateParcelToDrone(int droneId, int drone_id)
        //{

        //}
        public void UpdatePickUpParcelByDrone(int droneId)
        {
            //רק רחפן המבצע משלוח של חבילה ששויכה אליו אבל עוד לא  נאספה יוכל לאסוף אותו
            Drone drone = GetDrone(droneId);
            IDAL.DO.Parcel p = dalo.GetParcelByDroneId(droneId);
            if(p.create<=DateTime.MinValue)//the parcel was PickUp
            {
                throw new Exception("the parcel was PickUp ");
            }
            else
            { 
                IDAL.DO.Customer c= dalo.updateBatteryAndLocationDrone(droneId, p.SenderId,drone.Location.Longitude,drone.Location.Latitude);
                drone.Location.Latitude = c.Latitude;
                drone.Location.Latitude = c.Longitude;
                //לא בטוחה 
                p.PickedUp = DateTime.Now;
            }
        }
        public void UpdateParcelSupplyByDrone(int droneId)
        {
          IDAL.DO.Drone drone=  dalo.GetDrone(droneId);
          IDAL.DO.Parcel p = dalo.GetParcelByDroneId(droneId);
            if(!(p.create <= DateTime.MinValue&&p.Delivered>=DateTime.MinValue))
            {
                throw new Exception("the parcel Delivered ");
            }
            else
            {

            }
        }
        //SHOW:.
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
