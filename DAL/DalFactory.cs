using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DalApi
{
   public class DalFactory
    {
        public static IDal GetDal(string typ = "List")
        {
            switch(typ)
            {
                case "List":
                    return Dal.DalObject.Instance;
                default:
                    throw DO.StationException.IDalNotFound("IDal only have List type",typ);
            }  
        }
    }
}
