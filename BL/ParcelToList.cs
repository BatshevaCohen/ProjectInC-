using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelToList
    {
        public int Id { get; set; }
        public String SenderName { get; set; } //the name of the customer who sent the parcel
        public String ReciverName { get; set; } //the name of the customer who recived the parcel
        public Weight Weight { get; set; }
        public Priority Priority { get; set; }
        public ParcelStatus ParcelStatus { get; set; }
    }
}
