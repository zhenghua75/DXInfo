namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Collections;
    using System.Reflection;





    using System.Collections;
    using System.Globalization;

    public class JQGrid
    {
        private EventHandlerList _events;
        [CompilerGenerated]
        private Trirand.Web.Mvc.AddDialogSettings _AddDialogSettings_k__BackingField;
        [CompilerGenerated]
        private Trirand.Web.Mvc.AppearanceSettings _AppearanceSettings_k__BackingField;
        [CompilerGenerated]
        private bool _AutoWidth_k__BackingField;
        [CompilerGenerated]
        private Trirand.Web.Mvc.ClientSideEvents _ClientSideEvents_k__BackingField;
        [CompilerGenerated]
        private bool _ColumnReordering_k__BackingField;
        [CompilerGenerated]
        private List<JQGridColumn> Columns_k__BackingField;
        [CompilerGenerated]
        private IQueryable _DataSource_k__BackingField;
        [CompilerGenerated]
        private string _DataUrl_k__BackingField;
        [CompilerGenerated]
        private Trirand.Web.Mvc.DeleteDialogSettings _DeleteDialogSettings_k__BackingField;
        [CompilerGenerated]
        private Trirand.Web.Mvc.EditDialogSettings _EditDialogSettings_k__BackingField;
        [CompilerGenerated]
        private string _EditUrl_k__BackingField;
        [CompilerGenerated]
        private Trirand.Web.Mvc.GroupSettings _GroupSettings_k__BackingField;
        [CompilerGenerated]
        private Unit _Height_k__BackingField;
        [CompilerGenerated]
        private Trirand.Web.Mvc.HierarchySettings _HierarchySettings_k__BackingField;
        [CompilerGenerated]
        private string _ID_k__BackingField;
        [CompilerGenerated]
        private bool _MultiSelect_k__BackingField;
        [CompilerGenerated]
        private Trirand.Web.Mvc.MultiSelectKey _MultiSelectKey_k__BackingField;
        [CompilerGenerated]
        private Trirand.Web.Mvc.MultiSelectMode _MultiSelectMode_k__BackingField;
        [CompilerGenerated]
        private Trirand.Web.Mvc.PagerSettings _PagerSettings_k__BackingField;
        [CompilerGenerated]
        private Trirand.Web.Mvc.RenderingMode _RenderingMode_k__BackingField;
        [CompilerGenerated]
        private Trirand.Web.Mvc.SearchDialogSettings _SearchDialogSettings_k__BackingField;
        [CompilerGenerated]
        private bool _ShrinkToFit_k__BackingField;
        [CompilerGenerated]
        private Trirand.Web.Mvc.SortSettings _SortSettings_k__BackingField;
        [CompilerGenerated]
        private Trirand.Web.Mvc.ToolBarSettings _ToolBarSettings_k__BackingField;
        [CompilerGenerated]
        private Unit _Width_k__BackingField;
        private static readonly object EventDataResolved = new object();

        public event JQGridDataResolvedEventHandler DataResolved
        {
            add
            {
                this.Events.AddHandler(EventDataResolved, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventDataResolved, value);
            }
        }

        public bool TreeGrid { get; set; }
        public TreeGridModel TreeGridModel { get; set; }
        public string ExpandColumn { get; set; }
        public string ParentIdField { get; set; }
        public JQGrid()
        {
            this.AutoWidth = false;
            this.ShrinkToFit = true;
            this.EditDialogSettings = new Trirand.Web.Mvc.EditDialogSettings();
            this.AddDialogSettings = new Trirand.Web.Mvc.AddDialogSettings();
            this.DeleteDialogSettings = new Trirand.Web.Mvc.DeleteDialogSettings();
            this.SearchDialogSettings = new Trirand.Web.Mvc.SearchDialogSettings();
            this.PagerSettings = new Trirand.Web.Mvc.PagerSettings();
            this.ToolBarSettings = new Trirand.Web.Mvc.ToolBarSettings();
            this.SortSettings = new Trirand.Web.Mvc.SortSettings();
            this.AppearanceSettings = new Trirand.Web.Mvc.AppearanceSettings();
            this.HierarchySettings = new Trirand.Web.Mvc.HierarchySettings();
            this.GroupSettings = new Trirand.Web.Mvc.GroupSettings();
            this.ClientSideEvents = new Trirand.Web.Mvc.ClientSideEvents();
            this.Columns = new List<JQGridColumn>();
            this.DataUrl = "";
            this.EditUrl = "";
            this.ColumnReordering = false;
            this.RenderingMode = Trirand.Web.Mvc.RenderingMode.Default;
            this.MultiSelect = false;
            this.MultiSelectMode = Trirand.Web.Mvc.MultiSelectMode.SelectOnRowClick;
            this.MultiSelectKey = Trirand.Web.Mvc.MultiSelectKey.None;
            this.Width = Unit.Empty;
            this.Height = Unit.Empty;

            this.TreeGrid = false;
            this.TreeGridModel = Trirand.Web.Mvc.TreeGridModel.adjacency;
            this.ParentIdField = "";
        }

        public JsonResult DataBind()
        {
            if (this.AjaxCallBackMode == Trirand.Web.Mvc.AjaxCallBackMode.RequestData)
            {
            }
            return this.GetJsonResponse();
        }

        public JsonResult DataBind(IQueryable dataSource)
        {
            this.DataSource = dataSource;
            return this.DataBind();
        }
        private void DoExportToExcel(DataTable dataSource, string fileName)
        {
            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            //IQueryable qe = dataSource as IQueryable;
            //DataTable dt = dataSource as DataTable;
            //DataTable dt = qe.ToDataTable(this);
            //DataView dv = qe as DataView;
            foreach (JQGridColumn column in this.Columns)
            {
                if (!dataSource.Columns.Contains(column.DataField)) continue;
                BoundField field = new BoundField();
                field.DataField = column.DataField;
                string str = string.IsNullOrEmpty(column.HeaderText) ? column.DataField : column.HeaderText;
                field.HeaderText = str;
                field.DataFormatString = column.DataFormatString;
                field.FooterText = column.FooterValue;
                view.Columns.Add(field);
            }
            view.DataSource = dataSource;
            view.DataBind();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.Charset = "GB2312"; 
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.ContentType = "application/excel";
            StringWriter writer = new StringWriter();
            HtmlTextWriter writer2 = new HtmlTextWriter(writer);
            view.RenderControl(writer2);
            HttpContext.Current.Response.Write(writer.ToString());
            HttpContext.Current.Response.End();
        }
        private void DoExportToExcel(object dataSource, string fileName)
        {
            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            foreach (JQGridColumn column in this.Columns)
            {
                if (column.DataField == "Id") continue;
                if (column.DataField == "BalanceType") continue;
                if (column.DataField == "Lines") continue;
                BoundField field = new BoundField();
                field.DataField = column.DataField;
                string str = string.IsNullOrEmpty(column.HeaderText) ? column.DataField : column.HeaderText;
                field.HeaderText = str;
                field.DataFormatString = column.DataFormatString;
                field.FooterText = column.FooterValue;
                view.Columns.Add(field);
            }
            IQueryable<object> d = dataSource as IQueryable<object>;
            //var list = d.Select(s=>s).ToList();
            var l = (from d1 in d select d1).ToList();
            view.DataSource = l;//d.ToList();
            view.DataBind();
            HttpContext.Current.Response.ClearContent();
            //HttpContext.Current.Response.Charset = "GB2312";

            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8).ToString());
            HttpContext.Current.Response.ContentType = "application/excel";
            StringWriter writer = new StringWriter();
            HtmlTextWriter writer2 = new HtmlTextWriter(writer);
            view.RenderControl(writer2);
            //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
            HttpContext.Current.Response.Write(writer.ToString());
            HttpContext.Current.Response.End();
        }

        private void DoExportToExcelWithState(IQueryable dataSource, string fileName, JQGridState gridState)
        {
            IQueryable queryable;
            if (!gridState.CurrentPageOnly)
            {
                //gridState.QueryString["page"] = "0";
                //gridState.QueryString["rows"] = "1000000";
                this.ExcelFilterDataSource(dataSource, gridState.QueryString, out queryable);                
            }
            else
                this.FilterDataSource(dataSource, gridState.QueryString, out queryable);
            this.ExportToExcel(queryable, fileName);
        }
        private JsonResult ExcelFilterDataSource(object dataSource, NameValueCollection queryString, out IQueryable iqueryable)
        {
            iqueryable = dataSource as IQueryable;
            Guard.IsNotNull(iqueryable, "DataSource", "should implement the IQueryable interface.");
            int currentPage = 0;
            int count = 1000000;
            string primaryKeyField = "";
            string str2 = "";
            string text1 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            string searchString = "";
            string searchOper = "";


            currentPage = Convert.ToInt32(queryString["page"]);
            count = Convert.ToInt32(queryString["rows"]);
            primaryKeyField = queryString["sidx"];
            str2 = queryString["sord"];
            text1 = queryString["parentRowID"];
            str3 = queryString["_search"];
            str4 = queryString["filters"];
            str5 = queryString["searchField"];
            searchString = queryString["searchString"];
            searchOper = queryString["searchOper"];
            this.PagerSettings.CurrentPage = currentPage;
            this.PagerSettings.PageSize = count;
            if (!string.IsNullOrEmpty(str3) && (str3 != "false"))//ËÑË÷
            {
                try
                {
                    if (string.IsNullOrEmpty(str4) && !string.IsNullOrEmpty(str5))
                    {
                        iqueryable = iqueryable.Where(Trirand.Web.Mvc.Util.GetWhereClause(this, str5, searchString, searchOper), new object[0]);
                    }
                    else if (!string.IsNullOrEmpty(str4))
                    {
                        iqueryable = iqueryable.Where(Trirand.Web.Mvc.Util.GetWhereClause(this, str4), new object[0]);
                    }
                    else if (this.ToolBarSettings.ShowSearchToolBar || (str3 == "true"))
                    {
                        iqueryable = iqueryable.Where(Trirand.Web.Mvc.Util.GetWhereClause(this, queryString), new object[0]);
                    }
                }
                catch (DataTypeNotSetException exception)
                {
                    throw exception;
                }
                catch (Exception)
                {
                    JsonResult result = new JsonResult();
                    result.Data = new object();
                    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    return result;
                }
            }
            int totalRowCount = 0;
            int totalPagesCount = 0;
            totalRowCount = iqueryable.Count();
            totalPagesCount = (int)Math.Ceiling((double)(((float)totalRowCount) / ((float)count)));
            if (string.IsNullOrEmpty(primaryKeyField))
            {
                if (this.Columns.Count == 0)
                {
                    throw new Exception("JQGrid must have at least one column defined.");
                }
                primaryKeyField = Trirand.Web.Mvc.Util.GetPrimaryKeyField(this);
                str2 = "asc";
            }
            if (!string.IsNullOrEmpty(primaryKeyField))
            {
                string ordering = "";
                if (this.GroupSettings.GroupFields.Count > 0)
                {
                    string str9 = primaryKeyField.Split(new char[] { ' ' })[0];
                    string str10 = primaryKeyField.Split(new char[] { ' ' })[1].Split(new char[] { ',' })[0];
                    primaryKeyField = primaryKeyField.Split(new char[] { ',' })[1];
                    ordering = str9 + " " + str10;
                }
                if ((primaryKeyField != null) && (primaryKeyField == " "))
                {
                    primaryKeyField = "";
                }
                if (!string.IsNullOrEmpty(primaryKeyField))
                {
                    if ((this.GroupSettings.GroupFields.Count > 0) && !ordering.EndsWith(","))
                    {
                        ordering = ordering + ",";
                    }
                    ordering = ordering + primaryKeyField + " " + str2;
                }
                iqueryable = iqueryable.OrderBy(ordering, new object[0]);
            }
            //iqueryable = iqueryable.Skip(((currentPage - 1) * count)).Take(count);
            DataTable dt = iqueryable.ToDataTable(this);
            this.OnDataResolved(new JQGridDataResolvedEventArgs(this, iqueryable, this.DataSource as IQueryable));
            JsonResponse response = new JsonResponse(currentPage, totalPagesCount, totalRowCount, count, dt.Rows.Count, Trirand.Web.Mvc.Util.GetFooterInfo(this));
            return Trirand.Web.Mvc.Util.ConvertToJson(response, this, dt);
        }

        public void ExportToExcel(object dataSource)
        {
            this.DoExportToExcel(dataSource, "GridExcelExport.xls");
        }

        public void ExportToExcel(object dataSource, string fileName)
        {
            this.DoExportToExcel(dataSource, fileName);
        }
        public void ExportToExcel(DataTable dataSource)
        {
            this.DoExportToExcel(dataSource, "GridExcelExport.xls");
        }

        public void ExportToExcel(DataTable dataSource, string fileName)
        {
            this.DoExportToExcel(dataSource, fileName);
        }
        public void ExportToExcel(IQueryable dataSource, JQGridState gridState)
        {
            this.DoExportToExcelWithState(dataSource, "GridExcelExport.xls", gridState);
        }

        public void ExportToExcel(IQueryable dataSource, string fileName, JQGridState gridState)
        {
            this.DoExportToExcelWithState(dataSource, fileName, gridState);
        }

        private JsonResult FilterDataSource(IQueryable dataSource, HttpRequest request, out IQueryable iqueryable)
        {
            if (request.HttpMethod == "POST")
                return FilterDataSource(dataSource, request.Form, out iqueryable);

            return FilterDataSource(dataSource, request.QueryString, out iqueryable);
        }
        private JsonResult FilterDataSource(IQueryable dataSource, NameValueCollection queryString, out IQueryable iqueryable)
        {
            iqueryable = dataSource;// as IQueryable;
            Guard.IsNotNull(iqueryable, "DataSource", "should implement the IQueryable interface.");
            int currentPage = 0;
            int count = 0;
            string primaryKeyField = "";
            string str2 = "";
            string text1 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            string searchString = "";
            string searchOper = "";


            currentPage = Convert.ToInt32(queryString["page"]);
            count = Convert.ToInt32(queryString["rows"]);
            primaryKeyField = queryString["sidx"];
            str2 = queryString["sord"];
            text1 = queryString["parentRowID"];
            str3 = queryString["_search"];
            str4 = queryString["filters"];
            str5 = queryString["searchField"];
            searchString = queryString["searchString"];
            searchOper = queryString["searchOper"];
            this.PagerSettings.CurrentPage = currentPage;
            this.PagerSettings.PageSize = count;
            if (!string.IsNullOrEmpty(str3) && (str3 != "false"))//ËÑË÷
            {
                try
                {
                    if (string.IsNullOrEmpty(str4) && !string.IsNullOrEmpty(str5))
                    {
                        iqueryable = iqueryable.Where(Trirand.Web.Mvc.Util.GetWhereClause(this, str5, searchString, searchOper), new object[0]);
                    }
                    else if (!string.IsNullOrEmpty(str4))
                    {
                        iqueryable = iqueryable.Where(Trirand.Web.Mvc.Util.GetWhereClause(this, str4), new object[0]);
                    }
                    else if (this.ToolBarSettings.ShowSearchToolBar || (str3 == "true"))
                    {
                        iqueryable = iqueryable.Where(Trirand.Web.Mvc.Util.GetWhereClause(this, queryString), new object[0]);
                    }
                }
                catch (DataTypeNotSetException exception)
                {
                    throw exception;
                }
                catch (Exception)
                {
                    JsonResult result = new JsonResult();
                    result.Data = new object();
                    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    return result;
                }
            }
            int totalRowCount = 0;
            int totalPagesCount = 0;
            totalRowCount = iqueryable.Count();
            totalPagesCount = (int)Math.Ceiling((double)(((float)totalRowCount) / ((float)count)));
            if (string.IsNullOrEmpty(primaryKeyField))
            {
                if (this.Columns.Count == 0)
                {
                    throw new Exception("JQGrid must have at least one column defined.");
                }
                primaryKeyField = Trirand.Web.Mvc.Util.GetPrimaryKeyField(this);
                str2 = "asc";
            }
            if (!string.IsNullOrEmpty(primaryKeyField))
            {
                string ordering = "";
                if (this.GroupSettings.GroupFields.Count > 0)
                {
                    string str9 = primaryKeyField.Split(new char[] { ' ' })[0];
                    string str10 = primaryKeyField.Split(new char[] { ' ' })[1].Split(new char[] { ',' })[0];
                    primaryKeyField = primaryKeyField.Split(new char[] { ',' })[1];
                    ordering = str9 + " " + str10;
                }
                if ((primaryKeyField != null) && (primaryKeyField == " "))
                {
                    primaryKeyField = "";
                }
                if (!string.IsNullOrEmpty(primaryKeyField))
                {
                    if ((this.GroupSettings.GroupFields.Count > 0) && !ordering.EndsWith(","))
                    {
                        ordering = ordering + ",";
                    }
                    ordering = ordering + primaryKeyField + " " + str2;
                }
                iqueryable = iqueryable.OrderBy(ordering, new object[0]);
            }
            IQueryable allq = iqueryable;
            iqueryable = iqueryable.Skip(((currentPage - 1) * count)).Take(count);
            DataTable dt = iqueryable.ToDataTable(this);
            this.OnDataResolved(new JQGridDataResolvedEventArgs(this, iqueryable, allq));
            JsonResponse response = new JsonResponse(currentPage, totalPagesCount, totalRowCount, count, dt.Rows.Count, Trirand.Web.Mvc.Util.GetFooterInfo(this));
            return Trirand.Web.Mvc.Util.ConvertToJson(response, this, dt);
        }
        
        public JQGridEditData GetEditData()
        {
            NameValueCollection values = new NameValueCollection();
            foreach (string str in HttpContext.Current.Request.Form.Keys)
            {
                if (str != "oper")
                {
                    values[str] = HttpContext.Current.Request.Form[str];
                }
            }
            string dataField = string.Empty;
            foreach (JQGridColumn column in this.Columns)
            {
                if (column.PrimaryKey)
                {
                    dataField = column.DataField;
                    break;
                }
            }
            if (!string.IsNullOrEmpty(dataField) && !string.IsNullOrEmpty(values["id"]))
            {
                values[dataField] = values["id"];
            }
            JQGridEditData data = new JQGridEditData();
            data.RowData = values;
            data.RowKey = values["id"];
            string str3 = HttpContext.Current.Request.QueryString["parentRowID"];
            if (!string.IsNullOrEmpty(str3))
            {
                data.ParentRowKey = str3;
            }
            return data;
        }

        private JsonResult GetJsonResponse()
        {
            IQueryable queryable;
            Guard.IsNotNull(this.DataSource, "DataSource");
            //HttpContext.Current.Request.Form
            return this.FilterDataSource(this.DataSource, HttpContext.Current.Request, out queryable);
        }

        public JQGridState GetState(bool currentPageOnly)
        {
            JQGridState state = new JQGridState();
            state.QueryString = HttpContext.Current.Request.QueryString;
            state.CurrentPageOnly = currentPageOnly;
            return state;
        }

        protected internal virtual void OnDataResolved(JQGridDataResolvedEventArgs e)
        {
            JQGridDataResolvedEventHandler handler = (JQGridDataResolvedEventHandler) this.Events[EventDataResolved];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public ActionResult ShowEditValidationMessage(string errorMessage)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.StatusCode = 500;
            HttpContext.Current.Response.StatusDescription = errorMessage;
            ContentResult result = new ContentResult();
            result.Content = errorMessage;
            return result;
        }

        public Trirand.Web.Mvc.AddDialogSettings AddDialogSettings
        {
            [CompilerGenerated]
            get
            {
                return this._AddDialogSettings_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._AddDialogSettings_k__BackingField = value;
            }
        }

        public Trirand.Web.Mvc.AjaxCallBackMode AjaxCallBackMode
        {
            get
            {
                string str4;
                string str = HttpContext.Current.Request.Form["oper"];
                string str2 = HttpContext.Current.Request.QueryString["editMode"];
                string str3 = HttpContext.Current.Request.QueryString["_search"];
                Trirand.Web.Mvc.AjaxCallBackMode requestData = Trirand.Web.Mvc.AjaxCallBackMode.RequestData;
                if (!string.IsNullOrEmpty(str) && ((str4 = str) != null))
                {
                    if (str4 == "add")
                    {
                        return Trirand.Web.Mvc.AjaxCallBackMode.AddRow;
                    }
                    if (str4 == "edit")
                    {
                        return Trirand.Web.Mvc.AjaxCallBackMode.EditRow;
                    }
                    if (str4 == "del")
                    {
                        return Trirand.Web.Mvc.AjaxCallBackMode.DeleteRow;
                    }
                }
                if (!string.IsNullOrEmpty(str2))
                {
                    requestData = Trirand.Web.Mvc.AjaxCallBackMode.EditRow;
                }
                if (!string.IsNullOrEmpty(str3) && Convert.ToBoolean(str3))
                {
                    requestData = Trirand.Web.Mvc.AjaxCallBackMode.Search;
                }
                return requestData;
            }
        }

        public Trirand.Web.Mvc.AppearanceSettings AppearanceSettings
        {
            [CompilerGenerated]
            get
            {
                return this._AppearanceSettings_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._AppearanceSettings_k__BackingField = value;
            }
        }

        public bool AutoWidth
        {
            [CompilerGenerated]
            get
            {
                return this._AutoWidth_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._AutoWidth_k__BackingField = value;
            }
        }

        public Trirand.Web.Mvc.ClientSideEvents ClientSideEvents
        {
            [CompilerGenerated]
            get
            {
                return this._ClientSideEvents_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ClientSideEvents_k__BackingField = value;
            }
        }

        public bool ColumnReordering
        {
            [CompilerGenerated]
            get
            {
                return this._ColumnReordering_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ColumnReordering_k__BackingField = value;
            }
        }

        public List<JQGridColumn> Columns
        {
            [CompilerGenerated]
            get
            {
                return this.Columns_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.Columns_k__BackingField = value;
            }
        }

        public IQueryable DataSource
        {
            [CompilerGenerated]
            get
            {
                return this._DataSource_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._DataSource_k__BackingField = value;
            }
        }

        public string DataUrl
        {
            [CompilerGenerated]
            get
            {
                return this._DataUrl_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._DataUrl_k__BackingField = value;
            }
        }

        public Trirand.Web.Mvc.DeleteDialogSettings DeleteDialogSettings
        {
            [CompilerGenerated]
            get
            {
                return this._DeleteDialogSettings_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._DeleteDialogSettings_k__BackingField = value;
            }
        }

        public Trirand.Web.Mvc.EditDialogSettings EditDialogSettings
        {
            [CompilerGenerated]
            get
            {
                return this._EditDialogSettings_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._EditDialogSettings_k__BackingField = value;
            }
        }

        public string EditUrl
        {
            [CompilerGenerated]
            get
            {
                return this._EditUrl_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._EditUrl_k__BackingField = value;
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

        public Trirand.Web.Mvc.GroupSettings GroupSettings
        {
            [CompilerGenerated]
            get
            {
                return this._GroupSettings_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._GroupSettings_k__BackingField = value;
            }
        }

        public Unit Height
        {
            [CompilerGenerated]
            get
            {
                return this._Height_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Height_k__BackingField = value;
            }
        }

        public Trirand.Web.Mvc.HierarchySettings HierarchySettings
        {
            [CompilerGenerated]
            get
            {
                return this._HierarchySettings_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._HierarchySettings_k__BackingField = value;
            }
        }

        public string ID
        {
            [CompilerGenerated]
            get
            {
                return this._ID_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ID_k__BackingField = value;
            }
        }

        public bool MultiSelect
        {
            [CompilerGenerated]
            get
            {
                return this._MultiSelect_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._MultiSelect_k__BackingField = value;
            }
        }

        public Trirand.Web.Mvc.MultiSelectKey MultiSelectKey
        {
            [CompilerGenerated]
            get
            {
                return this._MultiSelectKey_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._MultiSelectKey_k__BackingField = value;
            }
        }

        public Trirand.Web.Mvc.MultiSelectMode MultiSelectMode
        {
            [CompilerGenerated]
            get
            {
                return this._MultiSelectMode_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._MultiSelectMode_k__BackingField = value;
            }
        }

        public Trirand.Web.Mvc.PagerSettings PagerSettings
        {
            [CompilerGenerated]
            get
            {
                return this._PagerSettings_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._PagerSettings_k__BackingField = value;
            }
        }

        public Trirand.Web.Mvc.RenderingMode RenderingMode
        {
            [CompilerGenerated]
            get
            {
                return this._RenderingMode_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._RenderingMode_k__BackingField = value;
            }
        }

        public Trirand.Web.Mvc.SearchDialogSettings SearchDialogSettings
        {
            [CompilerGenerated]
            get
            {
                return this._SearchDialogSettings_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._SearchDialogSettings_k__BackingField = value;
            }
        }

        internal bool ShowToolBar
        {
            get
            {
                if (((!this.ToolBarSettings.ShowAddButton && !this.ToolBarSettings.ShowDeleteButton) && (!this.ToolBarSettings.ShowEditButton && !this.ToolBarSettings.ShowRefreshButton)) && !this.ToolBarSettings.ShowSearchButton)
                {
                    return this.ToolBarSettings.ShowViewRowDetailsButton;
                }
                return true;
            }
        }

        public bool ShrinkToFit
        {
            [CompilerGenerated]
            get
            {
                return this._ShrinkToFit_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ShrinkToFit_k__BackingField = value;
            }
        }

        public Trirand.Web.Mvc.SortSettings SortSettings
        {
            [CompilerGenerated]
            get
            {
                return this._SortSettings_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._SortSettings_k__BackingField = value;
            }
        }

        public Trirand.Web.Mvc.ToolBarSettings ToolBarSettings
        {
            [CompilerGenerated]
            get
            {
                return this._ToolBarSettings_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ToolBarSettings_k__BackingField = value;
            }
        }

        public Unit Width
        {
            [CompilerGenerated]
            get
            {
                return this._Width_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Width_k__BackingField = value;
            }
        }
    }
}

