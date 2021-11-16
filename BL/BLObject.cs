using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDAL.DO;

namespace BL
{
    public class BLObject : IBL
    {
        private IDal dal;
        private List<IBL.BO.Drone> drones;
        public BLObject()
        {
            drones = new List<IBL.BO.Drone>();
            dal = new DalObject.DalObject();
        }
        public void AddStation(string stationName, int positions)
        {
            throw new NotImplementedException();
        }
        public int AddDrone()
        {
            throw new NotImplementedException();
        }
        public void AssignmentParcelToDrone(int parcelId, int station)
        {
            throw new NotImplementedException();
        }
        public void PickedupParcel(int parcelid)
        {
            throw new NotImplementedException();
        }
        public void SendDroneToRecharge(int droneId, int station)
        {
            throw new NotImplementedException();
        }
        public void RelizeDroneFromeRecharg(int droneId)
        {
            throw new NotImplementedException();

        }
        BaseStation IBL.GetStation(int requestedId)
        {
            throw new NotImplementedException();
        }

    }
}
