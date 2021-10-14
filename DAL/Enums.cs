﻿using System;
namespace IDAL
{
    namespace DO
    {
        public struct Enums
        {
            public enum Weight
            {
                Light,
                Medium,
                Heavy
            }

            public enum QuadocopterState
            {
                Available,
                Maintenance,
                Shipping
            }

            public enum Priority
            {
                Regular,
                Fast,
                Emergency
            }
        }
    }
}