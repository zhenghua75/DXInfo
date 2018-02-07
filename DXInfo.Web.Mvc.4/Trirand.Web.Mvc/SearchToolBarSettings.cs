using System;
namespace Trirand.Web.Mvc
{
	public class SearchToolBarSettings
	{
		public SearchToolBarAction SearchToolBarAction
		{
			get;
			set;
		}
		public SearchToolBarSettings()
		{
			this.SearchToolBarAction = SearchToolBarAction.SearchOnEnter;
		}
	}
}
