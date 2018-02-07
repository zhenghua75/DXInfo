using System;
using System.Collections;
namespace Trirand.Web.Mvc
{
	internal static class HashbableExtensions
	{
		public static void AddLiteral(this Hashtable h, string key, string value, JQChart chart)
		{
			h.Add(key, value);
			chart.AddJsonReplacement(string.Format("\"{0}\"", value), value);
		}
	}
}
