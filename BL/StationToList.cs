using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class StationToList
    {
        public int Id { get; set; }
        public String StationName { get; set; }
        public int AvailableChargingSpots { get; set; } // the number of available charging spots
        public int UnavailableChargingSpots { get; set; } // the number of unavailable charging spots
    }
}
