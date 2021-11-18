using IDAL.DO;
using DalObject;
using System.Collections.Generic;

namespace IDAL
{
    public interface IDal
    {
        void AddCustomer(Customer c);
        void AddDrone(Drone d);
        void AddParcel(Parcel p);
        void AddStation(Station s);
        double CalculateDistance(double longitude1, double latitude1, double longitude2, double latitude2);
        void CustomerException(int id, string errMsg, Severity severity);
        void DischargeDrone(int drone_id, int station_id);
        void DroneException(int id, string errMsg);
        Customer FindCustomer(int customerID);
        Station FindStetion(int stationID);
        Drone FindDrone(int DroneID);
        Station GetBaseStation(int id);
        Customer GetCustomer(int IDc);
        Drone GetDrone(int id);
        Parcel GetParcel(int id);
        void ParcelException(int id, string errMsg, Severity severity);
        List<Station> ShowStationList();
        List<Station> ShowChargeableBaseStationList();
        IEnumerable<Station> ShowChargeableStationList();
        List<Customer> ShowCustomerList();
        List<Drone> ShowDroneList();
        List<Parcel> ShowNonAssociatedParcelList();
        List<Parcel> ShowParcelList();
        void StationException(int id, string errMsg);
        void UpdateDeliveryToCustomer(int parcel_id, int customer_id);
        void UpdateDroneToCharge(int drone_id, int station_id);
        void UpdateParcelPickedupByDrone(int parcel_id, int drone_id);
        void UpdateParcelToDrone(int parcel_id, int drone_id);
        void UpdateDroneToStation(int StationId,Drone drone);
        void UpdateNameOfDrone(int DroneId, string model);
        void UpdateStetion(int StationId, string name, int charging_spots);
        void UpdateCustumer(int custumerId, string name, string phon);
        List<Distanse> MinimumDistance(double lang,double lati);
    }
}