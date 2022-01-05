using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    /// <summary>
    /// Factory Design
    /// get an istance of an object from a class that implements the IDal interface
    /// </summary>
    public static class DalFactory
    {
        /// <summary>
        /// get object instance of class specified by the xml file
        /// </summary>
        /// <returns>instance of an object from class implementing Idal interface </returns>
        public static IDal GetDal(string option ="list")
        {
            switch (option)
            {
                case "list": 
                    return DAL.DalObject.Instance;
                case "xml":
                   // return DAL.DalXML.Instance;
                default:
                    break;
            }
            return null;
        }
    }
}