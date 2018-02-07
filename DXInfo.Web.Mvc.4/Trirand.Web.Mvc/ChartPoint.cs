using System;
using System.Collections;
namespace Trirand.Web.Mvc
{
	public class ChartPoint
	{
		public object X
		{
			get;
			set;
		}
		public object Y
		{
			get;
			set;
		}
		public ChartMarkerSettings Marker
		{
			get;
			set;
		}
		public bool Sliced
		{
			get;
			set;
		}
		public bool Selected
		{
			get;
			set;
		}
		public string Color
		{
			get;
			set;
		}
		public ChartPoint()
		{
			this.X = null;
			this.Y = null;
			this.Marker = new ChartMarkerSettings();
			this.Sliced = false;
			this.Selected = false;
			this.Color = "";
		}
		public ChartPoint(object x) : this()
		{
			this.X = x;
		}
		public ChartPoint(object x, object y) : this()
		{
			this.X = x;
			this.Y = y;
		}
		internal Hashtable ToHashtable(JQChart chart)
		{
			Hashtable hashtable = new Hashtable();
			if (this.X != null)
			{
				hashtable.Add("x", this.X.ToJson(chart));
				hashtable.Add("name", this.X.ToJson(chart));
			}
			if (this.Y != null)
			{
				hashtable.Add("y", this.Y.ToJson(chart));
			}
			if (this.Marker.IsSet())
			{
				hashtable.Add("marker", this.Marker.ToHashtable());
			}
			if (this.Sliced)
			{
				hashtable.Add("sliced", true);
			}
			if (this.Selected)
			{
				hashtable.Add("selected", true);
			}
			if (!string.IsNullOrEmpty(this.Color))
			{
				hashtable.Add("color", this.Color);
			}
			return hashtable;
		}
	}
}
