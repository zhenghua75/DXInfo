using System;
using System.Collections.Generic;
namespace Trirand.Web.Mvc
{
	internal static class ListExtensions
	{
		public static string ToJSON(this List<ChartXAxisSettings> settings, JQChart chart)
		{
			string text = "";
			int num = 0;
			foreach (ChartXAxisSettings current in settings)
			{
				if (num > 0)
				{
					text += ",";
				}
				text += current.ToJSON(chart);
				num++;
			}
			if (num > 1)
			{
				text = "[" + text + "]";
			}
			return text;
		}
		public static string ToJSON(this List<ChartYAxisSettings> settings, JQChart chart)
		{
			string text = "";
			int num = 0;
			foreach (ChartYAxisSettings current in settings)
			{
				if (num > 0)
				{
					text += ",";
				}
				text += current.ToJSON(chart);
				num++;
			}
			if (num > 1)
			{
				text = "[" + text + "]";
			}
			return text;
		}
	}
}
