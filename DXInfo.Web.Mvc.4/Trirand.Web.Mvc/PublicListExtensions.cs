using System;
using System.Collections;
using System.Collections.Generic;
namespace Trirand.Web.Mvc
{
	public static class PublicListExtensions
	{
		public static List<ChartPoint> FromCollection(this List<ChartPoint> points, ICollection collection)
		{
			List<ChartPoint> list = new List<ChartPoint>();
			foreach (object current in collection)
			{
				list.Add(new ChartPoint(current));
			}
			return list;
		}
	}
}
