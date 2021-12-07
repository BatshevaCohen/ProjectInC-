﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject.DO;
using IDAL;
using IDAL.DO;


namespace DalObject
{
    public partial class DalObject 
    {
        /// <summary>
        /// add Customer to the Customers list
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public void AddCustomer(Customer c)
        {
            if (DataSource.Customer.Exists(client => client.Id == c.Id))
            {
                throw new CustomerException($"ID {c.Id} already exists!!");
            }
            else
                DataSource.Customer.Add(c);
        }
        /// <summary>
        /// update customee name and phone---
        /// </summary>
        /// <param name="custumerId"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        public void UpdateCustumer(int custumerId, string name, string phone)
        {
            if (!DataSource.Customer.Exists(x => x.Id == custumerId))
            {
                throw new Exception($"custumer {custumerId} is not exite!!");
            }
            Customer customer = DataSource.Customer.Find(x => x.Id == custumerId);
            DataSource.Customer.Remove(customer);
            customer.Name = name;
            customer.Phone = phone;
            DataSource.Customer.Add(customer);
        }
        /// <summary>
        /// Get Customer by id
        /// </summary>
        /// <param name="id"></param>
        public Customer GetCustomer(int IDc)
        {
            //if ID does not exist for customer
            if (!DataSource.Customer.Exists(item => item.Id == IDc))
            {
                throw new CustomerException($"ID: {IDc} does not exist!!");
            }
            return DataSource.Customer.First(c => c.Id == IDc);
        }

        /// <summary>
        /// Show list of Customers
        /// </summary>
        public IEnumerable<Customer> ShowCustomerList(Func<Customer,bool>predicat = null)
        {
            if (predicat == null)
            {
                List<Customer> CustomerList = new();
                foreach (Customer element in DataSource.Customer)
                {
                    CustomerList.Add(element);
                }
                return CustomerList;
            }
            else
                return DataSource.Customer.Where(predicate).ToList();
        }

    }
}
