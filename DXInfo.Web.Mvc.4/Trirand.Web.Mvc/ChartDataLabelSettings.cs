using System;
using System.Collections;
using System.Web.Script.Serialization;
namespace Trirand.Web.Mvc
{
	public class ChartDataLabelSettings
	{
		public ChartHorizontalAlign Align
		{
			get;
			set;
		}
		public string Color
		{
			get;
			set;
		}
		public bool Enabled
		{
			get;
			set;
		}
		public string Formatter
		{
			get;
			set;
		}
		public int Rotation
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
		public ChartDataLabelSettings()
		{
			this.Align = ChartHorizontalAlign.Center;
			this.Color = "";
			this.Enabled = false;
			this.Formatter = "";
			this.Rotation = 0;
			this.X = 0;
			this.Y = -6;
		}
		internal string ToJSON(JQChart chart)
		{
			return new JavaScriptSerializer().Serialize(this.ToHashtable(chart));
		}
		internal Hashtable ToHashtable(JQChart chart)
		{
			Hashtable hashtable = new Hashtable();
			if (this.Align != ChartHorizontalAlign.Center)
			{
				hashtable.Add("align", this.Align.ToString().ToLower());
			}
			if (!string.IsNullOrEmpty(this.Color))
			{
				hashtable.Add("color", this.Color);
			}
			if (this.Enabled)
			{
				hashtable.Add("enabled", true);
			}
			if (!string.IsNullOrEmpty(this.Formatter))
			{
				hashtable.Add("formatter", this.Formatter);
				chart.ReplaceTable.Add(string.Format("\"{0}\":\"{1}\"", "formatter", this.Formatter), string.Format("{0}:{1}", "formatter", this.Formatter));
			}
			if (this.Rotation != 0)
			{
				hashtable.Add("rotation", this.Rotation);
			}
			if (this.X != 0)
			{
				hashtable.Add("x", this.X);
			}
			if (this.Y != -6)
			{
				hashtable.Add("y", this.Y);
			}
			return hashtable;
		}
	}
}
