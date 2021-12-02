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
        static Random r = new() { };

        public BLObject()
        {
            //Access to the layer DAL
            dalo = new DalObject.DalObject();
            dronesL = new List<DroneToList>();
            var Drones = dalo.ShowDroneList();
            DronesInitialize(Drones);

        }
        /// <summary>
        /// Constractor for drones initializing
        /// </summary>
        /// <param name="drones"></param>
        public void DronesInitialize(IEnumerable<IDAL.DO.Drone> drones)
        {
            //find A package that has not yet been delivered but the drone has already been associated
            List<IDAL.DO.Parcel> parcels = dalo.ShowParcelList().ToList();
            DroneToList droneBL;

            foreach (var droneDL in drones)
            {
                droneBL = new DroneToList()
                {
                    Id = droneDL.Id,
                    Model = droneDL.Model,
                    Weight = (Weight)droneDL.MaxWeight
                };
                List<IDAL.DO.Parcel> parcelList = parcels.FindAll(p => p.DroneID == droneBL.Id);

                if (parcels != null) //If there is a package that has not yet been delivered but the drone has already been associated
                {
                    droneBL.DroneStatuses = DroneStatuses.Shipping;
                    //If the package was associated but not collected
                    foreach (var p in parcels.Where(p => p.PickedUp == DateTime.MinValue))
                    {
                        // The location of the drone will be at the station closest to the sender
                        int senderId = p.SenderId;
                        double senderLattitude = dalo.GetCustomer(senderId).Latitude;
                        double senderLongitude = dalo.GetCustomer(senderId).Longitude;
                        //מרחק בין 2 תחנות הנמוך ביותר
                        IDAL.DO.Station st = dalo.GetClosestStation(senderLattitude, senderLongitude);
                        droneBL.Location = new Location
                        {
                            Latitude = st.Latitude,
                            Longitude = st.Longitude
                        };
                        droneBL.Battery = r.Next(0, 101);
                    }
                    //If the package has been collected but has not yet been delivered
                    foreach (var p in parcels.Where(p => p.PickedUp != DateTime.MinValue && p.Supplied == DateTime.MinValue))
                    {
                        //The location of the drone will be at the location of the sender
                        int senderId = p.SenderId;
                        double senderLattitude = dalo.GetCustomer(senderId).Latitude;
                        double senderLongitude = dalo.GetCustomer(senderId).Longitude;
                        IDAL.DO.Station st = dalo.GetClosestStation(senderLattitude, senderLongitude);
                        droneBL.Location = new Location
                        {
                            Latitude = st.Latitude,
                            Longitude = st.Longitude
                        };

                        double distance = dalo.GetDistanceBetweenLocationsOfParcels(p.SenderId, p.ReceiverId)
                            + dalo.GetDistanceBetweenLocationAndClosestBaseStation(p.ReceiverId);
                        switch (p.Weight)
                        {
                            case IDAL.DO.WeightCategories.Light:
                                droneBL.Battery = r.Next((int)(distance * dalo.PowerConsumptionRequest()[1] + 1), 101);
                                break;
                            case IDAL.DO.WeightCategories.Medium:
                                droneBL.Battery = r.Next((int)(distance * dalo.PowerConsumptionRequest()[2] + 1), 101);
                                break;
                            case IDAL.DO.WeightCategories.Heavy:
                                droneBL.Battery = r.Next((int)(distance * dalo.PowerConsumptionRequest()[3] + 1), 101);
                                break;
                            default:
                                break;
                        }
                    }
                }
                else //the drone is not in delivery
                {
                    droneBL.DroneStatuses = (DroneStatuses)r.Next(2); //Maintenance or Available
                    if (droneBL.DroneStatuses == DroneStatuses.Maintenance)
                    {
                        //Its location will be drawn between the purchasing stations
                        List<IDAL.DO.Station> stations = dalo.ShowStationList().ToList();
                        int index = r.Next(0, stations.Count());
                        droneBL.Location = new()
                        {
                            Latitude = stations[index].Latitude,
                            Longitude = stations[index].Longitude
                        };

                        droneBL.Battery = r.Next(0, 21);
                    }
                    else if (droneBL.DroneStatuses == DroneStatuses.Available)
                    {
                        //Its location will be raffled off among customers who have packages provided to them
                        List<IDAL.DO.Parcel> parcelsDelivered = parcels.FindAll(p => p.Supplied != DateTime.MinValue);
                        int index = r.Next(0, parcelsDelivered.Count());
                        droneBL.Location = new()
                        {
                            Latitude = dalo.GetCustomer(parcelsDelivered[index].ReceiverId).Latitude,
                            Longitude = dalo.GetCustomer(parcelsDelivered[index].ReceiverId).Longitude
                        };
                        // Battery mode will be recharged between a minimal charge that will allow it to reach the station closest to charging and a full charge
                        double distance = dalo.GetDistanceBetweenLocationAndClosestBaseStation(parcelsDelivered[index].ReceiverId);
                        droneBL.Battery = r.Next((int)(distance * dalo.PowerConsumptionRequest()[0] + 1), 101);

                        droneBL.ParcelNumberTransferred= 0;
                    }
                }

                dronesL.Add(droneBL);
               
            }
            
        }

       
    }

}





 
 
 