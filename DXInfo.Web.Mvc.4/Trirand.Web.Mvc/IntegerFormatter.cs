using System;
namespace Trirand.Web.Mvc
{
	public class IntegerFormatter : JQGridColumnFormatter
	{
		public string ThousandsSeparator
		{
			get;
			set;
		}
		public string DefaultValue
		{
			get;
			set;
		}
	}
}
