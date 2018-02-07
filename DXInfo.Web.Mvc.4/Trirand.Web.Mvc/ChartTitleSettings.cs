using System;
using System.Collections;
using System.Web.Script.Serialization;
namespace Trirand.Web.Mvc
{
	public class ChartTitleSettings
	{
		public ChartHorizontalAlign Align
		{
			get;
			set;
		}
		public bool Floating
		{
			get;
			set;
		}
		public int Margin
		{
			get;
			set;
		}
		public string Text
		{
			get;
			set;
		}
		public ChartVerticalAlign VerticalAlign
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
		public ChartTitleSettings()
		{
			this.Align = ChartHorizontalAlign.Center;
			this.Floating = false;
			this.Margin = 15;
			this.Text = "";
			this.VerticalAlign = ChartVerticalAlign.Top;
			this.X = 0;
			this.Y = 25;
		}
		internal string ToJSON()
		{
			return new JavaScriptSerializer().Serialize(this.ToHashtable());
		}
		internal Hashtable ToHashtable()
		{
			Hashtable hashtable = new Hashtable();
			string value = string.IsNullOrEmpty(this.Text) ? " " : this.Text;
			if (this.Align != ChartHorizontalAlign.Center)
			{
				hashtable.Add("align", this.Align.ToString().ToLower());
			}
			if (this.Floating)
			{
				hashtable.Add("floating", true);
			}
			if (this.Margin != 15)
			{
				hashtable.Add("margin", this.Margin);
			}
			hashtable.Add("text", value);
			if (this.VerticalAlign != ChartVerticalAlign.Top)
			{
				hashtable.Add("verticalAlign", this.VerticalAlign.ToString().ToLower());
			}
			if (this.X != 0)
			{
				hashtable.Add("x", this.X);
			}
			if (this.Y != 25)
			{
				hashtable.Add("y", this.Y);
			}
			return hashtable;
		}
	}
}
