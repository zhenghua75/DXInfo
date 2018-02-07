using System;
using System.Collections;
using System.Web.Script.Serialization;
namespace Trirand.Web.Mvc
{
	public class ChartMarkerSettings
	{
		public bool Enabled
		{
			get;
			set;
		}
		public string FillCollor
		{
			get;
			set;
		}
		public string LineColor
		{
			get;
			set;
		}
		public int LineWidth
		{
			get;
			set;
		}
		public int Radius
		{
			get;
			set;
		}
		public string Symbol
		{
			get;
			set;
		}
		public ChartMarkerSettings()
		{
			this.Enabled = true;
			this.FillCollor = "";
			this.LineColor = "#FFFFFF";
			this.LineWidth = 0;
			this.Radius = 0;
			this.Symbol = "";
		}
		internal string ToJSON()
		{
			return new JavaScriptSerializer().Serialize(this.ToHashtable());
		}
		internal Hashtable ToHashtable()
		{
			Hashtable hashtable = new Hashtable();
			if (!this.Enabled)
			{
				hashtable.Add("enabled", false);
			}
			if (!string.IsNullOrEmpty(this.FillCollor))
			{
				hashtable.Add("fillColor", this.FillCollor);
			}
			if (this.LineColor != "#FFFFFF")
			{
				hashtable.Add("lineColor", this.LineColor);
			}
			if (this.LineWidth != 0)
			{
				hashtable.Add("lineWidth", this.LineWidth);
			}
			if (this.Radius != 0)
			{
				hashtable.Add("radius", this.Radius);
			}
			if (!string.IsNullOrEmpty(this.Symbol))
			{
				hashtable.Add("symbol", this.Symbol);
			}
			return hashtable;
		}
		internal bool IsSet()
		{
			return this.ToHashtable().Count > 0;
		}
	}
}
