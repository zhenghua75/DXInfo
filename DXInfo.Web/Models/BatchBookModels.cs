using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class BatchBookModels
    {
    }
    public class BatchBookGridModel
    {
        public JQGrid BatchBookGrid { get; set; }
        public BatchBookGridModel()
        {
            BatchBookGrid = new JQGrid()
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
                    },
                    new JQGridColumn()
                    {
                        DataField="STUnitName",
                        HeaderText="计量单位",
                        DataType=typeof(string),
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
            BatchBookGrid.ToolBarSettings.ShowRefreshButton = true;
            BatchBookGrid.ToolBarSettings.ShowSearchButton = true;
            BatchBookGrid.ToolBarSettings.ShowExcelButton = true;
            BatchBookGrid.ToolBarSettings.ShowColumnChooser = true;
            BatchBookGrid.SearchDialogSettings.MultipleSearch = true;
            BatchBookGrid.AppearanceSettings.ShowRowNumbers = true;
        }
    }
}