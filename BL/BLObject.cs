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
    public partial class BLObject : IBL
    {
        public IDAL.IDal dalo;
        public List<DroneToList> dronesL;
        public List<Drone> drone1;
        static Random r = new() { };

        public BLObject()
        {
            //Access to the layer DAL
            dalo = new DalObject.DalObject();
            dronesL = new List<DroneToList>();

        }

        /// <summary>
        /// Initialize constractor for drone
        /// </summary>
        /// <param name="drones"></param>
        private void DronesInitialize(List<IDAL.DO.Drone> drones)
        {
            List<IDAL.DO.Parcel> parcels = dalo.ShowParcelList().ToList();
            DroneToList droneBl;
            foreach (var droneDal in drones)
            {
                droneBl = new DroneToList()
                {
                    Id = droneDal.Id,
                    Model = droneDal.Model,
                    Weight = (Weight)droneDal.MaxWeight
                };

                List<IDAL.DO.Parcel> parcelList = parcels.FindAll(p => p.DroneID == droneBl.Id);

                // parcel have been assigned (to drone) bat have not supplied
                if (parcelList.Exists(p => p.Supplied == DateTime.MinValue))
                {
                    // the drone is currently on shipping status
                    droneBl.DroneStatuses = DroneStatuses.Shipping;
                    // for each parcel that have been assigned but have not picked up
                    foreach (var item in parcelList.Where(item => item.PickedUp == DateTime.MinValue))
                    {
                        int senderId = item.SenderId;
                        double senderLatitude = dalo.GetCustomer(senderId).Latitude;
                        double senderLongitude = dalo.GetCustomer(senderId).Longitude;
                        // the location will be in the closest station (to the sender)
                        IDAL.DO.Station st = dalo.GetClosestStation(senderLatitude, senderLongitude);
                        droneBl.Location = new Location { Latitude = st.Latitude, Longitude = st.Longitude };
                    }
                    //picked up (by the drone) but wasn't supplied
                    foreach (var item in parcelList.Where(item => item.PickedUp != DateTime.MinValue && item.Supplied == DateTime.MinValue))
                    {
                        int senderId = item.SenderId;
                        double senderLatitude = dalo.GetCustomer(senderId).Latitude;
                        double senderLongitude = dalo.GetCustomer(senderId).Longitude;
                        // the drone's location is the sender's location
                        droneBl.Location = new Location
                        {
                            Latitude = senderLatitude,
                            Longitude = senderLongitude
                        };
                    }
                    //////////////////////
                    //TO DO:
                    //יש לעדכן מצב סוללה:
                    //מצב סוללה יוגרל בין טעינה מינימלית שתאפשר לרחפן לבצע את המשלוח ולהגיע לטעינה לתחנה הקרובה ליעד המשלוח לבין טעינה מלאה
                    /////////////////////
                    
                    //minimum battery the drone needs for the delivery
                    int minBattery; 
                    droneBl.Battery = r.Next(minBattery, 100);
                }
                //else- the drone is not in delivery status
                else
                {
                    droneBl.DroneStatuses = (DroneStatuses)r.Next(2); //Maintenance or Available
                    // if the drone is on Maintenance status
                    if (droneBl.DroneStatuses == DroneStatuses.Maintenance)
                    {
                        List<Station> stations = (List<Station>)ShowStationList();
                        int index = r.Next(stations.Count());
                        //random station from all the stations
                        Station station = stations[index];
                        droneBl.Location = new Location
                        {
                            Latitude = station.Location.Latitude,
                            Longitude = station.Location.Longitude
                        };

                        // battery is random between 0% to 20%
                        droneBl.Battery = r.Next(0, 21);
                    }
                    
                    // else- if the drone is available
                    else if (droneBl.DroneStatuses == DroneStatuses.Available)
                    {
                        IEnumerable<IDAL.DO.Customer> customer = dalo.ShowCustomerList();
                        customer.ToList();
                        //צריך לבדוק איזה לקוחות כבר סופקו להם חבילות

                        /////////////////////////
                        //TO DO:
                        //מיקומו יוגרל בין לקוחות שיש חבילות שסופקו להם
                        // מצב סוללה יוגרל בין טעינה מינימלית שתאפשר לו להגיע לתחנה הקרובה לטעינה לבין טעינה מלאה
                        ///////////////////////

                        //minimum battery the drone needs for the delivery
                        int minBattery; 
                        droneBl.Battery = r.Next(minBattery, 100);
                    }
                }
                dronesL.Add(droneBl);
            }
        }
    }
}