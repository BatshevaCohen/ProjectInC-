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
    public partial class BLObject
    {
        /// <summary>
        /// Add customer
        /// </summary>
        /// <param name="customer"></param>
        /// <
        /// cref="NotImplementedException"></exception>
        public void AddCustomer(Customer customer)
        {
            // Customer ID must be 9 digits
            if (customer.Id < 100000000 || customer.Id >= 1000000000)
            {
                throw new CustomerIdExeption(customer.Id, "Customer ID must be 9 digits");
            }
            // Phone number is 10 digits (or 9 digits- for a telephone at home) + one "-"
            if (customer.Phone.Length != 10 && customer.Phone.Length != 11)
            {
                throw new PhoneException(customer.Phone, "Phone number is ilegal!");
            }
            // Phone number should start with a 0
            if (customer.Phone[0] != '0')
            {
                throw new PhoneException(customer.Phone, "Phone number should start with a 0");
            }
            IDAL.DO.Customer c = new()
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Longitude = customer.Location.Longitude,
                Latitude = customer.Location.Latitude
            };
           
            dalo.AddCustomer(c);
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
            Customer customer = new();
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
                ParcelCustomer parcelCustomer = new ();
                parcelCustomer.Id = item.Id;
                parcelCustomer.Priority = (Priority)item.Priority;
                parcelCustomer.Weight = (Weight)item.Weight;
                if(item.Create==DateTime.MinValue)
                {
                    parcelCustomer.ParcelStatus = ParcelStatus.Created;
                }
                if (item.Supplied == DateTime.MinValue)
                {
                    parcelCustomer.ParcelStatus = ParcelStatus.Delivered;
                }
                if (item.PickedUp == DateTime.MinValue)
                {
                    parcelCustomer.ParcelStatus = ParcelStatus.Picked;
                }
                if (item.Assigned == DateTime.MinValue)
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
                ParcelCustomer parcelCustomer = new()
                {
                    Id = item.Id,
                    Priority = (Priority)item.Priority,
                    Weight = (Weight)item.Weight
                };
                if (item.Create == DateTime.MinValue)
                {
                    parcelCustomer.ParcelStatus = ParcelStatus.Created;
                }
                if (item.Supplied == DateTime.MinValue)
                {
                    parcelCustomer.ParcelStatus = ParcelStatus.Delivered;
                }
                if (item.PickedUp == DateTime.MinValue)
                {
                    parcelCustomer.ParcelStatus = ParcelStatus.Picked;
                }
                if (item.Assigned == DateTime.MinValue)
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
        public IEnumerable<Customer> ShowCustomerList()
        {
            List<Customer> customersList = new();
            var custumers = dalo.ShowCustomerList();
            foreach (var item in custumers)
            {
                Customer customer = new()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Phone = item.Phone
                };
                customer.Location = new()
                {
                    Latitude = item.Latitude,
                    Longitude = item.Longitude,
            };
               

                //Packages that the sending customer has

                List<IDAL.DO.Parcel> parcelSendin = dalo.GetListOfParcelSending(customer.Id);
                List<IDAL.DO.Parcel> parcelReciever = dalo.GetListOfParcelRecirver(customer.Id);

                foreach (IDAL.DO.Parcel item1 in parcelSendin)
                {
                    ParcelCustomer parcelCustomer = new()
                    {
                        Id = item.Id,
                        Priority = (Priority)item1.Priority,
                        Weight = (Weight)item1.Weight
                    };
                    if (item1.Create == DateTime.MinValue)
                    {
                        parcelCustomer.ParcelStatus = ParcelStatus.Created;
                    }
                    if (item1.Supplied == DateTime.MinValue)
                    {
                        parcelCustomer.ParcelStatus = ParcelStatus.Delivered;
                    }
                    if (item1.PickedUp == DateTime.MinValue)
                    {
                        parcelCustomer.ParcelStatus = ParcelStatus.Picked;
                    }
                    if (item1.Supplied == DateTime.MinValue)
                    {
                        parcelCustomer.ParcelStatus = ParcelStatus.Assigned;
                    }
                    parcelCustomer.CustomerInParcel = new()
                    {
                         Id = customer.Id,
                         Name=customer.Name,
                    };


                    //add Details of the sending customer
                    customer.SentParcels = new();
                    customer.SentParcels.Add(parcelCustomer);
                }
                foreach (IDAL.DO.Parcel item2 in parcelReciever)
                {
                    ParcelCustomer parcelCustomer = new()
                    {
                        Id = item.Id,
                        Priority = (Priority)item2.Priority,
                        Weight = (Weight)item2.Weight
                    };
                    if (item2.Create == DateTime.MinValue)
                    {
                        parcelCustomer.ParcelStatus = ParcelStatus.Created;
                    }
                    if (item2.Supplied == DateTime.MinValue)
                    {
                        parcelCustomer.ParcelStatus = ParcelStatus.Delivered;
                    }
                    if (item2.PickedUp == DateTime.MinValue)
                    {
                        parcelCustomer.ParcelStatus = ParcelStatus.Picked;
                    }
                    if (item2.Assigned == DateTime.MinValue)
                    {
                        parcelCustomer.ParcelStatus = ParcelStatus.Assigned;
                    }

                    parcelCustomer.CustomerInParcel = new()
                    {
                        Id = customer.Id,
                        Name=customer.Name,
                    };


                    //add Details of the rciever customer
                    customer.ReceiveParcels = new();

                    customer.ReceiveParcels.Add(parcelCustomer);
                    customersList.Add(customer);
                }
               
            }
            return customersList;
        }
    }
}
/*
 
 
 
 */