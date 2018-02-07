using System;
namespace Trirand.Web.Mvc
{
	public class JQGridTreeExpandData
	{
		public int ParentLevel
		{
			get;
			set;
		}
		public string ParentID
		{
			get;
			set;
		}
		public JQGridTreeExpandData()
		{
			this.ParentLevel = -1;
			this.ParentID = null;
		}
	}
}
