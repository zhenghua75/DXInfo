namespace Trirand.Web.Mvc
{
    using System;

    internal class DataTypeNotSetException : Exception
    {
        public DataTypeNotSetException(string message) : base(message)
        {
        }
    }
}

