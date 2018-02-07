using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class LocatorBookModels
    {
    }
    public class LocatorBookGridModel
    {
        public JQGrid LocatorBookGrid { get; set; }
        public LocatorBookGridModel()
        {
            LocatorBookGrid = new JQGrid()
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
            LocatorBookGrid.ToolBarSettings.ShowRefreshButton = true;
            LocatorBookGrid.ToolBarSettings.ShowSearchButton = true;
            LocatorBookGrid.ToolBarSettings.ShowColumnChooser = true;
            LocatorBookGrid.ToolBarSettings.ShowExcelButton = true;
            LocatorBookGrid.SearchDialogSettings.MultipleSearch = true;
            LocatorBookGrid.AppearanceSettings.ShowRowNumbers = true;
        }
    }
}