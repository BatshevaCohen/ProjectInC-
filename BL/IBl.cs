using IBL.BO;
using System;
using System.Collections.Generic;


namespace IBL.BO
{
    public interface IBL
    {
        //ADD:
        void AddStation(Station station);
        void AddDrone(Drone drone, int stationId);
        void AddCustomer(Customer customer);
        void AddParcel(Parcel parcel);
        //UPDATE:
        void UpdateDroneName(int id, string model);
        void UpdateStetion(int id, string name, int charging_spots);
        void UpdateCustomer(int id, string name, string phone);
        void UpdateChargeDrone(int id);
        void UpdateParcelToDrone(int droneId);
        void UpdateParcelPickUpByDrone(int droneId);
        void UpdateParcelSuppliedByDrone(int droneId);
        //SHOW:
        Station GetStation(int requestedId);
        Drone GetDroneBL(int droneId);
        Customer GetCustomer(int customerId);
        Parcel GetParcel(int parcelId);
        //SHOW LIST:
        IEnumerable<Station> ShowStationList();
        IEnumerable<Drone> ShowDroneList();
        IEnumerable<Customer> ShowCustomerList();
        IEnumerable<Parcel> ShowParcelList();
        IEnumerable<Parcel> ShowNonAssociatedParcelList();
        IEnumerable<Station> ShowChargeableStationList();

        //void AddDroneToList(Drone drone, Station station);



    }
}