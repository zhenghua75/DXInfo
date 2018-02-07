using System;
using System.Collections;
using System.Web.Script.Serialization;
namespace Trirand.Web.Mvc
{
    /// <summary>
    /// zhh 20100826
    /// Õº∆¨µº≥ˆ…Ë÷√
    /// </summary>
	public class ExportSettings
	{
		public string Url
		{
			get;
			set;
		}
		public ExportSettings()
		{
			this.Url = "";
		}
		internal string ToJSON()
		{
			return new JavaScriptSerializer().Serialize(this.ToHashtable());
		}
		internal Hashtable ToHashtable()
		{
			Hashtable hashtable = new Hashtable();
			if (!string.IsNullOrEmpty(this.Url))
			{
				hashtable.Add("url", this.Url);
			}
			hashtable.Add("enableImages", true);
			return hashtable;
		}
	}
}
