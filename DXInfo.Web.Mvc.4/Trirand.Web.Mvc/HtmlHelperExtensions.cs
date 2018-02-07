using System;
using System.Web.Mvc;
namespace Trirand.Web.Mvc
{
	public static class HtmlHelperExtensions
	{
		public static TrirandNamespace Trirand(this HtmlHelper helper)
		{
			return new TrirandNamespace();
		}
	}
}
