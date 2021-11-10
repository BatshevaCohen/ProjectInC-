using System;
using System.Runtime.Serialization;

namespace DalObject
{
    [Serializable]
    internal class StationException : Exception
    {
        private int id;
        private string errMsg;

        public StationException()
        {
        }

        public StationException(string message) : base(message)
        {
        }

        public StationException(int id, string errMsg): this(string.Format("{0} {1]", id, errMsg))
        {
            this.id = id;
            this.errMsg = errMsg;
        }

        public StationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}