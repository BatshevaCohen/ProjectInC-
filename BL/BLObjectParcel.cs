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
            parcel.AssignmentToParcelTime = ZeroTime;
            parcel.CollectionTime = ZeroTime;
            parcel.SupplyTime = ZeroTime;
            parcel.DroneInParcel = null;
            IDAL.DO.Parcel p = new IDAL.DO.Parcel();

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
                var parcels = dalo.ShowParcelList().Where(p => p.Requested == null);
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
                    DroneToList dr = drones.Find(d => d.Id == droneId);
                    dr.location = new Location { Latitude = customer.Latitude, Longitude = customer.Longitude };

                    dalo.UpdateParcelToDrone(theParcel.Id, droneId);
                }

            }
            else
                throw new NotImplementedException();
        }
    }
}
