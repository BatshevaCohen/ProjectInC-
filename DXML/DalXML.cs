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
        string stationPath = @"Stations.xml";
        string parcelPath = @"Parcels.xml";
        string customerPath = @"Customers.xml";

        //זה גמור!
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
            if (!File.Exists(dir + dronePath))
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


        #endregion


        #region DalXML Stations

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
    }

    #endregion DalXML Coustumer

    #region DalXML Parcel

    #endregion DalXML Parcel

    //לעשות פונקציה למספר רץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץץ

}
