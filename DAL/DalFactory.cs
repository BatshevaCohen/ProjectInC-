using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
   public class DalFactory
    {
        public static IDal GetDal(string typ)
        {
            switch(typ)
            {
                case "List":
                    return DalObject.DalObject.Instance;
                default:
                    throw DO.StationException.IDalNotFound("IDal only have List type",typ);
            }
           
        }


    }
}
