using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class StockBookModels
    {
    }
    public class StockBookGridModel
    {
        public JQGrid StockBookGrid { get; set; }
        public StockBookGridModel()
        {
            StockBookGrid = new JQGrid()
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
                        HeaderText="月份",
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
                        DataField="InvName",
                        HeaderText="存货",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="Specs",
                        HeaderText="规格型号",
                        DataType=typeof(string),
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="STUnitName",
                        HeaderText="计量单位",
                        DataType=typeof(string),
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="InNum",
                        HeaderText="收入数量",
                        DataType=typeof(decimal),
                        Formatter=new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="OutNum",
                        HeaderText="发出数量",
                        DataType=typeof(decimal),
                        Formatter=new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="Num",
                        HeaderText="结存数量",
                        DataType=typeof(decimal),
                        Formatter=new DigitFormatter(),
                    },                    
                }
            };
            StockBookGrid.ToolBarSettings.ShowRefreshButton = true;
            StockBookGrid.ToolBarSettings.ShowSearchButton = true;
            StockBookGrid.ToolBarSettings.ShowColumnChooser = true;
            StockBookGrid.ToolBarSettings.ShowExcelButton = true;
            StockBookGrid.SearchDialogSettings.MultipleSearch = true;
            StockBookGrid.AppearanceSettings.ShowRowNumbers = true;
        }
    }
}