using System;
using IBL.BO;

namespace BL
{
    public class BLObject : IBl
    {
        IDAL.DO.IDal dal;
        public BLObject()
        {
            dal = new DalObject.DalObject();
        }
        public Customer GetCustomer(int id)
        {
            IDAL.DO.Customer someone;
            try
            {
                someone = dal.GetCustomer(id);
            }
            catch (IDAL.DO.ClientException cex)
            {
                // TO DO SOMETHING
                throw;
            }
            return new Customer
            {
                Id = someone.Id,
                Name = someone.Name,
                Phone = someone.Phone,
                Location = new Location { Latitude = someone.Latitude, Longitude = someone.Longitude },

            };
        }
    }
}
