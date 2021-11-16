using IDAL.DO;
using System;
using System.Runtime.Serialization;

namespace DalObject
{
    [Serializable]
    internal class ParcelException : Exception
    {
        private int id;
        private string errMsg;
        private Severity severity;

        //public ParcelException()
        //{
        //}

        public ParcelException(string message) : base(message)
        {
        }

        public ParcelException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ParcelException(int id, string errMsg, Severity severity) : base(errMsg)
        {
            this.id = id;
            this.errMsg = errMsg;
            this.severity = severity;
        }

        protected ParcelException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}