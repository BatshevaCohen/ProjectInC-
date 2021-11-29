using System;
using System.Runtime.Serialization;

namespace IBL.BO
{
    [Serializable]
    internal class StationIdException : Exception
    {
        private int stationId;
        private string v;

        public StationIdException()
        {
        }

        public StationIdException(string message) : base(message)
        {
        }
        /// <summary>
        /// Exception- station ID sould be 5-6 digits
        /// </summary>
        /// <param name="stationId"></param>
        /// <param name="v"></param>
        public StationIdException(int stationId, string v)
        {
            this.stationId = stationId;
            this.v = v;
        }

        public StationIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StationIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}