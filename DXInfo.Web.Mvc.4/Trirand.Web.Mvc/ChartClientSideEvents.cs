using System;
using System.Collections;
using System.Web.Script.Serialization;
namespace Trirand.Web.Mvc
{
	public class ChartClientSideEvents
	{
		public string AddSeries
		{
			get;
			set;
		}
		public string Click
		{
			get;
			set;
		}
		public string Load
		{
			get;
			set;
		}
		public string Redraw
		{
			get;
			set;
		}
		public string Selection
		{
			get;
			set;
		}
		public ChartClientSideEvents()
		{
			this.AddSeries = "";
			this.Click = "";
			this.Load = "";
			this.Redraw = "";
			this.Selection = "";
		}
		internal Hashtable ToHashtable(JQChart chart)
		{
			Hashtable hashtable = new Hashtable();
			if (!string.IsNullOrEmpty(this.AddSeries))
			{
				hashtable.AddLiteral("addSeries", this.AddSeries, chart);
			}
			if (!string.IsNullOrEmpty(this.Click))
			{
				hashtable.AddLiteral("click", this.Click, chart);
			}
			if (!string.IsNullOrEmpty(this.Load))
			{
				hashtable.AddLiteral("load", this.Load, chart);
			}
			if (!string.IsNullOrEmpty(this.Redraw))
			{
				hashtable.AddLiteral("redraw", this.Redraw, chart);
			}
			if (!string.IsNullOrEmpty(this.Selection))
			{
				hashtable.AddLiteral("selection", this.Selection, chart);
			}
			return hashtable;
		}
		internal string ToJSON(JQChart chart)
		{
			return new JavaScriptSerializer().Serialize(this.ToHashtable(chart));
		}
	}
}
