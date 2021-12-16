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
    public partial class BLObject
    {
        /// <summary>
        /// Add parcel
        /// </summary>
        /// <param name="parcel"></param>
        public void AddParcel(Parcel parcel)
        {
            //Parcel ID must be 7-10 digits
            if (parcel.Id < 1000000 || parcel.Id >= 1000000000)
            {
                throw new ParcelIdExeption(parcel.Id, "Parcel ID must be 7-10 digits");
            }
            //Parcel's priority should be between 1-3
            if ((int)parcel.Priority < 1 || (int)parcel.Priority > 3)
            {
                throw new PriorityException(parcel.Priority, "Parcel's priority should be between 1-3");
            }
            // Sender ID can't be like Reciver ID
            if (parcel.Sender.Id == parcel.Resiver.Id)
            {
                throw new CustomerIdExeption(parcel.Sender.Id, "Sender ID can't be like Reciver ID");
            }
            parcel.Id = ++(DataSource.OrdinalNumber); //static serial number for parcel id
            parcel.ParcelCreationTime = DateTime.Now;
            parcel.AssignmentToParcelTime = DateTime.MinValue;
            parcel.CollectionTime = DateTime.MinValue;
            parcel.SupplyTime = DateTime.MinValue;
            parcel.DroneInParcel = null;
            IDAL.DO.Parcel p = new()
            {
                Id = parcel.Id,
                SenderId = parcel.Sender.Id,
                ReceiverId = parcel.Resiver.Id,
                Weight = (WeightCategories)parcel.Weight,
                Priority = (Priorities)parcel.Priority,
            };
            dalo.AddParcel(p);
        }
        /// <summary>
        /// Assign parcel to drone
        /// </summary>
        /// <param name="droneId"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void UpdateParcelToDrone(int droneId)
        {
           DroneToList droneTL= dronesL.Find(x => x.Id == droneId);
            IDAL.DO.Drone drone = dalo.GetDrone(droneId);
            
            // only if the drone is available for shipping
            if (droneTL.DroneStatuses == DroneStatuses.Available)
            {
                var parcels = dalo.ShowParcelList().Where(p => p.Create == null);
                // list parcels ordered by priority and weight
                var orderedParcels = from parcel in parcels
                                     orderby parcel.Priority descending, parcel.Weight ascending
                                     where parcel.Weight<=drone.MaxWeight
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
                    //Update and return parcel if the parcel found
                    int parcelINTransfer = dalo.UpdateParcelToDrone(theParcel.Id, droneId);
                    {
                        dronesL.Find(x => x.Id == droneId).ParcelNumberTransferred = parcelINTransfer;
                    }
                }
                else
                {
                    throw new Exception("customer does not exist!");
                }
            }
            else
                throw new Exception("drone can not be released");
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
                //finds the sender and the reciver (-the customer) by its ID
                Customer sender = GetCustomer(parcel.SenderId);
                Customer reciver = GetCustomer(parcel.ReceiverId);
                //calculate the distance frome the current location of the drone- to the customer
                double distance = dalo.CalculateDistance(sender.Location.Longitude, sender.Location.Latitude, drone.Location.Longitude, drone.Location.Latitude);
                //update the location of the drone to where the sender is (sender's location)
                dronesL.Find(x => x.Id == droneId).Location.Latitude = sender.Location.Latitude;
                dronesL.Find(x => x.Id == droneId).Location.Longitude = sender.Location.Longitude;
                // for each KM - 1% of the battery
                dronesL.Find(x => x.Id == droneId).Battery -= distance * 0.01;
                //update the pick up time to the current time
                parcel.PickedUp = DateTime.Now;
                dalo.updateBatteryDrone(droneId, distance);
                //update the ParcelInTransfer
                drone.ParcelInTransfer = new()
                {
                    Id = parcel.Id,
                    ParcelTransferStatus = ParcelTransferStatus.WaitingToBePickedUp,
                    Priority = (Priority)parcel.Priority,
                    Weight = (Weight)parcel.Weight,
                    TransportDistance = distance,
                };
                drone.ParcelInTransfer.Sender = new()
                {
                    Id = sender.Id,
                    Name = sender.Name,
                };
                drone.ParcelInTransfer.Reciver = new()
                {
                    Id = reciver.Id,
                    Name = reciver.Name
                };
                drone.ParcelInTransfer.CollectingLocation = new()
                {
                    Longitude = sender.Location.Longitude,
                    Latitude = sender.Location.Latitude,
                };
                drone.ParcelInTransfer.SupplyTargetLocation = new()
                {
                    Longitude = reciver.Location.Longitude,
                    Latitude = reciver.Location.Latitude,
                };
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
                //finds the drone frome layer bl
                Drone d = GetDrone(droneId);
                //finds the parcel in transfer
                ParcelInTransfer parcelInTransfer = d.ParcelInTransfer;
                senderL = parcelInTransfer.CollectingLocation;
                reciverL = parcelInTransfer.SupplyTargetLocation;
                // the distance that the drone have drove
                double distanse = dalo.CalculateDistance(senderL.Longitude, senderL.Latitude, reciverL.Longitude, reciverL.Latitude);
                //for each KM - 1% of the battery
                d.Battery -= distanse * 0.01;
                // update drone's location to the supply target's location
                d.Location = parcelInTransfer.SupplyTargetLocation;
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
            Parcel parcel = new()
            {
                Id = p.Id,
                Priority = (Priority)p.Priority,
                Weight = (Weight)p.Weight,
                AssignmentToParcelTime = (DateTime)p.Supplied,
                ParcelCreationTime = (DateTime)p.Create,
                SupplyTime = (DateTime)p.Assigned,
                CollectionTime = (DateTime)p.PickedUp
            };

            IDAL.DO.Customer custumerSender = dalo.GetCustomer(p.SenderId);
            IDAL.DO.Customer custumerReceiver = dalo.GetCustomer(p.ReceiverId);
            parcel.Sender = new() { Id = custumerSender.Id, Name = custumerSender.Name };
            parcel.Resiver = new() { Id = custumerReceiver.Id, Name = custumerReceiver.Name };

            //If the parcel has already been associated-שוייכה
            //update DroneInParcel
            if (parcel.CollectionTime == DateTime.MinValue)
            {
                DroneToList droneToList = dronesL.Find(x => x.Id == p.DroneID);
                parcel.DroneInParcel = new() { Id = p.Id, Battery = droneToList.Battery, Location = droneToList.Location };
            }
            return parcel;
        }

        /// <summary>
        /// Show LIST of parcels
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<ParcelToList> ShowParcelList()
        {
            var parcels = dalo.ShowParcelList();
            List<ParcelToList> parcelList = new() { };
            foreach (IDAL.DO.Parcel item in parcels)
            {
                //find the parcel in the BL-- in order to find the raciver and the sender's name
                Parcel parcel = GetParcel(item.Id);
                ParcelToList parcelTL = new()
                {
                    Id = item.Id,
                    ReciverName = parcel.Resiver.Name,
                    SenderName = parcel.Sender.Name,
                    Weight = (Weight)item.Weight,
                    Priority = (Priority)item.Priority,
                    ParcelStatus = FindParcelStatus(item)
                };
                parcelList.Add(parcelTL);
            }
            return parcelList;
        }

        /// <summary>
        /// Show LIST of NON ASSOCIATED parsels
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<ParcelToList> ShowNonAssociatedParcelList()
        {
            IEnumerable<IDAL.DO.Parcel> parcels = dalo.ShowParcelList(x => x.Create != DateTime.MinValue);
            List<ParcelToList> parcelListNotAssociated = new();
            foreach (var item in parcels)
            {
                ParcelToList parcelTL = new();
                // only if the parcel created and did not assign
                if (item.Assigned == DateTime.MinValue)
                {
                    Parcel parcel = GetParcel(item.Id);
                    parcelTL.Id = item.Id;
                    parcelTL.ReciverName = parcel.Resiver.Name;
                    parcelTL.SenderName = parcel.Sender.Name;
                    parcelTL.Weight = (Weight)item.Weight;
                    parcelTL.Priority = (Priority)item.Priority;
                    parcelTL.ParcelStatus = FindParcelStatus(item);
                }
                parcelListNotAssociated.Add(parcelTL);
            }
            return parcelListNotAssociated;
        }

        /// <summary>
        /// the function recive a parcel and return its shipping status
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns></returns>
        public ParcelStatus FindParcelStatus(IDAL.DO.Parcel parcel)
        {
            ParcelStatus parcelStatus = ParcelStatus.Created;
            //the parcel was created but have not assigned to the drone
            if (parcel.Assigned == DateTime.MinValue && parcel.Create != DateTime.MinValue)
            {
                parcelStatus = ParcelStatus.Created;
            }
            //the parcel was assigned to drone but have not picked up by it yet
            if (parcel.Assigned != DateTime.MinValue && parcel.PickedUp == DateTime.MinValue)
            {
                parcelStatus = ParcelStatus.Assigned;
            }
            //the parcel was PickedUp by the drone but have not Supplied to the reciver yet
            if (parcel.Supplied == DateTime.MinValue && parcel.PickedUp != DateTime.MinValue)
            {
                parcelStatus = ParcelStatus.PickedUp;
            }
            //the parcel supplied to the reciver
            if (parcel.Supplied != DateTime.MinValue)
            {
                parcelStatus = ParcelStatus.Supplied;
            }
            return parcelStatus;
        }
        public Parcel GetParcelByDroneId(int droneId)
        {
           IDAL.DO.Parcel p= dalo.GetParcelByDroneId(droneId);
            Parcel par = new Parcel();
            par.Id = p.Id;
            par.ParcelCreationTime = p.Create;
            par.Priority = (Priority)p.Priority;
            par.Weight = (Weight)p.Weight;
            par.SupplyTime = p.Supplied;
            par.CollectionTime = p.PickedUp;
            par.ParcelCreationTime = p.Create;
            par.AssignmentToParcelTime= p.Assigned; ;
            IDAL.DO.Customer sender=  dalo.GetCustomer(p.SenderId);
            IDAL.DO.Customer reciver = dalo.GetCustomer(p.ReceiverId);
            par.Resiver = new()
            {
                Id = reciver.Id,
                Name=reciver.Name
            };

            par.Sender = new()
            {
                Id = sender.Id,
                Name = sender.Name
            };
            
            return par;
        }
    }
}
