using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
namespace Trirand.Web.Mvc
{
	public class ToolBarSettings
	{
		public bool ShowEditButton
		{
			get;
			set;
		}
		public bool ShowAddButton
		{
			get;
			set;
		}
		public bool ShowDeleteButton
		{
			get;
			set;
		}
		public bool ShowSearchButton
		{
			get;
			set;
		}
        //zhh
        public bool ShowExcelButton { get; set; }
		public bool ShowRefreshButton
		{
			get;
			set;
		}
		public bool ShowViewRowDetailsButton
		{
			get;
			set;
		}
		public bool ShowSearchToolBar
		{
			get;
			set;
		}
		public ToolBarPosition ToolBarPosition
		{
			get;
			set;
		}
		public ToolBarAlign ToolBarAlign
		{
			get;
			set;
		}
       
		public List<JQGridToolBarButton> CustomButtons
		{
			get;
			set;
		}
        //public string BeforeRefresh { get; set; }
        public bool ShowColumnChooser { get; set; }
		public ToolBarSettings()
		{
			this.ShowEditButton = false;
			this.ShowAddButton = false;
			this.ShowDeleteButton = false;
			this.ShowSearchButton = false;
            //zhh
            this.ShowExcelButton = false;
			this.ShowRefreshButton = false;
			this.ShowViewRowDetailsButton = false;
			this.ShowSearchToolBar = false;
            this.ShowColumnChooser = false;
			this.ToolBarAlign = ToolBarAlign.Left;
			this.ToolBarPosition = ToolBarPosition.TopAndBottom;
			this.CustomButtons = new List<JQGridToolBarButton>();
            //this.BeforeRefresh = "";
		}
	}
}
