using IDAL.DO;
using System;
using System.Runtime.Serialization;

namespace DalObject
{
    [Serializable]
    internal class CustomerException : Exception
    {
        private object id;
        private string errMsg;
        Severity severity;

        public CustomerException()
        {
        }

        public CustomerException(string message) : base(message)
        {
        }

        public CustomerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public CustomerException(int id, string errMsg, Severity severity): base(errMsg)
        {
            this.id = id;
            this.errMsg = errMsg;
            this.severity = severity;
        }

        protected CustomerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}