using IDAL.DO;
using System.Collections.Generic;

namespace IDAL.DO
{
    public interface IDal
    {
        void AddBaseStation(Station s);
        void AddCustomer(Customer c);
        void AddDrone(Drone d);
        void AddParcel(Parcel p);
        void DischargeDrone(int drone_id, int station_id);
        double CalculateDistance(double longitude1, double latitude1, double longitude2, double latitude2);
        Customer FindCustomer(int customerID);
        Station FindStetion(int stationID);
        Station GetBaseStation(int id);
        List<Station> ShowBaseStationList();
        List<Station> ShowChargeableBaseStationList();
        Customer GetCustomer(int IDc);
        List<Customer> ShowCustomerList();
        Drone GetDrone(int id);
        List<Drone> ShowDroneList();
        List<Parcel> ShowNonAssociatedParcelList();
        Parcel GetParcel(int id);
        List<Parcel> ShowParcelList();
        void UpdateDeliveryToCustomer(int parcel_id, int customer_id);
        void UpdateDroneToCharge(int drone_id, int station_id);
        void UpdateParcelPickedupByDrone(int parcel_id, int drone_id);
        void UpdateParcelToDrone(int parcel_id, int drone_id);
    }
}