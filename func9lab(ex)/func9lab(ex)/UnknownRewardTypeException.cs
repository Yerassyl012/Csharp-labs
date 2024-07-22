using System;

namespace func9lab_ex_.Chapter10.Data
{
    internal class UnknownRewardTypeException : Exception
    {
        public UnknownRewardTypeException()
        {
        }

        public UnknownRewardTypeException(string message) : base(message)
        {
        }

        public UnknownRewardTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}