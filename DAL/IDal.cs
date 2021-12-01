using IDAL.DO;
using DalObject;
using System.Collections.Generic;
using System;

namespace IDAL
{
    public interface IDal
    {
        void AddCustomer(Customer c);
        void AddDrone(Drone d);
        void AddParcel(Parcel p);
        void AddStation(Station s);
        double CalculateDistance(double longitude1, double latitude1, double longitude2, double latitude2);
        void DischargeDrone(int drone_id, double longt,double latit);
        void ReleaseDroneFromCharging(int droneId);       
        
        Station GetStation(int id);
        Customer GetCustomer(int IDc);
        Drone GetDrone(int id);
        Parcel GetParcel(int id);
       
        //SHOW LISTS:
        IEnumerable<Station> ShowStationList();
        IEnumerable<Station> ShowChargeableStationList();
        IEnumerable<Customer> ShowCustomerList();
        IEnumerable<Drone> ShowDroneList();
        IEnumerable<Parcel> ShowNonAssociatedParcelList();
        IEnumerable<Parcel> ShowParcelList();
        
        //Adds an organ to the list of droneCharge
        void UpdateAddDroneToCharge(int dronId, int stationId);
        void UpdateRemoveDroneToCharge(int dronId, int stationId);
        //return DronInCharge of the station
        List<DroneCharge> GetListOfDronInCharge(int stationId);
        //List of packages that belong to the customer sender
        List<Parcel> GetListOfParcelSending(int id);
        //List of packages that belong to the customer reciever
        List<Parcel> GetListOfParcelRecirver(int id);
        List<Distanse> MinimumDistance(double lang,double lati);
        Parcel GetParcelInTransferByDroneId(int droneId);

        //UPDATE
        void DischargeDroneByLocation(int droneID, double droneLatitude, double droneLongitude);
        void UpdateDeliveryToCustomer(int parcel_id, int customer_id);
        void UpdateParcelPickedupByDrone(int parcel_id, int drone_id);
        void UpdateParcelToDrone(int parcel_id, int drone_id);
        //return the station in order to update the locations drone 
        Station UpdateStationChargingSpots(int StationId);
        void UpdateNameOfDrone(int DroneId, string model);
        void UpdateStetion(int StationId, string name, int charging_spots);
        void UpdateCustumer(int custumerId, string name, string phon);


        void UpdateChargeSpots(int stationId);
        double[] PowerRequest();
        Parcel GetParcelByDroneId(int DroneId);
        Station GetClosestStation(double latitude, double longitude);
        double[] PowerConsumptionRequest();
        double GetDistanceBetweenLocationAndClosestBaseStation(int Reciverid);
        double GetDistanceBetweenLocationsOfParcels(int senderId, int targetId);
        //SHOW  FUNCTION


        //void StationException(int id, string errMsg);
        //void DroneException(int id, string errMsg);
        //void CustomerException(int id, string errMsg, Severity severity);
        //void ParcelException(int id, string errMsg, Severity severity);

    }
}