using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelCustomer
    {
        public int Id { get; set; }
        public Weight Weight { get; set; }
        public Priority Priority { get; set; }
        public ParcelStatus ParcelStatus { get; set; }
        public CustomerInParcel CustomerInParcel { get; set; }
        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            String result = "";
            result += $"Parcel ID is: {Id}, \n";
            result += $"The parcel's weight is: {Weight}, \n";
            result += $"The parcel's priority is: {Priority}, \n";
            result += $"The parcel's status is: {ParcelStatus}, \n";
            result += $"The details of the customer of this parcel: {CustomerInParcel}, \n";

            return result;
        }
    }
}
