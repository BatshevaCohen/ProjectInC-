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
            public double longitude { get; set; }
            public double latitude { get; set; }



            public override string ToString()
            {
                String result = "";
                result += $"ID is {ID}, \n";
                result += $"Name is {Name}, \n";
                //---BONOS OPTION---
                result += $"Latitude is {longitude}, \n";
                result += $"Longitude is {latitude}, \n";

                return result;
            }
        }
    }

}