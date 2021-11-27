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
        public List<DroneToList> drones;
        public List<Drone> drone1;
        static Random r = new Random();
        public BLObject()
        {
            //Access to the layer DAL
            dalo = new DalObject.DalObject();
            drones = new List<DroneToList>();

        }


        public void UpdateDeliveryToCustomer(int parcel_id3, int customer_id)
        {
            throw new NotImplementedException();
        }


    }
}
