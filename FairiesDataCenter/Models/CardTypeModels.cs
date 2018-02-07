using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace ynhnTransportManage.Models
{
    public class CardTypeModels
    {
    }
    public class CardTypeGridModel
    {
        public EntityJQGrid CardTypeGrid { get; set; }
        public CardTypeGridModel()
        {
            CardTypeGrid = new EntityJQGrid()
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
                        DataField="IsMoney",
                        HeaderText="不允许现金消费",
                        DataType=typeof(bool),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="IsVirtual",
                        HeaderText="是否虚拟卡",
                        DataType=typeof(bool),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="CardNoRule",
                        HeaderText="卡号规则",
                        DataType=typeof(string),
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="Comment",
                        HeaderText="描述",
                        DataType=typeof(string),
                        Editable=true
                    },
                }
            };
        }
    }
}