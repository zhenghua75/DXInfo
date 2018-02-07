using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace ynhnTransportManage.Models
{
    public class AuthorizationModels
    {
    }
    public class AuthorizationGridModel
    {
        public JQGrid RolesGrid { get; set; }
        public JQGrid AuthorizationGrid { get; set; }
        public AuthorizationGridModel()
        {

            this.RolesGrid = new JQGrid()
            {
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
                },
            };
            RolesGrid.ToolBarSettings.ShowRefreshButton = true;
            RolesGrid.ToolBarSettings.ShowSearchButton = true;

            this.AuthorizationGrid = new JQGrid()
            {
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
                        Sortable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="Code",
                        PrimaryKey=true,
                        HeaderText="功能编码",
                        DataType=typeof(string),
                        Editable=false,
                        Visible=false,
                        Sortable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="Title",
                        HeaderText="功能描述",
                        DataType=typeof(string),
                        Editable=false,
                        Width=300,
                        Sortable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="IsClient",
                        HeaderText="是否客户端",
                        DataType=typeof(bool),
                        Editable=false,
                        Sortable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="Sort",
                        DataType=typeof(string),
                        Editable=false,
                        Visible=false,
                        Searchable=false,
                        Sortable=true,
                    },
                },
            };

            AuthorizationGrid.AutoWidth = false;
            AuthorizationGrid.Width = 800;
            AuthorizationGrid.Height = 600;
            AuthorizationGrid.ToolBarSettings.ShowRefreshButton = true;
            AuthorizationGrid.ToolBarSettings.ShowSearchButton = true;
            AuthorizationGrid.TreeGridSettings.Enabled = true;
        }
    }
}