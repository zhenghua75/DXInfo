using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace DXInfo.Web.Models
{
    public class LoginOperModels
    {
    }
    public class LoginOperGridModel
    {
        public EntityJQGrid LoginOperGrid { get; set; }
        public LoginOperGridModel()
        {
            LoginOperGrid = new EntityJQGrid()
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
                        DataField="vcLoginID",
                        HeaderText="登录ID",
                        DataType=typeof(string),
                        PrimaryKey=true,
                        Editable=true,
                        //EditFieldAttributes = new List<JQGridEditFieldAttribute>{ new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } },
                    },
                    new JQGridColumn()
                    {
                        DataField="vcOperName",
                        HeaderText="操作员名称",
                        DataType=typeof(string),
                        Editable=true,
                    },  
                    new JQGridColumn()
                    {
                        DataField="vcDeptId",
                        HeaderText="门店",
                        DataType=typeof(string),
                        Editable=true,
                        Width=200,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="vcDeptName",
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
                        HeaderText="查看权限",
                        DataType=typeof(string),
                        Editable=true
                    }
                }
            };
            LoginOperGrid.AddDialogSettings.BottomInfo = "新增网站操作员时，密码默认为：123456";
            LoginOperGrid.ClientSideEvents.AfterAddDialogShown = "enableFields";
            LoginOperGrid.ClientSideEvents.AfterEditDialogShown = "disableFields";
        }
    }
}