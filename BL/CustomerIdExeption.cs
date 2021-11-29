using System;
using System.Runtime.Serialization;

namespace IBL.BO
{
    [Serializable]
    internal class CustomerIdExeption : Exception
    {
        private int id;
        private string v;

        public CustomerIdExeption()
        {
        }

        public CustomerIdExeption(string message) : base(message)
        {
        }
        /// <summary>
        /// Exception- Customer ID must be 9 digits
        /// </summary>
        /// <param name="id"></param>
        /// <param name="v"></param>
        public CustomerIdExeption(int id, string v)
        {
            this.id = id;
            this.v = v;
        }

        public CustomerIdExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomerIdExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}