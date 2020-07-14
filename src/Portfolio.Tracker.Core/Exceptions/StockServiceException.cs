using System;

namespace Portfolio.Tracker.Core.Exceptions
{
    public class StockServiceException : Exception
    {
        public StockServiceException(string message) : base(message)
        {
            
        }
    }
}