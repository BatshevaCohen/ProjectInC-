using System;
using System.Runtime.Serialization;

namespace IBL.BO
{
    [Serializable]
    internal class DronelIdException : Exception
    {
        private int id;
        private string v;

        public DronelIdException()
        {
        }

        public DronelIdException(string message) : base(message)
        {
        }
        /// <summary>
        /// ID is too long or too short (4-9 digits)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="v"></param>
        public DronelIdException(int id, string v)
        {
            this.id = id;
            this.v = v;
        }

        public DronelIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DronelIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}