using System;
namespace Trirand.Web.Mvc
{
	internal static class ObjectExtensions
	{
		public static object ToJson(this object value, JQChart chart)
		{
			if (value != null && value is DateTime)
			{
				string text = ((DateTime)value).ToJsonUTC();
				chart.AddJsonReplacement(string.Format("\"{0}\"", text), text);
				return text;
			}
			return value;
		}
	}
}
