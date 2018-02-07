using System;
using System.Collections;
using System.Web.Script.Serialization;
namespace Trirand.Web.Mvc
{
	public class ChartPointClientSideEvents
	{
		public string Click
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
		public string Remove
		{
			get;
			set;
		}
		public string Select
		{
			get;
			set;
		}
		public string UnSelect
		{
			get;
			set;
		}
		public string Update
		{
			get;
			set;
		}
		public ChartPointClientSideEvents()
		{
			this.Click = "";
			this.MouseOver = "";
			this.MouseOut = "";
			this.Remove = "";
			this.Select = "";
			this.UnSelect = "";
			this.Update = "";
		}
		internal string ToJSON()
		{
			return new JavaScriptSerializer().Serialize(this.ToHashtable());
		}
		internal Hashtable ToHashtable()
		{
			Hashtable hashtable = new Hashtable();
			if (!string.IsNullOrEmpty(this.Click))
			{
				hashtable.Add("click", this.Click);
			}
			if (!string.IsNullOrEmpty(this.MouseOver))
			{
				hashtable.Add("mouseOver", this.MouseOver);
			}
			if (!string.IsNullOrEmpty(this.MouseOut))
			{
				hashtable.Add("mouseOut", this.MouseOut);
			}
			if (!string.IsNullOrEmpty(this.Remove))
			{
				hashtable.Add("remove", this.Remove);
			}
			if (!string.IsNullOrEmpty(this.Select))
			{
				hashtable.Add("select", this.Select);
			}
			if (!string.IsNullOrEmpty(this.UnSelect))
			{
				hashtable.Add("unselect", this.UnSelect);
			}
			if (!string.IsNullOrEmpty(this.Update))
			{
				hashtable.Add("update", this.Update);
			}
			return hashtable;
		}
	}
}
