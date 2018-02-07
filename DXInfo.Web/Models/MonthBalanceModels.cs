using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class MonthBalanceModels
    {
    }
    public class MonthBalanceGridModel
    {
        public EntityJQGrid MonthBalanceGrid { get; set; }
        public MonthBalanceGridModel()
        {
            MonthBalanceGrid = new EntityJQGrid()
            {
                Width=1300,
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType=typeof(Guid),
                        PrimaryKey=true,
                        Visible=false,
                        Searchable=false,
                        Viewable=false,
                        Hidedlg=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="MBDate",
                        HeaderText="单据日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Period",
                        HeaderText="周期",
                        DataType=typeof(Guid),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="PeriodCode",
                        HeaderText="周期",
                        DataType=typeof(string),
                        Visible=false,
                        Searchable=false,
                        Exportable=true,
                        Hidedlg=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="WhId",
                        HeaderText="仓库",
                        DataType=typeof(Guid),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="WhName",
                        HeaderText="仓库",
                        DataType=typeof(string),
                        Visible=false,
                        Searchable=false,
                        Exportable=true,
                    },                                        
                    new JQGridColumn()
                    {
                        DataField="Salesman",
                        HeaderText="经手人",
                        DataType=typeof(Guid),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="SalesmanName",
                        HeaderText="经手人",
                        DataType=typeof(string),
                        Visible=false,
                        Searchable=false,
                        Exportable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField ="IsVerify",
                        HeaderText="审核",
                        DataType=typeof(bool),
                    },
                    new JQGridColumn()
                    {
                        DataField="Memo",
                        HeaderText="备注",
                        DataType=typeof(string),
                        Editable=true,
                        Searchable=true,
                    },
                }
            };
            MonthBalanceGrid.ToolBarSettings.CustomButtons = new List<JQGridToolBarButton>()
            {
                new JQGridToolBarButton()
                {
                    Position = ToolBarButtonPosition.Last,
                    ToolTip="审核",
                    Text="<span class='ui-pg-button-text'>审核</span>",
                    OnClick="customButtonClicked",
                    ButtonIcon="ui-icon-check"
                },
                new JQGridToolBarButton()
                {
                    Position = ToolBarButtonPosition.Last,
                    ToolTip="弃审",
                    Text="<span class='ui-pg-button-text'>弃审</span>",
                    OnClick="customButtonClicked2",
                    ButtonIcon="ui-icon-close"
                }
            };
        }
    }
}