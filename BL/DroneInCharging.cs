﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
   public class DroneInCharging
    {
        public int Id { get; set; }
        public double Battery { get; set; }
        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            String result = "";
            result += $"Drone ID is {Id}, \n";
            result += $"Battery precent is: {Battery}, \n";

            return result;
        }
    }
}
