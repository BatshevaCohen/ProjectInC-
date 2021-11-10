using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
   public class BaseStation
    {
        public int Id { get; set; }
        public string name { get; set; }
        public Location location { get; set; }
        public int NumberOfClaimPositions { get; set; }//Number of claim positions in free

        List<DroneInCharging> droneInChargings= new List<DroneInCharging>();
    }
}
