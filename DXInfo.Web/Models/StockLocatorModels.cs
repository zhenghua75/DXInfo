using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class StockLocatorModels
    {
    }
    public class StockLocatorGridModel
    {
        public JQGrid StockLocatorGrid { get; set; }
        public StockLocatorGridModel()
        {
            StockLocatorGrid = new JQGrid()
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
                        DataField="WhName",
                        HeaderText="仓库",
                        DataType=typeof(string),
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
                        HeaderText="台账结存数量",
                        DataType=typeof(decimal),
                        Formatter=new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="LocatorNum",
                        HeaderText="货位结存数量",
                        DataType=typeof(decimal),
                        Formatter=new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="NumDif",
                        HeaderText="结存数量差额",
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
                        DataField="ShelfLifeType",
                        HeaderText="保质期单位",
                        DataType=typeof(int),
                    },
                    new JQGridColumn()
                    {
                        DataField="InvalidDate",
                        HeaderText="失效日期",
                        DataType=typeof(DateTime),
                    },
                }
            };
            StockLocatorGrid.ToolBarSettings.ShowRefreshButton = true;
            StockLocatorGrid.ToolBarSettings.ShowSearchButton = true;
            StockLocatorGrid.ToolBarSettings.ShowExcelButton = true;
            StockLocatorGrid.ToolBarSettings.ShowColumnChooser = true;
            StockLocatorGrid.SearchDialogSettings.MultipleSearch = true;
            StockLocatorGrid.AppearanceSettings.ShowRowNumbers = true;
        }
    }
}