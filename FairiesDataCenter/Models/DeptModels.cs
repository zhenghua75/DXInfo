using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace ynhnTransportManage.Models
{
    public class DeptModels
    {
    }

    public class DeptGridModel
    {
        public EntityJQGrid DeptsGrid { get; set; }
        public DeptGridModel()
        {
            DeptsGrid = new EntityJQGrid()
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
                        DataField="DeptId",
                        PrimaryKey=true,
                        Visible=false,
                        Searchable=false
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
                        HeaderText="是否门店单价",
                        DataField="IsDeptPrice",
                        DataType=typeof(bool),
                        Editable=true,
                        Width=100,
                    },
                    new JQGridColumn()
                    {
                        
                        HeaderText="部门",
                        DataField="OrganizationId",
                        DataType=typeof(Guid),
                        Editable=true,                       
                    },
                    new JQGridColumn()
                    {
                        HeaderText="部门",
                        DataField="OrganizationName",
                        Searchable=false,
                        Visible=false,
                        Exportable=true,
                        DataType=typeof(string),                       
                    },
                    new JQGridColumn()
                    {
                        
                        HeaderText="门店类型",
                        DataField="DeptType",
                        DataType=typeof(int),
                        Editable=true,                       
                    },
                    new JQGridColumn()
                    {
                        HeaderText="门店类型",
                        DataField="DeptTypeName",
                        Searchable=false,
                        Visible=false,
                        Exportable=true,
                        DataType=typeof(string),                       
                    },
                }
            };
        }

    }
}