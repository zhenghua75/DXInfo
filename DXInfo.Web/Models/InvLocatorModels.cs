using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class InvLocatorModels
    {
    }
    public class InvLocatorGridModel
    {
        public JQGrid InvLocatorGrid { get; set; }
        public InvLocatorGridModel()
        {
            InvLocatorGrid = new JQGrid()
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
                        DataField="RdFlag",
                        HeaderText="收发标志",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="ILDate",
                        HeaderText="单据日期",
                        DataType=typeof(DateTime),
                    },   
                    new JQGridColumn()
                    {
                        DataField="WhName",
                        HeaderText="仓库",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="VenName",
                        HeaderText="供应商",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="LocatorName",
                        HeaderText="货位",
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
                        Editable=false,
                        Searchable=false,
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
                    new JQGridColumn()
                    {
                        DataField="SalesmanName",
                        HeaderText="经手人",
                        DataType=typeof(string),
                    },
                }
            };
            InvLocatorGrid.ToolBarSettings.ShowRefreshButton = true;
            InvLocatorGrid.ToolBarSettings.ShowColumnChooser = true;
            InvLocatorGrid.ToolBarSettings.ShowExcelButton = true;
            InvLocatorGrid.SearchDialogSettings.MultipleSearch = true;
            InvLocatorGrid.ToolBarSettings.ShowSearchButton = true;
        }
    }
}