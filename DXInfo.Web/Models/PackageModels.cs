using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace DXInfo.Web.Models
{
    public class PackageModels
    {
    }
    public class PackageGridModel
    {
        public int InvType { get; set; }
        public JQGrid PackageGrid { get; set; }
        public JQGrid InvGrid { get; set; }
        public PackageGridModel()
        {
            PackageGrid = new JQGrid()
            {
                Columns = new List<JQGridColumn>() 
                { 
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType=typeof(Guid),
                        PrimaryKey=true,
                        Visible=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="Code",
                        HeaderText="编码",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="Name",
                        HeaderText="名称",
                        DataType=typeof(string),
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
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePoint",
                        HeaderText="默认积分",
                        DataType=typeof(decimal),
                    },                    
                    new JQGridColumn()
                    {
                        DataField="Specs",
                        HeaderText="规格型号",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="InvType",
                        DataType=typeof(int),
                        Visible=false,
                        Searchable=false,
                        SearchToolBarOperation= SearchOperation.IsEqualTo,
                    }
                }
            };
            PackageGrid.ToolBarSettings.ShowSearchButton = true;
            PackageGrid.ToolBarSettings.ShowRefreshButton = true;
            PackageGrid.SearchDialogSettings.MultipleSearch = true;
            PackageGrid.HierarchySettings.HierarchyMode = HierarchyMode.Parent;
            PackageGrid.Width = 900;
            InvGrid = new JQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        EditActionIconsColumn=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType=typeof(Guid),
                        PrimaryKey=true,
                        Visible=false,
                        Searchable=false,
                    },
                    
                    new JQGridColumn()
                    {
                        DataField="InventoryId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Editable=true,
                        Visible=false,
                        DataInit = "InvIdColumnDataInit",
                    },
                    new JQGridColumn()
                    {
                        DataField="Code",
                        HeaderText="存货编码",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="Name",
                        HeaderText="存货名称",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="Price",
                        HeaderText="单价",
                        Editable=true,
                        DataType=typeof(Decimal),
                    },
                    new JQGridColumn()
                    {
                        DataField="IsOptional",
                        HeaderText="是否可选",
                        Editable=true,
                        DataType=typeof(bool),
                    },
                    new JQGridColumn()
                    {
                        DataField="OptionalGroup",
                        HeaderText="可选组名称",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Comment",
                        HeaderText="描述",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Quantity",
                        HeaderText="数量",
                        DataType=typeof(int),
                        Editable=true,
                    },
                }
            };
            InvGrid.AppearanceSettings.Caption = "存货";
            InvGrid.ToolBarSettings.ShowAddButton = true;
            InvGrid.ToolBarSettings.ShowRefreshButton = true;
            InvGrid.ToolBarSettings.ShowSearchButton = true;
            InvGrid.SearchDialogSettings.MultipleSearch = true;
            InvGrid.ToolBarSettings.ShowEditButton = true;
            InvGrid.ToolBarSettings.ShowDeleteButton = true;
            InvGrid.AutoWidth = false;
            InvGrid.Width = 800;
            InvGrid.Height = Unit.Percentage(100);
            InvGrid.HierarchySettings.HierarchyMode = HierarchyMode.Child;
            InvGrid.EditDialogSettings.Width = 450;
            InvGrid.AddDialogSettings.Width = 450;
            InvGrid.ClientSideEvents.AfterClickPgButtons = "afterclickPgButtons";
            InvGrid.ClientSideEvents.AfterAddDialogShown = "afterShowForm";
            InvGrid.ClientSideEvents.AfterEditDialogShown = "afterShowForm";
            InvGrid.ClientSideEvents.AfterDeleteDialogShown = "afterShowForm";
            InvGrid.ClientSideEvents.AfterSearchDialogShown = "afterShowForm";
        }
    }
}