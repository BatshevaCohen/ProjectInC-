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
        /// add Customer to the Customers list
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public void AddCustomer(Customer c)
        {
            if (DataSource.customer.Exists(client => client.Id == c.Id))
            {
                throw new CustomerException($"ID {c.Id} already exists!!");
            }
            else
                DataSource.customer.Add(c);
        }
        /// <summary>
        /// update customee
        /// </summary>
        /// <param name="custumerId"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        public void UpdateCustumer(int custumerId, string name, string phone)
        {
            Customer customer = DataSource.customer.Find(x => x.Id == custumerId);
            customer.Id = custumerId;
            customer.Name = name;
            customer.Phone = phone;
        }
        /// <summary>
        /// view function for Customer by id
        /// </summary>
        /// <param name="id"></param>
        public Customer GetCustomer(int IDc)
        {
            if (!DataSource.customer.Exists(item => item.Id == IDc))
            {
                throw new CustomerException($"ID: {IDc} does not exist!!");
            }
            return DataSource.customer.First(c => c.Id == IDc);
        }
        /// <summary>
        /// view lists functions for Customer
        /// </summary>
        public IEnumerable<Customer> ShowCustomerList()
        {
            List<Customer> CustomerList = new List<Customer>();
            foreach (Customer element in DataSource.customer)
            {
                CustomerList.Add(element);
            }
            return CustomerList;
        }
        /// <summary>
        /// find customer by ID
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public Customer FindCustomer(int customerID)
        {
            return DataSource.customer.Find(x => x.Id == customerID);
        }
    }
}
