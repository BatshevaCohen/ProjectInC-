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
            IDAL.DO.Customer c = dalo.GetCustomer(IDc);
            Customer customer = new Customer();
            customer.Id = c.Id;
            customer.Name = c.Name;
            customer.Phone = c.Phone;
            customer.Location.Latitude = c.Latitude;
            customer.Location.Longitude = c.Longitude;

            //Packages that the sending customer has

            List<IDAL.DO.Parcel> parcelSendin = dalo.GetListOfParcelSending(customer.Id);
            List<IDAL.DO.Parcel> parcelReciever = dalo.GetListOfParcelRecirver(customer.Id);
           
            foreach (IDAL.DO.Parcel item in parcelSendin)
            {
                ParcelCustomer parcelCustomer = new ParcelCustomer();
                parcelCustomer.Id = item.Id;
                parcelCustomer.Priority = (Priority)item.Priority;
                parcelCustomer.Weight = (Weight)item.Weight;
                if(item.create==DateTime.MinValue)
                {
                    parcelCustomer.ParcelStatus = ParcelStatus.Created;
                }
                if (item.Delivered == DateTime.MinValue)
                {
                    parcelCustomer.ParcelStatus = ParcelStatus.Delivered;
                }
                if (item.PickedUp == DateTime.MinValue)
                {
                    parcelCustomer.ParcelStatus = ParcelStatus.Picked;
                }
                if (item.Scheduled == DateTime.MinValue)
                {
                    parcelCustomer.ParcelStatus = ParcelStatus.Assigned;
                }
                parcelCustomer.CustomerInParcel.Id = customer.Id;
                parcelCustomer.CustomerInParcel.Name = customer.Name;
                //add Details of the sending customer
                customer.SentParcels.Add(parcelCustomer);
            }
            foreach (IDAL.DO.Parcel item in parcelReciever)
            {
                ParcelCustomer parcelCustomer = new ParcelCustomer();
                parcelCustomer.Id = item.Id;
                parcelCustomer.Priority = (Priority)item.Priority;
                parcelCustomer.Weight = (Weight)item.Weight;
                if (item.create == DateTime.MinValue)
                {
                    parcelCustomer.ParcelStatus = ParcelStatus.Created;
                }
                if (item.Delivered == DateTime.MinValue)
                {
                    parcelCustomer.ParcelStatus = ParcelStatus.Delivered;
                }
                if (item.PickedUp == DateTime.MinValue)
                {
                    parcelCustomer.ParcelStatus = ParcelStatus.Picked;
                }
                if (item.Scheduled == DateTime.MinValue)
                {
                    parcelCustomer.ParcelStatus = ParcelStatus.Assigned;
                }
                parcelCustomer.CustomerInParcel.Id = customer.Id;
                parcelCustomer.CustomerInParcel.Name = customer.Name;
                //add Details of the rciever customer
                customer.ReceiveParcels.Add(parcelCustomer);
            }
            return customer;
        }
        /// <summary>
        /// Show LIST of customers
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Customer> ShowCustomerList()
        {
            throw new NotImplementedException();
        }
    }
}
//זה בשביל החבילה 
