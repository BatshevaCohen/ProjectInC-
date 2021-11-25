using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
   public class ParcelInTransfer
    {
        public int Id { get; set; }
        public ParcelTransferStatus ParcelTransferStatus { get; set; }
        public Priority Priority { get; set; }
        public Weight Weight { get; set; }
        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Reciver { get; set; }
        public Location Collecting { get; set; }
        public Location SupplyTarget { get; set; }
        public Double TransportDistance { get; set; } //the dictance of the transportation
        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            String result = "";
            result += $"Parcel ID is: {Id}, \n";
            result += $"The Parcel transfer status is: {ParcelTransferStatus}, \n";
            result += $"The parcel's priority is: {Priority}, \n";
            result += $"The parcel's weight is: {Weight}, \n";
            result += $"The parcel's sender is: {Sender}, \n";
            result += $"The parcel's Reciver is: {Reciver}, \n";
            result += $"The location of parcel's collection is: {Collecting}, \n";
            result += $"The location to supply the parcel is: {SupplyTarget}, \n";
            result += $"The dictance of the transportation is: {TransportDistance}, \n";
            return result;
        }
    }
}
