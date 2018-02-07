using System;
using System.Collections.Specialized;
namespace Trirand.Web.Mvc
{
	public class JQGridEditData
	{
		public NameValueCollection RowData
		{
			get;
			set;
		}
		public string RowKey
		{
			get;
			set;
		}
		public string ParentRowKey
		{
			get;
			set;
		}
	}
}
