﻿using System;
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
            dalo.UpdateCustumer(id, name, phone);
        }
        //sending drone to charge
        public void UpdateChargeDrone(int droneId)
        {
            IDAL.DO.Station station = new IDAL.DO.Station();
            DroneToList dronel = drones.Find(x => x.Id == droneId);
            if (dronel.droneStatuses == DroneStatuses.Available)
            {
                List<Distanse> disStationFromDrone = dalo.MinimumDistance(dronel.location.Longitude, dronel.location.Latitude);//the distance all station from the drone
                double min = 9999999;
                int idS;
                bool flag = false;
                int sized = disStationFromDrone.Count;
                int counter = 0;
                while (!flag && counter <= sized)
                {
                    foreach (Distanse item in disStationFromDrone)
                    {
                        if (item.distance <= min)
                        {

                            min = item.distance;
                            idS = item.id;

                        }

                        station = dalo.GetBaseStation(item.id);
                        if (station.ChargeSlots > 0)
                        {
                            if (dronel.battery > min * 10 / 100)
                            {
                                flag = true;
                                //function to update Battery, drone mode drone location
                                updateDtoneAndStation(droneId, station.Id, min);
                            }

                        }
                        counter++;
                        disStationFromDrone.Remove(item);
                    }
                }
            }
        }
        public void updateDtoneAndStation(int droneId,int stationId,double minDistance )
        {
            //עדכון בעיקבות הנסיעה
            DroneToList dronel = drones.Find(x => x.Id == droneId);
            IDAL.DO.Station station = new IDAL.DO.Station();
            station = dalo.GetBaseStation(stationId);
            dronel.droneStatuses = DroneStatuses.Shipping;
            dronel.location.Latitude = station.Latitude;
            dronel.location.Longitude = station.Longitude;
            double droneBattery = minDistance * 10 / 100;
            dronel.battery = droneBattery;
            dalo.UpdateChargeSlots(station.Id);
            //להכניס את זה לרשימת רחפנים בטעינה 
            DroneInCharging droneInCharging = new DroneInCharging();
            droneInCharging.battery = droneBattery;
            droneInCharging.Id = droneId;
            Station s = new Station();
            s.droneInChargings.Add(droneInCharging);
           


        }
        public void UpdateDichargeDrone(int id, DateTime chargingTime)
        {

        }
        public void UpdateParcelToDrone(int droneId, int drone_id)
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

        public void DischargeDrone(int droneId, int TimeOfCharging)
        {
            DroneToList dronel = drones.Find(x => x.Id == droneId);
            if (dronel.droneStatuses != DroneStatuses.Maintenance)
            {
                throw new NotImplementedException();
            }
            //   else

        }

        public void UpdateDeliveryToCustomer(int parcel_id3, int customer_id)
        {
            throw new NotImplementedException();
        }

        public void UpdateParcelToDrone(int droneId)
        {
            throw new NotImplementedException();
        }
    }
}
