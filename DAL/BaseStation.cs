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

        }
    }
}