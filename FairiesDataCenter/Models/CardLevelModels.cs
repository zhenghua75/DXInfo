using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace ynhnTransportManage.Models
{
    public class CardLevelModels
    {
    }
    public class CardLevelGridModel
    {
        public EntityJQGrid CardLevelGrid { get; set; }
        public CardLevelGridModel()
        {
            CardLevelGrid = new EntityJQGrid()
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
                        DataField="Discount",
                        HeaderText="折扣",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(%)(*)",                        
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator()
                        },
                    },     
                    new JQGridColumn()
                    {
                        DataField="BeginLetter",
                        HeaderText="卡号首字母",
                        DataType=typeof(string),
                        Editable=true,
                    },      
                    new JQGridColumn()
                    {
                        DataField="Point",
                        HeaderText="积分",
                        DataType=typeof(decimal),
                        Editable=true,
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