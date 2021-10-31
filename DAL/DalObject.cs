using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject.DO;
using IDAL;
using IDAL.DO;
//
namespace DalObject
{
    public class DalObject
    {
       public DalObject()
        {
            DataSource.Initialize();
        }



        //add functions
        public  int AddBaseStation(int id,string name,double longtitut , double latitute,int chargesolt)
        {
            Station b = new Station();
            b.Id = id;
            b.Name = name;
            b.Latitude = latitute;
            b.Longitude = longtitut;
            b.ChargeSlots = chargesolt;
            DataSource.Stations.Add(b);

            return 1;

        }
        public int AddDrone(int id, string model,  double battery,int maxWeight,int status)
        {
            Drone d = new Drone();
            d.Id = id;
            d.Model = model;
            d.Battery = battery;
            d.Staus = (Enums.DroneStatuses)status;
            d.MaxWeight = (Enums.WeightCategories)maxWeight;
                 DataSource.drones.Add(d);

            return 1;
        }
        public static int AddCustomer(int id, string name, string pone, double longtitut, double latitute)
        {
            Customer c = new Customer();
            c.Id = id;
            c.Name = name;
            c.Latitude = latitute;
            c.Longitude = longtitut;
            DataSource.customer.Add(c);
            return 1;
        }
        public static int AddPackage(int id, int senderId, int targetId, Enum wheit, Enum priority, DateTime reqested, int droneId, DateTime steduled,DateTime pickedUp,DateTime delivary )
        {
            Parcel p = new Parcel();
            p.Id = id;
            p.SenderId = senderId;
            p.TargetId = targetId;
            p.Weight = (Enums.WeightCategories)wheit;
            p.Priority = (Enums.Priorities)priority;
            p.Requested = reqested;
            p.DroneID = droneId;
            p.Scheduled = steduled;
            p.PickedUp = pickedUp;
            p.Delivered = delivary;
            DataSource.parcels.Add(p);
            return 1;
        }

        //update functions
        public void UpdateParcelToDrone(Parcel p, Drone d)
        {
            p.DroneID = d.Id;
        }
        public void UpdateParcelCollectionByDrone(Parcel p, Drone d)
        {
            p.DroneID = d.Id;
            p.Scheduled = DateTime.Now;
            d.Staus = Enums.DroneStatuses.Shipping;
        }
        public void UpdateSupplyParcelToCustomer(Parcel p, Drone drone)
        {
            p.Delivered = DateTime.Now;
            drone.Staus = Enums.DroneStatuses.Available;
        }
        public void UpdateDroneToCharge(Drone drone, Station station)
        {
            drone.Staus = Enums.DroneStatuses.Maintenance;
            DroneCharge droneCharge = new DroneCharge();
            droneCharge.DroneId = drone.Id;
            droneCharge.StationId = station.Id;
            station.ChargeSlots--;
        }
        public void DischargeDrone(Drone drone, Station station)
        {
            station.ChargeSlots++;
            drone.Staus = Enums.DroneStatuses.Available;
        }

        //view function
        public void ShowBaseStation(int id)
        {
            DataSource.Stations.Find(x => x.Id == id).ToString();
        }
        public void ShowDrone(int id)
        {
            DataSource.drones.Find(x => x.Id == id).ToString();
        }
        public void ShowCustomer(int id)
        {
            DataSource.customer.Find(x => x.Id == id).ToString();
        }
        public void ShowParcel(int id)
        {
            DataSource.parcels.Find(x => x.Id == id).ToString();
        }

        //view lists functions
        public void ShowBaseStationList()
        {
            foreach(Station element in DataSource.Stations)
            {
                Console.WriteLine(element.ToString());
            }
        }
        public void ShowDroneList()
        {
            foreach (Drone element in DataSource.drones)
            {
                Console.WriteLine(element.ToString());
            }
        }
        public void ShowCustomerList()
        {
            foreach (Customer element in DataSource.customer)
            {
                Console.WriteLine(element.ToString());
            }
        }
        public void ShowParcelList()
        {
            foreach (Parcel element in DataSource.parcels)
            {
                Console.WriteLine(element.ToString());
            }
        }
        // shows the list of packages that haven't been associated to a drone
        public void ShowNonAssociatedParcelList()
        {
            foreach (Parcel element in DataSource.parcels)
            {
                if (element.DroneID == 0)
                    Console.WriteLine(element.ToString());
            }
        }
        // shows base stations with available charging spots
        public void ShowChargeableBaseStationList()
        {
            foreach (Station element in DataSource.Stations)
            {
                if (element.ChargeSlots > 0)
                    Console.WriteLine(element.ToString());
            }
        }
        //EXIT
        public int Exit()
        {
            return 0;
        }
    }
}
