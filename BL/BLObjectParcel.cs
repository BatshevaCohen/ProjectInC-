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
        /// Add parcel
        /// </summary>
        /// <param name="parcel"></param>
        public void AddParcel(Parcel parcel)
        {
            parcel.Id = ++(DalObject.DO.DataSource.OrdinalNumber); //static serial number for parcel id
            parcel.ParcelCreationTime = DateTime.Now;
            parcel.AssignmentToParcelTime = DateTime.MinValue;
            parcel.CollectionTime = DateTime.MinValue;
            parcel.SupplyTime = DateTime.MinValue;
            parcel.DroneInParcel = null;
            IDAL.DO.Parcel p = new() { };

            p.Id = parcel.Id;
            p.SenderId = parcel.Sender.Id;
            p.ReceiverId = parcel.Resiver.Id;
            p.Weight = (WeightCategories)parcel.Weight;
            p.Priority = (Priorities)parcel.Priority;
            dalo.AddParcel(p);
        }
        /// <summary>
        /// Update parcel to drone
        /// </summary>
        /// <param name="droneId"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void UpdateParcelToDrone(int droneId)
        {
            IDAL.DO.Drone drone = dalo.GetDrone(droneId);
            // only if the drone is available for shipping
            if (drone.Status == IDAL.DO.DroneStatuses.Available)
            {
                var parcels = dalo.ShowParcelList().Where(p => p.Create == null);
                // list parcels ordered by priority and weight
                var orderedParcels = from parcel in parcels
                                     orderby parcel.Priority descending, parcel.Weight ascending
                                     where parcel.Weight <= drone.MaxWeight
                                     select parcel;
                // choose the first parcel from the list of parcels
                var theParcel = orderedParcels.FirstOrDefault();
                // finds the customer's location
                IDAL.DO.Customer customer = dalo.ShowCustomerList().Where(c => c.Id == theParcel.SenderId).FirstOrDefault();
                // only if ID exists
                if (customer.Id != 0)
                {
                    DroneToList dr = dronesL.Find(d => d.Id == droneId);
                    dr.Location = new Location { Latitude = customer.Latitude, Longitude = customer.Longitude };

                    dalo.UpdateParcelToDrone(theParcel.Id, droneId);
                }

            }
            else
                throw new NotImplementedException();
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
            if (parcel.Assigned != DateTime.MinValue)
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
                Customer customer = GetCustomer(parcel.SenderId);
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
        public void UpdateParcelSuppliedByDrone(int droneId)
        {

            IDAL.DO.Parcel parcel = dalo.GetParcelByDroneId(droneId);
            //check if the parcel was picked up
            if (parcel.PickedUp == DateTime.MinValue)
            {
                throw new Exception("the parcel wasn't picked up yet!");
            }
            //check if the parcel was delivered
            if (parcel.Supplied != DateTime.MinValue)
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
                senderL = parcelInTransfer.Collecting;
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
                parcel.Supplied = DateTime.Now;
            }
        }
        /// <summary>
        /// View of parcel from bl
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns></returns>
        public Parcel GetParcel(int parcelId)
        {
            IDAL.DO.Parcel p = dalo.GetParcel(parcelId);
            Parcel parcel = new() { };
            parcel.Id = p.Id;
            parcel.Resiver.Id = p.SenderId;
            parcel.Sender.Id = p.ReceiverId;
            parcel.Priority = (Priority)p.Priority;
            parcel.Weight = (Weight)p.Weight;
            parcel.AssignmentToParcelTime = (DateTime)p.Supplied;
            parcel.ParcelCreationTime = (DateTime)p.Create;
            parcel.SupplyTime = (DateTime)p.Assigned;
            parcel.CollectionTime = (DateTime)p.PickedUp;
            //If the parcel has already been associated-שוייכה
            //update DroneInParcel
            if (parcel.CollectionTime != DateTime.MinValue)
            {
                DroneToList droneToList = dronesL.Find(x => x.Id == p.DroneID);
                parcel.DroneInParcel.Id = p.Id;
                parcel.DroneInParcel.Battery = droneToList.Battery;
                parcel.DroneInParcel.Location = droneToList.Location;
               
            }
            return parcel;
        }

        
        /// <summary>
        /// Show LIST of parcels
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Parcel> ShowParcelList()
        {
            var parcels = dalo.ShowParcelList();
            List<Parcel> parcelList = new() { };
            foreach (IDAL.DO.Parcel item in parcels)
            { 
                Parcel parcel = new() { };
                parcel.Id = item.Id;
                parcel.Resiver.Id = item.SenderId;
                parcel.Sender.Id = item.ReceiverId;
                parcel.Priority = (Priority)item.Priority;
                parcel.Weight = (Weight)item.Weight;
                parcel.AssignmentToParcelTime = (DateTime)item.Supplied;
                parcel.ParcelCreationTime = (DateTime)item.Create;
                parcel.SupplyTime = (DateTime)item.Assigned;
                parcel.CollectionTime = (DateTime)item.PickedUp;
                //If the parcel has already been associated-שוייכה
                //update DroneInParcel
                if (parcel.CollectionTime != DateTime.MinValue)
                {
                    DroneToList droneToList = dronesL.Find(x => x.Id == item.DroneID);
                    DroneInParcel droneInParcel = new() { };
                    droneInParcel.Id = item.Id;
                    droneInParcel.Battery = droneToList.Battery;
                    droneInParcel.Location = droneToList.Location;
                    parcel.DroneInParcel = droneInParcel;
                }
                return parcelList;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Show LIST of NON ASSOCIATED parsels
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Parcel> ShowNonAssociatedParcelList()
        {
            var parcels = dalo.ShowParcelList();
            List<Parcel> parcelListNotAssociated = new ();
            foreach (IDAL.DO.Parcel item in parcels)
            {
                if (item.PickedUp != DateTime.MinValue)
                {
                    Parcel parcel = new() { };
                    parcel.Id = item.Id;
                    parcel.Resiver.Id = item.ReceiverId;
                    parcel.Sender.Id = item.SenderId;
                    parcel.Priority = (Priority)item.Priority;
                    parcel.Weight = (Weight)item.Weight;
                    parcel.AssignmentToParcelTime = (DateTime)item.Supplied;
                    parcel.ParcelCreationTime = (DateTime)item.Create;
                    parcel.SupplyTime = (DateTime)item.Assigned;
                    parcel.CollectionTime = (DateTime)item.PickedUp;
                    parcelListNotAssociated.Add(parcel);
                }
            }
            return parcelListNotAssociated;

            throw new NotImplementedException();
        }


        /// <summary>
        /// Get parcel by ID
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        Parcel IBL.GetParcel(int parcelId)
        {
            throw new NotImplementedException();
        }
    }
}
