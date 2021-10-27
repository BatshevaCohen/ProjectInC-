using System;
namespace IDAL
{
    namespace DO
    {
        public struct BaseStation
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int NumColumns { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }


            public override string ToString()
            {
                String result = "";
                result += $"ID is {ID}, \n";
                result += $"Name is {Name}, \n";
                //---BONOS OPTION---
                result += $"Latitude is {Longitude}, \n";
                result += $"Longitude is {Latitude}, \n";

                return result;
            }
        }
    }

}