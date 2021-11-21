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
    public class DalObject : IDal
    {
        public DalObject()
        {
            DataSource.Initialize();
        }


        // ADD:
        /// <summary>
        /// add Station to the stations list
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public void AddStation(Station s)
        {
            if (DataSource.Stations.Exists(station => station.Id == s.Id))
            {
                throw new StationException($"ID {s.Id} already exists!!");
            }
            else
                DataSource.Stations.Add(s);
        }
        /// <summary>
        /// add Drone to the drons list
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public void AddDrone(Drone d)
        {
            if (DataSource.drones.Exists(drone => drone.Id == d.Id))
            {
                throw new DroneException($"ID {d.Id} already exists!!");
            }
            else
                DataSource.drones.Add(d);
        }
        /// <summary>
        /// add Customer to the Customers list
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public void AddCustomer(Customer c)
        {
            if (DataSource.customer.Exists(client => client.Id == c.Id))
            {
                throw new CustomerException($"ID {c.Id} already exists!!");
            }
            else
                DataSource.customer.Add(c);
        }
        /// <summary>
        /// add parcel to the parcels list
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public void AddParcel(Parcel p)
        {
            if (DataSource.parcels.Exists(parcel => parcel.Id == p.Id))
            {
                throw new ParcelException($"ID {p.Id} already exists!!");
            }
            else
                DataSource.parcels.Add(p);
        }


        //UPDATE:
        /// <summary>
        /// update function: parcel to drone by id
        /// </summary>
        /// <param name="parcel_id"></param>
        /// <param name="drone_id"></param>
        public void UpdateParcelToDrone(int parcel_id, int drone_id)
        {
            Parcel parcel = default;
            try
            {
                parcel = GetParcel(parcel_id); //finds the parcel by its ID
            }
            catch (ParcelException pex)
            {
                throw new ParcelException($"Couldn't attribute drone {drone_id} to parcel", pex);
            }
            Drone drone = DataSource.drones.Find(x => x.Id == drone_id); //finds the drone by its ID
            parcel.DroneID = drone.Id;
            parcel.Scheduled = DateTime.Now;
            //d.Status = DroneStatuses.Shipping;
        }
        /// <summary>
        /// Update function for parcel
        /// </summary>
        /// <param name="parcel_id"></param>
        /// <param name="drone_id"></param>
        public void UpdateParcelPickedupByDrone(int parcel_id, int drone_id)
        {
            Parcel p = DataSource.parcels.Find(x => x.Id == parcel_id);
            Drone d = DataSource.drones.Find(x => x.Id == drone_id);
            p.PickedUp = DateTime.Now;
            //d.Status = DroneStatuses.Shipping;
        }
        /// <summary>
        ///  Update function for parcel Customer
        /// </summary>
        /// <param name="parcel_id"></param>
        /// <param name="customer_id"></param>
        public void UpdateDeliveryToCustomer(int parcel_id, int customer_id)
        {
            Parcel p = DataSource.parcels.Find(x => x.Id == parcel_id);
            Customer c = DataSource.customer.Find(x => x.Id == customer_id);
            p.Delivered = DateTime.Now;
            // finding the drone that sent the parcel-- to make it available to the next ship
            int drone_id = p.DroneID;
            Drone d = DataSource.drones.Find(x => x.Id == drone_id);
            //d.Status = DroneStatuses.Available;
        }
        /// <summary>
        ///  Update function for parcel DroneToCharge
        /// </summary>
        /// <param name="drone_id"></param>
        /// <param name="station_id"></param>
        public void UpdateDroneToCharge(int drone_id, int station_id)
        {
            Drone d = DataSource.drones.Find(x => x.Id == drone_id;
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
            //d.Status = DroneStatuses.Available;
            s.ChargeSlots++;
        }
        /// <summary>
        /// Put the skimmer in it for initial charging
        /// </summary>
        /// <param name="StationId"></param>
        /// <param name="drone"></param>
        public void UpdateDroneToStation(int StationId,Drone drone)
        {
            Station station = DataSource.Stations.Find(x => x.Id == StationId);
            station.ChargeSlots = drone.Id;
        }
         public  void UpdateNameOfDrone(int id, string model)
        {
            Drone drone = DataSource.drones.Find(x => x.Id == id);
            drone.Model = model;
        }
        /// <summary>
        /// Update station data
        /// </summary>
        /// <param name="StationId"></param>
        /// <param name="name"></param>
        /// <param name="charging_spots"></param>
        public void UpdateStetion(int StationId, string name, int charging_spots)
        {
            Station station = DataSource.Stations.Find(x => x.Id == StationId);
            station.ChargeSlots = charging_spots;
            station.Name = name;
            station.Id = StationId;
        }
       public void UpdateCustumer(int custumerId, string name, string phone)
        {
            Customer customer = DataSource.customer.Find(x => x.Id == custumerId);
            customer.Id = custumerId;
            customer.Name = name;
            customer.Phone = phone;
        }
        /// <summary>
        /// Looking for the most base with upcoming vacancies
        /// </summary>
       public List<Distanse> MinimumDistance(double lang, double lati)
        {
            List<Distanse> listDis=new List<Distanse>();
            foreach (Station element in DataSource.Stations)
            {

                Distanse distanse =new Distanse();
                double dis=0;
                dis = (element.Longitude - lang)* (element.Longitude - lang) + (element.Latitude - lang)* (element.Latitude - lang);
                dis= Math.Sqrt(dis);
                distanse.id = element.Id;
                distanse.distance = dis;

                listDis.Add(distanse);


            }
            return listDis;

        }
       public void UpdateChargeSlots(int stationId)
        {
            Station station = DataSource.Stations.Find(x => x.Id == stationId);
            station.ChargeSlots--;
        }



        //GET:

        /// <summary>
        /// view function for Station
        /// </summary>
        /// <param name="id"></param>
        public Station GetBaseStation(int id)
        {
            if (!DataSource.Stations.Exists(item => item.Id == id))
            {
                throw new StationException($"ID: {id} does not exist!!");
            }
            return DataSource.Stations.First(c => c.Id == id);
        }
        /// <summary>
        /// view function for Drone
        /// </summary>
        /// <param name="id"></param>
        public Drone GetDrone(int id)
        {
            if (!DataSource.drones.Exists(item => item.Id == id))
            {
                throw new DroneException($"ID: {id} does not exist!!");
            }
            return DataSource.drones.First(c => c.Id == id);
        }
        /// <summary>
        /// view function for Customer by id
        /// </summary>
        /// <param name="id"></param>
        public Customer GetCustomer(int IDc)
        {
            if (!DataSource.customer.Exists(item => item.Id == IDc))
            {
                throw new CustomerException($"ID: {IDc} does not exist!!");
            }
            return DataSource.customer.First(c => c.Id == IDc);
        }
        /// <summary>
        /// view function for Parcel with id
        /// </summary>
        /// <param name="id"></param>
        public Parcel GetParcel(int id)
        {
            if (!DataSource.parcels.Exists(item => item.Id == id))
            {
                throw new ParcelException($"ID: {id} does not exist!!");
            };
            return DataSource.parcels.First(c => c.Id == id);
        }


        /// <summary>
        /// view lists functions for BaseStation
        /// </summary>
        public List<Station> ShowStationList()
        {
            List<Station> baseStationList = new List<Station>();
            foreach (Station element in DataSource.Stations)
            {
                baseStationList.Add(element);
            }
            return baseStationList;
        }
        /// <summary>
        /// view lists functions for Drone
        /// </summary>
        public List<Drone> ShowDroneList()
        {
            List<Drone> DroneList = new List<Drone>();
            foreach (Drone element in DataSource.drones)
            {
                DroneList.Add(element);
            }
            return DroneList;
        }
        /// <summary>
        /// view lists functions for Customer
        /// </summary>
        public List<Customer> ShowCustomerList()
        {
            List<Customer> CustomerList = new List<Customer>();
            foreach (Customer element in DataSource.customer)
            {
                CustomerList.Add(element);
            }
            return CustomerList;
        }
        /// <summary>
        /// view lists functions for Parcel
        /// </summary>
        public List<Parcel> ShowParcelList()
        {
            List<Parcel> ParcelList = new List<Parcel>();
            foreach (Parcel element in DataSource.parcels)
            {
                ParcelList.Add(element);
            }
            return ParcelList;
        }

        /// <summary>
        /// shows the list of packages that haven't been associated to a drone
        /// </summary>
        /// <returns></returns>
        public List<Parcel> ShowNonAssociatedParcelList()
        {
            List<Parcel> NonAssociatedParcelList = new List<Parcel>();
            foreach (Parcel element in DataSource.parcels)
            {
                if (element.DroneID == 0)
                    NonAssociatedParcelList.Add(element);
            }
            return NonAssociatedParcelList;
        }
        /// <summary>
        ///  shows base stations with available charging spots
        /// </summary>
        public List<Station> ShowChargeableBaseStationList()
        {
            List<Station> ChargeableBaseStationList = new List<Station>();
            foreach (Station element in DataSource.Stations)
            {
                if (element.ChargeSlots > 0)
                    ChargeableBaseStationList.Add(element);
            }
            return ChargeableBaseStationList;
        }
        //--BONUS--: another option that recives coordinates and print the distance from it to a station or a customer
        public double CalculateDistance(double longitude1, double latitude1, double longitude2, double latitude2)
        {
            // this function is from https://stackoverflow.com/questions/27928/calculate-distance-between-two-latitude-longitude-points-haversine-formula
            // (with changes for C#)
            var p = 0.017453292519943295;    // Math.PI / 180
            var a = 0.5 - Math.Cos((latitude2 - latitude1) * p) / 2 +
                    Math.Cos(latitude1 * p) * Math.Cos(latitude2 * p) *
                    (1 - Math.Cos((longitude2 - longitude1) * p)) / 2;

            return 12742 * Math.Asin(Math.Sqrt(a)); // 2 * R; R = 6371 km

        }

        /// <summary>
        /// find customer by ID
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public Customer FindCustomer(int customerID)
        {
            return DataSource.customer.Find(x => x.Id == customerID);
        }
        /// <summary>
        /// find station by ID
        /// </summary>
        /// <param name="stationID"></param>
        /// <returns></returns
        public Station FindStetion(int stationID)
        {
            return DataSource.Stations.Find(x => x.Id == stationID);
        }
        /// <summary>
        /// Exit function
        /// </summary>
        /// <returns></returns>
        /// 
        public Drone FindDrone(int droneId)
        {
            return DataSource.drones.Find(x => x.Id == droneId);
        }
        public static int Exit()
        {
            return 0;
        }

        //IEnumerable<Station> IDal.ShowBaseStationList()
        //{
        //    throw new NotImplementedException();
           
        //}

        //IEnumerable<Drone> IDal.ShowDroneList()
        //{
        //    throw new NotImplementedException();
        //}

        //IEnumerable<Customer> IDal.ShowCustomerList()
        //{
        //    throw new NotImplementedException();
        //}

        //IEnumerable<Parcel> IDal.ShowParcelList()
        //{
        //    throw new NotImplementedException();
        //}

        //IEnumerable<Parcel> IDal.ShowNonAssociatedParcelList()
        //{
        //    throw new NotImplementedException();
        //}

        public IEnumerable<Station> ShowChargeableStationList()
        {
            throw new NotImplementedException();
        }

        public void StationException(int id, string errMsg)
        {
            throw new NotImplementedException();
        }

        public void CustomerException(int id, string errMsg, Severity severity)
        {
            throw new NotImplementedException();
        }

        public void ParcelException(int id, string errMsg, Severity severity)
        {
            throw new NotImplementedException();
        }

        public void DroneException(int id, string errMsg)
        {
            throw new NotImplementedException();
        }

     
        //List<Distanse> IDal(double lang, double lati)
        //{
        //    throw new NotImplementedException();
        //}

    }
  
}
