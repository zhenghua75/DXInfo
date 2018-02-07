using System;
namespace Trirand.Web.Mvc
{
	public class PagerSettings
	{
		public int PageSize
		{
			get;
			set;
		}
		public int CurrentPage
		{
			get;
			set;
		}
		public string PageSizeOptions
		{
			get;
			set;
		}
		public string NoRowsMessage
		{
			get;
			set;
		}
		public bool ScrollBarPaging
		{
			get;
			set;
		}
		public string PagingMessage
		{
			get;
			set;
		}
		public PagerSettings()
		{
			this.PageSize = 100;
			this.CurrentPage = 1;
			//this.PageSizeOptions = "[10,20,30]";
            //zhh
            this.PageSizeOptions = "[100,200,300,500,1000]";
			this.NoRowsMessage = "";
			this.ScrollBarPaging = false;
			this.PagingMessage = "";
		}
	}
}
