using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class AssInfoModels
    {
    }
    public class AssInfoGridModel
    {
        public JQGrid AssInfoGrid { get; set; }
        public AssInfoGridModel()
        {
            AssInfoGrid = new JQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="iAssID",
                        DataType=typeof(int),
                        PrimaryKey=true,
                        Visible=false,
                    }, 
                    new JQGridColumn()
                    {
                        DataField="vcCardID",
                        HeaderText="会员卡号",
                        DataType=typeof(string),
                        PrimaryKey=true,
                        Editable=true,
                        EditFieldAttributes = new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } },
                    }, 
                    new JQGridColumn()
                    {
                        DataField="vcAssName",
                        HeaderText="会员姓名",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="vcLinkPhone",
                        HeaderText="联系电话",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="vcSpell",
                        HeaderText="拼音简码",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="vcAssType",
                        HeaderText="会员类型",
                        DataType=typeof(string),
                        Exportable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="vcAssState",
                        HeaderText="会员状态",
                        DataType=typeof(string),
                        Exportable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="nCharge",
                        HeaderText="当前余额",
                        DataType=typeof(decimal),
                    },
                    new JQGridColumn()
                    {
                        DataField="iIgValue",
                        HeaderText="当前积分",
                        DataType=typeof(decimal),
                    },
                    new JQGridColumn()
                    {
                        DataField="vcDeptID",
                        HeaderText="门店",
                        DataType=typeof(string),
                        Exportable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="dtCreateDate",
                        HeaderText="创建日期",
                        DataType=typeof(DateTime),
                    },
                    new JQGridColumn()
                    {
                        DataField="vcAssNbr",
                        HeaderText="身份证号",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="vcLinkAddress",
                        HeaderText="联系地址",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="vcEmail",
                        HeaderText="Email",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="dtOperDate",
                        HeaderText="操作日期",
                        DataType=typeof(DateTime),
                    },
                    new JQGridColumn()
                    {
                        DataField="vcComments",
                        HeaderText="备注",
                        DataType=typeof(string),
                        Editable=true,
                    },
                }
            };
            AssInfoGrid.ToolBarSettings.ShowEditButton = true;
        }
    }
}