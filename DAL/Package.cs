﻿using System;

namespace IDAL
{
    namespace DO
    {
        public struct Package
        {

            public int ID { get; set; }
            public int SendingCustomer { get; set; }
            public int GettingCustomer { get; set; }
            public Enums.Weight PWeight { get; set; }

            public Enums.Priority Priority { get; set; }
            public  int QuadocopterID { get; set; }

            public DateTime Create { get; set; }//Time to create a package for the sender
            public DateTime Affiliation { get; set; }//Time to assign the package to the glider
            public DateTime Collection { get; set; }//Time to pick up the package from the sender
            public DateTime Supply { get; set; }//Time of arrival of the package to the recipient
        }

    }
}
