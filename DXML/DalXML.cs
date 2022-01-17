using Dal;
using DO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DXML
{
    internal sealed partial class DalXml// : DalApi.IDal
    {
        XMLTools XMLTools;
        string dronePath = @"drons.xml";
        string customerPath = @"Customers.xml";
        string stationPath = @"Stations.xml";
        string parcelPath = @"Parcels.xml";
        string droneChargePath = @"droneCharges.xml";

        #region singelton
        static DalXml instance;
        private static object locker = new object();
        private XElement CustumerRoot;

        /// <summary>
        /// constructor - calls DataSource.initialize() to initialize lists
        /// </summary>
        private DalXml()
        {
            string dir = @"..\xml\";
            XMLTools = new XMLTools();
            if (!File.Exists(dir + customerPath))
                CreateFiles();
            else
                LoadData();
        }

        /// <summary>
        /// instance of DalObject class - same object is always returned
        /// </summary>
        public static DalXml Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                            instance = new DalXml();
                    }
                }
                return instance;
            }
        }

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
        public double[] PowerConsumptionRequest()
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
            d.Battery -= dis * 0.01;
            dronsList.Add(d);
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

        #endregion DalXML Stations
        //זה גמור!
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
        public void UpdateCustumer(Customer customer)
        {
            LoadData();

            XElement cus = (from cus1 in CustumerRoot.Elements()
                            where Convert.ToInt32(cus1.Element("ID").Value) == customer.Id
                            select cus1).FirstOrDefault();
            cus.Element("Name").Value = customer.Name;
            cus.Element("Phone").Value = customer.Phone;
            CustumerRoot.Save(customerPath);

        }
        #endregion DalXML Coustumer
        //גמור בה
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
        public void DischargeDrone(int drone_id, double longt, double latit)
        {
            throw new NotImplementedException();
        }
        #endregion DalXML Parcel

        //לעשות פונקציה למספר רץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץ
    

    }



}
