
using Dal;
using DO;
using DXML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace DAL
{
     sealed class DXML : DalApi.IDal
    {
        private XElement CustumerRoot;
        //XMLTools XMLTools;
        static string dronePath = @"Drones.xml";
        static string customerPath = @"Customers.xml";
        static string stationPath = @"Stations.xml";
        static string parcelPath = @"Parcels.xml";
        static string droneChargePath = @"droneCharges.xml";
        static string UserPath = @"users.xml";

        #region singelton
        public static DXML Instance { get; } = new DXML();
        private static object locker = new object();
        

        /// <summary>
        /// constructor - calls DataSource.initialize() to initialize lists
        /// </summary>
        private DXML()
        {
            //string dir = @"..\xml\";
            //XMLTools = new XMLTools();
            //if (!File.Exists(dir + customerPath))
            //    CreateFiles();
            //else
            //    LoadData();
        }
    static DXML()
        {
            DataSource.Initialize();
            XMLTools.SaveListToXMLSerializer<Drone>(DataSource.Drones, dronePath);
            //XMLTools.SaveListToXMLSerializer<Parcel>(DataSource.Parcels, parcelPath);
            //XMLTools.SaveListToXMLSerializer<Station>(DataSource.Stations, stationPath);
            //XMLTools.SaveListToXMLSerializer<User>(DataSource.userList, UserPath);
            //XMLTools.SaveListToXMLSerializer<DroneCharge>(DataSource.DroneCharges, droneChargePath);
        }

        ///// <summary>
        ///// instance of DalObject class - same object is always returned
        ///// </summary>
        //public static DXML Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //         //   lock (locker)
        //            {
        //                if (instance == null) 
                           
        //            }
        //        }
        //        return instance;
        //    }
        //}

        #endregion

        #region DalXML Drones

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        public void AddDrone(Drone d)
        {
            List<DO.Drone> dronsList = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            if (dronsList.Exists(drone => drone.Id == d.Id))
            {
                throw new DroneException($"ID {d.Id} already exists!!");
            }
            else
            {
                dronsList.Add(d);
                XMLTools.SaveListToXMLSerializer<Drone>(dronsList, dronePath);
            }
        }
        /// <summary>
        /// Update Name Of Drone
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public void UpdateNameOfDrone(int id, string model)
        {
            List<DO.Drone> dronsList = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            Drone drone = dronsList.Find(x => x.Id == id);
            dronsList.Remove(drone);
            drone.Model = model;
            dronsList.Add(drone);
            XMLTools.SaveListToXMLSerializer<Drone>(dronsList, dronePath);
        }
        public Drone GetDrone(int id)
        {
            List<DO.Drone> dronsList = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            if (dronsList.Exists(item => item.Id == id))
            {
                throw new DroneException($"ID: {id} does not exist!!");
            }
            return dronsList.Find(c => c.Id == id);
        }

        public IEnumerable<Drone> ShowDroneList(Func<Drone, bool> predicate = null)
        {
            List<DO.Drone> dronsList = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            if (predicate == null)
            {
                List<Drone> DroneList = new List<Drone>();
                foreach (Drone element in dronsList)
                {
                    DroneList.Add(element);
                }
                return DroneList;
            }
            else
                return dronsList.Where(predicate).ToList();
        }
        /// <summary>
        /// Send a drone to charge
        /// </summary>
        /// <param name="droneId">the drone to send to charge</param>
        /// <param name="stationId">the station to send it to charge</param>
        public void SendDroneToCharge(int droneId, int stationId)
        {
            List<DO.Drone> dronsList = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            List<DO.Station> stationList = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            List<DO.DroneCharge> dronechargeList = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargePath);

            Drone drone = GetDrone(droneId);
            Station station = GetStation(stationId);
            stationList.Remove(station);

            dronechargeList.Add(new DroneCharge
            {
                DroneId = drone.Id,
                StationId = station.Id
            });
            station.ChargeSpots--;

            stationList.Add(station);
        }
        /// <summary>
        /// Uncharge drone and update the station
        /// </summary>
        /// <param name="dronId"></param>
        /// <param name="stationId"></param>
        public void UpdateRemoveDroneToCharge(int dronId, int stationId)
        {
            List<DO.Drone> dronsList = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
           
            List<DO.DroneCharge> dronechargeList = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargePath);
            DroneCharge droneCharge = new()
            {
                DroneId = dronId,
                StationId = stationId
            };
            //מחיקת מופע לרשימה
            dronechargeList.Remove(droneCharge);
            Drone d = dronsList.Find(x => x.Id == dronId);
            dronsList.Remove(d);
            d.Status = DroneStatuses.Available;
            dronsList.Add(d);
        }
        /// <summary>
        /// release a drone from charge
        /// </summary>
        /// <param name="droneId">the id of the drone to release</param>
        public void ReleaseDroneFromCharging(int droneId)
        {
            List<DO.Drone> dronsList = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
               List<DO.DroneCharge> dronechargeList = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargePath);
            List<DO.Station> stationList = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            Drone drone = GetDrone(droneId);
            dronsList.Remove(drone);

            DroneCharge droneCharge = dronechargeList.Find(x => x.DroneId == droneId);


            int stationId = droneCharge.StationId;
            Station station = GetStation(stationId);
            stationList.Remove(station);

            station.ChargeSpots++;

            stationList.Add(station);
            dronsList.Add(drone);
            dronechargeList.Remove(droneCharge);

        }

        /// <summary>
        /// discharge drone
        /// <param name="droneID"></param>
        /// <param name="droneLatitude"></param>
        /// <param name="droneLongitude"></param>
        /// <exception cref="Exception"></exception>
        public Station DischargeDroneByLocation(int droneID, double droneLatitude, double droneLongitude)
        {
            List<DO.Drone> dronsList = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            List<DO.Station> stationList = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            Drone d = dronsList.Find(x => x.Id == droneID);
            Station s = new Station();
            foreach (Station item in stationList) //finds the station
            {
                if (item.Latitude == droneLatitude && item.Longitude == droneLongitude)
                {
                    stationList.Remove(s);
                    s = item;
                    s.ChargeSpots++;
                    stationList.Add(s);
                    return s;
                }
            }
            throw new Exception("couldn't find station by drones location");
        }
        /// <summary>
        /// Update the station to have one less spot for charging (because we sent a drone to charg there)
        /// </summary>
        /// <param name="StationId"></param>
        /// <param name="drone"></param>
        public Station UpdateStationChargingSpots(int StationId)
        {
            List<DO.Station> stationList = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            Station station = stationList.Find(x => x.Id == StationId);
            station.ChargeSpots -= 1;
            return station;
        }


        /// <summary>
        /// Method of applying drone power
        /// </summary>
        /// <returns>An array of the amount of power consumption of a drone for each situation</returns>
        public double[] PowerRequest()
        {
            XElement Config = XElement.Load(@"..\xml\config.xml");
             double[] result = { double.Parse(Config.Element("DroneElecUseEmpty").Value),
             double.Parse(Config.Element("Light").Value),
             double.Parse(Config.Element("Heavy").Value),
             double.Parse(Config.Element("Medium").Value),
             double.Parse(Config.Element("ChargingRate").Value), };
             return result;
        }
        public void updateBatteryDrone(int id, double dis)
        {
            List<DO.Drone> dronsList = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            Drone d = dronsList.Find(x => x.Id == id);
            dronsList.Remove(d);
            d.Battery -= dis * 0.001;
            dronsList.Add(d);
        }
        public void UpdateAddDroneToCharge(int dronId, int stationId)
        {
            List<DO.DroneCharge> droneChargeList = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargePath);
            droneChargeList.Add(new DroneCharge()
            {
                DroneId = dronId,
                StationId = stationId
            });
        }
        public List<Tuple<int, double>> GetListOfDronInChargeing(int stationId)
        {
           
            List<DO.DroneCharge> droneChargeList = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargePath);
            List<DO.Drone> droneList = XMLTools.LoadListFromXMLSerializer<Drone>(dronePath);
            List<Tuple<int, double>> listOnIDandBattery = new List<Tuple<int, double>>();
            foreach (DroneCharge item in droneChargeList)
            {
                if (item.StationId == stationId)
                {
                    Drone d = droneList.Find(x => x.Id == item.DroneId);
                    listOnIDandBattery.Add(new Tuple<int, double>(d.Id, d.Battery));
                }
            }
            return listOnIDandBattery;
        }

        /// <summary>
        /// The function recives station ID and returns all of the drones that are charging in that station
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        public List<DroneCharge> GetListOfDronInCharge(int stationId)
        {
            List<DO.DroneCharge> droneChargeList = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargePath);
            List<DroneCharge> newDroneCharges = new();
            foreach (DroneCharge item in droneChargeList)
            {
                if (item.StationId == stationId)
                {
                    newDroneCharges.Add(item);
                }
            }
            return newDroneCharges;
        }

        #endregion

        #region DalXML Stations
        /// <summary>
        /// View Station
        /// </summary>
        /// <param name="id"></param>
        public Station GetStation(int id)
        {
            List<DO.Station> stationList = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            if (!stationList.Exists(item => item.Id == id))
            {
                throw new StationException($"ID: {id} does not exist!!");
            }
            return stationList.First(c => c.Id == id);
        }
        public void AddStation(Station s)
        {
            List<DO.Station> stationList = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            if (stationList.Exists(station => station.Id == s.Id))
            {
                throw new StationException($"ID {s.Id} already exists!!");
            }
            else
                stationList.Add(s);
        }
        /// <summary>
        /// Update station data
        /// </summary>
        /// <param name="StationId"></param>
        /// <param name="name"></param>
        /// <param name="charging_spots"></param>
        public void UpdateStetion(int StationId, string name, int charging_spots)
        {
            List<DO.Station> stationList = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            if (!stationList.Exists(x => x.Id == StationId))
            {
                throw new Exception($"station id {StationId} dose not exit!");
            }
            Station station = stationList.Find(x => x.Id == StationId);
            stationList.Remove(station);
            station.ChargeSpots = charging_spots;
            station.Name = name;
            stationList.Add(station);
        }
        /// <summary>
        /// View Station List
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> ShowStationList(Func<Station, bool> predicate = null)
        {
            List<DO.Station> stationList = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            if (predicate == null)
            {
               
                foreach (Station element in stationList)
                {
                    stationList.Add(element);
                }
                return stationList;
            }
            return stationList.Where(predicate).ToList();
        }
        /// <summary>
        /// A function that returns a minimum distance between a point and a station
        /// </summary>
        /// <param name="senderLattitude">Lattitude of sender</param>
        /// <param name="senderLongitude">longitude of sender</param>
        /// <param name="flag">Optional field for selecting a nearby station flag = false or available nearby station flag = true</param>
        /// <returns>Station closest to the point</returns>
        public Station GetClosestStation(double senderLattitude, double senderLongitude)
        {
            List<DO.Station> stationList = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            bool flag = false;
            double minDistance = 1000000000000;
            Station station = new();
            if (!flag)
            {
                foreach (var s in stationList)
                {
                    double dictance = Math.Sqrt(Math.Pow(s.Latitude - senderLattitude, 2) + Math.Pow(s.Longitude - senderLongitude, 2));
                    if (minDistance > dictance)
                    {
                        minDistance = dictance;
                        station = s;
                    }
                }
            }
            else
            {
                foreach (var s in stationList.Where(s => s.ChargeSpots > 0))
                {
                    double dictance = Math.Sqrt(Math.Pow(s.Latitude - senderLattitude, 2) + Math.Pow(s.Longitude - senderLongitude, 2));
                    if (minDistance > dictance)
                    {
                        minDistance = dictance;
                        station = s;
                    }
                }
            }
            return station;
        }
        /// <summary>
        /// updates the number of available charging spots
        /// </summary>
        /// <param name="stationId"></param>
        public void UpdateChargeSpots(int stationId)
        {
            List<DO.Station> stationList = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            Station station = stationList.Find(x => x.Id == stationId);
            stationList.Remove(station);
            station.ChargeSpots--;
            stationList.Add(station);
        }
        /// <summary>
        /// A function that calculates the distance between a customer's location and a station for charging
        /// </summary>
        /// <param name="targetId">target Id</param>
        /// <returns>Minimum distance to the nearest station</returns>
        public double GetDistanceBetweenLocationAndClosestStation(int Reciverid)
        {
            List<DO.Station> stationList = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            double minDistance = 1000000000000;
            Customer target = GetCustomer(Reciverid);
            foreach (var s in stationList)
            {
                double dictance = Math.Sqrt(Math.Pow(s.Latitude - target.Latitude, 2) + Math.Pow(s.Longitude - target.Longitude, 2));
                if (minDistance > dictance)
                {
                    minDistance = dictance;
                }
            }
            return minDistance;
        }
      

        #endregion DalXML Stations
 
        #region DalXML Coustumer
        private void CreateFiles()
        {
            string dir = @"..\xml\";
            CustumerRoot = new XElement("Custumer");
            CustumerRoot.Save(dir + customerPath);
        }

        private void LoadData()
        {
            string dir = @"..\xml\";
            try
            {
                CustumerRoot = XElement.Load(dir + customerPath);
            }
            catch
            {
                Console.WriteLine("File upload problem");
            }
        }

        public void AddCustomer(Customer customer)
        {
            LoadData();
            if (CustumerRoot.Elements().Any(custo => Convert.ToInt32(custo.Element("ID").Value) == customer.Id))
                throw new CustomerException($"ID {customer.Id} already exists!!");

            CustumerRoot.Add(new XElement("Custumer",
                new XElement("ID", customer.Id),
                new XElement("Name", customer.Name),
                new XElement("Phone", customer.Phone)));
            CustumerRoot.Save(customer + customerPath);
        }
        public Customer GetCustomer(int Custumerid)
        {
            LoadData();

            Customer? c = new Customer();

            c = (from cus in CustumerRoot.Elements()
                 where Convert.ToInt32(cus.Element("ID").Value) == Custumerid
                 select new Customer()
                 {
                     Id = Convert.ToInt32(cus.Element("ID").Value),
                     Name = cus.Element("Name").Value,
                     Phone = cus.Element("Phone").Value,
                 }).FirstOrDefault();
            if (c.Value.Id == 0)
                throw new Exception($"custumer {Custumerid} is not exite!!");

            return (Customer)c;
        }
        public IEnumerable<Customer> ShowCustomerList(Func<Customer, bool> predicate = null)
        {
            LoadData();
            IEnumerable<Customer> custumers;
            custumers = from cus in CustumerRoot.Elements()
                        select new Customer()
                        {
                            Id = Convert.ToInt32(cus.Element("ID").Value),
                            Name = cus.Element("Name").Value,
                            Phone = cus.Element("Phone").Value
                        };

            return custumers;
        }
        public void UpdateCustumer(int custumerId, string name, string phone)
        {
            LoadData();

            XElement cus = (from cus1 in CustumerRoot.Elements()
                            where Convert.ToInt32(cus1.Element("ID").Value) == custumerId
                            select cus1).FirstOrDefault();
            cus.Element("Name").Value = name;
            cus.Element("Phone").Value = phone;
            CustumerRoot.Save(customerPath);

        }

        #endregion DalXML Coustumer

        #region DalXML Parcel
        public void AddParcel(Parcel p)
        {
            List<DO.Parcel> parcelList = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            if (parcelList.Exists(parcel => parcel.Id == p.Id))
            {
                throw new ParcelException($"ID {p.Id} already exists!!");
            }
            else
            {
                p.Id = getParcelRunIdConfig();
                parcelList.Add(p);
                XMLTools.SaveListToXMLSerializer<Parcel>(parcelList, parcelPath);
            }
        }
        /// <summary>
        /// view function for Parcel with id
        /// </summary>
        /// <param name="id"></param>
        public Parcel GetParcel(int id)
        {
            List<Parcel> parcelList = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            if (!parcelList.Exists(item => item.Id == id))
            {
                throw new ParcelException($"ID: {id} does not exist!!");
            };
            return parcelList.First(c => c.Id == id);
        }
        /// <summary>
        /// view lists functions for Parcel
        /// </summary>
        public IEnumerable<Parcel> ShowParcelList(Func<Parcel, bool> predicate = null)
        {
            List<Parcel> parcelList = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            if (predicate == null)
            {

                foreach (Parcel element in parcelList)
                {
                    parcelList.Add(element);
                }
                return parcelList;
            }
            return parcelList.Where(predicate).ToList();
        }
        /// <summary>
        /// Search for the package in delivery mode
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        public Parcel GetParcelInTransferByDroneId(int droneId)
        {
            List<Parcel> parcelList = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            Parcel p = parcelList.Find(x => x.Id == droneId);
            return p;
        }
        /// <summary>
        /// shows the list of packages that haven't been associated to a drone
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parcel> ShowNonAssociatedParcelList()
        {
            List<Parcel> NonAssociatedParcelList = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            foreach (Parcel element in NonAssociatedParcelList)
            {
                if (element.DroneID == 0)
                    NonAssociatedParcelList.Add(element);
            }
            return NonAssociatedParcelList;
        }
        public double GetDistanceBetweenLocationsOfParcels(int senderId, int targetId)
        {
            List<Station> stationList = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            double minDistance = 1000000000000;
            Customer sender = GetCustomer(senderId);
            Customer target = GetCustomer(targetId);
            foreach (var s in stationList)
            {
                double dictance = Math.Sqrt(Math.Pow(sender.Latitude - target.Latitude, 2) + Math.Pow(sender.Longitude - target.Longitude, 2));
                if (minDistance > dictance)
                {
                    minDistance = dictance;
                }
            }
            return minDistance;
        }
        /// <summary>
        /// remove parcel frome the list
        /// </summary>
        /// <param name="p"></param>
        public void RemoveParcel(Parcel p)
        {
            List<Parcel> parcelList = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            int dellParcel_index = parcelList.FindIndex(x => (x.Id == p.Id));
            if (dellParcel_index == -1)
                throw new ParcelException($"ID {p.Id} not dound!");
            parcelList.RemoveAt(dellParcel_index);
            XMLTools.SaveListToXMLSerializer<Parcel>(parcelList, parcelPath);
        }
        public List<Parcel> GetListOfParcelSending(int id)
        {
            List<Parcel> Senderparcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            
            foreach (Parcel item in Senderparcels)
            {
                if (item.SenderId == id)
                {
                    Senderparcels.Add(item);
                }
            }
            return Senderparcels;
        }
        public List<Parcel> GetListOfParcelRecirver(int id)
        {
            List<Parcel> Recieverparcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
           
            foreach (Parcel item in Recieverparcels)
            {
                if (item.ReceiverId == id)
                {
                    Recieverparcels.Add(item);
                }
            }
            return Recieverparcels;
        }
        /// <summary>
        /// update function: parcel to drone by id
        /// </summary>
        /// <param name="parcel_id"></param>
        /// <param name="drone_id"></param>
        public int UpdateParcelToDrone(int parcel_id, int drone_id)
        {
            List<Drone> droneList = XMLTools.LoadListFromXMLSerializer<Drone>(parcelPath);
            Parcel parcel = default;
            try
            {
                parcel = GetParcel(parcel_id);
            }
            catch (ParcelException pex)
            {
                throw new ParcelException($"Couldn't attribute drone {drone_id} to parcel", pex);
            }

            Drone drone = droneList.Find(x => x.Id == drone_id); //finds the drone by its ID
            if (drone.Id == 0)
            {
                throw new DroneException($"noo drone found");
            }
            parcel.DroneID = drone.Id;
            parcel.Assigned = DateTime.Now;
            return parcel.Id;


        }
        /// <summary>
        ///  Update parcel delivered to Customer
        /// </summary>
        /// <param name="parcel_id"></param>
        /// <param name="customer_id"></param>
        public void UpdateDeliveryToCustomer(int parcel_id, int customer_id)
        {
            List<Parcel> parcelList = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            Parcel p = parcelList.Find(x => x.Id == parcel_id);
            p.Supplied = DateTime.Now;
        }
           /// <summary>
        /// Get Parce lBy DroneIds parcel
        /// </summary>
        /// <param name="DroneId"></param>
        /// <returns></returns>
        public Parcel GetParcelByDroneId(int DroneId)
        {

            List<Parcel> parcelList = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            Parcel p = parcelList.Find(x => x.DroneID == DroneId);
            return p;
        }
        /// <summary>
        /// Update function for parcel
        /// </summary>
        /// <param name="parcel_id"></param>
        /// <param name="drone_id"></param>
        public void UpdateParcelPickedupByDrone(int parcel_id, int drone_id)
        {
            List<Parcel> parcelList = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            List<Drone> droneList = XMLTools.LoadListFromXMLSerializer<Drone>(parcelPath);
            Parcel p = parcelList.Find(x => x.Id == parcel_id);
            Drone d = droneList.Find(x => x.Id == drone_id);
            p.PickedUp = DateTime.Now;
            //d.Status = DroneStatuses.Shipping;
        }
        public void DischargeDrone(int drone_id, double longt, double latit)
        {
            throw new NotImplementedException();
        }
        #endregion DalXML Parcel

        #region DalXML User
        
        public User GetUser(string userName)
        {
            List<DO.User> userList = XMLTools.LoadListFromXMLSerializer<User>(UserPath);
            User myUser = userList.FirstOrDefault(user => user.UserName == userName && user.MyActivity == Activity.On);
            if (myUser != null)
                return myUser.Clone();
            throw new BadUserException("User doesn't exist", userName);
        }

        public void AddUser(User tmpUser)
        {
            List<DO.User> userList = XMLTools.LoadListFromXMLSerializer<User>(UserPath);
            if (userList.FirstOrDefault(user => user.UserName == tmpUser.UserName && user.MyActivity == Activity.On) != null)
                throw new BadUserException("User already exist", tmpUser.UserName);
            userList.Add(tmpUser.Clone());
        }
        public IEnumerable<User> GetAllUsers()
        {
            List<DO.User> userList = XMLTools.LoadListFromXMLSerializer<User>(UserPath);
            return from user in userList
                   where user.MyActivity == Activity.On
                   select user.Clone();
        }
        public IEnumerable<User> GetAllUsersBy(Predicate<User> predicate)
        {
            List<DO.User> userList = XMLTools.LoadListFromXMLSerializer<User>(UserPath);
            IEnumerable<User> myUsers = from user in userList
                                        where user.MyActivity == Activity.On
                                        where predicate(user)
                                        select user.Clone();
            if (myUsers == null)
                throw new ReadDataException("No User meets the conditions");
            return myUsers;
        }
        public void UpdateUser(User userToUpdate)
        {
            List<DO.User> userList = XMLTools.LoadListFromXMLSerializer<User>(UserPath);
            User tmpUser = userList.FirstOrDefault(user => user.UserName == userToUpdate.UserName && user.MyActivity == Activity.On);
            if (tmpUser == null)
                throw new BadUserException("User doesn't exist", userToUpdate.UserName);
            DeleteUser(tmpUser.UserName);
            AddUser(userToUpdate);
        }
        public void DeleteUser(string userName)
        {
            List<DO.User> userList = XMLTools.LoadListFromXMLSerializer<User>(UserPath);
            User myUser = userList.FirstOrDefault(user => user.UserName == userName && user.MyActivity == Activity.On);
            if (myUser != null)
                myUser.MyActivity = Activity.Off;
            else throw new BadUserException("User doesn't exist", userName);
        }
        #endregion DalXML User

        #region help func
        private int getParcelRunIdConfig()
        {
            XElement config = XElement.Load(@"..\xml\config.xml");
            int runId = Convert.ToInt32(config.Element("runId").Value);
            XElement configElement = (from dr in config.Elements()
                                      where dr.Name == "runId"
                                      select dr).FirstOrDefault();
            configElement.Value = (runId + 1).ToString();
            config.Save(@"..\xml\config.xml");
            return runId + 1;
        }
        /// <summary>
        /// Looking for the closest station with available charging spots
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <returns></returns>
        public List<Distance> MinimumDistance(double longitude, double latitude)
        {
            List<Station> stationList = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            List<Distance> listDis = new List<Distance>();
            foreach (Station element in stationList)
            {
                Distance Distance = new() { };
                double dis;
                dis = (element.Longitude - longitude) * (element.Longitude - longitude) + (element.Latitude - longitude) * (element.Latitude - longitude);
                dis = Math.Sqrt(dis);
                Distance.Id = element.Id;
                Distance.Length = dis;

                listDis.Add(Distance);
            }
            return listDis;
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
        #endregion 

    }



}
