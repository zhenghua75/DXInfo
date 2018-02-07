using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class BatchStopModels
    {
    }
    public class BatchStopGridModel
    {
        public JQGrid BatchStopGrid { get; set; }
        public BatchStopGridModel()
        {
            BatchStopGrid = new JQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        EditActionIconsColumn = true,
                        EditActionIconsSettings = new EditActionIconsSettings
                        {
                            ShowDeleteIcon=false,
                            SaveOnEnterKeyPress = true                    
                        },
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
                        DataField="StopFlag",
                        HeaderText="是否冻结",
                        DataType=typeof(bool),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="WhName",
                        HeaderText="仓库",
                        DataType=typeof(string),
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvName",
                        HeaderText="存货",
                        DataType=typeof(string),
                        Searchable=false,
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
                        Searchable=false,
                    },       
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeType",
                        HeaderText="保质期单位",
                        DataType=typeof(int),
                        Visible=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeTypeName",
                        HeaderText="保质期单位",
                        DataType=typeof(string),
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvalidDate",
                        HeaderText="失效日期",
                        DataType=typeof(DateTime),
                    },
                }
            };
            BatchStopGrid.AppearanceSettings.AlternateRowBackground = true;    
            BatchStopGrid.ToolBarSettings.ShowRefreshButton = true;
            BatchStopGrid.ToolBarSettings.ShowSearchButton = true;
            BatchStopGrid.SearchDialogSettings.MultipleSearch = true;
            BatchStopGrid.AppearanceSettings.ShowRowNumbers = true;

        }
    }
}