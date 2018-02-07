using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class CurrentStockModels
    {
    }
    public class CurrentStockGridModel
    {
        public JQGrid CurrentStockGrid { get; set; }
        public CurrentStockGridModel()
        {
            CurrentStockGrid = new JQGrid()
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
                        DataField="CategoryCode",
                        HeaderText="分类编码",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="CategoryName",
                        HeaderText="分类名称",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="InvCode",
                        HeaderText="存货编码",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="InvName",
                        HeaderText="存货名称",
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
                        DataField="Num",
                        HeaderText="数量",
                        DataType=typeof(decimal),
                        Formatter = new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="Price",
                        HeaderText="单价",
                        DataType=typeof(decimal),
                        Formatter= new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="Amount",
                        HeaderText="金额",
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
                    new JQGridColumn()
                    {
                        DataField="StopFlag",
                        HeaderText="是否冻结",
                        DataType=typeof(bool),
                    },
                }
            };
            CurrentStockGrid.AppearanceSettings.Caption = "库存现存量";
            CurrentStockGrid.ToolBarSettings.ShowRefreshButton = true;
            CurrentStockGrid.ToolBarSettings.ShowSearchButton = true;
            CurrentStockGrid.ToolBarSettings.ShowColumnChooser = true;
            CurrentStockGrid.ToolBarSettings.ShowExcelButton = true;
            CurrentStockGrid.SearchDialogSettings.MultipleSearch = true;
            CurrentStockGrid.AppearanceSettings.ShowRowNumbers = true;
        }
    }
}