using System;
namespace Trirand.Web.Mvc
{
	internal class DataTypeNotSetException : Exception
	{
		public DataTypeNotSetException(string message) : base(message)
		{
		}
	}
}
