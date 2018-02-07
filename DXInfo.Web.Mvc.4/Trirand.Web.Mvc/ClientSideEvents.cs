using System;
namespace Trirand.Web.Mvc
{
	public class ClientSideEvents
	{
		public string BeforeAddDialogShown
		{
			get;
			set;
		}
		public string AfterAddDialogShown
		{
			get;
			set;
		}
		public string AfterAddDialogRowInserted
		{
			get;
			set;
		}
		public string BeforeEditDialogShown
		{
			get;
			set;
		}
		public string AfterEditDialogShown
		{
			get;
			set;
		}
		public string AfterEditDialogRowInserted
		{
			get;
			set;
		}
		public string BeforeDeleteDialogShown
		{
			get;
			set;
		}
		public string AfterDeleteDialogShown
		{
			get;
			set;
		}
		public string AfterDeleteDialogRowDeleted
		{
			get;
			set;
		}
		public string RowSelect
		{
			get;
			set;
		}
		public string RowDoubleClick
		{
			get;
			set;
		}
		public string RowRightClick
		{
			get;
			set;
		}
		public string GridInitialized
		{
			get;
			set;
		}
		public string BeforeAjaxRequest
		{
			get;
			set;
		}
		public string AfterAjaxRequest
		{
			get;
			set;
		}
		public string ServerError
		{
			get;
			set;
		}
		public string LoadDataError
		{
			get;
			set;
		}
		public string SubGridRowExpanded
		{
			get;
			set;
		}
		public string ColumnSort
		{
			get;
			set;
		}
        public string BeforeAddDialogSubmit { get; set; }
        public string BeforeEditDialogSubmit { get; set; }
        public string BeforeDelDialogSubmit { get; set; }
        public string BeforeRefresh { get; set; }
        public string SerializeGridData { get; set; }
        public string AfterClickPgButtons { get; set; }
        public string SerializeRowData { get; set; }
        public string BeforeAddDialogInitData { get; set; }
        public string BeforeEditDialogInitData { get; set; }
        public string AddDialogOnClickSubmit { get; set; }
        public string EditDialogOnClickSubmit { get; set; }
        public string AddDialogOnInitializeForm { get; set; }
        public string EditDialogOnInitializeForm { get; set; }
        public string AddDialogSerializeEditData { get; set; }
        public string EditDialogSerializeEditData { get; set; }
        public string AddDialogBeforeCheckValues { get; set; }
        public string EditDialogBeforeCheckValues { get; set; }
        public string SerializeDelData { get; set; }
        public string AfterSearchDialogShown { get; set; }
        public string BeforeSearchDialogShown { get; set; }
		public ClientSideEvents()
		{
			this.BeforeAddDialogShown = "";
			this.AfterAddDialogShown = "";
			this.AfterAddDialogRowInserted = "";
			this.BeforeEditDialogShown = "";
			this.AfterEditDialogShown = "";
			this.AfterEditDialogRowInserted = "";
			this.BeforeDeleteDialogShown = "";
			this.AfterDeleteDialogShown = "";
			this.AfterDeleteDialogRowDeleted = "";
			this.RowSelect = "";
            this.RowDoubleClick = "";
			this.RowRightClick = "";
            this.GridInitialized = "";
			//this.BeforeAjaxRequest = "";
			this.AfterAjaxRequest = "";
			this.ServerError = "";
			this.LoadDataError = "";
			this.SubGridRowExpanded = "";
			this.ColumnSort = "";
            this.BeforeAddDialogSubmit = "";
            this.BeforeEditDialogSubmit = "";
            this.BeforeRefresh = "";
            this.SerializeGridData = "";
            this.AfterClickPgButtons = "";
            this.SerializeRowData = "";
            this.BeforeAddDialogInitData = "";
            this.BeforeEditDialogInitData = "";
            this.AddDialogOnClickSubmit = "";
            this.EditDialogOnClickSubmit = "";
            this.AddDialogOnInitializeForm = "";
            this.EditDialogOnInitializeForm = "";
            this.AddDialogSerializeEditData = "";
            this.EditDialogSerializeEditData = "";
            this.AddDialogBeforeCheckValues = "";
            this.EditDialogBeforeCheckValues = "";
            this.SerializeDelData = "";
            this.AfterSearchDialogShown = "";
            this.BeforeSearchDialogShown = "";
		}
	}
}
