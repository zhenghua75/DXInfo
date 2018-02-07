using System;
using System.Collections;
using System.Web.Script.Serialization;
namespace Trirand.Web.Mvc
{
	public class ChartLabelSettings
	{
		public ChartHorizontalAlign Align
		{
			get;
			set;
		}
		public bool Enabled
		{
			get;
			set;
		}
		public string Color
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
		public int StaggerLines
		{
			get;
			set;
		}
		public int Step
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
		public ChartLabelSettings()
		{
			this.Align = ChartHorizontalAlign.Center;
			this.Enabled = true;
			this.Color = "";
			this.Formatter = "";
			this.Rotation = 0;
			this.StaggerLines = 0;
			this.Step = 0;
			this.X = 0;
			this.Y = 0;
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
			if (!this.Enabled)
			{
				hashtable.Add("enabled", false);
			}
			if (!string.IsNullOrEmpty(this.Color))
			{
				hashtable.Add("color", this.Color);
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
			if (this.StaggerLines != 0)
			{
				hashtable.Add("staggerLines", this.StaggerLines);
			}
			if (this.Step != 0)
			{
				hashtable.Add("step", this.Step);
			}
			if (this.X != 0)
			{
				hashtable.Add("x", this.X);
			}
			if (this.Y != 0)
			{
				hashtable.Add("y", this.Y);
			}
			return hashtable;
		}
		internal bool IsSet(JQChart chart)
		{
			return this.ToHashtable(chart).Count > 0;
		}
	}
}
