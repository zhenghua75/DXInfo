using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace ynhnTransportManage.Models
{
    public class ConsumePointModels
    {
    }
    public class ConsumePointGridModel
    {
        public EntityJQGrid ConsumePointGrid { get; set; }
        public ConsumePointGridModel()
        {
            ConsumePointGrid = new EntityJQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        EditActionIconsColumn=true,
                        EditActionIconsSettings = new EditActionIconsSettings(){ ShowEditIcon=true,ShowDeleteIcon=true},
                    },
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType=typeof(Guid),
                        PrimaryKey=true,
                        Visible=false,
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptId",
                        HeaderText="部门",
                        DataType=typeof(Guid),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptName",
                        HeaderText="部门",
                        DataType=typeof(string),
                        Searchable=false,
                        Visible=false,
                        Exportable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Category",
                        HeaderText="分类",
                        DataType=typeof(Guid),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="CategoryName",
                        HeaderText="分类",
                        DataType=typeof(string),
                        Searchable=false,
                        Visible=false,
                        Exportable=true,
                    },                    
                    new JQGridColumn()
                    {
                        DataField="Amount",
                        HeaderText="金额",
                        DataType=typeof(decimal),
                        Editable=true,                        
                        EditDialogFieldSuffix="(*)",                        
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator()
                        },
                    },        
                    new JQGridColumn()
                    {
                        DataField="Point",
                        HeaderText="积分",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",                        
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator()
                        },
                    },       
                    new JQGridColumn()
                    {
                        DataField="Comment",
                        HeaderText="描述",
                        DataType=typeof(string),
                        Editable=true
                    }
                }
            };
        }
    }
}