using System;
using System.Collections;
using System.Web.Script.Serialization;
namespace Trirand.Web.Mvc
{
	public class ChartPlotLineSettings
	{
		public string Color
		{
			get;
			set;
		}
		public ChartLineDashStyle DashStyle
		{
			get;
			set;
		}
		public double? Number
		{
			get;
			set;
		}
		public int Width
		{
			get;
			set;
		}
		public int? ZIndex
		{
			get;
			set;
		}
		public ChartPlotLineSettings()
		{
			this.Color = "";
			this.DashStyle = ChartLineDashStyle.Solid;
			this.Number = null;
			this.Width = 1;
			this.ZIndex = null;
		}
		internal string ToJSON()
		{
			return new JavaScriptSerializer().Serialize(this.ToHashtable());
		}
		internal Hashtable ToHashtable()
		{
			Hashtable hashtable = new Hashtable();
			if (!string.IsNullOrEmpty(this.Color))
			{
				hashtable.Add("color", this.Color);
			}
			if (this.DashStyle != ChartLineDashStyle.Solid)
			{
				hashtable.Add("dashStyle", this.DashStyle.ToString().ToLower());
			}
			if (this.Number.HasValue)
			{
				hashtable.Add("number", this.Number);
			}
			if (this.Width != 1)
			{
				hashtable.Add("width", this.Width);
			}
			if (this.ZIndex.HasValue)
			{
				hashtable.Add("zIndex", this.ZIndex);
			}
			return hashtable;
		}
	}
}
