using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Reflection;
namespace Trirand.Web.Mvc
{
    public class CheckBoxColumn : DataGridColumn
    {
        public ICollection DataSource { get; set; }
        public string DataField { get; set; }
        public string DataTextField { get; set; }
        public string DataValueField { get; set; }

        public override void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType)
        {
            base.InitializeCell(cell, columnIndex, itemType);
            switch (itemType)
            {
                case ListItemType.Header:
                    cell.Text = HeaderText;
                    break;
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    cell.DataBinding += new EventHandler(cell_DataBinding);
                    break;
                case ListItemType.EditItem:
                    break;
            }
        }

        void cell_DataBinding(object sender, EventArgs e)
        {
            TableCell cell = sender as TableCell;
            DataGridItem dgi = cell.NamingContainer as DataGridItem;
            try
            {
                cell.Text = Convert.ToBoolean(((DataRowView)dgi.DataItem).Row[DataField])?"是":"否";
            }
            catch (IndexOutOfRangeException)
            {
                throw new Exception("Specified DataField was not found.");
            }
            catch (Exception OtherEx)
            {
                throw new Exception(OtherEx.InnerException.Message);
            }
        }
    }
    public class DropDownColumn : DataGridColumn
    {
        public ICollection DataSource { get; set; }
        public string DataField { get; set; }
        //public string DataTextField { get; set; }
        //public string DataValueField { get; set; }
        public List<SelectListItem> EditList { get; set; }

        public override void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType)
        {
            base.InitializeCell(cell, columnIndex, itemType);
            switch (itemType)
            {
                case ListItemType.Header:
                    cell.Text = HeaderText;
                    break;
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    cell.DataBinding += new EventHandler(cell_DataBinding);
                    break;
                case ListItemType.EditItem:
                    break;
            }
        }

        void cell_DataBinding(object sender, EventArgs e)
        {
            TableCell cell = sender as TableCell;
            DataGridItem dgi = cell.NamingContainer as DataGridItem;
            try
            {
                string value = ((DataRowView)dgi.DataItem).Row[DataField].ToString();
                SelectListItem item = EditList.Find(f=>f.Value==value);
                if (item != null)
                {
                    cell.Text = item.Text;
                }
            }
            catch (IndexOutOfRangeException)
            {
                throw new Exception("Specified DataField was not found.");
            }
            catch (Exception OtherEx)
            {
                throw new Exception(OtherEx.InnerException.Message);
            }
        }
    }
	public class JQGrid
	{
		private EventHandlerList _events;
		private static readonly object EventDataResolved;
		public event JQGridDataResolvedEventHandler DataResolved
		{
			add
			{
				this.Events.AddHandler(JQGrid.EventDataResolved, value);
			}
			remove
			{
				this.Events.RemoveHandler(JQGrid.EventDataResolved, value);
			}
		}
		public bool AutoWidth
		{
			get;
			set;
		}
		public bool ShrinkToFit
		{
			get;
			set;
		}
		public List<JQGridColumn> Columns
		{
			get;
			set;
		}
		public List<JQGridHeaderGroup> HeaderGroups
		{
			get;
			set;
		}
		public EditDialogSettings EditDialogSettings
		{
			get;
			set;
		}
		public AddDialogSettings AddDialogSettings
		{
			get;
			set;
		}
		public DeleteDialogSettings DeleteDialogSettings
		{
			get;
			set;
		}
		public SearchDialogSettings SearchDialogSettings
		{
			get;
			set;
		}
		public SearchToolBarSettings SearchToolBarSettings
		{
			get;
			set;
		}
		public PagerSettings PagerSettings
		{
			get;
			set;
		}
		public ToolBarSettings ToolBarSettings
		{
			get;
			set;
		}
		public SortSettings SortSettings
		{
			get;
			set;
		}
		public AppearanceSettings AppearanceSettings
		{
			get;
			set;
		}
		public HierarchySettings HierarchySettings
		{
			get;
			set;
		}
		public GroupSettings GroupSettings
		{
			get;
			set;
		}
		public TreeGridSettings TreeGridSettings
		{
			get;
			set;
		}
		public GridExportSettings ExportSettings
		{
			get;
			set;
		}
        public ExcelExportSettings ExcelExportSettings { get; set; }
		public ClientSideEvents ClientSideEvents
		{
			get;
			set;
		}
		public string ID
		{
			get;
			set;
		}
		public string DataUrl
		{
			get;
			set;
		}
		public string EditUrl
		{
			get;
			set;
		}
		public bool ColumnReordering
		{
			get;
			set;
		}
		public RenderingMode RenderingMode
		{
			get;
			set;
		}
		public bool MultiSelect
		{
			get;
			set;
		}
		public MultiSelectMode MultiSelectMode
		{
			get;
			set;
		}
		public MultiSelectKey MultiSelectKey
		{
			get;
			set;
		}
		public Unit Width
		{
			get;
			set;
		}
		public Unit Height
		{
			get;
			set;
		}
		public object DataSource
		{
			get;
			set;
		}
		internal bool ShowToolBar
		{
			get
			{
				return this.ToolBarSettings.ShowAddButton || this.ToolBarSettings.ShowDeleteButton || this.ToolBarSettings.ShowEditButton || this.ToolBarSettings.ShowRefreshButton || this.ToolBarSettings.ShowSearchButton || this.ToolBarSettings.ShowViewRowDetailsButton ||this.ToolBarSettings.ShowExcelButton|| this.ToolBarSettings.CustomButtons.Count > 0;
			}
		}
        
		public AjaxCallBackMode AjaxCallBackMode
		{
			get
			{
				string text = HttpContext.Current.Request.Form["oper"];
                string text2 = HttpContext.Current.Request.QueryString["oper"];
                if (string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
                {
                    text = text2;
                }
				string value = HttpContext.Current.Request.QueryString["editMode"];
				string value2 = HttpContext.Current.Request.QueryString["_search"];
				AjaxCallBackMode result = AjaxCallBackMode.RequestData;
				string a;
				if (!string.IsNullOrEmpty(text) && (a = text) != null)
				{
					if (a == "add")
					{
						return AjaxCallBackMode.AddRow;
					}
					if (a == "edit")
					{
						return AjaxCallBackMode.EditRow;
					}
					if (a == "del")
					{
						return AjaxCallBackMode.DeleteRow;
					}
                    if (a == "excel")
                    {
                        return AjaxCallBackMode.Excel;
                    }
				}
				if (!string.IsNullOrEmpty(value))
				{
					result = AjaxCallBackMode.EditRow;
				}
				if (!string.IsNullOrEmpty(value2) && Convert.ToBoolean(value2))
				{
					result = AjaxCallBackMode.Search;
				}
				return result;
			}
		}
		private EventHandlerList Events
		{
			get
			{
				if (this._events == null)
				{
					this._events = new EventHandlerList();
				}
				return this._events;
			}
		}
		static JQGrid()
		{
			JQGrid.EventDataResolved = new object();
		}
        public string DataType { get; set; }
        public string LoadUI { get; set; }
        public string IgnoreFilterField { get; set; }
		public JQGrid()
		{
			this.AutoWidth = false;
			this.ShrinkToFit = false;
			this.EditDialogSettings = new EditDialogSettings();
			this.AddDialogSettings = new AddDialogSettings();
			this.DeleteDialogSettings = new DeleteDialogSettings();
			this.SearchDialogSettings = new SearchDialogSettings();
			this.SearchToolBarSettings = new SearchToolBarSettings();
			this.PagerSettings = new PagerSettings();
			this.ToolBarSettings = new ToolBarSettings();
			this.SortSettings = new SortSettings();
			this.AppearanceSettings = new AppearanceSettings();
			this.HierarchySettings = new HierarchySettings();
			this.GroupSettings = new GroupSettings();
			this.TreeGridSettings = new TreeGridSettings();
			this.ExportSettings = new GridExportSettings();
            this.ExcelExportSettings = new ExcelExportSettings();
			this.ClientSideEvents = new ClientSideEvents();
			this.Columns = new List<JQGridColumn>();
			this.HeaderGroups = new List<JQGridHeaderGroup>();
			this.DataUrl = "";
			this.EditUrl = "";
			this.ColumnReordering = false;
			this.RenderingMode = RenderingMode.Default;
			this.MultiSelect = false;
			this.MultiSelectMode = MultiSelectMode.SelectOnRowClick;
			this.MultiSelectKey = MultiSelectKey.None;
			this.Width = Unit.Empty;
            this.Height = Unit.Percentage(100);//Unit.Empty;
            this.DataType = "json";
            this.LoadUI = "block";
            this.IgnoreFilterField = "";
		}
		public JsonResult DataBind(object dataSource)
		{
			this.DataSource = dataSource;
			return this.DataBind();
		}
		public JsonResult DataBind()
		{
            //AjaxCallBackMode ajaxCallBackMode = this.AjaxCallBackMode;
            //if (ajaxCallBackMode != AjaxCallBackMode.RequestData)
            //{
            //    //IL_0F:
            //    return this.GetJsonResponse();
            //}
            return this.GetJsonResponse();//goto IL_0F;// IL_0F;
		}
        //public ActionResult ShowEditValidationMessage(string errorMessage)
        //{
        //    HttpContext.Current.Response.Clear();
        //    HttpContext.Current.Response.StatusCode = 500;
        //    HttpContext.Current.Response.TrySkipIisCustomErrors = true;
        //    return new ContentResult
        //    {
        //        Content = errorMessage,
        //    };
        //}
        //private IQueryable FilterDataSource2(object dataSource, NameValueCollection queryString)
        //{

        //    IQueryable iqueryable = (dataSource as IQueryable);
        //    Guard.IsNotNull(iqueryable, "DataSource", "should implement the IQueryable interface.");
        //    int pageIndex = this.GetPageIndex(queryString["page"]);
        //    int num = Convert.ToInt32(queryString["rows"]);
        //    string text = queryString["sidx"];
        //    string str = queryString["sord"];
        //    string arg_5F_0 = queryString["parentRowID"];
        //    string text2 = queryString["_search"];
        //    string text3 = queryString["filters"];
        //    string text4 = queryString["searchField"];
        //    string searchString = queryString["searchString"];
        //    string searchOper = queryString["searchOper"];
        //    this.PagerSettings.CurrentPage = pageIndex;
        //    this.PagerSettings.PageSize = num;
        //    if (!string.IsNullOrEmpty(text2) && text2 != "false")
        //    {
        //        try
        //        {
        //            if (string.IsNullOrEmpty(text3) && !string.IsNullOrEmpty(text4))
        //            {
        //                iqueryable = iqueryable.Where(Util.GetWhereClause(this, text4, searchString, searchOper), new object[0]);
        //            }
        //            else
        //            {
        //                if (!string.IsNullOrEmpty(text3))
        //                {
        //                    iqueryable = iqueryable.Where(Util.GetWhereClause(this, text3), new object[0]);
        //                }
        //                else
        //                {
        //                    if (this.ToolBarSettings.ShowSearchToolBar || text2 == "true")
        //                    {
        //                        iqueryable = iqueryable.Where(Util.GetWhereClause(this, queryString), new object[0]);
        //                    }
        //                }
        //            }
        //        }
        //        catch (DataTypeNotSetException ex)
        //        {
        //            throw ex;
        //        }
        //        catch (ParseException pex)
        //        {
        //            throw pex;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //    int num2 = iqueryable.Count();
        //    int totalPagesCount = (int)Math.Ceiling((double)((float)num2 / (float)num));
        //    if (string.IsNullOrEmpty(text) && this.SortSettings.AutoSortByPrimaryKey)
        //    {
        //        if (this.Columns.Count == 0)
        //        {
        //            throw new Exception("JQGrid must have at least one column defined.");
        //        }
        //        text = Util.GetPrimaryKeyField(this);
        //        str = "asc";
        //    }
        //    if (!string.IsNullOrEmpty(text))
        //    {
        //        string text5 = "";
        //        if (this.GroupSettings.GroupFields.Count > 0)
        //        {
        //            string str2 = text.Split(new char[]
        //            {
        //                ' '
        //            })[0];
        //            string str3 = text.Contains(" ") ? text.Split(new char[]
        //            {
        //                ' '
        //            })[1].Split(new char[]
        //            {
        //                ','
        //            })[0] : "asc";
        //            if (text.Contains(","))
        //            {
        //                text = text.Split(new char[]
        //                {
        //                    ','
        //                })[1];
        //            }
        //            text5 = str2 + " " + str3;
        //        }
        //        if (text != null && text == " ")
        //        {
        //            text = "";
        //        }
        //        if (!string.IsNullOrEmpty(text))
        //        {
        //            if (this.GroupSettings.GroupFields.Count > 0 && !text5.EndsWith(","))
        //            {
        //                text5 += ",";
        //            }
        //            text5 = text5 + text + " " + str;
        //        }
        //        iqueryable = iqueryable.OrderBy(text5, new object[0]);
        //    }
        //    return iqueryable;
        //}
		
        private JsonResult FilterDataSource(object dataSource, NameValueCollection queryString, out IQueryable iqueryable)
		{
            iqueryable = (dataSource as IQueryable);
            IQueryable ignoreFilterFieldData = iqueryable;
            Guard.IsNotNull(iqueryable, "DataSource", "should implement the IQueryable interface.");
            int pageIndex = this.GetPageIndex(queryString["page"]);
            int num = Convert.ToInt32(queryString["rows"]);
            string text = queryString["sidx"];
            string str = queryString["sord"];
            string arg_5F_0 = queryString["parentRowID"];
            bool isSearch = false;
            string search = queryString["_search"];
            if (!string.IsNullOrEmpty(search))
            {
                isSearch = search.ToLower() == "true";
            }
            //if(!string.IsNullOrEmpty(this.ClientSideEvents.SerializeGridData))
            //{
            //    text2 = "true";
            //}
            //if (!string.IsNullOrEmpty(this.ExcelExportSettings.Url))
            //{
            //    text2 = "true";
            //}
            string filters = queryString["filters"];
            string searchFiled = queryString["searchField"];
            string searchString = queryString["searchString"];
            string searchOper = queryString["searchOper"];
            this.PagerSettings.CurrentPage = pageIndex;
            this.PagerSettings.PageSize = num;
            if (isSearch)
            {
                try
                {
                    //search
                    if (!string.IsNullOrEmpty(searchFiled))
                    {
                        ignoreFilterFieldData = ignoreFilterFieldData.Where(Util.GetWhereClause(this, searchFiled, searchString, searchOper), new object[0]);
                        iqueryable = iqueryable.Where(Util.GetWhereClause(this, searchFiled, searchString, searchOper), new object[0]);
                    }
                    //else
                    //{
                    //filters
                    if (!string.IsNullOrEmpty(filters))
                    {
                        ignoreFilterFieldData = ignoreFilterFieldData.Where(Util.GetWhereClause(this, filters, this.IgnoreFilterField), new object[0]);
                        iqueryable = iqueryable.Where(Util.GetWhereClause(this, filters), new object[0]);
                    }
                    //else if (!string.IsNullOrEmpty(filters) && !string.IsNullOrEmpty(searchFiled))
                    //{
                    //    //filters+search
                    //    iqueryable = iqueryable.Where(Util.GetWhereClause(this, searchFiled, searchString, searchOper), new object[0]);
                    //    iqueryable = iqueryable.Where(Util.GetWhereClause(this, filters), new object[0]);
                    //}
                    //else
                    //{
                    //if (this.ToolBarSettings.ShowSearchToolBar || isSearch)
                    //{

                    string whereStr = Util.GetWhereClause(this, queryString);
                    if (!string.IsNullOrEmpty(whereStr))
                    {
                        ignoreFilterFieldData = ignoreFilterFieldData.Where(whereStr, new object[0]);
                        iqueryable = iqueryable.Where(whereStr, new object[0]);
                    }
                    //}
                    //}
                    //}
                }
                catch (DataTypeNotSetException ex)
                {
                    throw ex;
                }
                catch (ParseException)
                {
                    return new JsonResult
                    {
                        Data = new { Error = "表达式错误" },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                catch (Exception)
                {
                    return new JsonResult
                    {
                        Data = new object(),
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            }
            int num2 = iqueryable.Count();
            int totalPagesCount = (int)Math.Ceiling((double)((float)num2 / (float)num));
            if (string.IsNullOrEmpty(text) && this.SortSettings.AutoSortByPrimaryKey)
            {
                if (this.Columns.Count == 0)
                {
                    throw new Exception("JQGrid must have at least one column defined.");
                }
                text = Util.GetPrimaryKeyField(this);
                str = "asc";
            }
            if (!string.IsNullOrEmpty(text))
            {
                string text5 = "";
                if (this.GroupSettings.GroupFields.Count > 0)
                {
                    string str2 = text.Split(new char[]
					{
						' '
					})[0];
                    string str3 = text.Contains(" ") ? text.Split(new char[]
					{
						' '
					})[1].Split(new char[]
					{
						','
					})[0] : "asc";
                    if (text.Contains(","))
                    {
                        text = text.Split(new char[]
						{
							','
						})[1];
                    }
                    text5 = str2 + " " + str3;
                }
                if (text != null && text == " ")
                {
                    text = "";
                }
                if (!string.IsNullOrEmpty(text))
                {
                    if (this.GroupSettings.GroupFields.Count > 0 && !text5.EndsWith(","))
                    {
                        text5 += ",";
                    }
                    text5 = text5 + text + " " + str;
                }
                iqueryable = iqueryable.OrderBy(text5, new object[0]);
            }
            IQueryable filterData = iqueryable;
			iqueryable = iqueryable.Skip((pageIndex - 1) * num).Take(num);
			DataTable dataTable = iqueryable.ToDataTable(this);
            this.OnDataResolved(new JQGridDataResolvedEventArgs(this, iqueryable, this.DataSource as IQueryable, filterData, ignoreFilterFieldData));
			if (this.TreeGridSettings.Enabled)
			{
				JsonTreeResponse response = new JsonTreeResponse(pageIndex, totalPagesCount, num2, num, dataTable.Rows.Count, Util.GetFooterInfo(this));
				return Util.ConvertToTreeJson(response, this, dataTable);
			}
			JsonResponse response2 = new JsonResponse(pageIndex, totalPagesCount, num2, num, dataTable.Rows.Count, Util.GetFooterInfo(this));
			return Util.ConvertToJson(response2, this, dataTable);
		}
        
		private JsonResult GetJsonResponse()
		{
			Guard.IsNotNull(this.DataSource, "DataSource");
			IQueryable queryable;
			return this.FilterDataSource(this.DataSource, HttpContext.Current.Request.QueryString, out queryable);
		}
		public JQGridEditData GetEditData()
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			foreach (string text in HttpContext.Current.Request.Form.Keys)
			{
				if (text != "oper")
				{
					nameValueCollection[text] = HttpContext.Current.Request.Form[text];
				}
			}
			string text2 = string.Empty;
			foreach (JQGridColumn current in this.Columns)
			{
				if (current.PrimaryKey)
				{
					text2 = current.DataField;
					break;
				}
			}
			if (!string.IsNullOrEmpty(text2) && !string.IsNullOrEmpty(nameValueCollection["id"]))
			{
				nameValueCollection[text2] = nameValueCollection["id"];
			}
			JQGridEditData jQGridEditData = new JQGridEditData();
			jQGridEditData.RowData = nameValueCollection;
			jQGridEditData.RowKey = nameValueCollection["id"];
			string text3 = HttpContext.Current.Request.QueryString["parentRowID"];
			if (!string.IsNullOrEmpty(text3))
			{
				jQGridEditData.ParentRowKey = text3;
			}
			return jQGridEditData;
		}
		public JQGridTreeExpandData GetTreeExpandData()
		{
			JQGridTreeExpandData jQGridTreeExpandData = new JQGridTreeExpandData();
			if (HttpContext.Current.Request["nodeid"] != null)
			{
				jQGridTreeExpandData.ParentID = HttpContext.Current.Request["nodeid"];
			}
			if (HttpContext.Current.Request["n_level"] != null)
			{
				jQGridTreeExpandData.ParentLevel = Convert.ToInt32(HttpContext.Current.Request["n_level"]);
			}
			return jQGridTreeExpandData;
		}
		private int GetPageIndex(string value)
		{
			int result = 1;
			try
			{
				result = Convert.ToInt32(value);
			}
			catch (Exception)
			{
			}
			return result;
		}
		private DataGrid GetExportGrid()
		{
            DataGrid dataGrid = new DataGrid();
			dataGrid.AutoGenerateColumns = false;
			dataGrid.ID = this.ID + "_exportGrid";
            dataGrid.Caption = this.AppearanceSettings.Caption;
			foreach (JQGridColumn current in this.Columns)
			{
                if ((current.Visible || current.Exportable) && 
                    !current.EditActionIconsColumn)//&& 
                    //!(current.Formatter is DropDownFormatter))
				{
                    string headerText = string.IsNullOrEmpty(current.HeaderText) ? current.DataField : current.HeaderText;
                    
                    if (current.Formatter is CheckBoxFormatter)
                    {
                        CheckBoxColumn cbc = new CheckBoxColumn();
                        cbc.DataField = current.DataField;
                        cbc.HeaderText = headerText;
                        cbc.FooterText = current.FooterValue;
                        dataGrid.Columns.Add(cbc);
                    }
                    else if (current.Formatter is DropDownFormatter)
                    {
                        DropDownColumn ddc = new DropDownColumn();
                        ddc.DataField = current.DataField;
                        ddc.HeaderText = headerText;
                        ddc.FooterText = current.FooterValue;
                        ddc.EditList = current.EditList;
                        dataGrid.Columns.Add(ddc);
                    }
                    else
                    {
                        BoundColumn boundColumn = new BoundColumn();
                        boundColumn.DataField = current.DataField;
                        boundColumn.HeaderText = headerText;
                        boundColumn.DataFormatString = current.DataFormatString;
                        boundColumn.FooterText = current.FooterValue;
                        dataGrid.Columns.Add(boundColumn);
                    }
				}
			}
			return dataGrid;
		}
		private IQueryable GetFilteredDataSource(object dataSource, JQGridState gridState)
		{
			if (this.ExportSettings.ExportDataRange != ExportDataRange.FilteredAndPaged)
			{
				gridState.QueryString["page"] = "1";
				gridState.QueryString["rows"] = "1000000";
			}
			IQueryable result;
			this.FilterDataSource(dataSource, gridState.QueryString, out result);
			return result;
		}
		public JQGridState GetState()
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			foreach (string name in HttpContext.Current.Request.QueryString.Keys)
			{
				nameValueCollection.Add(name, HttpContext.Current.Request.QueryString[name]);
			}
			return new JQGridState
			{
				QueryString = nameValueCollection
			};
		}
		public void ExportToCSV(object dataSource, string fileName)
		{
			DataGrid exportGrid = this.GetExportGrid();
			exportGrid.DataSource = dataSource;
			exportGrid.DataBind();
			this.RenderCSVToStream(exportGrid, fileName);
		}
		public void ExportToCSV(object dataSource, string fileName, JQGridState gridState)
		{
			IQueryable filteredDataSource = this.GetFilteredDataSource(dataSource, gridState);
			this.ExportToCSV(filteredDataSource, fileName);
		}
		private void RenderCSVToStream(DataGrid grid, string fileName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.ExportSettings.ExportHeaders)
			{
				foreach (BoundColumn boundColumn in grid.Columns)
				{
					stringBuilder.AppendFormat("{0}{1}", boundColumn.HeaderText, this.ExportSettings.CSVSeparator);
				}
			}
			stringBuilder.Append("\n");
			foreach (DataGridItem dataGridItem in grid.Items)
			{
				for (int i = 0; i < this.Columns.Count; i++)
				{
					if (this.Columns[i].Visible)
					{
						stringBuilder.AppendFormat("{0}{1}", dataGridItem.Cells[i].Text, this.ExportSettings.CSVSeparator);
					}
				}
				stringBuilder.Append("\n");
			}
			HttpResponse response = HttpContext.Current.Response;
			response.ClearContent();
			response.AddHeader("content-disposition", "attachment; filename=" + fileName);
			response.ContentType = "application/excel";
			response.Clear();
			response.Write(stringBuilder.ToString());
			response.Flush();
			response.SuppressContent = true;
		}
		protected internal virtual void OnDataResolved(JQGridDataResolvedEventArgs e)
		{
			JQGridDataResolvedEventHandler jQGridDataResolvedEventHandler = (JQGridDataResolvedEventHandler)this.Events[JQGrid.EventDataResolved];
			if (jQGridDataResolvedEventHandler != null)
			{
				jQGridDataResolvedEventHandler(this, e);
			}
		}
		public void ExportToExcel(object dataSource, string fileName)
		{
            DataGrid exportGrid = this.GetExportGrid();
            //zhh
            exportGrid.ItemDataBound += new DataGridItemEventHandler(exportGrid_ItemDataBound);
            
            exportGrid.DataSource = ((IQueryable)dataSource).ToDataTable(this);
			exportGrid.DataBind();
			this.RenderExcelToStream(exportGrid, fileName);
		}

        void exportGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                for (int i = 0; i < dg.Columns.Count; i++)
                {
                    if (dg.Columns[i] is BoundColumn && e.Item.Cells[i].Text.StartsWith("0"))
                    {
                        e.Item.Cells[i].Attributes.Add("style", "mso-number-format:General;mso-number-format:'@';");
                    }
                }
            }
        }
		public void ExportToExcel(object dataSource, string fileName, JQGridState gridState)
		{
			IQueryable filteredDataSource = this.GetFilteredDataSource(dataSource, gridState);
			this.ExportToExcel(filteredDataSource, fileName);
		}
        private void RenderExcelToStream(DataGrid grid, string fileName)
		{
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
			grid.RenderControl(writer);
			HttpResponse response = HttpContext.Current.Response;
			response.ClearContent();
			//response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            response.AppendHeader("content-disposition", "attachment;filename=\"" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8) + "\"");
            response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            response.Charset = "utf-8";
            response.ContentType = "application/vnd.ms-excel";
            //response.Write("<meta http-equiv=Content-Type content=application/excel;charset=gb2312>"); 
			response.Clear();
            string style = "";// "<style　type=\"text/css\">td{mso-number-format:General;mso-number-format:'@';}</style>";
            response.Write("<html><head><meta http-equiv=Content-Type content=\"text/html; charset=utf-8\">" + style + "</head><body>");
			response.Write(stringWriter.ToString());
            response.Write("</body></html>");
			response.Flush();
			response.SuppressContent = true;
		}
		public DataTable GetExportData(object dataSource)
		{
			DataGrid exportGrid = this.GetExportGrid();
			exportGrid.DataSource = dataSource;
			exportGrid.DataBind();
			return this.ConvertDataGridToDataTable(exportGrid);
		}
		public DataTable GetExportData(object dataSource, JQGridState gridState)
		{
			IQueryable filteredDataSource = this.GetFilteredDataSource(dataSource, gridState);
			return this.GetExportData(filteredDataSource);
		}
		private DataTable ConvertDataGridToDataTable(DataGrid grid)
		{
			DataTable dataTable = new DataTable();
			foreach (DataGridColumn dataGridColumn in grid.Columns)
			{
				dataTable.Columns.Add(dataGridColumn.HeaderText);
			}
			foreach (DataGridItem dataGridItem in grid.Items)
			{
				DataRow dataRow = dataTable.NewRow();
				for (int i = 0; i < grid.Columns.Count; i++)
				{
					dataRow[i] = dataGridItem.Cells[i].Text;
				}
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}


        public List<JQGridColumn> GetColumns(Type t)
        {
            List<JQGridColumn> ljc = new List<JQGridColumn>();
            PropertyInfo[] members = t.GetProperties();
            foreach (PropertyInfo member in members)
            {
                JQGridColumn col = new JQGridColumn();
                col.DataField = member.Name;
                col.DataType = member.PropertyType;
                col.Editable = true;
                if (col.DataType == typeof(decimal) || col.DataType == typeof(int))
                {
                    col.EditClientSideValidators.Add(new NumberValidator());
                    col.Formatter = new CustomFormatter() { FormatFunction = "FormatNumber", UnFormatFunction = "UnFormatNumber" };
                }
                ShowAttribute(member, col);
                ljc.Add(col);
            }
            return ljc.OrderBy(o => o.Order).ToList();
        }
        private void ShowAttribute(PropertyInfo attributeTarget, JQGridColumn col)
        {
            object[] attributes = attributeTarget.GetCustomAttributes(false);
            foreach (object attribute in attributes)
            {
                if (attribute is System.ComponentModel.DataAnnotations.KeyAttribute)
                {
                    col.PrimaryKey = true;
                }
                if (attribute is System.ComponentModel.DataAnnotations.Schema.ColumnAttribute)
                {
                    System.ComponentModel.DataAnnotations.Schema.ColumnAttribute ca = attribute as System.ComponentModel.DataAnnotations.Schema.ColumnAttribute;
                    col.Order = ca.Order;
                }
                if (attribute is System.ComponentModel.DisplayNameAttribute)
                {
                    System.ComponentModel.DisplayNameAttribute dn = attribute as System.ComponentModel.DisplayNameAttribute;
                    col.HeaderText = dn.DisplayName;
                }
                if (attribute is System.ComponentModel.DataAnnotations.DisplayFormatAttribute)
                {
                    System.ComponentModel.DataAnnotations.DisplayFormatAttribute dfa = attribute as System.ComponentModel.DataAnnotations.DisplayFormatAttribute;
                    col.DataFormatString = dfa.DataFormatString;
                }
                if (attribute is System.ComponentModel.DataAnnotations.EditableAttribute)
                {
                    System.ComponentModel.DataAnnotations.EditableAttribute ea = attribute as System.ComponentModel.DataAnnotations.EditableAttribute;
                    col.Editable = ea.AllowEdit;
                }
                if (attribute is System.ComponentModel.DataAnnotations.RequiredAttribute)
                {
                    col.EditDialogFieldSuffix = "(*)";
                    col.EditClientSideValidators.Add(new RequiredValidator());
                }
                if (attribute is SearchableAttribute)
                {
                    SearchableAttribute sa = attribute as SearchableAttribute;
                    col.Searchable = sa.Searchable;
                }
                if (attribute is SearchRequiredAttribute)
                {
                    SearchRequiredAttribute sa = attribute as SearchRequiredAttribute;
                    if (sa.SearchRequired)
                    {
                        col.EditClientSideValidators.Add(new SearchRequiredValidator());
                    }

                }
                if (attribute is VisiableAttribute)
                {
                    VisiableAttribute va = attribute as VisiableAttribute;
                    col.Visible = va.Visiable;
                }
                if (attribute is EditTypeAttribute)
                {
                    EditTypeAttribute ea = attribute as EditTypeAttribute;
                    col.EditType = ea.EditType;
                    switch (col.EditType)
                    {
                        case EditType.DatePicker:
                            col.EditorControlID = "DatePicker";
                            break;
                        case EditType.DateTimePicker:
                            col.EditorControlID = "DateTimePicker";
                            break;
                        case EditType.TimePicker:
                            col.EditorControlID = "TimePicker";
                            break;
                        case EditType.AutoComplete:
                            col.EditorControlID = "AutoComplete_"+col.DataField;
                            break;
                    
                    }
                }
                if (attribute is SearchTypeAttribute)
                {
                    SearchTypeAttribute sa = attribute as SearchTypeAttribute;
                    if (sa.SearchType == SearchType.CheckBox)
                    {
                        col.SearchType = SearchType.DropDown;
                        List<SelectListItem> lsli = new List<SelectListItem>();
                        lsli.Add(new SelectListItem() { Text = "所有", Value = "" });
                        lsli.Add(new SelectListItem() { Text = "是", Value = "true" });
                        lsli.Add(new SelectListItem() { Text = "否", Value = "false" });
                        col.SearchList = lsli;
                    }
                    else
                    {
                        col.SearchType = sa.SearchType;
                    }
                    switch (col.SearchType)
                    {
                        case SearchType.DatePicker:
                            col.SearchControlID = "DatePicker";
                            break;
                        case SearchType.DateTimePicker:
                            col.SearchControlID = "DateTimePicker";
                            break;
                        case SearchType.TimePicker:
                            col.SearchControlID = "TimePicker";
                            break;
                        case SearchType.AutoComplete:
                            col.SearchControlID = "AutoComplete_" + col.DataField;
                            break;
                    }
                }
                if (attribute is FormatterAttribute)
                {
                    FormatterAttribute fa = attribute as FormatterAttribute;
                    switch (fa.FormatterType)
                    {
                        case FormatterType.CheckBox:
                            col.Formatter = new CheckBoxFormatter();
                            break;
                    }
                }
            }
        }
	}
    public class VouchGrid : JQGrid
    {
        public VouchGrid(string caption)
            : base()
        {
            this.ToolBarSettings.ToolBarPosition = ToolBarPosition.TopAndBottom;
            this.AppearanceSettings.AlternateRowBackground = true;
            this.AppearanceSettings.Caption = caption;
            this.AppearanceSettings.HighlightRowsOnHover = true;
            this.AppearanceSettings.ShowRowNumbers = true;
            this.ToolBarSettings.ShowSearchButton = true;
            this.ToolBarSettings.ShowAddButton = true;
            this.ToolBarSettings.ShowEditButton = true;
            this.ToolBarSettings.ShowDeleteButton = true;
            this.ToolBarSettings.ShowRefreshButton = true;
            this.ToolBarSettings.ShowExcelButton = true;
            this.ToolBarSettings.ShowColumnChooser = true;
            this.SearchDialogSettings.MultipleSearch = true;
            //this.EditDialogSettings.Width = 600;
            //this.AddDialogSettings.Width = 600;
            //this.SearchDialogSettings.Width = 600;
            this.SearchDialogSettings.Resizable = true;
            this.AutoWidth = true;
            this.Height = Unit.Percentage(100);
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchableAttribute : Attribute
    {
        public bool Searchable { get; set; }
        public SearchableAttribute(bool searchable)
        {
            this.Searchable = searchable;
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchRequiredAttribute : Attribute
    {
        public bool SearchRequired { get; set; }
        public SearchRequiredAttribute(bool searchRequired)
        {
            this.SearchRequired = searchRequired;
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class VisiableAttribute : Attribute
    {
        public bool Visiable { get; set; }
        public VisiableAttribute(bool visiable)
        {
            this.Visiable = visiable;
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class EditTypeAttribute : Attribute
    {
        public EditType EditType { get; set; }
        public EditTypeAttribute(EditType editType)
        {
            this.EditType = editType;
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchTypeAttribute : Attribute
    {
        public SearchType SearchType { get; set; }
        public SearchTypeAttribute(SearchType searchType)
        {
            this.SearchType = searchType;
        }
    }
    public enum FormatterType
    {
        CheckBox
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class FormatterAttribute : Attribute
    {
        public FormatterType FormatterType { get; set; }
        public FormatterAttribute(FormatterType formatterType)
        {
            this.FormatterType = formatterType;
        }
    }
}
