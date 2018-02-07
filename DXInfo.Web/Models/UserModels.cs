using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Web.UI.WebControls;

namespace DXInfo.Web.Models
{
    public class UserModels
    {
    }    
    public class UserGridModel
    {
        public EntityJQGrid UserGrid { get; set; }
        public UserGridModel()
        {
            UserGrid = new EntityJQGrid()
            {
                Width=Unit.Pixel(1400),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        Viewable=false,
                        Hidedlg=true,
                        EditActionIconsColumn=true,
                        EditActionIconsSettings = new EditActionIconsSettings(){ ShowEditIcon=true,ShowDeleteIcon=false},
                    },
                    new JQGridColumn 
                    { 
                        DataField = "UserId", 
                        PrimaryKey = true,
                        Visible=false,
                        Searchable=false,
                        Hidedlg=true,
                    },
                    new JQGridColumn 
                    { 
                        DataField="UserName",
                        Width=100, 
                        HeaderText="用户名",
                        DataType=typeof(string)
                    },
                    new JQGridColumn 
                    {
                        DataField="FullName",
                        Editable=true,
                        Width=100, 
                        HeaderText="姓名",
                        DataType=typeof(string),
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()      
                        }
                    },
                    new JQGridColumn 
                    { 
                        DataField = "DeptId", 
                        DataType=typeof(Guid),
                        EditDialogFieldSuffix="(*)",
                        Editable = true,
                        HeaderText="门店", 
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()     
                        },
                    },               
                    new JQGridColumn 
                    {
                        DataField="DeptName",
                        HeaderText="门店",
                        DataType=typeof(string),
                        Searchable=false,
                        Visible=false,
                        Exportable=true,
                        Hidedlg=true,
                    },                    
                    new JQGridColumn 
                    {
                        DataField="IsApproved", 
                        HeaderText="是否启用",
                        DataType=typeof(bool),
                        Editable=true,
                    },
                    new JQGridColumn 
                    {
                        DataField="IsLockedOut",
                        HeaderText="是否锁定",
                        DataType=typeof(bool)
                    },
                    new JQGridColumn()
                    {
                        DataField="AuthorityType",
                        HeaderText="权限类型",
                        DataType=typeof(int),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="AuthorityTypeName",
                        HeaderText="权限类型",
                        DataType=typeof(string),
                        Visible=false,
                        Searchable=false,
                        Exportable=true,
                        Hidedlg=true,
                    },
                    new JQGridColumn 
                    { 
                        DataField = "LastActivityDate", 
                        HeaderText="最近活动日期",
                        DataType = typeof(DateTime),
                    },
                    new JQGridColumn 
                    { 
                        DataField = "LastLoginDate", 
                        HeaderText="最近的登录日期",
                        DataType=typeof(DateTime),
                    },
                    new JQGridColumn
                    { 
                        DataField = "CreateDate", 
                        HeaderText="创建日期",
                        DataType=typeof(DateTime),
                    }
                }
            };
        }
    }
}