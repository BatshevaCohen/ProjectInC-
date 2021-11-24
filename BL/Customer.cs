using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Customer
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }
        public Location Location { get; set; }
        public List<ParcelCustomer> SentParcels { get; set; }
        public List<ParcelCustomer> ReceiveParcels { get; set; }
        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            String result = "";
            result += $"Customer ID is: {Id}, \n";
            result += $"Customer name is {Name}, \n";
            result += $"Customer phone number is: {Phone}, \n";
            result += $"Customer Location is: {Location}, \n";
            result += $"List of parcels from the sender: {SentParcels}, \n";
            result += $"List of parcels to the reciver: {SentParcels}, \n";
            
            return result;
        }
    }
}
