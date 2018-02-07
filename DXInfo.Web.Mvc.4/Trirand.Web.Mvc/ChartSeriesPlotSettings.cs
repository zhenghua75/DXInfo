using System;
using System.Collections;
using System.Web.Script.Serialization;
namespace Trirand.Web.Mvc
{
	public class ChartSeriesPlotSettings
	{
		public ChartSeriesStacking Stacking
		{
			get;
			set;
		}
		public ChartSeriesPlotSettings()
		{
			this.Stacking = ChartSeriesStacking.None;
		}
		internal string ToJSON()
		{
			return new JavaScriptSerializer().Serialize(this.ToHashtable());
		}
		internal Hashtable ToHashtable()
		{
			Hashtable hashtable = new Hashtable();
			if (this.Stacking != ChartSeriesStacking.None)
			{
				hashtable.Add("stacking", this.Stacking.ToString().ToLower());
			}
			return hashtable;
		}
	}
}
