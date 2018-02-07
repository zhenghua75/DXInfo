using System;
using System.Collections;
using System.Web.Script.Serialization;
namespace Trirand.Web.Mvc
{
	public class ChartToolTipSettings
	{
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
		public string Formatter
		{
			get;
			set;
		}
		public bool Shared
		{
			get;
			set;
		}
		public ChartCrossHairSettings XAxisCrossHair
		{
			get;
			set;
		}
		public ChartCrossHairSettings YAxisCrossHair
		{
			get;
			set;
		}
		public ChartToolTipSettings()
		{
			this.BackgroundColor = "rgba(255, 255, 255, .85)";
			this.BorderColor = "auto";
			this.BorderRadius = 5;
			this.BorderWidth = 2;
			this.Formatter = "";
			this.Enabled = true;
			this.Shared = false;
			this.XAxisCrossHair = new ChartCrossHairSettings();
			this.YAxisCrossHair = new ChartCrossHairSettings();
		}
		internal string ToJSON()
		{
			Hashtable hashtable = new Hashtable();
			if (this.BackgroundColor != "rgba(255, 255, 255, .85)")
			{
				hashtable.Add("backgroundColor", this.BackgroundColor);
			}
			if (this.BorderColor != "auto")
			{
				hashtable.Add("borderColor", this.BorderColor);
			}
			if (this.BorderRadius != 5)
			{
				hashtable.Add("borderRadius", this.BorderRadius);
			}
			if (this.BorderWidth != 2)
			{
				hashtable.Add("borderWidth", this.BorderWidth);
			}
			if (this.Shared)
			{
				hashtable.Add("shared", true);
			}
			if (!this.Enabled)
			{
				hashtable.Add("enabled", false);
			}
			Hashtable hashtable2 = this.XAxisCrossHair.ToHashtable();
			Hashtable hashtable3 = this.XAxisCrossHair.ToHashtable();
			if (hashtable2.Count > 0 || hashtable3.Count > 0)
			{
				Hashtable[] value = new Hashtable[]
				{
					hashtable2,
					hashtable3
				};
				hashtable.Add("crosshairs", value);
			}
			string json = new JavaScriptSerializer().Serialize(hashtable);
			return JsonUtil.RenderClientSideEvent(json, "formatter", this.Formatter);
		}
	}
}
