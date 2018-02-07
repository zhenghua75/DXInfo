using System;
namespace Trirand.Web.Mvc
{
	public class TreeViewClientSideEvents
	{
		public string Expand
		{
			get;
			set;
		}
		public string Collapse
		{
			get;
			set;
		}
		public string Check
		{
			get;
			set;
		}
		public string Select
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
		public TreeViewClientSideEvents()
		{
			this.Expand = "";
			this.Collapse = "";
			this.Check = "";
			this.Select = "";
			this.MouseOver = "";
			this.MouseOut = "";
		}
	}
}
