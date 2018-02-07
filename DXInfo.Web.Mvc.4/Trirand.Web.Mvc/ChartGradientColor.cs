using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;
namespace Trirand.Web.Mvc
{
	public class ChartGradientColor
	{
		public ChartLinearGradient LinearGradient
		{
			get;
			set;
		}
		public List<string> Stops
		{
			get;
			set;
		}
		public ChartGradientColor()
		{
			this.LinearGradient = new ChartLinearGradient();
			this.Stops = new List<string>();
		}
		internal string ToJSON()
		{
			return new JavaScriptSerializer().Serialize(this.ToHashtable());
		}
		internal Hashtable ToHashtable()
		{
			Hashtable hashtable = new Hashtable();
			List<object> list = new List<object>();
			string[] value = new string[]
			{
				this.LinearGradient.FromX.ToString(),
				this.LinearGradient.FromY.ToString(),
				this.LinearGradient.ToX.ToString(),
				this.LinearGradient.ToY.ToString()
			};
			int num = 0;
			foreach (string current in this.Stops)
			{
				object[] item = new object[]
				{
					num++,
					current
				};
				list.Add(item);
			}
			hashtable.Add("linearGradient", value);
			hashtable.Add("stops", list);
			return hashtable;
		}
		internal bool IsSet()
		{
			return this.Stops.Count > 0;
		}
	}
}
