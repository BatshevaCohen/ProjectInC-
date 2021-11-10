using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class CustomerToList
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }
        public int SentAndProvidedParcels { get; set; } //the number of parcels thet the customer sent and been provided
        public int SentButNOTProvidedParcels { get; set; } //the number of parcels thet the customer sent but have not provided
        public int RecivedParcels { get; set; } // the number of parcels the customer recived
        public int ParcelsOnTheWay { get; set; } // the number of the parcels that on the way to the customer
    }
}
