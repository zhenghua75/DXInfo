using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace ynhnTransportManage.Models
{
    public class CategoryGridDeptModels
    {
    }
    public class CategoryDeptGridModel
    {
        public int CategoryType { get; set; }
        public int DeptType { get; set; }
        public JQGrid CategoryGrid { get; set; }
        public JQGrid DeptGrid { get; set; }
        public JQGrid Dept2Grid { get; set; }
        public CategoryDeptGridModel()
        {
            CategoryGrid = new JQGrid()
            {
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
                        DataField="Comment",
                        HeaderText="备注",
                        DataType=typeof(string),
                    },   
                    new JQGridColumn()
                    {
                        DataField="CategoryType",
                        DataType=typeof(int),
                        Visible=false,
                        Searchable=false,
                    },
                }
            };
            CategoryGrid.ToolBarSettings.ShowSearchButton = true;
            CategoryGrid.ToolBarSettings.ShowRefreshButton = true;
            CategoryGrid.SearchDialogSettings.MultipleSearch = true;
            CategoryGrid.MultiSelect = true;
            CategoryGrid.MultiSelectKey = MultiSelectKey.None;
            CategoryGrid.MultiSelectMode = MultiSelectMode.SelectOnCheckBoxClickOnly;
            CategoryGrid.HierarchySettings.HierarchyMode = HierarchyMode.Parent;
            CategoryGrid.ClientSideEvents.SubGridRowExpanded = "showDeptSubGrid";
            CategoryGrid.ClientSideEvents.SerializeGridData = "serializeGridData";

            CategoryGrid.ToolBarSettings.CustomButtons = new List<JQGridToolBarButton>()
            {
                new JQGridToolBarButton()
                {
                    Position = ToolBarButtonPosition.Last,
                    ToolTip="批量设置分类门店",
                    Text="批量设置分类门店",
                    OnClick="customButtonClicked",
                    ButtonIcon="ui-icon-extlink"
                }
            };

            DeptGrid = new JQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        EditActionIconsColumn = true,
                        EditActionIconsSettings = new EditActionIconsSettings(){ ShowDeleteIcon=false},
                    },
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
                        HeaderText="选择",
                        DataField="IsSelected",
                        DataType = typeof(bool),
                        Editable = true,
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptType",
                        DataType = typeof(int),
                        Visible=false,
                        Searchable=false,
                    },
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
                        Visible=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        HeaderText="编码",
                        DataField="DeptCode",
                        Editable=true,
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
                        DataType=typeof(string),
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        HeaderText="描述",
                        DataField="Comment",
                        DataType=typeof(string),
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptType",
                        DataType = typeof(int),
                        Visible=false,
                        Searchable=false,
                    },
                }
            };
            Dept2Grid.ToolBarSettings.ShowRefreshButton = true;
            Dept2Grid.ToolBarSettings.ShowSearchButton = true;
            Dept2Grid.AutoWidth = true;
            Dept2Grid.Height = Unit.Percentage(100);
            Dept2Grid.MultiSelect = true;
            Dept2Grid.MultiSelectKey = MultiSelectKey.None;
            Dept2Grid.MultiSelectMode = MultiSelectMode.SelectOnCheckBoxClickOnly;
            Dept2Grid.ClientSideEvents.SerializeGridData = "serializeGridData2";
        }
    }
}