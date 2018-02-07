using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web.Script.Serialization;
namespace Trirand.Web.Mvc
{
	public class ChartLegendSettings
	{
		public ChartHorizontalAlign Align
		{
			get;
			set;
		}
		public string BackgroundColor
		{
			get;
			set;
		}
		public string BorderColor
		{
			get;
			set;
		}
		public int BorderRadius
		{
			get;
			set;
		}
		public int BorderWidth
		{
			get;
			set;
		}
		public bool Enabled
		{
			get;
			set;
		}
		public bool Floating
		{
			get;
			set;
		}
		public NameValueCollection ItemHiddenStyle
		{
			get;
			set;
		}
		public NameValueCollection ItemHoverStyle
		{
			get;
			set;
		}
		public NameValueCollection ItemStyle
		{
			get;
			set;
		}
		public int ItemWidth
		{
			get;
			set;
		}
		public ChartLegendLayout Layout
		{
			get;
			set;
		}
		public string LabelFormatter
		{
			get;
			set;
		}
		public int Margin
		{
			get;
			set;
		}
		public bool Reversed
		{
			get;
			set;
		}
		public bool Shadow
		{
			get;
			set;
		}
		public NameValueCollection Style
		{
			get;
			set;
		}
		public int SymbolPadding
		{
			get;
			set;
		}
		public int SymbolWidth
		{
			get;
			set;
		}
		public ChartVerticalAlign VerticalAlign
		{
			get;
			set;
		}
		public int Width
		{
			get;
			set;
		}
		public int X
		{
			get;
			set;
		}
		public int Y
		{
			get;
			set;
		}
		public ChartLegendSettings()
		{
			this.Align = ChartHorizontalAlign.Center;
			this.BackgroundColor = "";
			this.BorderColor = "#909090";
			this.BorderRadius = 5;
			this.BorderWidth = 1;
			this.Enabled = true;
			this.Floating = false;
			this.ItemHiddenStyle = new NameValueCollection();
			this.ItemHoverStyle = new NameValueCollection();
			this.ItemStyle = new NameValueCollection();
			this.ItemWidth = 0;
			this.Layout = ChartLegendLayout.Horizontal;
			this.LabelFormatter = "";
			this.Margin = 15;
			this.Reversed = false;
			this.Shadow = false;
			this.Style = new NameValueCollection();
			this.SymbolPadding = 5;
			this.SymbolWidth = 30;
			this.VerticalAlign = ChartVerticalAlign.Bottom;
			this.Width = 0;
			this.X = 15;
			this.Y = 0;
		}
		internal string ToJSON()
		{
			Hashtable hashtable = new Hashtable();
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			if (this.Align != ChartHorizontalAlign.Center)
			{
				hashtable.Add("align", this.Align.ToString().ToLower());
			}
			if (!string.IsNullOrEmpty(this.BackgroundColor))
			{
				hashtable.Add("backgroundColor", this.BackgroundColor);
			}
			if (this.BorderColor != "#909090")
			{
				hashtable.Add("borderColor", this.BorderColor);
			}
			if (this.BorderRadius != 5)
			{
				hashtable.Add("borderRadius", this.BorderRadius);
			}
			if (this.BorderWidth != 1)
			{
				hashtable.Add("borderWidth", this.BorderWidth);
			}
			if (!this.Enabled)
			{
				hashtable.Add("enabled", false);
			}
			if (this.Floating)
			{
				hashtable.Add("floating", true);
			}
			if (this.ItemHiddenStyle.Count > 0)
			{
				hashtable.Add("itemHiddenStyle", this.ItemHiddenStyle);
			}
			if (this.ItemHoverStyle.Count > 0)
			{
				hashtable.Add("itemHoverStyle", this.ItemHoverStyle);
			}
			if (this.ItemStyle.Count > 0)
			{
				hashtable.Add("itemStyle", this.ItemStyle);
			}
			if (this.ItemWidth != 0)
			{
				hashtable.Add("itemWidth", this.ItemWidth);
			}
			if (!string.IsNullOrEmpty(this.LabelFormatter))
			{
				hashtable.Add("labelFormatter", this.LabelFormatter);
			}
			if (this.Layout != ChartLegendLayout.Horizontal)
			{
				hashtable.Add("layout", this.Layout.ToString().ToLower());
			}
			if (this.Margin != 15)
			{
				hashtable.Add("margin", this.Margin);
			}
			if (this.Reversed)
			{
				hashtable.Add("reversed", true);
			}
			if (this.Shadow)
			{
				hashtable.Add("shadow", true);
			}
			if (this.Style.Count > 0)
			{
				hashtable.Add("style", this.Style);
			}
			if (this.SymbolPadding != 5)
			{
				hashtable.Add("symbolPadding", this.SymbolPadding);
			}
			if (this.SymbolWidth != 30)
			{
				hashtable.Add("symbolWidth", this.SymbolWidth);
			}
			if (this.VerticalAlign != ChartVerticalAlign.Bottom)
			{
				hashtable.Add("verticalAlign", this.VerticalAlign.ToString().ToLower());
			}
			if (this.Width != 0)
			{
				hashtable.Add("width", this.Width);
			}
			if (this.X != 15)
			{
				hashtable.Add("x", this.X);
			}
			if (this.Y != 0)
			{
				hashtable.Add("y", this.Y);
			}
			return javaScriptSerializer.Serialize(hashtable);
		}
	}
}
