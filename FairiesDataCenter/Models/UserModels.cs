using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Trirand.Web.Mvc;

namespace ynhnTransportManage.Models
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
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        EditActionIconsColumn=true,
                        EditActionIconsSettings = new EditActionIconsSettings(){ ShowEditIcon=true,ShowDeleteIcon=false},
                    },
                    new JQGridColumn 
                    { 
                        DataField = "UserId", 
                        PrimaryKey = true,
                        Visible=false,
                        Searchable=false,
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
                        Width=100, 
                        HeaderText="门店",
                        DataType=typeof(string),
                        Searchable=false,
                        Visible=false,
                        Exportable=true,
                    },
                    new JQGridColumn 
                    { 
                        DataField = "LastActivityDate", 
                        HeaderText="最近活动日期",
                        Width = 150, 
                        DataType = typeof(DateTime),
                    },
                    new JQGridColumn 
                    {
                        DataField="IsApproved",                        
                        Width=100, 
                        HeaderText="是否启用",
                        DataType=typeof(bool),
                        Editable=true,
                    },
                    new JQGridColumn 
                    {
                        DataField="IsLockedOut",
                        Width=100, 
                        HeaderText="是否锁定",
                        DataType=typeof(bool)
                    },
                    new JQGridColumn 
                    { 
                        DataField = "LastLoginDate", 
                        Width = 150, 
                        HeaderText="最近的登录日期",
                        DataType=typeof(DateTime),
                    },
                    new JQGridColumn
                    { 
                        DataField = "CreateDate", 
                        Width = 150, 
                        HeaderText="创建日期",
                        DataType=typeof(DateTime),
                    }
                }
            };
        }
    }
}