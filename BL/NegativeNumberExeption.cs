using System;
using System.Runtime.Serialization;

namespace IBL.BO
{
    [Serializable]
    internal class NegativeNumberExeption : Exception
    {
        private int id;
        private string v;

        public NegativeNumberExeption()
        {
        }

        public NegativeNumberExeption(string message) : base(message)
        {
        }

        public NegativeNumberExeption(int id, string v)
        {
            this.id = id;
            this.v = v;
        }

        public NegativeNumberExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NegativeNumberExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}