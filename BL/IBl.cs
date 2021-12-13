using IBL.BO;
using System;
using System.Collections.Generic;


namespace IBL.BO
{
    public interface IBL
    {
        #region Add:
        /// <summary>
        /// Add Station
        /// </summary>
        /// <param name="station"></param>
        void AddStation(Station station);

        /// <summary>
        /// Add Drone
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="stationId"></param>
        void AddDrone(Drone drone, int stationId);

        /// <summary>
        /// Add Customer
        /// </summary>
        /// <param name="customer"></param>
        void AddCustomer(Customer customer);

        /// <summary>
        /// Add Parcel
        /// </summary>
        /// <param name="parcel"></param>
        void AddParcel(Parcel parcel);
        #endregion

        #region Update:


        /// <summary>
        /// Update drone's name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        void UpdateDroneName(int id, string model);

        /// <summary>
        /// Update station
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="charging_spots"></param>
        void UpdateStetion(int id, string name, int charging_spots);
        /// <summary>
        /// Update Customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        void UpdateCustomer(int id, string name, string phone);

        /// <summary>
        ///  Charging drone
        /// </summary>
        /// <param name="id"></param>
        void UpdateChargeDrone(int id);
        /// <summary>
        /// discharge drone
        /// </summary>
        /// <param name="droneID"></param>
        /// <param name="chargingTime"></param>
        void DischargeDrone(int droneID, TimeSpan chargingTime);

        /// <summary>
        /// Update parcel to drone
        /// </summary>
        /// <param name="droneId"></param>
        void UpdateParcelToDrone(int droneId);

        /// <summary>
        /// Updete that the parcel has picked up by a drone
        /// </summary>
        /// <param name="droneId"></param>
        void UpdateParcelPickUpByDrone(int droneId);
        void UpdateParcelSuppliedByDrone(int droneId);
        void UpdateStationListDroneInCharge(int stationId, int droneId);
        #endregion

        #region Show Lists:
        IEnumerable<StationToList> ShowStationList();
        IEnumerable<DroneToList> ShowDroneList();
        IEnumerable<CustomerToList> ShowCustomerList();
        IEnumerable<ParcelToList> ShowParcelList();
        IEnumerable<ParcelToList> ShowNonAssociatedParcelList();
        IEnumerable<StationToList> ShowChargeableStationList();

        #endregion

        #region Get by ID;
        Station GetStation(int requestedId);
        Drone GetDrone(int droneId);
        Customer GetCustomer(int customerId);
        Parcel GetParcel(int parcelId);
        #endregion


        ParcelStatus FindParcelStatus(IDAL.DO.Parcel parcel);




    }
}