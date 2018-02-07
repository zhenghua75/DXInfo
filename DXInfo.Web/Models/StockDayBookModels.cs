using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class StockDayBookModels
    {
    }
    public class StockDayBookGridModel
    {
        public JQGrid StockDayBookGrid { get; set; }
        public StockDayBookGridModel()
        {
            StockDayBookGrid = new JQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
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
                        HeaderText="单据号",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="RdDate",
                        HeaderText="单据日期",
                        DataType=typeof(DateTime),   
                    },
                    new JQGridColumn()
                    {
                        DataField="RdName",
                        HeaderText="收发类别",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="WhName",
                        HeaderText="仓库",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="SalesmanName",
                        HeaderText="业务员",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="IsVerify",
                        HeaderText="是否审核",
                        DataType=typeof(bool),
                    },
                    new JQGridColumn()
                    {
                        DataField="VerifyDate",
                        HeaderText="审核日期",
                        DataType=typeof(DateTime),      
                    },
                    new JQGridColumn()
                    {
                        DataField="InvName",
                        HeaderText="存货",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="Specs",
                        HeaderText="规格型号",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="STUnitName",
                        HeaderText="计量单位",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="Num",
                        HeaderText="数量",
                        DataType=typeof(decimal),
                        Formatter=new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="Batch",
                        HeaderText="批号",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="MadeDate",
                        HeaderText="生产日期",
                        DataType=typeof(DateTime), 
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLife",
                        HeaderText="保质期",
                        DataType=typeof(int),
                    },  
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeTypeName",
                        HeaderText="保质期单位",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="InvalidDate",
                        HeaderText="失效日期",
                        DataType=typeof(DateTime),
                    },
                }
            };
            StockDayBookGrid.ToolBarSettings.ShowRefreshButton = true;
            StockDayBookGrid.ToolBarSettings.ShowSearchButton = true;
            StockDayBookGrid.ToolBarSettings.ShowExcelButton = true;
            StockDayBookGrid.ToolBarSettings.ShowColumnChooser = true;
            StockDayBookGrid.SearchDialogSettings.MultipleSearch = true;
            StockDayBookGrid.AppearanceSettings.ShowRowNumbers = true;
        }
    }
}