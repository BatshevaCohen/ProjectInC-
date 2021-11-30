using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public partial class BLObject
    {


        public void AddDroneToList(Drone drone,IDAL.DO.Station station)
        {

            DroneToList droneToList = new DroneToList()
            {
                Id = drone.Id,
                Model = drone.Model,
                Battery = drone.Battery,
                DroneStatuses = drone.DroneStatuses,
                Weight = drone.Weight,
            };
            droneToList.Location = new Location()
            {
                Latitude = station.Latitude,
                Longitude=station.Longitude,
            };
            if (drone.ParcelInTransfer.Id != 0)
            {
                droneToList.ParcelNumberTransferred = drone.ParcelInTransfer.Id;
            }
            dronesL.Add(droneToList);
        }
    }  
}
    

