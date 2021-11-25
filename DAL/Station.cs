using System;
using System.Collections.Generic;

namespace IDAL
{
    namespace DO
    {
        public struct Station
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int ChargeSlots { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }

            /// <summary>
            /// ToString
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                String result = "";
                result += $"ID is {Id}, \n";
                result += $"Name is {Name}, \n";
                //---BONOS OPTION---
                result += $"Latitude is {Longitude}, \n Sexagesimal angle: {SexagesimalAngle.FromDouble(Latitude)}\n";
                result += $"Longitude is {Latitude}, \n Sexagesimal angle: {SexagesimalAngle.FromDouble(Longitude)}\n";
                result += $"Charge level is {ChargeSlots}, \n";

                return result;
            }

        
        }
    }

}