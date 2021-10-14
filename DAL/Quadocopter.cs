using System;
namespace IDAL
{
    namespace DO
    {
        public struct Quadocopster
        {
            public int ID { get; set; }
            public int Model { get; set; }
            public Enums.Weight QWeight { get; set; }
            public int Battery { get; set; } //charging level
            public Enums.QuadocopterState State { get; set; }
        }
    }
}