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
        /// <summary>
        /// Add station
        /// </summary>
        /// <param name="station"></param>
        /// <exception cref="NotImplementedException"></exception>
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


        /// <summary>
        /// Update station
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="charging_spots"></param>
        public void UpdateStetion(int id, string name, int charging_spots)
        {
            dalo.UpdateStetion(id, name, charging_spots);
        }
        /// <summary>
        /// Get station by ID
        /// </summary>
        /// <param name="requestedId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        Station GetStation1(int stationID)
        {
           return
        }
        /// <summary>
        /// Show LIST of stations
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Station> ShowStationList()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Show LIST of chargeable stations
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Station> ShowChargeableStationList()
        {
            throw new NotImplementedException();
        }

    }
}
