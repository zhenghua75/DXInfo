using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class StockManageJQGrid:JQGrid
    {
        private List<JQGridColumn> allColumn;
        public StockManageJQGrid(string VouchType)
        {
            this.ToolBarSettings.ShowAddButton = true;
            this.ToolBarSettings.ShowEditButton = true;
            this.ToolBarSettings.ShowDeleteButton = true;
            this.ToolBarSettings.ShowRefreshButton = true;
            this.AppearanceSettings.ShowRowNumbers = true;
            this.EditDialogSettings.Width = 450;
            this.AddDialogSettings.Width = 450;
            this.DataType = "local";

            #region 客户端事件
            this.ClientSideEvents.BeforeAddDialogInitData = "beforeInitData";
            this.ClientSideEvents.BeforeEditDialogInitData = "beforeInitData";
            this.ClientSideEvents.BeforeAddDialogSubmit = "beforeSubmit";
            this.ClientSideEvents.BeforeEditDialogSubmit = "beforeSubmit";
            this.ClientSideEvents.BeforeDelDialogSubmit = "beforeSubmit";
            this.ClientSideEvents.SerializeGridData = "serializeGridData";
            this.ClientSideEvents.AddDialogOnClickSubmit = "onclickSubmitLocal";
            this.ClientSideEvents.EditDialogOnClickSubmit = "onclickSubmitLocal";
            this.ClientSideEvents.AddDialogBeforeCheckValues = "beforeCheckValues";
            this.ClientSideEvents.EditDialogBeforeCheckValues = "beforeCheckValues";
            this.ClientSideEvents.AfterClickPgButtons = "afterclickPgButtons";

            this.ClientSideEvents.SerializeDelData = "serializeGridData";
            #endregion

            this.SetAllColumn();
            SetColumn(VouchType);
        }
        private void SetAllColumn()
        {
            allColumn = new List<JQGridColumn>();
            allColumn.Add(new JQGridColumn()
            {
                DataField = "Id",
                DataType = typeof(Guid),
                PrimaryKey = true,
                Visible = false,
                Searchable = false
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "RdId",
                DataType = typeof(Guid),
                Visible = false,
                Searchable = false,
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "SVId",
                DataType = typeof(Guid),
                Visible = false,
                Searchable = false
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "TVId",
                DataType = typeof(Guid),
                Visible = false,
                Searchable = false
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "CVId",
                DataType = typeof(Guid),
                Visible = false,
                Searchable = false
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "MVId",
                DataType = typeof(Guid),
                Visible = false,
                Searchable = false
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "ALVId",
                DataType = typeof(Guid),
                Visible = false,
                Searchable = false
            });
            allColumn.Add(new JQGridColumn()
           {
               DataField = "InvId",
               HeaderText = "存货",
               DataType = typeof(Guid),
               Editable = true,
               Visible = false,
               Searchable = false,
               EditType = Trirand.Web.Mvc.EditType.DropDown,
               DataEvents = new List<DataEvent>() { new DataEvent() { Type = DataEventType.Change, Function = "InvIdChange" } },
               DataInit = "InvIdColumnDataInit",
           });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "InvName",
                HeaderText = "存货",
                DataType = typeof(string),
                Editable = false,
                Searchable = false,
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "Specs",
                HeaderText = "规格型号",
                DataType = typeof(string),
                Editable = true,
                EditFieldAttributes = new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } },
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "STUnitName",
                HeaderText = "计量单位",
                DataType = typeof(string),
                Editable = true,
                EditFieldAttributes = new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } },
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "DueNum",
                HeaderText = "应发数",
                DataType = typeof(decimal),
                Editable = true,
                EditFieldAttributes = new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } },
                Formatter = new DigitFormatter(),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "Num",
                HeaderText = "数量",
                DataType = typeof(decimal),
                Editable = true,
                Formatter = new DigitFormatter(),
                DataEvents = new List<DataEvent>() { new DataEvent() { Type = DataEventType.Change, Function = "NumChange" } },
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "CNum",
                HeaderText = "盘点数",
                DataType = typeof(decimal),
                Editable = true,
                Searchable = false,
                Formatter = new DigitFormatter(),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "AddInNum",
                HeaderText = "调整入库数",
                DataType = typeof(decimal),
                Searchable = false,
                Formatter = new DigitFormatter(),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "AddOutNum",
                HeaderText = "调整出库数",
                DataType = typeof(decimal),
                Searchable = false,
                Formatter = new DigitFormatter(),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "Price",
                HeaderText = "单价",
                DataType = typeof(decimal),
                Editable = true,
                Formatter = new DigitFormatter(),
                DataEvents = new List<DataEvent>() { new DataEvent() { Type = DataEventType.Change, Function = "PriceChange" } },
            });

            allColumn.Add(new JQGridColumn()
            {
                DataField = "Amount",
                HeaderText = "金额",
                DataType = typeof(decimal),
                Editable = true,
                Formatter = new DigitFormatter(),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "Batch",
                HeaderText = "批号",
                DataType = typeof(string),
                Editable = true,
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "MadeDate",
                HeaderText = "生产日期",
                DataType = typeof(DateTime),
                Editable = true,
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "ShelfLife",
                HeaderText = "保质期",
                DataType = typeof(int),
                Editable = true,
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "ShelfLifeType",
                HeaderText = "保质期单位",
                DataType = typeof(int),
                Editable = true,
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "InvalidDate",
                HeaderText = "失效日期",
                DataType = typeof(DateTime),
                Editable = false,
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "Locator",
                HeaderText = "货位",
                DataType = typeof(Guid),
                Editable = true,
                Visible = false,
                Searchable = false,
                EditType = Trirand.Web.Mvc.EditType.DropDown,
                DataEvents = new List<DataEvent>() { new DataEvent() { Type = DataEventType.Change, Function = "LocatorChange" } },
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "LocatorName",
                HeaderText = "货位",
                DataType = typeof(string),
                Searchable = false,
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "OutLocator",
                HeaderText = "调整前货位",
                DataType = typeof(Guid),
                Editable = true,
                Visible = false,
                Searchable = false,
                EditType = Trirand.Web.Mvc.EditType.DropDown,
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "OutLocatorName",
                HeaderText = "调整前货位",
                DataType = typeof(string),
                Searchable = false,
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "InLocator",
                HeaderText = "调整后货位",
                DataType = typeof(Guid),
                Editable = true,
                Visible = false,
                Searchable = false,
                EditType = Trirand.Web.Mvc.EditType.DropDown,
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "InLocatorName",
                HeaderText = "调整后货位",
                DataType = typeof(string),
                Searchable = false,
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "AvaNum",
                HeaderText = "可用量",
                DataType = typeof(string),
                Searchable = false,
                Visible = false,
                EditFieldAttributes = new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } },
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "Memo",
                HeaderText = "行备注",
                DataType = typeof(string),
                Editable = true,
            });
        }

        private void SetColumn(string VouchType)
        {
            string[] strs=null;
            string[] strs1 = { "Id", "RdId", "InvId", "InvName", "Specs", "STUnitName", "DueNum", "Num", "Price", "Amount",
                                "Batch", "MadeDate", "ShelfLife", "ShelfLifeType","InvalidDate","Locator","LocatorName",
                                "AvaNum","Memo"};
            string[] strTransVouch = { "Id", "TVId", "InvId", "InvName", "Specs", "STUnitName", "Num", "Price", "Amount", 
                                 "Batch", "MadeDate", "ShelfLife", "ShelfLifeType", "InvalidDate", "Locator", "LocatorName", 
                                 "AvaNum", "Memo"};
            string[] strScrapVouch = { "Id", "SVId", "InvId", "InvName", "Specs", "STUnitName", "Num", "Price", "Amount", 
                                         "Batch", "MadeDate", "ShelfLife", "ShelfLifeType", "InvalidDate", "Locator", "LocatorName", 
                                         "AvaNum", "Memo"};
            string[] strCheckVouch = { "Id", "CVId", "InvId", "InvName", "Specs", "STUnitName", "Num", "CNum", "AddInNum", "AddOutNum", 
                                         "Batch", "MadeDate", "ShelfLife", "ShelfLifeType", "InvalidDate", "LocatorName",
                                         "Memo"};
            string[] strAdjustLocatorVouch = { "Id", "ALVId", "InvId", "InvName", "Specs", "STUnitName", "Num",
                                             "Batch", "MadeDate", "ShelfLife", "ShelfLifeType", "InvalidDate",
                                             "OutLocator","OutLocatorName","InLocator","InLocatorName","AvaNum",
                                             "Memo"};
            string[] strMixVouch = { "Id", "MVId", "InvId", "InvName", "Specs", "STUnitName", "Num", "Memo"};
            switch (VouchType)
            {
                case DXInfo.Models.VouchTypeCode.ScrapVouch:
                    strs = strScrapVouch;
                    break;
                case DXInfo.Models.VouchTypeCode.TransVouch:
                    strs = strTransVouch;
                    break;
                case DXInfo.Models.VouchTypeCode.CheckVouch:
                    strs = strCheckVouch;
                    break;
                case DXInfo.Models.VouchTypeCode.AdjustLocatorVouch:
                    strs = strAdjustLocatorVouch;
                    break;
                case DXInfo.Models.VouchTypeCode.MixVouch:
                    strs = strMixVouch;
                    break;
                default:
                    strs = strs1;
                    break;
            }
            foreach (string str in strs)
            {
                JQGridColumn col = allColumn.Find(f => f.DataField == str);
                if (col != null)
                {
                    this.Columns.Add(col);
                }
            }
        }

    }

    public class StockManageSearchJQGrid : JQGrid
    {
        private List<JQGridColumn> allColumn;
        public StockManageSearchJQGrid(string VouchType)
        {
            this.ToolBarSettings.ShowSearchButton = true;            
            this.ToolBarSettings.ShowRefreshButton = true;
            this.AppearanceSettings.ShowRowNumbers = true;
            this.ToolBarSettings.ShowExcelButton = true;
            this.ToolBarSettings.ShowColumnChooser = true;
            this.SearchDialogSettings.MultipleSearch = true;
            #region 客户端事件
            this.ClientSideEvents.RowDoubleClick = "DblClick";
            this.ClientSideEvents.SerializeGridData = "serializeGridData";
            #endregion

            this.SetAllColumn();
            SetColumn(VouchType);
        }
        private void SetAllColumn()
        {
            allColumn = new List<JQGridColumn>();
            allColumn.Add(new JQGridColumn()
            {
                DataField = "Id",
                DataType = typeof(Guid),
                //PrimaryKey = true,
                Visible = false,
                Searchable = false
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "VouchType",
                DataType = typeof(string),
                Visible = false,
                Searchable = false,
                Hidedlg = true,
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "Code",
                //HeaderText = "入库单号",
                DataType = typeof(string),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "RdDate",
                //HeaderText = "入库日期",
                DataType = typeof(DateTime),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "TVDate",
                HeaderText = "单据日期",
                DataType = typeof(DateTime),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "SVDate",
                HeaderText = "单据日期",
                DataType = typeof(DateTime),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "CVDate",
                HeaderText = "盘点日期",
                DataType = typeof(DateTime),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "ALVDate",
                HeaderText = "单据日期",
                DataType = typeof(DateTime),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "MVDate",
                HeaderText = "单据日期",
                DataType = typeof(DateTime),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "WhName",
                HeaderText = "仓库",
                DataType = typeof(string),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "OutWhName",
                HeaderText = "转出仓库",
                DataType = typeof(string),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "InWhName",
                HeaderText = "转入仓库",
                DataType = typeof(string),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "ARVCode",
                HeaderText = "到货单号",
                DataType = typeof(string),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "ArvDate",
                HeaderText = "到货日期",
                DataType = typeof(DateTime),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "VenName",
                HeaderText = "供货单位",
                DataType = typeof(string),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "BusTypeName",
                HeaderText = "入库类别",
                DataType = typeof(string),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "SalesmanName",
                HeaderText = "业务员",
                DataType = typeof(string),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "IsVerify",
                HeaderText = "是否审核",
                DataType = typeof(bool),
            });

            allColumn.Add(new JQGridColumn()
            {
                DataField = "VerifyDate",
                HeaderText = "审核日期",
                DataType = typeof(DateTime),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "Memo",
                HeaderText = "备注",
                DataType = typeof(string),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "SubId",
                DataType = typeof(Guid),
                PrimaryKey = true,
                Visible = false,
                Searchable = false
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "InvName",
                HeaderText = "存货",
                DataType = typeof(string),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "Specs",
                HeaderText = "规格型号",
                DataType = typeof(string),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "STUnitName",
                HeaderText = "计量单位",
                DataType = typeof(string),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "Num",
                //HeaderText = "数量",
                DataType = typeof(decimal),
                Formatter = new DigitFormatter(),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "CNum",
                HeaderText = "盘点数",
                DataType = typeof(decimal),
                Formatter = new DigitFormatter(),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "AddOutNum",
                HeaderText = "调整出库数",
                DataType = typeof(decimal),
                Formatter = new DigitFormatter(),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "AddInNum",
                HeaderText = "调整入库数",
                DataType = typeof(decimal),
                Formatter = new DigitFormatter(),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "Price",
                HeaderText = "单价",
                DataType = typeof(decimal),
                Formatter = new DigitFormatter(),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "Amount",
                HeaderText = "金额",
                DataType = typeof(decimal),
                Formatter = new DigitFormatter(),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "Batch",
                HeaderText = "批号",
                DataType = typeof(string),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "MadeDate",
                HeaderText = "生产日期",
                DataType = typeof(DateTime),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "ShelfLife",
                HeaderText = "保质期",
                DataType = typeof(int),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "ShelfLifeTypeName",
                HeaderText = "保质期单位",
                DataType = typeof(string),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "InvalidDate",
                HeaderText = "失效日期",
                DataType = typeof(DateTime),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "LocatorName",
                HeaderText = "货位",
                DataType = typeof(string),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "OutLocatorName",
                HeaderText = "调整前货位",
                DataType = typeof(string),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "InLocatorName",
                HeaderText = "调整后货位",
                DataType = typeof(string),
            });
            allColumn.Add(new JQGridColumn()
            {
                DataField = "SubMemo",
                HeaderText = "行备注",
                DataType = typeof(string),
            });
        }

        private void SetColumn(string VouchType)
        {
            string[] strs = null;
            string[] strs1 = { "Id", "VouchType", "Code", "RdDate", "WhName", "ARVCode", "ArvDate", "VenName",
                                "BusTypeName", "SalesmanName","IsVerify","VerifyDate","Memo",
                                "SubId","InvName","Specs","STUnitName","Num","Price","Amount","Batch","MadeDate",
                             "ShelfLife","ShelfLifeTypeName","InvalidDate","LocatorName","SubMemo"};
            string[] strTransVouch = { "Id", "Code", "TVDate", "OutWhName", "InWhName", "SalesmanName","IsVerify","VerifyDate","Memo",
                                "SubId","InvName","Specs","STUnitName","Num","Price","Amount","Batch","MadeDate",
                             "ShelfLife","ShelfLifeTypeName","InvalidDate","LocatorName","SubMemo"};
            string[] strScrapVouch = { "Id", "Code", "SVDate", "WhName", "SalesmanName","IsVerify","VerifyDate","Memo",
                                "SubId","InvName","Specs","STUnitName","Num","Price","Amount","Batch","MadeDate",
                             "ShelfLife","ShelfLifeTypeName","InvalidDate","LocatorName","SubMemo"};
            string[] strCheckVouch = { "Id", "Code", "CVDate", "WhName","SalesmanName", "IsVerify","VerifyDate","Memo",
                                "SubId","InvName","Specs","STUnitName","Num","CNum","AddInNum","AddOutNum","Batch","MadeDate",
                             "ShelfLife","ShelfLifeTypeName","InvalidDate","LocatorName","SubMemo"};
            string[] strAdjustLocatorVouch = { "Id", "Code", "ALVDate", "WhName", "SalesmanName","IsVerify","VerifyDate","Memo",
                                "SubId","InvName","Specs","STUnitName","Num","Batch","MadeDate",
                             "ShelfLife","ShelfLifeTypeName","InvalidDate","OutLocatorName","InLocatorName","SubMemo"};
            string[] strMixVouch = { "Id", "Code", "MVDate", "InWhName","OutWhName","SalesmanName","IsVerify","VerifyDate","Memo",
                                "SubId","InvName","Specs","STUnitName","Num","SubMemo"};
            switch (VouchType)
            {
                case DXInfo.Models.VouchTypeCode.ScrapVouch:
                    strs = strScrapVouch;
                    break;
                case DXInfo.Models.VouchTypeCode.TransVouch:
                    strs = strTransVouch;
                    break;
                case DXInfo.Models.VouchTypeCode.CheckVouch:
                    strs = strCheckVouch;
                    break;
                case DXInfo.Models.VouchTypeCode.AdjustLocatorVouch:
                    strs = strAdjustLocatorVouch;
                    break;
                case DXInfo.Models.VouchTypeCode.MixVouch:
                    strs = strMixVouch;
                    break;
                default:
                    strs = strs1;
                    break;
            }
            foreach (string str in strs)
            {
                JQGridColumn col = allColumn.Find(f => f.DataField == str);
                if (col != null)
                {
                    this.Columns.Add(col);
                }
            }
        }

    }
}