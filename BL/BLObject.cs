using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using DalObject.DO;
using IBL.BO;
using IDAL;


namespace IBL.BO
{
    public class BLObject : IDAL
    {
        private IDal dal;
        private List<DroneToList> drones;
        static Random r = new Random();
        DateTime ZeroTime = new DateTime(2000, 1, 1, 00, 00, 00); //default time when nothing inserted 
        public BLObject()
        {
            drones = new List<DroneToList>();
            dal = new DalObject.DalObject();
        }
        //ADD:
        public void AddStation(Station station)
        {
            throw new NotImplementedException();
        }
        public int AddDrone(Drone drone, int stationId)
        {
            Station station= DataSource.Stations.Find(x => x.Id == stationId);
            drone.Location = station.Location;
            drone.Battery = r.Next(20, 40);
            drone.DroneStatuses = DroneStatuses.Maintenance;
            throw new NotImplementedException();
        }
        public void AddCustomer(Customer customer)
        {

        }
        public void AddParcel(Parcel parcel)
        {
            parcel.Id = ++(DalObject.DO.DataSource.OrdinalNumber); //static serial number for parcel id
            parcel.ParcelCreationTime = DateTime.Now;
            parcel.AssignmentToParcelTime = ZeroTime;
            parcel.CollectionTime = ZeroTime;
            parcel.SupplyTime = ZeroTime;
            parcel.DroneInParcel = null;
        }
        //UPDATE:
        public void UpdateDroneName(int id, string model)
        {
            Drone drone = DataSource.drones.Find(x => x.Id == id);
            drone.Model = model;
        }
        public void UpdateStetion(int id, string name, int charging_spots)
        {
            Station station = DataSource.Stations.Find(x => x.Id == id);
            station.Name = name;
            int takenSpots = DataSource.Stations.droneInChargings.Count;
            station.AvailableChargingSpots = charging_spots- takenSpots;
        }
        public void UpdateCustomer(int id, string name, string phone)
        {

        }
        public void UpdateChargeDrone(int id)
        {

        }
        public void UpdateDichargeDrone(int id, DateTime chargingTime)
        {

        }
        public void UpdateParcelToDrone(int droneId)
        {

        }
        public void UpdatePickUpParcelByDrone(int droneId)
        {

        }
        public void UpdateParcelSupplyByDrone(int droneId)
        {

        }
        //SHOW:
        BO.Station GetStation(int requestedId)
        {
            throw new NotImplementedException();
        }
        BO.Drone GetDrone(int droneId)
        {

        }
        Customer GetCustomer(int customerId)
        {

        }
        Parcel GetParcel(int parcelId)
        {

        }
        //SHOW LIST:
        public List<Station> ShowStationList()
        {

        }
        public List<Drone> ShowDroneList()
        {

        }
        public List<Customer> ShowCustomerList()
        {

        }
        public List<Parcel> ShowParcelList()
        {

        }
        public List<Parcel> ShowNonAssociatedParcelList()
        {

        }
        public List<Station> ShowChargeableStationList()
        {

        }



        //Station IBL.GetStation(int requestedId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
