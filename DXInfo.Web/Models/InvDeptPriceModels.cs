using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace DXInfo.Web.Models
{
    public class InvDeptPriceModels
    {
    }
    public class InvDeptPriceGridModel
    {
        public int InvType { get; set; }
        public int DeptType { get; set; }
        public JQGrid InvGrid { get; set; }
        public JQGrid DeptGrid { get; set; }
        public InvDeptPriceGridModel()
        {

            DeptGrid = new JQGrid()
            {
                Width=2000,
                Columns = new List<JQGridColumn>()
                {
                    
                    new JQGridColumn()
                    {
                        DataField="DeptId",
                        PrimaryKey=true,
                        Visible=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        HeaderText="编码",
                        DataField="DeptCode",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        HeaderText="名称",
                        DataField="DeptName",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        HeaderText="地址",
                        DataField="Address",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        HeaderText="描述",
                        DataField="Comment",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptType",
                        DataType=typeof(int),
                        Visible=false,
                        Searchable=false,
                        SearchToolBarOperation= SearchOperation.IsEqualTo,
                    }
                }
            };
            DeptGrid.ToolBarSettings.ShowRefreshButton = true;
            DeptGrid.HierarchySettings.HierarchyMode = HierarchyMode.Parent;            

            InvGrid = new JQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        EditActionIconsColumn=true,
                        EditActionIconsSettings = new EditActionIconsSettings(){ ShowEditIcon=true,ShowDeleteIcon=false},
                    },
                    new JQGridColumn()
                    {
                        DataField="InvId",
                        DataType=typeof(Guid),
                        PrimaryKey=true,
                        Visible=false,
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptId",
                        DataType=typeof(Guid),
                        Visible=false,
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="Code",
                        HeaderText="编码",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Name",
                        HeaderText="名称",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="CategoryName",
                        HeaderText="存货分类",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="UnitName",
                        HeaderText="计量单位",
                        DataType=typeof(string),
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice",
                        HeaderText="默认单价",
                        DataType=typeof(decimal),
                        Editable=true,
                        Searchable=false,  
                        Formatter = new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice0",
                        HeaderText="大杯单价",
                        DataType=typeof(decimal),
                        Editable=true,
                        Searchable=false,
                        Formatter = new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice1",
                        HeaderText="中杯单价",
                        DataType=typeof(decimal),
                        Editable=true,
                        Searchable=false,
                        Formatter = new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice2",
                        HeaderText="小杯单价",
                        DataType=typeof(decimal),
                        Editable=true,
                        Searchable=false,
                        Formatter = new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePoint",
                        HeaderText="默认积分",
                        DataType=typeof(decimal),
                        Editable=true,
                        Searchable=false,
                        Formatter = new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePoint0",
                        HeaderText="大杯积分",
                        DataType=typeof(decimal),
                        Editable=true,
                        Searchable=false,
                        Formatter = new DigitFormatter(),
                    },
                     new JQGridColumn()
                    {
                        DataField="SalePoint1",
                        HeaderText="中杯积分",
                        DataType=typeof(decimal),
                        Editable=true,
                        Searchable=false,
                        Formatter = new DigitFormatter(),
                    },
                     new JQGridColumn()
                    {
                        DataField="SalePoint2",
                        HeaderText="小杯积分",
                        DataType=typeof(decimal),
                        Editable=true,
                        Searchable=false,
                        Formatter = new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="Specs",
                        HeaderText="规格型号",
                        DataType=typeof(string),
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="IsDonate",
                        HeaderText="是否赠送商品",
                        DataType=typeof(bool),
                    },
                    new JQGridColumn()
                    {
                        DataField="InvType",
                        DataType=typeof(int),
                        Visible=false,
                        Searchable=false,
                        SearchToolBarOperation = SearchOperation.IsEqualTo,
                    },
                }
            };
            InvGrid.AppearanceSettings.Caption = "商品门店单价设置-商品";
            InvGrid.ToolBarSettings.ShowSearchButton = true;
            InvGrid.ToolBarSettings.ShowRefreshButton = true;
            InvGrid.SearchDialogSettings.MultipleSearch = true;
            InvGrid.ToolBarSettings.ShowEditButton = true;
            InvGrid.HierarchySettings.HierarchyMode = HierarchyMode.Child;
            InvGrid.AutoWidth = false;
        }
    }
}