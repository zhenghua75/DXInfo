using System;
namespace Trirand.Web.Mvc
{
	public class DropDownListClientSideEvents
	{
		public string Show
		{
			get;
			set;
		}
		public string Hide
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
		public string Initialized
		{
			get;
			set;
		}
		public string KeyDown
		{
			get;
			set;
		}
		public DropDownListClientSideEvents()
		{
			this.Show = "";
			this.Hide = "";
			this.Select = "";
			this.MouseOver = "";
			this.MouseOut = "";
			this.Initialized = "";
			this.KeyDown = "";
		}
	}
}
