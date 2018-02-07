using System;
using System.Collections.Specialized;
namespace Trirand.Web.Mvc
{
	public class JQGridState
	{
		public NameValueCollection QueryString
		{
			get;
			set;
		}
		public JQGridState()
		{
			this.QueryString = new NameValueCollection();
		}
	}
}
