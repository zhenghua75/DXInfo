using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace ynhnTransportManage.Models
{
    public class InvDeptModels
    {
    }
    public class InvDeptGridModel
    {
        public int InvType { get; set; }
        public int DeptType { get; set; }
        public JQGrid InvGrid { get; set; }
        public JQGrid DeptGrid { get; set; }
        public JQGrid Dept2Grid { get; set; }
        public InvDeptGridModel()
        {
            InvGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType=typeof(Guid),
                        PrimaryKey=true,
                        Visible=false
                    },
                    new JQGridColumn()
                    {
                        DataField="Code",
                        HeaderText="编码",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="Name",
                        HeaderText="名称",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
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
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice",
                        HeaderText="默认单价",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice0",
                        HeaderText="大杯单价",
                        DataType=typeof(decimal),
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice1",
                        HeaderText="中杯单价",
                        DataType=typeof(decimal),
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice2",
                        HeaderText="小杯单价",
                        DataType=typeof(decimal),
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePoint",
                        HeaderText="默认积分",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePoint0",
                        HeaderText="大杯积分",
                        DataType=typeof(decimal),
                        Editable=true
                    },
                     new JQGridColumn()
                    {
                        DataField="SalePoint1",
                        HeaderText="中杯积分",
                        DataType=typeof(decimal),
                        Editable=true
                    },
                     new JQGridColumn()
                    {
                        DataField="SalePoint2",
                        HeaderText="小杯积分",
                        DataType=typeof(decimal),
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="Specs",
                        HeaderText="规格型号",
                        DataType=typeof(string),
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="IsDonate",
                        HeaderText="是否赠送商品",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvType",
                        DataType=typeof(int),
                        Visible=false,
                        Searchable=false,
                        SearchToolBarOperation= SearchOperation.IsEqualTo,
                    },
                }
            };
            //InvGrid.AppearanceSettings.Caption = "商品门店设置";
            InvGrid.ToolBarSettings.ShowSearchButton = true;
            InvGrid.ToolBarSettings.ShowRefreshButton = true;
            InvGrid.SearchDialogSettings.MultipleSearch = true;
            InvGrid.MultiSelect = true;
            InvGrid.MultiSelectKey = MultiSelectKey.None;
            InvGrid.MultiSelectMode = MultiSelectMode.SelectOnCheckBoxClickOnly;
            InvGrid.HierarchySettings.HierarchyMode = HierarchyMode.Parent;
            InvGrid.ClientSideEvents.SubGridRowExpanded = "showDeptSubGrid";
            InvGrid.ToolBarSettings.CustomButtons = new List<JQGridToolBarButton>()
            {
                new JQGridToolBarButton()
                {
                    Position = ToolBarButtonPosition.Last,
                    ToolTip="批量设置商品门店",
                    Text="批量设置商品门店",
                    OnClick="customButtonClicked",
                    ButtonIcon="ui-icon-extlink"
                }
            };
            InvGrid.ClientSideEvents.SerializeGridData = "serializeGridData";
            DeptGrid = new JQGrid()
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
                        HeaderText="选择",
                        DataField="IsSelected",
                        DataType = typeof(bool),
                        Editable = true,
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptId",
                        PrimaryKey=true,
                        Visible=false
                    },
                    new JQGridColumn()
                    {
                        HeaderText="编码",
                        DataField="DeptCode",
                        Sortable=false,
                        Searchable=true,
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        HeaderText="名称",
                        DataField="DeptName",
                        Searchable=true,
                        Sortable=false,
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        HeaderText="地址",
                        DataField="Address",
                        Searchable=true,
                        Sortable=false,
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        HeaderText="描述",
                        DataField="Comment",
                        Searchable=true,
                        Sortable=false,
                        DataType=typeof(string),
                    },
                    
                    new JQGridColumn()
                    {
                        DataField="DeptType",
                        DataType = typeof(int),
                        Visible=false,
                        SearchToolBarOperation= SearchOperation.IsEqualTo,
                        Searchable=false,
                    }
                }
            };
            DeptGrid.AppearanceSettings.Caption = "门店";
            DeptGrid.ToolBarSettings.ShowRefreshButton = true;
            DeptGrid.ToolBarSettings.ShowSearchButton = true;
            DeptGrid.ToolBarSettings.ShowEditButton = true;
            DeptGrid.AutoWidth = false;
            DeptGrid.Width = 600;
            DeptGrid.Height = Unit.Percentage(100);
            DeptGrid.HierarchySettings.HierarchyMode = HierarchyMode.Child;
            DeptGrid.ClientSideEvents.SerializeGridData = "serializeGridData2";
            DeptGrid.ClientSideEvents.BeforeAddDialogSubmit = "beforeSubmit";
            DeptGrid.ClientSideEvents.BeforeEditDialogSubmit = "beforeSubmit";
            DeptGrid.ClientSideEvents.SerializeRowData = "serializeRowData";
            Dept2Grid = new JQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="DeptId",
                        PrimaryKey=true,
                        Visible=false
                    },
                    new JQGridColumn()
                    {
                        HeaderText="编码",
                        DataField="DeptCode",
                        Editable=true,
                        Sortable=false,
                        Searchable=true,
                        DataType=typeof(string),
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        HeaderText="名称",
                        DataField="DeptName",
                        Editable=true,
                        Searchable=true,
                        Sortable=false,
                        DataType=typeof(string),
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        HeaderText="地址",
                        DataField="Address",
                        Searchable=true,
                        Sortable=false,
                        DataType=typeof(string),
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        HeaderText="描述",
                        DataField="Comment",
                        Searchable=true,
                        Sortable=false,
                        DataType=typeof(string),
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptType",
                        DataType=typeof(int),
                        Visible=false,
                        Searchable=false,
                        SearchToolBarOperation= SearchOperation.IsEqualTo,
                    },
                }
            };
            Dept2Grid.AppearanceSettings.Caption = "门店";
            Dept2Grid.ToolBarSettings.ShowRefreshButton = true;
            Dept2Grid.ToolBarSettings.ShowSearchButton = true;
            Dept2Grid.AutoWidth = false;
            Dept2Grid.Width = 600;
            Dept2Grid.Height = Unit.Percentage(100);
            Dept2Grid.MultiSelect = true;
            Dept2Grid.MultiSelectKey = MultiSelectKey.None;
            Dept2Grid.MultiSelectMode = MultiSelectMode.SelectOnCheckBoxClickOnly;
            Dept2Grid.ClientSideEvents.SerializeGridData = "serializeGridData2";
        }
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
            DeptGrid.AutoWidth = true;
            DeptGrid.Height = Unit.Percentage(100);
            DeptGrid.HierarchySettings.HierarchyMode = HierarchyMode.Parent;
            DeptGrid.ClientSideEvents.SubGridRowExpanded = "showInvSubGrid";
            DeptGrid.ClientSideEvents.SerializeGridData = "serializeGridData2";

            InvGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
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
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="Name",
                        HeaderText="名称",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
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
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice0",
                        HeaderText="大杯单价",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice1",
                        HeaderText="中杯单价",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice2",
                        HeaderText="小杯单价",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePoint",
                        HeaderText="默认积分",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePoint0",
                        HeaderText="大杯积分",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Searchable=false
                    },
                     new JQGridColumn()
                    {
                        DataField="SalePoint1",
                        HeaderText="中杯积分",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Searchable=false
                    },
                     new JQGridColumn()
                    {
                        DataField="SalePoint2",
                        HeaderText="小杯积分",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Searchable=false
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
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="InvType",
                        DataType=typeof(int),
                        Visible=false,
                        Searchable=false
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
            InvGrid.ClientSideEvents.SerializeGridData = "serializeGridData";
            InvGrid.ClientSideEvents.BeforeAddDialogSubmit = "beforeSubmit";
            InvGrid.ClientSideEvents.BeforeEditDialogSubmit = "beforeSubmit";
            InvGrid.ClientSideEvents.SerializeRowData = "serializeRowData";
        }
    }
}