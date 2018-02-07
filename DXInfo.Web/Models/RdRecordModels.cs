using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class RdRecordModels
    {
    }
    
    public class BatchInventoryGridModel
    {
        //public Guid WhId { get; set; }
        public JQGrid BatchInventoryGrid { get; set; }
        public BatchInventoryGridModel()
        {
            BatchInventoryGrid = new JQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    //new JQGridColumn()
                    //{
                    //    DataField="Id",
                    //    DataType=typeof(Guid),
                    //    PrimaryKey=true,
                    //    Visible=false,
                    //    Searchable=false
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="MVId",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Searchable=false
                    //},
                    new JQGridColumn()
                    {
                        DataField="InvId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Visible=false,
                        //EditDialogFieldSuffix="(*)",
                        //EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        //{
                        //    new RequiredValidator()                            
                        //},
                        PrimaryKey=true,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="WhId",
                        DataType=typeof(Guid),
                        Visible=false,
                        Searchable=false,
                        SearchToolBarOperation = SearchOperation.IsEqualTo,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvName",
                        HeaderText="存货",
                        DataType=typeof(string),
                        Searchable=true,
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
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator(),
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="Memo",
                        HeaderText="行备注",
                        DataType=typeof(string),
                        Editable=true,
                    },
                }
            };
            BatchInventoryGrid.ToolBarSettings.ShowAddButton = false;
            BatchInventoryGrid.ToolBarSettings.ShowEditButton = false;
            BatchInventoryGrid.ToolBarSettings.ShowDeleteButton = false;
            BatchInventoryGrid.ToolBarSettings.ShowRefreshButton = true;
            BatchInventoryGrid.ToolBarSettings.ShowSearchButton = true;
            BatchInventoryGrid.SearchDialogSettings.MultipleSearch = true;
            BatchInventoryGrid.AppearanceSettings.ShowRowNumbers = true;
            BatchInventoryGrid.ClientSideEvents.SerializeGridData = "serializeGridData";
            BatchInventoryGrid.ClientSideEvents.RowSelect = "editRow";
            BatchInventoryGrid.PagerSettings.PageSize = 1000;
            BatchInventoryGrid.PagerSettings.PageSizeOptions = "[100,200,300,400,1000]";
        }
    }
    public class BatchInventoryList
    {
        //public Guid Id { get; set; }
        //public Guid MVId { get; set; }
        public Guid InvId { get; set; }
        public Guid WhId { get; set; }
        public string InvCode { get; set; }
        public string InvName { get; set; }
        public string Specs { get; set; }
        public string STUnitName { get; set; }
        public decimal Num { get; set; }
        public string Memo { get; set; }
    }
    public class BatchWarehouseInventoryGridModel
    {
        //public Guid WhId { get; set; }
        public JQGrid BatchWarehouseInventoryGrid { get; set; }
        public BatchWarehouseInventoryGridModel()
        {
            BatchWarehouseInventoryGrid = new JQGrid()
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
                    //new JQGridColumn()
                    //{
                    //    DataField="MVId",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Searchable=false
                    //},
                    new JQGridColumn()
                    {
                        DataField="InvId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Visible=false,
                        //EditDialogFieldSuffix="(*)",
                        //EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        //{
                        //    new RequiredValidator()                            
                        //},
                        //PrimaryKey=true,
                        Searchable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="WhId",
                        DataType=typeof(Guid),
                        Visible=false,
                        Searchable=false,
                        SearchToolBarOperation = SearchOperation.IsEqualTo,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvName",
                        HeaderText="存货",
                        DataType=typeof(string),
                        Searchable=true,
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
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator(),
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="Memo",
                        HeaderText="行备注",
                        DataType=typeof(string),
                        Editable=true,
                    },
                }
            };
            BatchWarehouseInventoryGrid.ToolBarSettings.ShowAddButton = false;
            BatchWarehouseInventoryGrid.ToolBarSettings.ShowEditButton = false;
            BatchWarehouseInventoryGrid.ToolBarSettings.ShowDeleteButton = false;
            BatchWarehouseInventoryGrid.ToolBarSettings.ShowRefreshButton = true;
            BatchWarehouseInventoryGrid.ToolBarSettings.ShowSearchButton = true;
            BatchWarehouseInventoryGrid.SearchDialogSettings.MultipleSearch = true;
            BatchWarehouseInventoryGrid.AppearanceSettings.ShowRowNumbers = true;
            BatchWarehouseInventoryGrid.ClientSideEvents.SerializeGridData = "serializeGridData";
            BatchWarehouseInventoryGrid.ClientSideEvents.RowSelect = "editRow";
            BatchWarehouseInventoryGrid.PagerSettings.PageSize = 1000;
            BatchWarehouseInventoryGrid.PagerSettings.PageSizeOptions = "[100,200,300,400,1000]";
        }
    }
    public class BatchWarehouseInventoryList
    {
        public System.Data.SqlTypes.SqlGuid Id { get; set; }
        //public Guid MVId { get; set; }
        public Guid InvId { get; set; }
        public Guid WhId { get; set; }
        public string InvCode { get; set; }
        public string InvName { get; set; }
        public string Specs { get; set; }
        public string STUnitName { get; set; }
        public decimal Num { get; set; }
        public string Memo { get; set; }
    }
    
}