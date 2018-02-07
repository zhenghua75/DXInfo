using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace DXInfo.Web.Models
{
    public class RoleAssignModels
    {
    }
    public class RoleAssignGridModel
    {
        public JQGrid RolesGrid { get; set; }
        public JQGrid UsersGrid { get; set; }
        public RoleAssignGridModel()
        {
            this.RolesGrid = new JQGrid()
            {
                ID = "RolesGrid",
                Width=Unit.Pixel(1100),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="RoleId",
                        PrimaryKey=true,
                        Visible=false,
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="RoleName",
                        HeaderText="角色名",
                        DataType=typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="Description",
                        HeaderText="角色描述",
                        DataType=typeof(string)
                    }
                }
            };
            RolesGrid.ToolBarSettings.ShowRefreshButton = true;
            RolesGrid.ToolBarSettings.ShowSearchButton = true;
            RolesGrid.HierarchySettings.HierarchyMode = HierarchyMode.Parent;
            RolesGrid.ClientSideEvents.SubGridRowExpanded = "showUsersSubGrid";
            this.UsersGrid = new JQGrid()
            {
                ID = "UsersGrid",
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        Width=100,
                        EditActionIconsColumn=true,
                        EditActionIconsSettings = new EditActionIconsSettings(){ ShowEditIcon=true,ShowDeleteIcon=false},
                    },
                    new JQGridColumn()
                    {
                        DataField="IsInRole",
                        HeaderText="选择",
                        DataType=typeof(bool),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="UserId",
                        PrimaryKey=true,
                        Visible=false,
                        Searchable=false
                    },                    
                    new JQGridColumn()
                    {
                        DataField="UserName",
                        HeaderText="用户名",
                        DataType=typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="FullName",
                        HeaderText="姓名",
                        DataType=typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptName",
                        HeaderText="门店",
                        DataType=typeof(string)
                    }
                }
            };
            UsersGrid.AutoWidth = false;
            UsersGrid.Width = 600;
            UsersGrid.ToolBarSettings.ShowEditButton = true;
            UsersGrid.ToolBarSettings.ShowRefreshButton = true;
            UsersGrid.ToolBarSettings.ShowSearchButton = true;
            UsersGrid.HierarchySettings.HierarchyMode = HierarchyMode.Child;
        }
    }
}