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
        public List<Drone> drone1;
        static Random r = new Random();
        public BLObject()
        {
            //Access to the layer DAL
            dalo = new DalObject.DalObject();
            drones = new List<DroneToList>();
           
        }
        /// <summary>
        /// Update for the way to the station
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="stationId"></param>
        /// <param name="minDistance"></param>
        public void UpdateDroneAndStation(int droneId,int stationId,double minDistance )
        {
            //finds the drone by its ID
            DroneToList dronel = drones.Find(x => x.Id == droneId);
            IDAL.DO.Station station = new IDAL.DO.Station();
            //finds the station by its ID
            station = dalo.GetStation(stationId);
            //update the drone to charging status
            dronel.droneStatuses = DroneStatuses.Maintenance;
            //update the drone's location to the charging station location - latitude
            dronel.location.Latitude = station.Latitude;
            //update the drone's location to the charging station location - longitudw
            dronel.location.Longitude = station.Longitude;
            double droneBattery = minDistance * 10 / 100;
            dronel.battery = droneBattery;
            dalo.UpdateChargeSpots(station.Id);
            DroneInCharging droneInCharging = new DroneInCharging();
            droneInCharging.battery = droneBattery;
            droneInCharging.Id = droneId;
            Station s = new Station();
            s.droneInChargings.Add(droneInCharging);
        }
        /// <summary>
        /// Updete that the parcel has picked up by a drone
        /// </summary>
        /// <param name="droneId"></param>
        /// <exception cref="Exception"></exception>
        public void UpdateParcelPickUpByDrone(int droneId)
        {
            //the drone collect a parcel only if the parcel has been assigned to it and haven't picked up yet
            Drone drone = GetDrone(droneId);
            IDAL.DO.Parcel parcel = dalo.GetParcelByDroneId(droneId);
            //check if the parcel was assigned
            if (parcel.Scheduled != DateTime.MinValue)
            {
                throw new Exception("the parcel wasn't assigned to the drone!");
            }
            //check if the parcel was picked up
            if (parcel.PickedUp != DateTime.MinValue)
            {
                throw new Exception("the parcel was picked up already!");
            }
            else
            {
                //finds the sender (-the customer) by its ID
                Customer customer= GetCustomer(parcel.SenderId);
                //calculate the distance frome the current location of the drone- to the customer
                double distance = dalo.CalculateDistance(customer.Location.Longitude, customer.Location.Latitude, drone.Location.Longitude, drone.Location.Latitude);
                //update the location of the drone to where the sender is (sender's location)
                drone.Location.Latitude = customer.Location.Latitude;
                drone.Location.Latitude = customer.Location.Longitude;
                // for each KM - 1% of the battery
                drone.Battery -= distance * 0.01;
                //update the pick up time to the current time
                parcel.PickedUp = DateTime.Now;
            }
        }
        /// <summary>
        /// Update that the parcel supplied to the reciver (by the drone)
        /// </summary>
        /// <param name="droneId"></param>
        /// <exception cref="Exception"></exception>
        public void UpdateParcelSupplyByDrone(int droneId)
        {
         
            IDAL.DO.Parcel parcel = dalo.GetParcelByDroneId(droneId);
            //check if the parcel was picked up
            if (parcel.PickedUp == DateTime.MinValue)
            {
                throw new Exception("the parcel wasn't picked up yet!");
            }
            //check if the parcel was delivered
            if (parcel.Delivered != DateTime.MinValue)
            {
                throw new Exception("the parcel delivered already!");
            }
            else
            {
                Location senderL, reciverL;
                //finds the drone
                Drone d = drone1.Find(x => x.Id == droneId);
                //finds the parcel in transfer
                ParcelInTransfer parcelInTransfer = d.ParcelInTransfer;
                senderL = parcelInTransfer.collecting;
                reciverL = parcelInTransfer.SupplyTarget;
                // the distance that the drone have drove
                double distanse = dalo.CalculateDistance(senderL.Longitude, senderL.Latitude, reciverL.Longitude, reciverL.Latitude);
                //for each KM - 1% of the battery
                d.Battery -= distanse * 0.01;
                // update drone's location to the supply target's location
                d.Location = parcelInTransfer.SupplyTarget;
                //changing the drone's status to be available
                d.DroneStatuses = DroneStatuses.Available;
                //update the supply time
                parcel.Delivered = DateTime.Now;
            }
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
