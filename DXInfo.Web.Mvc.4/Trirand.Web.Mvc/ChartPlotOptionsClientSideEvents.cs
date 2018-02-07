using System;
using System.Collections;
using System.Web.Script.Serialization;
namespace Trirand.Web.Mvc
{
	public class ChartPlotOptionsClientSideEvents
	{
		public string Click
		{
			get;
			set;
		}
		public string CheckBoxClick
		{
			get;
			set;
		}
		public string Hide
		{
			get;
			set;
		}
		public string LegendItemClick
		{
			get;
			set;
		}
		public string MouseOver
		{
			get;
			set;
		}
		public string MouseOut
		{
			get;
			set;
		}
		public string Show
		{
			get;
			set;
		}
		public ChartPlotOptionsClientSideEvents()
		{
			this.Click = "";
			this.CheckBoxClick = "";
			this.Hide = "";
			this.LegendItemClick = "";
			this.MouseOver = "";
			this.MouseOut = "";
			this.Show = "";
		}
		internal string ToJSON()
		{
			return new JavaScriptSerializer().Serialize(this.ToHashtable());
		}
		internal Hashtable ToHashtable()
		{
			return new Hashtable();
		}
	}
}
