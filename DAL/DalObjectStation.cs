using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject.DO;
using IDAL;
using IDAL.DO;

namespace DalObject
{
    public partial class DalObject : IDal
    {
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
        /// <summary>
        /// view function for Station
        /// </summary>
        /// <param name="id"></param>
        public Station GetStation(int id)
        {
            if (!DataSource.Stations.Exists(item => item.Id == id))
            {
                throw new StationException($"ID: {id} does not exist!!");
            }
            return DataSource.Stations.First(c => c.Id == id);
        }
        /// <summary>
        /// view lists functions for BaseStation
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> ShowStationList()
        {
            List<Station> baseStationList = new List<Station>();
            foreach (Station element in DataSource.Stations)
            {
                baseStationList.Add(element);
            }
            return baseStationList;
        }
        /// <summary>
        ///  shows base stations with available charging spots
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> ShowChargeableBaseStationList()
        {
            List<Station> ChargeableBaseStationList = new List<Station>();
            foreach (Station element in DataSource.Stations)
            {
                if (element.ChargeSlots > 0)
                    ChargeableBaseStationList.Add(element);
            }
            return ChargeableBaseStationList;
        }
        /// <summary>
        /// updates the number of available charging spots
        /// </summary>
        /// <param name="stationId"></param>
        public void UpdateChargeSpots(int stationId)
        {
            Station station = DataSource.Stations.Find(x => x.Id == stationId);
            station.ChargeSlots--;
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
    }
}
