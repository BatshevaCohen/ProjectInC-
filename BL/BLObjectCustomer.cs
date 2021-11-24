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
        /// Add customer
        /// </summary>
        /// <param name="customer"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddCustomer(Customer customer)
        {
            IDAL.DO.Customer c = new IDAL.DO.Customer();
            c.Id = customer.Id;
            c.Name = customer.Name;
            c.Phone = customer.Phone;
            dalo.AddCustomer(c);

            throw new NotImplementedException();
        }
        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        public void UpdateCustomer(int id, string name, string phone)
        {
            dalo.UpdateCustumer(id, name, phone);
        }
        //יש לאכלס את רשימת החבילות שהלקוח שולח ואת רשימת BL- שימו לב שב
        //החבילות שהלקוח מקבל
        public Customer GetCustomer(int IDc)
        {

        }
    }
}
