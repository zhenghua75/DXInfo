using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class DeptOperManageModels
    {
    }
    public class DeptOperManageGridModel
    {
        public EntityJQGrid DeptOperManageGrid { get; set; }
        public JQGrid FuncGrid { get; set; }
        public DeptOperManageGridModel()
        {
            DeptOperManageGrid = new EntityJQGrid()
            {
                Width=1100,
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        EditActionIconsColumn=true,
                        Viewable=false,
                        Hidedlg=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="vcOperID",
                        HeaderText="客户端操作员编号",
                        DataType=typeof(string),
                        PrimaryKey=true,
                        Editable=true,
                        Width=200,
                    },
                    new JQGridColumn()
                    {
                        DataField="vcOperName",
                        HeaderText="客户端操作员名称",
                        DataType=typeof(string),
                        Editable=true,
                        Width=200,
                    },  
                    new JQGridColumn()
                    {
                        DataField="vcDeptId",
                        HeaderText="门店",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="DeptName",
                    //    HeaderText="门店",
                    //    DataType=typeof(string),
                    //    Searchable=false,
                    //    Visible=false,
                    //    Exportable=true,
                    //    Hidedlg=true,
                    //},
                    
                    new JQGridColumn()
                    {
                        DataField="vcLimit",
                        HeaderText="权限",
                        DataType=typeof(string),
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="vcActiveFlag",
                        HeaderText="激活标志",
                        DataType=typeof(string),
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="vcPwdBeginFlag",
                        HeaderText="密码初始化标志",
                        DataType=typeof(string),
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="IsDiscount",
                        HeaderText="是否折扣",
                        DataType=typeof(bool),
                        Editable=true
                    },
                }
            };
            DeptOperManageGrid.AddDialogSettings.BottomInfo = "新增客户端操作员时，密码默认为：000000";
            DeptOperManageGrid.EditDialogSettings.BottomInfo = "密码初始化将会把客户端操作员登录的密码初始化为：000000";
            DeptOperManageGrid.HierarchySettings.HierarchyMode = HierarchyMode.Parent;
            DeptOperManageGrid.ClientSideEvents.SubGridRowExpanded = "showSubGrid";
            DeptOperManageGrid.ClientSideEvents.AfterAddDialogShown = "enableFields";
            DeptOperManageGrid.ClientSideEvents.AfterEditDialogShown = "disableFields";

            FuncGrid = new JQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        Width=100,
                        EditActionIconsColumn=true,
                        EditActionIconsSettings = new EditActionIconsSettings(){ ShowEditIcon=true,ShowDeleteIcon=false},
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="cnvcFuncAddress",
                    //    DataType=typeof(string),
                    //    Visible=false, 
                    //    PrimaryKey=true,
                    //},
                    new JQGridColumn()
                    {
                        DataField="IsInRole",
                        HeaderText="选择",
                        DataType=typeof(bool),
                        Editable=true,
                    },                 
                    new JQGridColumn()
                    {
                        DataField="cnvcFuncName",
                        HeaderText="功能名称",
                        DataType=typeof(string),  
                        PrimaryKey=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="cnvcFuncParentName",
                        HeaderText="菜单名称",
                        DataType=typeof(string),
                        //EditFieldAttributes=new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } },
                    },
                    
                }
            };
            FuncGrid.AutoWidth = false;
            FuncGrid.Width = 600;
            FuncGrid.ToolBarSettings.ShowEditButton = true;
            FuncGrid.ToolBarSettings.ShowRefreshButton = true;
            FuncGrid.ToolBarSettings.ShowSearchButton = true;
            FuncGrid.HierarchySettings.HierarchyMode = HierarchyMode.Child;
        }
    }
}