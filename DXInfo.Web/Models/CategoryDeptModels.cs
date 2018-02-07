using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace DXInfo.Web.Models
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
                Width=900,
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
                        SearchToolBarOperation= SearchOperation.IsEqualTo,
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
            

            CategoryGrid.ToolBarSettings.CustomButtons = new List<JQGridToolBarButton>()
            {
                new JQGridToolBarButton()
                {
                    Position = ToolBarButtonPosition.Last,
                    ToolTip="批量设置分类门店",
                    Text="<span class='ui-pg-button-text'>批量设置分类门店</span>",
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
                        HeaderText="选择",
                        DataField="IsSelected",
                        DataType = typeof(bool),
                        Editable = true,
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
                        DataType = typeof(int),
                        Visible=false,
                        Searchable=false,
                        SearchToolBarOperation= SearchOperation.IsEqualTo,
                    },
                }
            };
            DeptGrid.AppearanceSettings.Caption = "门店";
            DeptGrid.ToolBarSettings.ShowRefreshButton = true;
            DeptGrid.ToolBarSettings.ShowSearchButton = true;
            DeptGrid.ToolBarSettings.ShowEditButton = true;
            DeptGrid.AutoWidth = false;
            DeptGrid.Width = 700;
            DeptGrid.Height = Unit.Percentage(100);
            DeptGrid.HierarchySettings.HierarchyMode = HierarchyMode.Child;
            
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
                        DataType = typeof(int),
                        Visible=false,
                        Searchable=false,
                        SearchToolBarOperation= SearchOperation.IsEqualTo,
                    },
                }
            };
            Dept2Grid.ToolBarSettings.ShowRefreshButton = true;
            Dept2Grid.ToolBarSettings.ShowSearchButton = true;
            Dept2Grid.AutoWidth = false;
            Dept2Grid.Width = 600;
            Dept2Grid.Height = Unit.Percentage(100);
            Dept2Grid.MultiSelect = true;
            Dept2Grid.MultiSelectKey = MultiSelectKey.None;
            Dept2Grid.MultiSelectMode = MultiSelectMode.SelectOnCheckBoxClickOnly;
            
        }
    }
}