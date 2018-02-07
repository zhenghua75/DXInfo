using System;
using System.Collections;
using System.Web.Script.Serialization;
namespace Trirand.Web.Mvc
{
	public class ChartPlotOptionsSettings
	{
		public ChartAreaSettings Area
		{
			get;
			set;
		}
		public ChartAreaSplineSettings AreaSpline
		{
			get;
			set;
		}
		public ChartBarSettings Bar
		{
			get;
			set;
		}
		public ChartColumnSettings Column
		{
			get;
			set;
		}
		public ChartLineSettings Line
		{
			get;
			set;
		}
		public ChartPieSettings Pie
		{
			get;
			set;
		}
		public ChartSeriesPlotSettings Series
		{
			get;
			set;
		}
		public ChartScatterSettings Scatter
		{
			get;
			set;
		}
		public ChartSplineSettings Spline
		{
			get;
			set;
		}
		public ChartPlotOptionsSettings()
		{
			this.Area = new ChartAreaSettings();
			this.AreaSpline = new ChartAreaSplineSettings();
			this.Bar = new ChartBarSettings();
			this.Column = new ChartColumnSettings();
			this.Line = new ChartLineSettings();
			this.Pie = new ChartPieSettings();
			this.Series = new ChartSeriesPlotSettings();
			this.Scatter = new ChartScatterSettings();
			this.Spline = new ChartSplineSettings();
		}
		internal string ToJSON(JQChart chart)
		{
			return new JavaScriptSerializer().Serialize(this.ToHashtable(chart));
		}
		internal Hashtable ToHashtable(JQChart chart)
		{
			return new Hashtable
			{

				{
					"area",
					this.Area.ToHashtable(chart)
				},

				{
					"areaspline",
					this.AreaSpline.ToHashtable(chart)
				},

				{
					"bar",
					this.Bar.ToHashtable(chart)
				},

				{
					"column",
					this.Column.ToHashtable(chart)
				},

				{
					"line",
					this.Line.ToHashtable(chart)
				},

				{
					"pie",
					this.Pie.ToHashtable(chart)
				},

				{
					"series",
					this.Series.ToHashtable()
				},

				{
					"scatter",
					this.Scatter.ToHashtable(chart)
				},

				{
					"spline",
					this.Spline.ToHashtable(chart)
				}
			};
		}
	}
}
