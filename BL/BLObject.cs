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
    public class BLObject : IBL
    {
        public static IDAL.IDal dalo = new DalObject.DalObject();//Access to the layer DAL
        public List<DroneToList> drones;
        static Random r = new Random();
        DateTime ZeroTime = new DateTime(2000, 1, 1, 00, 00, 00); //default time when nothing inserted 
        public BLObject()
        {
            drones = new List<DroneToList>();
           // dal = new DalObject.DalObject();
        }
        //ADD:
        public void AddStation(Station station)
        {
            IDAL.DO.Station s = new IDAL.DO.Station();
            s.Name = station.Name;
            s.Id = station.Id;
            s.Longitude = station.Location.Longitude;
            s.Latitude = station.Location.Latitude;
            dalo.AddStation(s);//send the new station to DAL 
            throw new NotImplementedException();
        }
        public int AddDrone(Drone drone, int stationId)
        {
            IDAL.DO.Drone d = new IDAL.DO.Drone();
            d.Id = drone.Id;
            d.Model = drone.Model;
            d.MaxWeight = (WeightCategories)drone.Weight;
            dalo.AddDrone(d);


            dalo.UpdateDroneToStation(stationId, d);
           
            
           
            throw new NotImplementedException();
        }
        public void AddCustomer(Customer customer)
        {
            IDAL.DO.Customer c = new IDAL.DO.Customer();
            c.Id = customer.Id;
            c.Name = customer.Name;
            c.Phone = customer.Phone;
            dalo.AddCustomer(c);
            
            throw new NotImplementedException();
        }
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
            p.Resiver = parcel.Resiver.Id;
            p.Weight = (WeightCategories)parcel.Weight;
            p.Priority = (Priorities)parcel.Priority;
            dalo.AddParcel(p);
            
        }
        //UPDATE:
        public void UpdateDroneName(int id, string model)
        {
           
            dalo.UpdateNameOfDrone(id, model);
          
        }
        public void UpdateStetion(int id, string name, int charging_spots)
        {
            dalo.UpdateStetion(id, name, charging_spots);
        }
        public void UpdateCustomer(int id, string name, string phone)
        {
            //???????????????????????????????????????????????????????????????????
        }
        public void UpdateChargeDrone(int id)
        {
            IDAL.DO.Station station = new IDAL.DO.Station();
            DroneToList dronel = drones.Find(x => x.Id == id);
            if (dronel.droneStatuses == DroneStatuses.Available)
            {
                List<Distanse> d = dalo.MinimumDistance(dronel.location.Longitude, dronel.location.Latitude);
                double min = 9999999;
                int id1;
                bool flag = false;
                int sized = d.Count;int counter = 0;
                while (!flag&&counter<=sized)
                {
                    foreach (Distanse item in d)
                    {
                        if (item.distanse <= min)
                        {
                            min = item.distanse;
                            id1 = item.id;
                        }
                        station = dalo.GetBaseStation(id);
                        if (station.ChargeSlots > 0)
                        {
                            if (dronel.battery > 50)///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            {
                                flag = true;
                            }

                        }
                        counter++;
                        d.Remove(item);
                    }
                }
            }
            


              




            


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
            throw new NotImplementedException();
        }
        Customer GetCustomer(int customerId)
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



       
    }
}
