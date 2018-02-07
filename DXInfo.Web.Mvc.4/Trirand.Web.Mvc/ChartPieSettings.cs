using System;
using System.Collections;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
namespace Trirand.Web.Mvc
{
	public class ChartPieSettings
	{
		public bool AllowPointSelect
		{
			get;
			set;
		}
		public bool Animation
		{
			get;
			set;
		}
		public string Color
		{
			get;
			set;
		}
		public string Cursor
		{
			get;
			set;
		}
		public ChartLineDashStyle DashStyle
		{
			get;
			set;
		}
		public ChartDataLabelSettings DataLabels
		{
			get;
			set;
		}
		public bool EnableMouseTracking
		{
			get;
			set;
		}
		public ChartPlotOptionsClientSideEvents ClientSideEvents
		{
			get;
			set;
		}
		public string ID
		{
			get;
			set;
		}
		public int LineWidth
		{
			get;
			set;
		}
		public ChartMarkerSettings Marker
		{
			get;
			set;
		}
		public ChartMarkerSettings MarkerHover
		{
			get;
			set;
		}
		public ChartMarkerSettings MarkerSelect
		{
			get;
			set;
		}
		public ChartPointClientSideEvents PointClientSideEvents
		{
			get;
			set;
		}
		public object PointStart
		{
			get;
			set;
		}
		public int PointInterval
		{
			get;
			set;
		}
		public bool Selected
		{
			get;
			set;
		}
		public bool Shadow
		{
			get;
			set;
		}
		public bool ShowCheckBox
		{
			get;
			set;
		}
		public bool ShowInLegend
		{
			get;
			set;
		}
		public ChartSeriesStacking Stacking
		{
			get;
			set;
		}
		public bool StickyTracking
		{
			get;
			set;
		}
		public bool Visible
		{
			get;
			set;
		}
		public int ZIndex
		{
			get;
			set;
		}
		public Unit Size
		{
			get;
			set;
		}
		public int SlicedOffset
		{
			get;
			set;
		}
		public ChartPieSettings()
		{
			this.AllowPointSelect = false;
			this.Animation = true;
			this.Color = "";
			this.Cursor = "";
			this.DashStyle = ChartLineDashStyle.Solid;
			this.DataLabels = new ChartDataLabelSettings();
			this.EnableMouseTracking = true;
			this.ClientSideEvents = new ChartPlotOptionsClientSideEvents();
			this.ID = "";
			this.LineWidth = 2;
			this.Marker = new ChartMarkerSettings();
			this.MarkerHover = new ChartMarkerSettings();
			this.MarkerSelect = new ChartMarkerSettings();
			this.PointClientSideEvents = new ChartPointClientSideEvents();
			this.PointStart = null;
			this.PointInterval = 1;
			this.Selected = false;
			this.Shadow = true;
			this.ShowCheckBox = false;
			this.ShowInLegend = true;
			this.Stacking = ChartSeriesStacking.None;
			this.StickyTracking = true;
			this.Visible = true;
			this.ZIndex = 0;
			this.Size = Unit.Percentage(75.0);
			this.SlicedOffset = 10;
		}
		internal string ToJSON(JQChart chart)
		{
			return new JavaScriptSerializer().Serialize(this.ToHashtable(chart));
		}
		internal Hashtable ToHashtable(JQChart chart)
		{
			Hashtable hashtable = new Hashtable();
			if (this.AllowPointSelect)
			{
				hashtable.Add("allowPointSelect", true);
			}
			if (!this.Animation)
			{
				hashtable.Add("animation", false);
			}
			if (!string.IsNullOrEmpty(this.Color))
			{
				hashtable.Add("color", this.Color);
			}
			if (!string.IsNullOrEmpty(this.Cursor))
			{
				hashtable.Add("cursor", this.Cursor);
			}
			if (this.DashStyle != ChartLineDashStyle.Solid)
			{
				hashtable.Add("dashStyle", this.DashStyle.ToString().ToLower());
			}
			hashtable.Add("dataLabels", this.DataLabels.ToHashtable(chart));
			if (!this.EnableMouseTracking)
			{
				hashtable.Add("enableMouseTracking", false);
			}
			if (!string.IsNullOrEmpty(this.ID))
			{
				hashtable.Add("id", this.ID);
			}
			if (this.LineWidth != 2)
			{
				hashtable.Add("lineWidth", 2);
			}
			hashtable.Add("marker", this.Marker.ToHashtable());
			hashtable.Add("markerHover", this.MarkerHover.ToHashtable());
			hashtable.Add("markerSelect", this.MarkerSelect.ToHashtable());
			if (this.PointStart != null)
			{
				hashtable.Add("pointStart", this.PointStart.ToJson(chart));
			}
			if (this.PointInterval != 1)
			{
				hashtable.Add("pointInterval", 1);
			}
			if (this.Selected)
			{
				hashtable.Add("selected", true);
			}
			if (!this.Shadow)
			{
				hashtable.Add("shadow", false);
			}
			if (this.ShowCheckBox)
			{
				hashtable.Add("showCheckBox", true);
			}
			if (!this.ShowInLegend)
			{
				hashtable.Add("showInLegend", false);
			}
			if (this.Stacking != ChartSeriesStacking.None)
			{
				hashtable.Add("stacking", this.Stacking.ToString().ToLower());
			}
			if (!this.StickyTracking)
			{
				hashtable.Add("stickTracking", false);
			}
			if (!this.Visible)
			{
				hashtable.Add("visible", false);
			}
			if (this.ZIndex != 0)
			{
				hashtable.Add("zIndex", this.ZIndex);
			}
			if (this.Size != Unit.Percentage(75.0))
			{
				hashtable.Add("size", this.Size.Value.ToString());
			}
			if (this.SlicedOffset != 10)
			{
				hashtable.Add("slicedOffet", this.SlicedOffset);
			}
			return hashtable;
		}
	}
}
