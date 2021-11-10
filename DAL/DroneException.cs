using System;
using System.Runtime.Serialization;

namespace DalObject
{
    [Serializable]
    internal class DroneException : Exception
    {
        private int id;
        private string errMsg;

        public DroneException()
        {
        }

        public DroneException(string message) : base(message)
        {
        }

        public DroneException(int id, string errMsg): this(string.Format("{0} {1]", id, errMsg))
        {
            this.id = id;
            this.errMsg = errMsg;
        }

        public DroneException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DroneException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}