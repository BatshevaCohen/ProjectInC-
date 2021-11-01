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
    /// <summary>
    /// constractor 
    /// </summary>
    public class DalObject
    {
        public DalObject()
        {
            DataSource.Initialize();
        }

        /// <summary>
        /// add Station to the stations list
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int AddBaseStation(Station s)
        { 
            
            DataSource.Stations.Add(s);

            return 1;
        }
        /// <summary>
        /// add Drone to the drons list
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public int AddDrone(Drone d)
        {
            
            DataSource.drones.Add(d);
            return 1;
        }
        /// <summary>
        /// add Customer to the Customers list
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int AddCustomer(Customer c)
        {
            DataSource.customer.Add(c);
            return 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int AddParcel(Parcel p )
        {
          
            DataSource.parcels.Add(p);
            return 1;
        }

        //update functions
        public void UpdateParcelToDrone(int parcel_id, int drone_id)
        {
            Parcel p = DataSource.parcels.Find(x => x.Id == parcel_id);
            Drone d = DataSource.drones.Find(x => x.Id == drone_id);
            p.DroneID = d.Id;
            p.Scheduled = DateTime.Now;
            d.Staus = Enums.DroneStatuses.Shipping;
        }
        public void UpdateParcelPickedupByDrone(int parcel_id, int drone_id)
        {
            Parcel p = DataSource.parcels.Find(x => x.Id == parcel_id);
            Drone d = DataSource.drones.Find(x => x.Id == drone_id);
            p.PickedUp = DateTime.Now;
            d.Staus = Enums.DroneStatuses.Shipping;
        }
        public void UpdateDeliveryToCustomer(int parcel_id, int customer_id)
        {
            Parcel p = DataSource.parcels.Find(x => x.Id == parcel_id);
            Customer c = DataSource.customer.Find(x => x.Id == customer_id);
            p.Delivered= DateTime.Now;
            // finding the drone that sent the parcel-- to make it available to the next ship
            int drone_id = p.DroneID;
            Drone d= DataSource.drones.Find(x => x.Id == drone_id);
            d.Staus = Enums.DroneStatuses.Available;
        }
        public void UpdateDroneToCharge(int drone_id, int station_id)
        {
            Drone d = DataSource.drones.Find(x => x.Id == drone_id);
            d.Staus = Enums.DroneStatuses.Maintenance;
            DroneCharge droneCharge = new DroneCharge();
            droneCharge.DroneId = drone_id;
            Station s = DataSource.Stations.Find(x => x.Id == station_id);
            droneCharge.StationId = station_id;
            s.ChargeSlots--;
        }
        public void DischargeDrone(int drone_id, int station_id)
        {
            Drone d = DataSource.drones.Find(x => x.Id == drone_id);
            Station s = DataSource.Stations.Find(x => x.Id == station_id);
            d.Staus = Enums.DroneStatuses.Available;
            s.ChargeSlots++;
        }

        //view function
        public void ShowBaseStation(int id)
        {
            foreach (Station element in DataSource.Stations)
            {
                if (element.Id == id)
                    Console.WriteLine(element.ToString());
            }
        }
        public void ShowDrone(int id)
        {
            foreach (Drone element in DataSource.drones)
            {
                if(element.Id==id)
                Console.WriteLine(element.ToString());
            }
        }
        public void ShowCustomer(int id)
        {
            foreach (Customer element in DataSource.customer)
            {
                if (element.Id == id)
                    Console.WriteLine(element.ToString());
            }
        }
        public void ShowParcel(int id)
        {
            foreach (Parcel element in DataSource.parcels)
            {
                if (element.Id == id)
                    Console.WriteLine(element.ToString());
            }
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
        public static int Exit()
        {
            return 0;
        }
    }
}
