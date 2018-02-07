using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Reflection;

namespace ynhnTransportManage.Models.StockManage
{
    public class StockMangeModels
    {
    }

    public class WarehouseGridModel
    {
        public JQGrid WarehouseGrid { get; set; }
        public WarehouseGridModel()
        {
            WarehouseGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
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
                        HeaderText="编码",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="Name",
                        HeaderText="名称",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },       
                    new JQGridColumn()
                    {
                        DataField="Dept",
                        HeaderText="部门",
                        DataType=typeof(Guid),
                        Editable=true,
                        Visible=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptName",
                        HeaderText="部门",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="Principal",
                        HeaderText="负责人",
                        DataType=typeof(string),
                        Editable=true                        
                    },
                    new JQGridColumn()
                    {
                        DataField="Tele",
                        HeaderText="电话",
                        DataType=typeof(string),
                        Editable=true                        
                    },
                    new JQGridColumn()
                    {
                        DataField="Address",
                        HeaderText="地址",
                        DataType=typeof(string),
                        Editable=true                        
                    },
                    new JQGridColumn()
                    {
                        DataField="Comment",
                        HeaderText="描述",
                        DataType=typeof(string),
                        Editable=true                        
                    },
                }
            };
            WarehouseGrid.AppearanceSettings.Caption = "库存仓库档案";
            WarehouseGrid.ToolBarSettings.ShowSearchButton = true;
            WarehouseGrid.ToolBarSettings.ShowAddButton = true;
            WarehouseGrid.ToolBarSettings.ShowEditButton = true;
            WarehouseGrid.ToolBarSettings.ShowDeleteButton = true;
            WarehouseGrid.ToolBarSettings.ShowRefreshButton = true;
            WarehouseGrid.ToolBarSettings.ShowExcelButton = true;
            WarehouseGrid.ToolBarSettings.ShowColumnChooser = true;
            WarehouseGrid.SearchDialogSettings.MultipleSearch = true;
        }
    }
    public class WarehouseDeptGridModel
    {
        public JQGrid WarehouseDeptGrid { get; set; }
        public WarehouseDeptGridModel()
        {
            WarehouseDeptGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
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
                        DataField="Dept",
                        HeaderText="门店",
                        DataType=typeof(Guid),
                        Editable=true,
                        Visible=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptName",
                        HeaderText="门店",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },        
                    new JQGridColumn()
                    {
                        DataField="Warehouse",
                        HeaderText="配料仓",
                        DataType=typeof(Guid),
                        Editable=true,
                        Visible=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="WarehouseName",
                        HeaderText="配料仓",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },                    
                }
            };
            WarehouseDeptGrid.AppearanceSettings.Caption = "库存部门配料仓";
            WarehouseDeptGrid.ToolBarSettings.ShowSearchButton = true;
            WarehouseDeptGrid.ToolBarSettings.ShowAddButton = true;
            WarehouseDeptGrid.ToolBarSettings.ShowEditButton = true;
            WarehouseDeptGrid.ToolBarSettings.ShowDeleteButton = true;
            WarehouseDeptGrid.ToolBarSettings.ShowRefreshButton = true;
            WarehouseDeptGrid.ToolBarSettings.ShowExcelButton = true;
            WarehouseDeptGrid.ToolBarSettings.ShowColumnChooser = true;
            WarehouseDeptGrid.SearchDialogSettings.MultipleSearch = true;
        }
    }
    public class WarehouseInventoryGridModel
    {
        public JQGrid WarehouseInventoryGrid { get; set; }
        public WarehouseInventoryGridModel()
        {
            WarehouseInventoryGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
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
                        DataField="Warehouse",
                        HeaderText="仓库",
                        DataType=typeof(Guid),
                        Editable=true,
                        Visible=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="WarehouseName",
                        HeaderText="仓库",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },    
                    new JQGridColumn()
                    {
                        DataField="Inventory",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Editable=true,
                        Visible=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="InventoryName",
                        HeaderText="存货",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },        
                }
            };
            
            WarehouseInventoryGrid.ToolBarSettings.ShowSearchButton = true;
            WarehouseInventoryGrid.ToolBarSettings.ShowAddButton = true;
            WarehouseInventoryGrid.ToolBarSettings.ShowEditButton = true;
            WarehouseInventoryGrid.ToolBarSettings.ShowDeleteButton = true;
            WarehouseInventoryGrid.ToolBarSettings.ShowRefreshButton = true;
            WarehouseInventoryGrid.ToolBarSettings.ShowExcelButton = true;
            WarehouseInventoryGrid.ToolBarSettings.ShowColumnChooser = true;
            WarehouseInventoryGrid.SearchDialogSettings.MultipleSearch = true;
            
            WarehouseInventoryGrid.AddDialogSettings.Height = 600;
            WarehouseInventoryGrid.AddDialogSettings.DataHeight = 500;
            WarehouseInventoryGrid.EditDialogSettings.Height = 600;
            WarehouseInventoryGrid.EditDialogSettings.DataHeight = 500;

            WarehouseInventoryGrid.AddDialogSettings.ClearAfterAdd = false;
        }
    }
    public class LocatorGridModel
    {
        public JQGrid LocatorGrid { get; set; }
        public LocatorGridModel()
        {
            LocatorGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
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
                        HeaderText="编码",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="Name",
                        HeaderText="名称",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },       
                    new JQGridColumn()
                    {
                        DataField="Warehouse",
                        HeaderText="仓库",
                        DataType=typeof(int),
                        Editable=true,
                        Visible=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="WarehouseName",
                        HeaderText="仓库",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="Comment",
                        HeaderText="描述",
                        DataType=typeof(string),
                        Editable=true                        
                    },
                }
            };
            LocatorGrid.AppearanceSettings.Caption = "库存货位档案";
            LocatorGrid.ToolBarSettings.ShowSearchButton = true;
            LocatorGrid.ToolBarSettings.ShowAddButton = true;
            LocatorGrid.ToolBarSettings.ShowEditButton = true;
            LocatorGrid.ToolBarSettings.ShowDeleteButton = true;
            LocatorGrid.ToolBarSettings.ShowRefreshButton = true;
            LocatorGrid.ToolBarSettings.ShowColumnChooser = true;
            LocatorGrid.ToolBarSettings.ShowExcelButton = true;
            LocatorGrid.SearchDialogSettings.MultipleSearch = true;
        }
    }

    public class VendorGridModel
    {
        public int VendorType { get; set; }
        public JQGrid VendorGrid { get; set; }
        public VendorGridModel()
        {
            VendorGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
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
                        DataField="VendorType",
                        DataType=typeof(int),
                        Visible=false,
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="Code",
                        HeaderText="编码",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="Name",
                        HeaderText="名称",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },      
                    new JQGridColumn()
                    {
                        DataField="Tel",
                        HeaderText="电话",
                        DataType=typeof(string),
                        Editable=true,
                    },   
                    new JQGridColumn()
                    {
                        DataField="Fax",
                        HeaderText="传真",
                        DataType=typeof(string),
                        Editable=true,
                    }, 
                    new JQGridColumn()
                    {
                        DataField="Phone",
                        HeaderText="手机",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Zip",
                        HeaderText="邮编",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Linkman",
                        HeaderText="联系人",
                        DataType=typeof(string),
                        Editable=true,
                    },                    
                    new JQGridColumn()
                    {
                        DataField="Address",
                        HeaderText="地址",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Email",
                        HeaderText="Email",
                        DataType=typeof(string),
                        Editable=true,
                    },
                }
            };
            
            VendorGrid.ToolBarSettings.ShowSearchButton = true;
            VendorGrid.ToolBarSettings.ShowAddButton = true;
            VendorGrid.ToolBarSettings.ShowEditButton = true;
            VendorGrid.ToolBarSettings.ShowDeleteButton = true;
            VendorGrid.ToolBarSettings.ShowRefreshButton = true;
            VendorGrid.ToolBarSettings.ShowExcelButton = true;
            VendorGrid.ToolBarSettings.ShowColumnChooser = true;
            VendorGrid.SearchDialogSettings.MultipleSearch = true;
        }
    }

    public class VouchAuthorityGridModel
    {
        public JQGrid VouchAuthorityGrid { get; set; }
        public VouchAuthorityGridModel()
        {
            VouchAuthorityGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="UserId",
                        DataType=typeof(Guid),
                        PrimaryKey=true,
                        HeaderText="姓名",
                        Visible=false,
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="UserName",
                        HeaderText="姓名",
                        Searchable=false,
                        DataType=typeof(string),
                    },      
                    
                    new JQGridColumn()
                    {
                        DataField="AuthorityType",
                        HeaderText="库存单据权限类型",
                        DataType=typeof(int),
                        Editable=true,
                        Visible = false,
                    },
                    new JQGridColumn()
                    {
                        DataField="AuthorityTypeName",
                        HeaderText="库存单据权限类型",
                        DataType=typeof(string),
                        Editable=false,
                        Width=140,
                        
                    },
                }
            };
            VouchAuthorityGrid.AppearanceSettings.Caption = "库存单据权限";
            VouchAuthorityGrid.ToolBarSettings.ShowSearchButton = true;
            VouchAuthorityGrid.ToolBarSettings.ShowAddButton = true;
            VouchAuthorityGrid.ToolBarSettings.ShowEditButton = true;
            VouchAuthorityGrid.ToolBarSettings.ShowDeleteButton = true;
            VouchAuthorityGrid.ToolBarSettings.ShowRefreshButton = true;
            VouchAuthorityGrid.ToolBarSettings.ShowColumnChooser = true;
            VouchAuthorityGrid.ToolBarSettings.ShowExcelButton = true;
            VouchAuthorityGrid.SearchDialogSettings.MultipleSearch = true;
        }
    }
    #region 收发记录表
    public class RdRecord
    {
        public Guid? Id { get; set; }
        public bool IsVerify { get; set; }
        public bool IsModify { get; set; }
        public DateTime? MakeTime { get; set; }
        [Display(Name = "入库单号"),Required()]
        public string Code { get; set; }
        [Display(Name = "入库日期"), Required()]
        public DateTime? RdDate { get; set; }

        [Display(Name = "仓库"), Required()]
        public Guid WhId { get; set; }

        [Display(Name = "到货单号")]
        public string ARVCode { get; set; }
        [Display(Name = "供货单位")]
        public Guid? VenId { get; set; }
        [Display(Name = "业务员"), Required()]
        public Guid Salesman { get; set; }
        [Display(Name = "到货日期")]
        public DateTime? ARVDate { get; set; }
        [Display(Name = "业务类型"),Required()]
        public string BusType { get; set; }
        [Display(Name = "审核日期")]
        public DateTime? VerifyDate { get; set; }
        [Display(Name = "备注")]
        public string Memo { get; set; }
        public string PTCode { get; set; }
        public string STCode { get; set; }
    }
    public class RdRecordGridModel
    {
        public DXInfo.Models.VouchType vouchType { get; set; }
        public JQGrid RdRecordGrid { get; set; }
        public RdRecordGridModel()
        {
            vouchType = new DXInfo.Models.VouchType();
            RdRecordGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType=typeof(Guid),
                        
                        Visible=false,
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="VouchType",
                        DataType=typeof(string),
                        Visible=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="Code",
                        HeaderText="入库单号",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="RdDate",
                        HeaderText="入库日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    new JQGridColumn()
                    {
                        DataField="WhId",
                        HeaderText="仓库",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="WhName",
                        HeaderText="仓库",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="ARVCode",
                        HeaderText="到货单号",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="ArvDate",
                        HeaderText="到货日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    new JQGridColumn()
                    {
                        DataField="VenId",
                        HeaderText="供货单位",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="VenName",
                        HeaderText="供货单位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="DeptId",
                    //    HeaderText="部门",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="DeptName",
                    //    HeaderText="部门",
                    //    DataType=typeof(string),
                    //    Editable=false,
                    //    Searchable=false,
                    //},

                    new JQGridColumn() 
                    {
                    DataField = "BusType",
                    HeaderText = "入库类别",
                    DataType = typeof(string),
                    Editable = true,
                    Visible = false,
                    EditDialogFieldSuffix = "(*)",
                    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField = "BusTypeName",
                        HeaderText = "入库类别",
                        DataType = typeof(string),
                        Editable = false,
                        Searchable = false,
                    },
                    new JQGridColumn()
                    {
                        DataField="Salesman",
                        HeaderText="业务员",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="SalesmanName",
                        HeaderText="业务员",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="IsVerify",
                        HeaderText="是否审核",
                        DataType=typeof(bool),
                        Editable=false,
                        Formatter = new CheckBoxFormatter(),
                        SearchType = SearchType.DropDown,
                    },
                    new JQGridColumn()
                    {
                        DataField="VerifyDate",
                        HeaderText="审核日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    new JQGridColumn()
                    {
                        DataField="Memo",
                        HeaderText="备注",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="SubId",
                        DataType=typeof(Guid),
                        PrimaryKey=true,
                        Visible=false,
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="InvId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="InvName",
                        HeaderText="存货名称",
                        DataType=typeof(string),
                        Editable=false,
                        //Searchable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Specs",
                        HeaderText="规格型号",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },                    
                    //new JQGridColumn()
                    //{
                    //    DataField="STUnit",
                    //    HeaderText="计量单位",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
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
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="Price",
                        HeaderText="单价",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    },
                    new JQGridColumn()
                    {
                        DataField="Amount",
                        HeaderText="金额",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    },
                    new JQGridColumn()
                    {
                        DataField="Batch",
                        HeaderText="批号",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="MadeDate",
                        HeaderText="生产日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}",
                        //Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLife",
                        HeaderText="保质期",
                        DataType=typeof(int),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator()
                        },
                        Searchable=false,
                    },       
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeType",
                        HeaderText="保质期单位",
                        DataType=typeof(int),
                        Editable=true,
                        Visible=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeTypeName",
                        HeaderText="保质期单位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvalidDate",
                        HeaderText="失效日期",
                        DataType=typeof(DateTime),
                        Editable=false,
                        Searchable=false,
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="Locator",
                    //    HeaderText="货位",
                    //    DataType=typeof(Guid),
                    //    Editable=true,
                    //    Visible=false,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    new JQGridColumn()
                    {
                        DataField="LocatorName",
                        HeaderText="货位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="SubMemo",
                        HeaderText="行备注",
                        DataType=typeof(string),
                    },
                }
            };
            RdRecordGrid.ToolBarSettings.ShowRefreshButton = true;
            RdRecordGrid.ToolBarSettings.ShowSearchButton = true;
            RdRecordGrid.ToolBarSettings.ShowExcelButton = true;
            RdRecordGrid.ToolBarSettings.ShowColumnChooser = true;
            RdRecordGrid.SearchDialogSettings.MultipleSearch = true;
            RdRecordGrid.AppearanceSettings.ShowRowNumbers = true;
            RdRecordGrid.AppearanceSettings.Caption = "采购入库单";
            RdRecordGrid.ClientSideEvents.RowDoubleClick = "DblClick";
            RdRecordGrid.ClientSideEvents.BeforeRefresh = "beforeRefresh";
            RdRecordGrid.ClientSideEvents.SerializeGridData = "serializeGridData";
        }
    }
    public class RdRecordsGridModel
    {
        public JQGrid RdRecordsGrid { get; set; }
        
        public RdRecordsGridModel()
        {            
            RdRecordsGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
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
                        DataField="RdId",
                        DataType=typeof(Guid),
                        Visible=false,
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="InvId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        //Width=800,
                        EditDialogFieldSuffix="(*)<div id='somediv'></div><a id='aInvId'><span id='asInvId' class='ui-icon ui-icon-plus' style='position:absolute; top:2px; right:25px; '></span></a>",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        
                    },
                    new JQGridColumn()
                    {
                        DataField="InvName",
                        HeaderText="存货",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="Specs",
                        HeaderText="规格型号",
                        DataType=typeof(string),
                        Editable=true,
                        Searchable=false,
                        //Width=300,
                    },    
                    new JQGridColumn()
                    {
                        DataField="STUnitName",
                        HeaderText="计量单位",
                        DataType=typeof(string),
                        Editable=true,
                        Searchable=false,
                    },   
                    new JQGridColumn()
                    {
                        DataField="DueNum",
                        HeaderText="应发数",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new NumberValidator(),
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
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
                        DataField="Price",
                        HeaderText="单价",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator(),
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    },
                    
                    new JQGridColumn()
                    {
                        DataField="Amount",
                        HeaderText="金额",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator(),
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    },
                    new JQGridColumn()
                    {
                        DataField="Batch",
                        HeaderText="批号",
                        DataType=typeof(string),
                        Editable=true,
                        //EditDialogFieldSuffix="(*)",
                        //EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        //{
                        //    new RequiredValidator()                            
                        //},
                    },
                    new JQGridColumn()
                    {
                        DataField="MadeDate",
                        HeaderText="生产日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLife",
                        HeaderText="保质期",
                        DataType=typeof(int),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator()
                        },
                    },       
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeType",
                        HeaderText="保质期单位",
                        DataType=typeof(int),
                        Editable=true,
                        Visible=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeTypeName",
                        HeaderText="保质期单位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvalidDate",
                        HeaderText="失效日期",
                        DataType=typeof(DateTime),
                        Editable=false,
                        Searchable=false,
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    new JQGridColumn()
                    {
                        DataField="Locator",
                        HeaderText="货位",
                        DataType=typeof(Guid),
                        Editable=true,
                        Visible=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="LocatorName",
                        HeaderText="货位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="AvaNum",
                        HeaderText="可用量",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                        Visible=false,
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
            RdRecordsGrid.ToolBarSettings.ShowAddButton = true;
            RdRecordsGrid.ToolBarSettings.ShowEditButton = true;
            RdRecordsGrid.ToolBarSettings.ShowDeleteButton = true;
            RdRecordsGrid.ToolBarSettings.ShowRefreshButton = true;
            RdRecordsGrid.SearchDialogSettings.MultipleSearch = true;
            RdRecordsGrid.AppearanceSettings.ShowRowNumbers = true;
            RdRecordsGrid.ClientSideEvents.BeforeAddDialogSubmit = "beforeSubmit";
            RdRecordsGrid.ClientSideEvents.BeforeEditDialogSubmit = "beforeSubmit";
            RdRecordsGrid.ClientSideEvents.BeforeDelDialogSubmit = "beforeDelSubmit";
            RdRecordsGrid.ClientSideEvents.BeforeRefresh = "beforeRefresh";
            RdRecordsGrid.ClientSideEvents.SerializeGridData = "serializeGridData";
            RdRecordsGrid.ClientSideEvents.BeforeAddDialogShown = "beforeShow";
            RdRecordsGrid.ClientSideEvents.AfterEditDialogShown = "beforeShow";
            RdRecordsGrid.ClientSideEvents.AfterClickPgButtons = "afterclickPgButtons";
            RdRecordsGrid.EditDialogSettings.Width = 450;
            RdRecordsGrid.AddDialogSettings.Width = 450;
        }
    }

    public class BatchInventoryGridModel
    {
        public Guid WhId { get; set; }
        public JQGrid BatchInventoryGrid { get; set; }
        public BatchInventoryGridModel()
        {
            BatchInventoryGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
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
        public Guid WhId { get; set; }
        public JQGrid BatchWarehouseInventoryGrid { get; set; }
        public BatchWarehouseInventoryGridModel()
        {
            BatchWarehouseInventoryGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
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
    public class RdRecordModel
    {
        public RdRecordsGridModel rdRecordsGridModel{get;set;}
        public RdRecord rdRecord { get; set; }
        public DXInfo.Models.VouchType vouchType { get; set; }
        public bool IsBatch { get; set; }
        public bool IsShelfLife { get; set; }
        public bool IsLocator { get; set; }
        public RdRecordModel()
        {
            rdRecordsGridModel = new RdRecordsGridModel();
            rdRecord = new RdRecord();
        }
    }
    #endregion

    #region 调拨单
    public class TransVouch
    {
        public Guid? Id { get; set; }
        public bool IsVerify { get; set; }
        public bool IsModify { get; set; }
        public DateTime? MakeTime { get; set; }

        [Display(Name = "单据号"), Required()]
        public string Code { get; set; }
        [Display(Name = "单据日期"), Required()]
        public DateTime? TVDate { get; set; }

        [Display(Name = "转出仓库"), Required()]
        public Guid OutWhId { get; set; }
        [Display(Name = "转入仓库"), Required()]
        public Guid InWhId { get; set; }

        [Display(Name = "经手人"), Required()]
        public Guid Salesman { get; set; }

        [Display(Name = "审核日期")]
        public DateTime? VerifyDate { get; set; }

        [Display(Name = "备注")]
        public string Memo { get; set; }

    }
    public class TransVouchs 
    {
        public JQGrid TransVouchsGrid { get; set; }
        public TransVouchs()
        {
            TransVouchsGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
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
                        DataField="TVId",
                        DataType=typeof(Guid),
                        Visible=false,
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="InvId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="InvName",
                        HeaderText="存货",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="Specs",
                        HeaderText="规格型号",
                        DataType=typeof(string),
                        Editable=true,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="STUnitName",
                        HeaderText="计量单位",
                        DataType=typeof(string),
                        Editable=true,
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
                        DataField="Price",
                        HeaderText="单价",
                        DataType=typeof(decimal),
                        //Editable=true,
                        Visible=false,
                        Searchable=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    },
                    new JQGridColumn()
                    {
                        DataField="Amount",
                        HeaderText="金额",
                        DataType=typeof(decimal),
                        //Editable=true,
                        Visible=false,
                        Searchable=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    },
                    new JQGridColumn()
                    {
                        DataField="Batch",
                        HeaderText="批号",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="MadeDate",
                        HeaderText="生产日期",
                        DataType=typeof(DateTime),
                        Editable=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLife",
                        HeaderText="保质期",
                        DataType=typeof(int),
                        Editable=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator()
                        },
                    },       
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeType",
                        HeaderText="保质期单位",
                        DataType=typeof(int),
                        Editable=false,
                        Visible=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeTypeName",
                        HeaderText="保质期单位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvalidDate",
                        HeaderText="失效日期",
                        DataType=typeof(DateTime),
                        Editable=false,
                        Searchable=false,
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    new JQGridColumn()
                    {
                        DataField="Locator",
                        HeaderText="货位",
                        DataType=typeof(Guid),
                        Editable=true,
                        Visible=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="LocatorName",
                        HeaderText="货位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="AvaNum",
                        HeaderText="可用量",
                        DataType=typeof(string),
                        Editable=true,
                        Searchable=false,
                        Visible=false,
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
            TransVouchsGrid.ToolBarSettings.ShowAddButton = true;
            TransVouchsGrid.ToolBarSettings.ShowEditButton = true;
            TransVouchsGrid.ToolBarSettings.ShowDeleteButton = true;
            TransVouchsGrid.ToolBarSettings.ShowRefreshButton = true;
            TransVouchsGrid.SearchDialogSettings.MultipleSearch = true;
            TransVouchsGrid.AppearanceSettings.ShowRowNumbers = true;
            TransVouchsGrid.ClientSideEvents.BeforeAddDialogSubmit = "beforeSubmit";
            TransVouchsGrid.ClientSideEvents.BeforeEditDialogSubmit = "beforeSubmit";
            TransVouchsGrid.ClientSideEvents.BeforeDelDialogSubmit = "beforeDelSubmit";
            TransVouchsGrid.ClientSideEvents.BeforeRefresh = "beforeRefresh";
            TransVouchsGrid.ClientSideEvents.SerializeGridData = "serializeGridData";
            TransVouchsGrid.ClientSideEvents.AfterClickPgButtons = "afterclickPgButtons";
            TransVouchsGrid.EditDialogSettings.Width = 450;
            TransVouchsGrid.AddDialogSettings.Width = 450;
            
        }
    }
    public class TransVouchModel
    {
        public TransVouch transVouch { get; set; }
        public TransVouchs transVouchs { get; set; }
        public DXInfo.Models.VouchType vouchType { get; set; }
        public bool IsBatch { get; set; }
        public bool IsLocator { get; set; }
        public bool IsShelfLife { get; set; }
        public TransVouchModel()
        {
            transVouch = new TransVouch();
            transVouchs = new TransVouchs();
        }
    }
    public class TransVouchGridModel
    {
        public JQGrid TransVouchGrid { get; set; }
        public TransVouchGridModel()
        {
            TransVouchGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType=typeof(Guid),
                        
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
                        DataField="TVDate",
                        HeaderText="单据日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },                    
                    new JQGridColumn()
                    {
                        DataField="OutWhId",
                        HeaderText="转出仓库",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="OutWhName",
                        HeaderText="转出仓库",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="InWhId",
                        HeaderText="转入仓库",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="InWhName",
                        HeaderText="转入仓库",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="Salesman",
                        HeaderText="业务员",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="SalesmanName",
                        HeaderText="业务员",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="IsVerify",
                        HeaderText="是否审核",
                        DataType=typeof(bool),
                        Editable=false,
                        Formatter = new CheckBoxFormatter(),
                        SearchType = SearchType.DropDown,
                    },
                    new JQGridColumn()
                    {
                        DataField="VerifyDate",
                        HeaderText="审核日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    new JQGridColumn()
                    {
                        DataField="Memo",
                        HeaderText="备注",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="SubId",
                        DataType=typeof(Guid),
                        PrimaryKey=true,
                        Visible=false,
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="InvId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="InvName",
                        HeaderText="存货",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="Specs",
                        HeaderText="规格型号",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
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
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },  
                    new JQGridColumn()
                    {
                        DataField="Price",
                        HeaderText="单价",
                        DataType=typeof(decimal),
                        //Editable=true,
                        Visible=false,
                        Searchable=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    },
                    new JQGridColumn()
                    {
                        DataField="Amount",
                        HeaderText="金额",
                        DataType=typeof(decimal),
                        //Editable=true,
                        Visible=false,
                        Searchable=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    },
                    new JQGridColumn()
                    {
                        DataField="Batch",
                        HeaderText="批号",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="MadeDate",
                        HeaderText="生产日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}",
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLife",
                        HeaderText="保质期",
                        DataType=typeof(int),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator()
                        },
                        Searchable=false,
                    },       
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeType",
                        HeaderText="保质期单位",
                        DataType=typeof(int),
                        Editable=true,
                        Visible=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeTypeName",
                        HeaderText="保质期单位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvalidDate",
                        HeaderText="失效日期",
                        DataType=typeof(DateTime),
                        Editable=false,
                        Searchable=false,
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="Locator",
                    //    HeaderText="货位",
                    //    DataType=typeof(Guid),
                    //    Editable=true,
                    //    Visible=false,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    new JQGridColumn()
                    {
                        DataField="LocatorName",
                        HeaderText="货位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="SubMemo",
                        HeaderText="行备注",
                        DataType=typeof(string),
                    },
                }
            };
            TransVouchGrid.ToolBarSettings.ShowRefreshButton = true;
            TransVouchGrid.ToolBarSettings.ShowSearchButton = true;
            TransVouchGrid.ToolBarSettings.ShowColumnChooser = true;
            TransVouchGrid.ToolBarSettings.ShowExcelButton = true;
            TransVouchGrid.SearchDialogSettings.MultipleSearch = true;
            TransVouchGrid.AppearanceSettings.ShowRowNumbers = true;
            TransVouchGrid.AppearanceSettings.Caption = "搜索库存调拨单";
            TransVouchGrid.ClientSideEvents.RowDoubleClick = "DblClick"; 
        }
    }
    #endregion

    #region 不合格品记录单
    public class ScrapVouch
    {
        public Guid? Id { get; set; }
        public bool IsVerify { get; set; }
        public bool IsModify { get; set; }
        public DateTime? MakeTime { get; set; }

        [Display(Name = "单据号"), Required()]
        public string Code { get; set; }
        [Display(Name = "日期"), Required()]
        public DateTime? SVDate { get; set; }

        [Display(Name = "仓库"), Required()]
        public Guid WhId { get; set; }

        //[Display(Name = "部门"), Required()]
        //public Guid DeptId { get; set; }

        [Display(Name = "经手人"), Required()]
        public Guid Salesman { get; set; }

        [Display(Name = "审核日期")]
        public DateTime? VerifyDate { get; set; }

        [Display(Name = "备注")]
        public string Memo { get; set; }
    }
    public class ScrapVouchs
    {
        public JQGrid ScrapVouchsGrid { get; set; }
        public ScrapVouchs()
        {
            ScrapVouchsGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
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
                        DataField="SVId",
                        DataType=typeof(Guid),
                        Visible=false,
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="InvId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="InvName",
                        HeaderText="存货",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="Specs",
                        HeaderText="规格型号",
                        DataType=typeof(string),
                        Editable=true,
                        Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="MainUnit",
                    //    HeaderText="计量单位",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="MainUnitName",
                    //    HeaderText="计量单位",
                    //    DataType=typeof(string),
                    //    Editable=false,
                    //    Searchable=false,
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="STUnit",
                    //    HeaderText="库存单位",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    new JQGridColumn()
                    {
                        DataField="STUnitName",
                        HeaderText="计量单位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="ExchRate",
                    //    HeaderText="换算率",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="Quantity",
                    //    HeaderText="数量",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
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
                        DataField="Price",
                        HeaderText="单价",
                        DataType=typeof(decimal),
                        //Editable=true,
                        Visible=false,
                        Searchable=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    },
                    new JQGridColumn()
                    {
                        DataField="Amount",
                        HeaderText="金额",
                        DataType=typeof(decimal),
                        //Editable=true,
                        Visible=false,
                        Searchable=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    },
                    new JQGridColumn()
                    {
                        DataField="Batch",
                        HeaderText="批号",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="MadeDate",
                        HeaderText="生产日期",
                        DataType=typeof(DateTime),
                        Editable=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLife",
                        HeaderText="保质期",
                        DataType=typeof(int),
                        Editable=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator()
                        },
                    },       
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeType",
                        HeaderText="保质期单位",
                        DataType=typeof(int),
                        Editable=false,
                        Visible=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeTypeName",
                        HeaderText="保质期单位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvalidDate",
                        HeaderText="失效日期",
                        DataType=typeof(DateTime),
                        Editable=false,
                        Searchable=false,
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    new JQGridColumn()
                    {
                        DataField="Locator",
                        HeaderText="货位",
                        DataType=typeof(Guid),
                        Editable=true,
                        Visible=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="LocatorName",
                        HeaderText="货位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="InLocator",
                    //    HeaderText="入库货位",
                    //    DataType=typeof(Guid),
                    //    Editable=true,
                    //    Visible=false,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="InLocatorName",
                    //    HeaderText="入库货位",
                    //    DataType=typeof(string),
                    //    Editable=false,
                    //    Searchable=false,
                    //},
                    new JQGridColumn()
                    {
                        DataField="AvaNum",
                        HeaderText="可用量",
                        DataType=typeof(string),
                        Editable=true,
                        Searchable=false,
                        Visible=false,
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
            ScrapVouchsGrid.ToolBarSettings.ShowAddButton = true;
            ScrapVouchsGrid.ToolBarSettings.ShowEditButton = true;
            ScrapVouchsGrid.ToolBarSettings.ShowDeleteButton = true;
            ScrapVouchsGrid.ToolBarSettings.ShowRefreshButton = true;
            ScrapVouchsGrid.SearchDialogSettings.MultipleSearch = true;
            ScrapVouchsGrid.AppearanceSettings.ShowRowNumbers = true;
            ScrapVouchsGrid.ClientSideEvents.BeforeAddDialogSubmit = "beforeSubmit";
            ScrapVouchsGrid.ClientSideEvents.BeforeEditDialogSubmit = "beforeSubmit";
            ScrapVouchsGrid.ClientSideEvents.BeforeDelDialogSubmit = "beforeDelSubmit";
            ScrapVouchsGrid.ClientSideEvents.BeforeRefresh = "beforeRefresh";
            ScrapVouchsGrid.ClientSideEvents.SerializeGridData = "serializeGridData";
            ScrapVouchsGrid.ClientSideEvents.AfterClickPgButtons = "afterclickPgButtons";
            ScrapVouchsGrid.EditDialogSettings.Width = 450;
            ScrapVouchsGrid.AddDialogSettings.Width = 450;
            
        }
    }
    public class ScrapVouchModel
    {
        public ScrapVouch scrapVouch { get; set; }
        public ScrapVouchs scrapVouchs { get; set; }
        public DXInfo.Models.VouchType vouchType { get; set; }
        public bool IsBatch { get; set; }
        public bool IsLocator { get; set; }
        public bool IsShelfLife { get; set; }
        public ScrapVouchModel()
        {
            scrapVouch = new ScrapVouch();
            scrapVouchs = new ScrapVouchs();
        }
    }
    public class ScrapVouchGridModel
    {
        public JQGrid ScrapVouchGrid { get; set; }
        public ScrapVouchGridModel()
        {
            ScrapVouchGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType=typeof(Guid),
                        
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
                        DataField="SVDate",
                        HeaderText="单据日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    new JQGridColumn()
                    {
                        DataField="WhId",
                        HeaderText="仓库",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="WhName",
                        HeaderText="仓库",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="InWhId",
                    //    HeaderText="转入仓库",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="InWhName",
                    //    HeaderText="转入仓库",
                    //    DataType=typeof(string),
                    //    Editable=false,
                    //    Searchable=false,
                    //},
                    
                    //new JQGridColumn()
                    //{
                    //    DataField="DeptId",
                    //    HeaderText="部门",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="DeptName",
                    //    HeaderText="部门",
                    //    DataType=typeof(string),
                    //    Editable=false,
                    //    Searchable=false,
                    //},

                    new JQGridColumn()
                    {
                        DataField="Salesman",
                        HeaderText="业务员",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="SalesmanName",
                        HeaderText="业务员",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="IsVerify",
                        HeaderText="是否审核",
                        DataType=typeof(bool),
                        Editable=false,
                        Formatter = new CheckBoxFormatter(),
                        SearchType = SearchType.DropDown,
                    },
                    new JQGridColumn()
                    {
                        DataField="VerifyDate",
                        HeaderText="审核日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    new JQGridColumn()
                    {
                        DataField="Memo",
                        HeaderText="备注",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="SubId",
                        DataType=typeof(Guid),
                        PrimaryKey=true,
                        Visible=false,
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="InvId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="InvName",
                        HeaderText="存货",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="Specs",
                        HeaderText="规格型号",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="MainUnit",
                    //    HeaderText="计量单位",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="MainUnitName",
                    //    HeaderText="计量单位",
                    //    DataType=typeof(string),
                    //    Editable=false,
                    //    Searchable=false,
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="STUnit",
                    //    HeaderText="库存单位",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    new JQGridColumn()
                    {
                        DataField="STUnitName",
                        HeaderText="计量单位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="ExchRate",
                    //    HeaderText="换算率",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="Quantity",
                    //    HeaderText="数量",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    new JQGridColumn()
                    {
                        DataField="Num",
                        HeaderText="数量",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="Price",
                        HeaderText="单价",
                        DataType=typeof(decimal),
                        //Editable=true,
                        Visible=false,
                        Searchable=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    },
                    new JQGridColumn()
                    {
                        DataField="Amount",
                        HeaderText="金额",
                        DataType=typeof(decimal),
                        //Editable=true,
                        Visible=false,
                        Searchable=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    },
                    new JQGridColumn()
                    {
                        DataField="Batch",
                        HeaderText="批号",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="MadeDate",
                        HeaderText="生产日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}",
                        //Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLife",
                        HeaderText="保质期",
                        DataType=typeof(int),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator()
                        },
                        Searchable=false,
                    },       
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeType",
                        HeaderText="保质期单位",
                        DataType=typeof(int),
                        Editable=true,
                        Visible=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeTypeName",
                        HeaderText="保质期单位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvalidDate",
                        HeaderText="失效日期",
                        DataType=typeof(DateTime),
                        Editable=false,
                        Searchable=false,
                        DataFormatString="{0:yyyy-MM-dd}",
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="Locator",
                    //    HeaderText="货位",
                    //    DataType=typeof(Guid),
                    //    Editable=true,
                    //    Visible=false,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    new JQGridColumn()
                    {
                        DataField="LocatorName",
                        HeaderText="货位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=true,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="InLocator",
                    //    HeaderText="转入货位",
                    //    DataType=typeof(Guid),
                    //    Editable=true,
                    //    Visible=false,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="InLocatorName",
                    //    HeaderText="转入货位",
                    //    DataType=typeof(string),
                    //    Editable=false,
                    //    Searchable=false,
                    //},
                    new JQGridColumn()
                    {
                        DataField="SubMemo",
                        HeaderText="行备注",
                        DataType=typeof(string),
                    },
                }
            };
            ScrapVouchGrid.ToolBarSettings.ShowRefreshButton = true;
            ScrapVouchGrid.ToolBarSettings.ShowSearchButton = true;
            ScrapVouchGrid.ToolBarSettings.ShowExcelButton = true;
            ScrapVouchGrid.ToolBarSettings.ShowColumnChooser = true;
            ScrapVouchGrid.SearchDialogSettings.MultipleSearch = true;
            ScrapVouchGrid.AppearanceSettings.ShowRowNumbers = true;
            ScrapVouchGrid.AppearanceSettings.Caption = "搜索库存不合格品记录单";
            ScrapVouchGrid.ClientSideEvents.RowDoubleClick = "DblClick";
        }
    }
    #endregion

    public class PeriodGridModel
    {
        public JQGrid PeriodGrid { get; set; }
        public PeriodGridModel()
        {
            PeriodGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
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
                        HeaderText="编码",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="BeginDate",
                        HeaderText="开始日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    new JQGridColumn()
                    {
                        DataField="EndDate",
                        HeaderText="结束日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    new JQGridColumn()
                    {
                        DataField="Memo",
                        HeaderText="描述",
                        DataType=typeof(string),
                        Editable=true                        
                    },
                }
            };
            PeriodGrid.AppearanceSettings.Caption = "库存月结周期";
            PeriodGrid.ToolBarSettings.ShowSearchButton = true;
            PeriodGrid.ToolBarSettings.ShowDeleteButton = true;
            PeriodGrid.ToolBarSettings.ShowAddButton = true;
            PeriodGrid.ToolBarSettings.ShowEditButton = true;
            PeriodGrid.ToolBarSettings.ShowRefreshButton = true;
            PeriodGrid.ToolBarSettings.ShowColumnChooser = true;
            PeriodGrid.ToolBarSettings.ShowExcelButton = true;
            PeriodGrid.SearchDialogSettings.MultipleSearch = true;
        }
    }

    #region 盘点单
    public class CheckVouch
    {
        public Guid? Id { get; set; }
        public bool IsVerify { get; set; }
        public bool IsModify { get; set; }
        public DateTime? MakeTime { get; set; }

        [Display(Name = "盘点单号"), Required()]
        public string Code { get; set; }
        [Display(Name = "盘点日期"), Required()]
        public DateTime? CVDate { get; set; }

        //[Display(Name = "部门"), Required()]
        //public Guid DeptId { get; set; }
        [Display(Name = "仓库"), Required()]
        public Guid WhId { get; set; }

        [Display(Name = "经手人"), Required()]
        public Guid Salesman { get; set; }

        [Display(Name = "审核日期")]
        public DateTime? VerifyDate { get; set; }

        [Display(Name = "备注")]
        public string Memo { get; set; }
        //[Display(Name = "盘点周期"), Required()]
        //public Guid Period { get; set; }

    }
    public class CheckVouchs
    {
        public JQGrid CheckVouchsGrid { get; set; }
        public CheckVouchs()
        {
            CheckVouchsGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
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
                        DataField="CVId",
                        DataType=typeof(Guid),
                        Visible=false,
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="InvId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="InvName",
                        HeaderText="存货",
                        DataType=typeof(string),
                        Editable=true,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="Specs",
                        HeaderText="规格型号",
                        DataType=typeof(string),
                        Editable=true,
                        Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="MainUnit",
                    //    HeaderText="计量单位",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="MainUnitName",
                    //    HeaderText="计量单位",
                    //    DataType=typeof(string),
                    //    Editable=false,
                    //    Searchable=false,
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="STUnit",
                    //    HeaderText="库存单位",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    new JQGridColumn()
                    {
                        DataField="STUnitName",
                        HeaderText="计量单位",
                        DataType=typeof(string),
                        Editable=true,
                        Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="ExchRate",
                    //    HeaderText="换算率",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="Quantity",
                    //    HeaderText="盘点数量",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="CQuantity",
                    //    HeaderText="账面数量",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="AddInQuantity",
                    //    HeaderText="调整入库数量",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="AddOutQuantity",
                    //    HeaderText="调整出库数量",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    new JQGridColumn()
                    {
                        DataField="Num",
                        HeaderText="账面数",
                        DataType=typeof(decimal),
                        Editable=true,
                        Searchable=false,
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
                        DataField="CNum",
                        HeaderText="盘点数",
                        DataType=typeof(decimal),
                        Editable=true,
                        Searchable=false,
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
                        DataField="AddInNum",
                        HeaderText="调整入库数",
                        DataType=typeof(decimal),
                        Editable=false,
                        Searchable=false,
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
                        DataField="AddOutNum",
                        HeaderText="调整出库数",
                        DataType=typeof(decimal),
                        Editable=false,
                        Searchable=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator(),
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="Price",
                    //    HeaderText="单价",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="Amount",
                    //    HeaderText="盘点金额",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="CAmount",
                    //    HeaderText="账面金额",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="AddInAmount",
                    //    HeaderText="调整入库金额",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="AddOutAmount",
                    //    HeaderText="调整出库金额",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    new JQGridColumn()
                    {
                        DataField="Batch",
                        HeaderText="批号",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="MadeDate",
                        HeaderText="生产日期",
                        DataType=typeof(DateTime),
                        Editable=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}",
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLife",
                        HeaderText="保质期",
                        DataType=typeof(int),
                        Editable=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator()
                        },
                        Searchable=false,
                    },       
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeType",
                        HeaderText="保质期单位",
                        DataType=typeof(int),
                        Editable=false,
                        Visible=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeTypeName",
                        HeaderText="保质期单位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvalidDate",
                        HeaderText="失效日期",
                        DataType=typeof(DateTime),
                        Editable=false,
                        Searchable=false,
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="Locator",
                    //    HeaderText="货位",
                    //    DataType=typeof(Guid),
                    //    Editable=true,
                    //    Visible=false,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    new JQGridColumn()
                    {
                        DataField="LocatorName",
                        HeaderText="货位",
                        DataType=typeof(string),
                        Editable=true,
                        Searchable=true,
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
            CheckVouchsGrid.ToolBarSettings.ShowEditButton = true;
            CheckVouchsGrid.ToolBarSettings.ShowSearchButton = true;
            CheckVouchsGrid.ToolBarSettings.ShowRefreshButton = true;
            CheckVouchsGrid.SearchDialogSettings.MultipleSearch = true;
            CheckVouchsGrid.AppearanceSettings.ShowRowNumbers = true;
            CheckVouchsGrid.ClientSideEvents.BeforeAddDialogSubmit = "beforeSubmit";
            CheckVouchsGrid.ClientSideEvents.BeforeEditDialogSubmit = "beforeSubmit";
            CheckVouchsGrid.ClientSideEvents.BeforeDelDialogSubmit = "beforeDelSubmit";
            CheckVouchsGrid.ClientSideEvents.BeforeRefresh = "beforeRefresh";
            CheckVouchsGrid.ClientSideEvents.SerializeGridData = "serializeGridData";

        }
    }
    public class CheckVouchModel
    {
        public CheckVouch checkVouch { get; set; }
        public CheckVouchs checkVouchs { get; set; }
        public DXInfo.Models.VouchType vouchType { get; set; }
        public bool IsBatch { get; set; }
        public bool IsLocator { get; set; }
        public bool IsShelfLife { get; set; }
        public CheckVouchModel()
        {
            checkVouch = new CheckVouch();
            checkVouchs = new CheckVouchs();
        }
    }
    public class CheckVouchGridModel
    {
        public JQGrid CheckVouchGrid { get; set; }
        public CheckVouchGridModel()
        {
            CheckVouchGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType=typeof(Guid),
                        
                        Visible=false,
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="Code",
                        HeaderText="盘点单号",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="CVDate",
                        HeaderText="盘点日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="DeptId",
                    //    HeaderText="部门",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="DeptName",
                    //    HeaderText="部门",
                    //    DataType=typeof(string),
                    //    Editable=false,
                    //    Searchable=false,
                    //},                    
                    new JQGridColumn()
                    {
                        DataField="WhId",
                        HeaderText="仓库",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="WhName",
                        HeaderText="仓库",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },                              
                    new JQGridColumn()
                    {
                        DataField="Salesman",
                        HeaderText="业务员",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="SalesmanName",
                        HeaderText="业务员",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="IsVerify",
                        HeaderText="是否审核",
                        DataType=typeof(bool),
                        Editable=false,
                        Formatter = new CheckBoxFormatter(),
                        SearchType = SearchType.DropDown,
                    },
                    new JQGridColumn()
                    {
                        DataField="VerifyDate",
                        HeaderText="审核日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    new JQGridColumn()
                    {
                        DataField="Memo",
                        HeaderText="备注",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="SubId",
                        DataType=typeof(Guid),
                        PrimaryKey=true,
                        Visible=false,
                        Searchable=false
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="Locator",
                    //    HeaderText="货位",
                    //    DataType=typeof(Guid),
                    //    Editable=true,
                    //    Visible=false,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    new JQGridColumn()
                    {
                        DataField="LocatorName",
                        HeaderText="货位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=true,
                    },     
                    new JQGridColumn()
                    {
                        DataField="InvId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="InvName",
                        HeaderText="存货",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="Specs",
                        HeaderText="规格型号",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="MainUnit",
                    //    HeaderText="计量单位",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="MainUnitName",
                    //    HeaderText="计量单位",
                    //    DataType=typeof(string),
                    //    Editable=false,
                    //    Searchable=false,
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="STUnit",
                    //    HeaderText="库存单位",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    new JQGridColumn()
                    {
                        DataField="STUnitName",
                        HeaderText="库存单位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="ExchRate",
                    //    HeaderText="换算率",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="Quantity",
                    //    HeaderText="盘点数量",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="CQuantity",
                    //    HeaderText="账面数量",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="AddInQuantity",
                    //    HeaderText="调整入库数量",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="AddOutQuantity",
                    //    HeaderText="调整出库数量",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    new JQGridColumn()
                    {
                        DataField="Num",
                        HeaderText="账面数",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="CNum",
                        HeaderText="盘点数",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="AddInNum",
                        HeaderText="调整入库数",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="AddOutNum",
                        HeaderText="调整出库数",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="Price",
                    //    HeaderText="单价",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="Amount",
                    //    HeaderText="盘点金额",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="CAmount",
                    //    HeaderText="账面金额",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="AddInAmount",
                    //    HeaderText="调整入库金额",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="AddOutAmount",
                    //    HeaderText="调整出库金额",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    new JQGridColumn()
                    {
                        DataField="Batch",
                        HeaderText="批号",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="MadeDate",
                        HeaderText="生产日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}",
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLife",
                        HeaderText="保质期",
                        DataType=typeof(int),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator()
                        },
                        Searchable=false,
                    },       
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeType",
                        HeaderText="保质期单位",
                        DataType=typeof(int),
                        Editable=true,
                        Visible=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeTypeName",
                        HeaderText="保质期单位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvalidDate",
                        HeaderText="失效日期",
                        DataType=typeof(DateTime),
                        Editable=false,
                        //Searchable=false,
                        DataFormatString="{0:yyyy-MM-dd}",
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                    },
                    new JQGridColumn()
                    {
                        DataField="SubMemo",
                        HeaderText="行备注",
                        DataType=typeof(string),
                    },
                                   
                }
            };
            CheckVouchGrid.ToolBarSettings.ShowRefreshButton = true;
            CheckVouchGrid.ToolBarSettings.ShowSearchButton = true;
            CheckVouchGrid.ToolBarSettings.ShowExcelButton = true;
            CheckVouchGrid.ToolBarSettings.ShowColumnChooser = true;
            CheckVouchGrid.SearchDialogSettings.MultipleSearch = true;
            CheckVouchGrid.AppearanceSettings.ShowRowNumbers = true;
            CheckVouchGrid.AppearanceSettings.Caption = "搜索库存盘点单";
            CheckVouchGrid.ClientSideEvents.RowDoubleClick = "DblClick";
            CheckVouchGrid.ClientSideEvents.BeforeRefresh = "beforeRefresh";

        }
    }
    #endregion

    public class InvLocatorGridModel
    {
        public JQGrid InvLocatorGrid { get; set; }
        public InvLocatorGridModel()
        {
            InvLocatorGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
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
                        Editable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="ILDate",
                        HeaderText="单据日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },                    
                    new JQGridColumn()
                    {
                        DataField="WhId",
                        HeaderText="仓库",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="WhName",
                        HeaderText="仓库",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="VenId",
                        HeaderText="供应商",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="VenName",
                        HeaderText="供应商",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="Locator",
                    //    HeaderText="货位",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    new JQGridColumn()
                    {
                        DataField="LocatorName",
                        HeaderText="货位",
                        DataType=typeof(string),
                        Editable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="InvName",
                        HeaderText="存货",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="Specs",
                        HeaderText="规格型号",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="MainUnit",
                    //    HeaderText="计量单位",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="MainUnitName",
                    //    HeaderText="计量单位",
                    //    DataType=typeof(string),
                    //    Editable=false,
                    //    Searchable=false,
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="STUnit",
                    //    HeaderText="库存单位",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    new JQGridColumn()
                    {
                        DataField="STUnitName",
                        HeaderText="计量单位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="ExchRate",
                    //    HeaderText="换算率",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="Quantity",
                    //    HeaderText="数量",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    
                    new JQGridColumn()
                    {
                        DataField="Num",
                        HeaderText="数量",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    
                    new JQGridColumn()
                    {
                        DataField="Batch",
                        HeaderText="批号",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="MadeDate",
                        HeaderText="生产日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLife",
                        HeaderText="保质期",
                        DataType=typeof(int),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator()
                        },
                        Searchable=false,
                    },       
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeType",
                        HeaderText="保质期单位",
                        DataType=typeof(int),
                        Editable=true,
                        Visible=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeTypeName",
                        HeaderText="保质期单位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvalidDate",
                        HeaderText="失效日期",
                        DataType=typeof(DateTime),
                        Editable=false,
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },                    
                    new JQGridColumn()
                    {
                        DataField="Salesman",
                        HeaderText="经手人",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="SalesmanName",
                        HeaderText="经手人",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                }
            };
            InvLocatorGrid.AppearanceSettings.Caption = "库存货位流水账";
            InvLocatorGrid.ToolBarSettings.ShowRefreshButton = true;
            InvLocatorGrid.ToolBarSettings.ShowColumnChooser = true;
            InvLocatorGrid.ToolBarSettings.ShowExcelButton = true;
            InvLocatorGrid.SearchDialogSettings.MultipleSearch = true;
            InvLocatorGrid.ToolBarSettings.ShowSearchButton = true;
        }
    }

    #region 货位调整单
    public class AdjustLocatorVouch
    {
        public Guid? Id { get; set; }
        public bool IsVerify { get; set; }
        public bool IsModify { get; set; }
        public DateTime? MakeTime { get; set; }

        [Display(Name = "单据号"), Required()]
        public string Code { get; set; }
        [Display(Name = "日期"), Required()]
        public DateTime? ALVDate { get; set; }

        [Display(Name = "仓库"), Required()]
        public Guid WhId { get; set; }

        [Display(Name = "经手人"), Required()]
        public Guid Salesman { get; set; }

        [Display(Name = "审核日期")]
        public DateTime? VerifyDate { get; set; }

        [Display(Name = "备注")]
        public string Memo { get; set; }
    }
    public class AdjustLocatorVouchs
    {
        public JQGrid AdjustLocatorVouchsGrid { get; set; }
        public AdjustLocatorVouchs()
        {
            AdjustLocatorVouchsGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
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
                        DataField="ALVId",
                        DataType=typeof(Guid),
                        Visible=false,
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="InvId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="InvName",
                        HeaderText="存货",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="Specs",
                        HeaderText="规格型号",
                        DataType=typeof(string),
                        Editable=true,
                        Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="MainUnit",
                    //    HeaderText="计量单位",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="MainUnitName",
                    //    HeaderText="计量单位",
                    //    DataType=typeof(string),
                    //    Editable=false,
                    //    Searchable=false,
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="STUnit",
                    //    HeaderText="库存单位",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    new JQGridColumn()
                    {
                        DataField="STUnitName",
                        HeaderText="库存单位",
                        DataType=typeof(string),
                        Editable=true,
                        Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="ExchRate",
                    //    HeaderText="换算率",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="Quantity",
                    //    HeaderText="数量",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    new JQGridColumn()
                    {
                        DataField="Num",
                        HeaderText="件数",
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
                        DataField="Batch",
                        HeaderText="批号",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="MadeDate",
                        HeaderText="生产日期",
                        DataType=typeof(DateTime),
                        Editable=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLife",
                        HeaderText="保质期",
                        DataType=typeof(int),
                        Editable=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator()
                        },
                    },       
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeType",
                        HeaderText="保质期单位",
                        DataType=typeof(int),
                        Editable=false,
                        Visible=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeTypeName",
                        HeaderText="保质期单位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvalidDate",
                        HeaderText="失效日期",
                        DataType=typeof(DateTime),
                        Editable=false,
                        Searchable=false,
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    new JQGridColumn()
                    {
                        DataField="OutLocator",
                        HeaderText="调整前货位",
                        DataType=typeof(Guid),
                        Editable=true,
                        Visible=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="OutLocatorName",
                        HeaderText="调整前货位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="InLocator",
                        HeaderText="调整后货位",
                        DataType=typeof(Guid),
                        Editable=true,
                        Visible=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="InLocatorName",
                        HeaderText="调整后货位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="AvaNum",
                        HeaderText="可用量",
                        DataType=typeof(string),
                        Editable=true,
                        Searchable=false,
                        Visible=false,
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
            AdjustLocatorVouchsGrid.ToolBarSettings.ShowAddButton = true;
            AdjustLocatorVouchsGrid.ToolBarSettings.ShowEditButton = true;
            AdjustLocatorVouchsGrid.ToolBarSettings.ShowDeleteButton = true;
            AdjustLocatorVouchsGrid.ToolBarSettings.ShowRefreshButton = true;
            AdjustLocatorVouchsGrid.SearchDialogSettings.MultipleSearch = true;
            AdjustLocatorVouchsGrid.AppearanceSettings.ShowRowNumbers = true;
            AdjustLocatorVouchsGrid.ClientSideEvents.BeforeAddDialogSubmit = "beforeSubmit";
            AdjustLocatorVouchsGrid.ClientSideEvents.BeforeEditDialogSubmit = "beforeSubmit";
            AdjustLocatorVouchsGrid.ClientSideEvents.BeforeDelDialogSubmit = "beforeDelSubmit";
            AdjustLocatorVouchsGrid.ClientSideEvents.BeforeRefresh = "beforeRefresh";
            AdjustLocatorVouchsGrid.ClientSideEvents.SerializeGridData = "serializeGridData";
            AdjustLocatorVouchsGrid.ClientSideEvents.AfterClickPgButtons = "afterclickPgButtons";
            AdjustLocatorVouchsGrid.EditDialogSettings.Width = 450;
            AdjustLocatorVouchsGrid.AddDialogSettings.Width = 450;
        }
    }
    public class AdjustLocatorVouchModel
    {
        public AdjustLocatorVouch adjustLocatorVouch { get; set; }
        public AdjustLocatorVouchs adjustLocatorVouchs { get; set; }
        public DXInfo.Models.VouchType vouchType { get; set; }
        public bool IsBatch { get; set; }
        public bool IsLocator { get; set; }
        public bool IsShelfLife { get; set; }
        public AdjustLocatorVouchModel()
        {
            adjustLocatorVouch = new AdjustLocatorVouch();
            adjustLocatorVouchs = new AdjustLocatorVouchs();
        }
    }
    public class AdjustLocatorVouchGridModel
    {
        public JQGrid AdjustLocatorVouchGrid { get; set; }
        public AdjustLocatorVouchGridModel()
        {
            AdjustLocatorVouchGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType=typeof(Guid),
                        
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
                        DataField="ALVDate",
                        HeaderText="单据日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    new JQGridColumn()
                    {
                        DataField="WhId",
                        HeaderText="仓库",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="WhName",
                        HeaderText="仓库",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },                    
                    //new JQGridColumn()
                    //{
                    //    DataField="DeptId",
                    //    HeaderText="部门",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="DeptName",
                    //    HeaderText="部门",
                    //    DataType=typeof(string),
                    //    Editable=false,
                    //    Searchable=false,
                    //},
                    new JQGridColumn()
                    {
                        DataField="Salesman",
                        HeaderText="经手人",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="SalesmanName",
                        HeaderText="经手人",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="IsVerify",
                        HeaderText="是否审核",
                        DataType=typeof(bool),
                        Editable=false,
                        Formatter = new CheckBoxFormatter(),
                        SearchType = SearchType.DropDown,
                    },
                    new JQGridColumn()
                    {
                        DataField="VerifyDate",
                        HeaderText="审核日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    new JQGridColumn()
                    {
                        DataField="Memo",
                        HeaderText="备注",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="SubId",
                        DataType=typeof(Guid),
                        PrimaryKey=true,
                        Visible=false,
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="InvId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="InvName",
                        HeaderText="存货",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="Specs",
                        HeaderText="规格型号",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="MainUnit",
                    //    HeaderText="计量单位",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="MainUnitName",
                    //    HeaderText="计量单位",
                    //    DataType=typeof(string),
                    //    Editable=false,
                    //    Searchable=false,
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="STUnit",
                    //    HeaderText="库存单位",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    new JQGridColumn()
                    {
                        DataField="STUnitName",
                        HeaderText="计量单位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="ExchRate",
                    //    HeaderText="换算率",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="Quantity",
                    //    HeaderText="数量",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    new JQGridColumn()
                    {
                        DataField="Num",
                        HeaderText="数量",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },                    
                    new JQGridColumn()
                    {
                        DataField="Batch",
                        HeaderText="批号",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="MadeDate",
                        HeaderText="生产日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLife",
                        HeaderText="保质期",
                        DataType=typeof(int),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator()
                        },
                        Searchable=false,
                    },       
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeType",
                        HeaderText="保质期单位",
                        DataType=typeof(int),
                        Editable=true,
                        Visible=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeTypeName",
                        HeaderText="保质期单位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvalidDate",
                        HeaderText="失效日期",
                        DataType=typeof(DateTime),
                        Editable=false,
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="OutLocator",
                    //    HeaderText="调整前货位",
                    //    DataType=typeof(Guid),
                    //    Editable=true,
                    //    Visible=false,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    new JQGridColumn()
                    {
                        DataField="OutLocatorName",
                        HeaderText="调整前货位",
                        DataType=typeof(string),
                        Editable=false,
                        //Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="InLocator",
                    //    HeaderText="调整后货位",
                    //    DataType=typeof(Guid),
                    //    Editable=true,
                    //    Visible=false,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    new JQGridColumn()
                    {
                        DataField="InLocatorName",
                        HeaderText="调整后货位",
                        DataType=typeof(string),
                        Editable=false,
                        //Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="SubMemo",
                        HeaderText="行备注",
                        DataType=typeof(string),
                    },
                }
            };
            AdjustLocatorVouchGrid.ToolBarSettings.ShowRefreshButton = true;
            AdjustLocatorVouchGrid.ToolBarSettings.ShowSearchButton = true;
            AdjustLocatorVouchGrid.ToolBarSettings.ShowExcelButton = true;
            AdjustLocatorVouchGrid.ToolBarSettings.ShowColumnChooser = true;
            AdjustLocatorVouchGrid.SearchDialogSettings.MultipleSearch = true;
            AdjustLocatorVouchGrid.AppearanceSettings.ShowRowNumbers = true;
            AdjustLocatorVouchGrid.AppearanceSettings.Caption = "搜索库存货位调整单";
            AdjustLocatorVouchGrid.ClientSideEvents.RowDoubleClick = "DblClick";
            AdjustLocatorVouchGrid.ClientSideEvents.BeforeRefresh = "beforeRefresh";
        }
    }
    #endregion

    #region 配料单
    public class MixVouch
    {
        public Guid? Id { get; set; }
        public bool IsVerify { get; set; }
        public bool IsModify { get; set; }
        public DateTime? MakeTime { get; set; }

        [Display(Name = "单据号"), Required()]
        public string Code { get; set; }
        [Display(Name = "日期"), Required()]
        public DateTime? MVDate { get; set; }
        [Display(Name = "门店仓库"), Required()]
        public Guid InWhId { get; set; }
        [Display(Name = "配料仓库"), Required()]
        public Guid OutWhId { get; set; }
        [Display(Name = "经手人"), Required()]
        public Guid Salesman { get; set; }
        [Display(Name = "审核日期")]
        public DateTime? VerifyDate { get; set; }
        [Display(Name = "备注")]
        public string Memo { get; set; }
    }
    public class MixVouchs
    {
        public JQGrid MixVouchsGrid { get; set; }
        public MixVouchs()
        {
            MixVouchsGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
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
                        DataField="MVId",
                        DataType=typeof(Guid),
                        Visible=false,
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="InvId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="InvName",
                        HeaderText="存货",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="Specs",
                        HeaderText="规格型号",
                        DataType=typeof(string),
                        Editable=true,
                        Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="MainUnit",
                    //    HeaderText="计量单位",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="MainUnitName",
                    //    HeaderText="计量单位",
                    //    DataType=typeof(string),
                    //    Editable=false,
                    //    Searchable=false,
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="STUnit",
                    //    HeaderText="库存单位",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    new JQGridColumn()
                    {
                        DataField="STUnitName",
                        HeaderText="计量单位",
                        DataType=typeof(string),
                        Editable=true,
                        Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="ExchRate",
                    //    HeaderText="换算率",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="Quantity",
                    //    HeaderText="数量",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
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
                    //new JQGridColumn()
                    //{
                    //    DataField="Price",
                    //    HeaderText="单价",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="Amount",
                    //    HeaderText="金额",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="Batch",
                    //    HeaderText="批号",
                    //    DataType=typeof(string),
                    //    Editable=true,
                    //    //EditDialogFieldSuffix="(*)",
                    //    //EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    //{
                    //    //    new RequiredValidator()                            
                    //    //},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="MadeDate",
                    //    HeaderText="生产日期",
                    //    DataType=typeof(DateTime),
                    //    Editable=true,
                    //    //EditDialogFieldSuffix="(*)",
                    //    //EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    //{
                    //    //    //new RequiredValidator(),
                    //    //    new DateValidator()
                    //    //},
                    //    EditType= EditType.DatePicker,
                    //    EditorControlID="DatePicker",
                    //    SearchType = SearchType.DatePicker,
                    //    SearchControlID="DatePicker",
                    //    DataFormatString="{0:yyyy-MM-dd}"
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="ShelfLife",
                    //    HeaderText="保质期",
                    //    DataType=typeof(int),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator(),
                    //        new NumberValidator()
                    //    },
                    //},       
                    //new JQGridColumn()
                    //{
                    //    DataField="ShelfLifeType",
                    //    HeaderText="保质期单位",
                    //    DataType=typeof(int),
                    //    Editable=true,
                    //    Visible=false,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="ShelfLifeTypeName",
                    //    HeaderText="保质期单位",
                    //    DataType=typeof(string),
                    //    Editable=false,
                    //    Searchable=false,
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="InvalidDate",
                    //    HeaderText="失效日期",
                    //    DataType=typeof(DateTime),
                    //    Editable=false,
                    //    Searchable=false,
                    //    DataFormatString="{0:yyyy-MM-dd}"
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="Locator",
                    //    HeaderText="货位",
                    //    DataType=typeof(Guid),
                    //    Editable=true,
                    //    Visible=false,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="LocatorName",
                    //    HeaderText="货位",
                    //    DataType=typeof(string),
                    //    Editable=false,
                    //    Searchable=false,
                    //},  
                    new JQGridColumn()
                    {
                        DataField="Memo",
                        HeaderText="行备注",
                        DataType=typeof(string),
                        Editable=true,
                    },
                }
            };
            MixVouchsGrid.ToolBarSettings.ShowAddButton = true;
            MixVouchsGrid.ToolBarSettings.ShowEditButton = true;
            MixVouchsGrid.ToolBarSettings.ShowDeleteButton = true;
            MixVouchsGrid.ToolBarSettings.ShowRefreshButton = true;
            MixVouchsGrid.SearchDialogSettings.MultipleSearch = true;
            MixVouchsGrid.AppearanceSettings.ShowRowNumbers = true;
            MixVouchsGrid.ClientSideEvents.BeforeAddDialogSubmit = "beforeSubmit";
            MixVouchsGrid.ClientSideEvents.BeforeEditDialogSubmit = "beforeSubmit";
            MixVouchsGrid.ClientSideEvents.BeforeDelDialogSubmit = "beforeDelSubmit";
            MixVouchsGrid.ClientSideEvents.BeforeRefresh = "beforeRefresh";
            MixVouchsGrid.ClientSideEvents.SerializeGridData = "serializeGridData";
            MixVouchsGrid.ClientSideEvents.AfterClickPgButtons = "afterclickPgButtons";
            MixVouchsGrid.EditDialogSettings.Width = 450;
            MixVouchsGrid.AddDialogSettings.Width = 450;
            MixVouchsGrid.ToolBarSettings.CustomButtons.Add(new JQGridToolBarButton() {                
                Position = ToolBarButtonPosition.Last,
                ToolTip = "批量添加",
                Text = "批量添加",
                OnClick = "customButtonClicked",
                ButtonIcon = "ui-icon-extlink",                  
            });
            MixVouchsGrid.ToolBarSettings.CustomButtons.Add(new JQGridToolBarButton()
            {
                Position = ToolBarButtonPosition.Last,
                ToolTip = "日常补货",
                Text = "日常补货",
                OnClick = "customButtonClicked1",
                ButtonIcon = "ui-icon-extlink",
            });
        }
    }
    public class MixVouchModel
    {
        public MixVouch mixVouch { get; set; }
        public MixVouchs mixVouchs { get; set; }
        public DXInfo.Models.VouchType vouchType { get; set; }
        public MixVouchModel()
        {
            mixVouch = new MixVouch();
            mixVouchs = new MixVouchs();
        }
    }
    public class MixVouchGridModel
    {
        public JQGrid MixVouchGrid { get; set; }
        public MixVouchGridModel()
        {
            MixVouchGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType=typeof(Guid),
                        
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
                        DataField="MVDate",
                        HeaderText="单据日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    new JQGridColumn()
                    {
                        DataField="InWhId",
                        HeaderText="门店仓库",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="InWhName",
                        HeaderText="门店仓库",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },           
                    new JQGridColumn()
                    {
                        DataField="OutWhId",
                        HeaderText="配料仓库",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="OutWhName",
                        HeaderText="配料仓库",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },         
                    //new JQGridColumn()
                    //{
                    //    DataField="DeptId",
                    //    HeaderText="部门",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="DeptName",
                    //    HeaderText="部门",
                    //    DataType=typeof(string),
                    //    Editable=false,
                    //    Searchable=false,
                    //},
                    new JQGridColumn()
                    {
                        DataField="Salesman",
                        HeaderText="经手人",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="SalesmanName",
                        HeaderText="经手人",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="IsVerify",
                        HeaderText="是否审核",
                        DataType=typeof(bool),
                        Editable=false,
                        Formatter = new CheckBoxFormatter(),
                        SearchType = SearchType.DropDown,
                    },
                    new JQGridColumn()
                    {
                        DataField="VerifyDate",
                        HeaderText="审核日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    new JQGridColumn()
                    {
                        DataField="Memo",
                        HeaderText="备注",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="SubId",
                        DataType=typeof(Guid),
                        PrimaryKey=true,
                        Visible=false,
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="InvId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="InvName",
                        HeaderText="存货",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="Specs",
                        HeaderText="规格型号",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="MainUnit",
                    //    HeaderText="计量单位",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="MainUnitName",
                    //    HeaderText="计量单位",
                    //    DataType=typeof(string),
                    //    Editable=false,
                    //    Searchable=false,
                    //},
                    new JQGridColumn()
                    {
                        DataField="STUnit",
                        HeaderText="计量单位",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="STUnitName",
                        HeaderText="计量单位",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="ExchRate",
                    //    HeaderText="换算率",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="Quantity",
                    //    HeaderText="数量",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    new JQGridColumn()
                    {
                        DataField="Num",
                        HeaderText="件数",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },          
                    //new JQGridColumn()
                    //{
                    //    DataField="Price",
                    //    HeaderText="单价",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="Amount",
                    //    HeaderText="金额",
                    //    DataType=typeof(decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //    Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"},
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="Batch",
                    //    HeaderText="批号",
                    //    DataType=typeof(string),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="MadeDate",
                    //    HeaderText="生产日期",
                    //    DataType=typeof(DateTime),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator(),
                    //        new DateValidator()
                    //    },
                    //    EditType= EditType.DatePicker,
                    //    EditorControlID="DatePicker",
                    //    SearchType = SearchType.DatePicker,
                    //    SearchControlID="DatePicker",
                    //    DataFormatString="{0:yyyy-MM-dd}"
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="ShelfLife",
                    //    HeaderText="保质期",
                    //    DataType=typeof(int),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator(),
                    //        new NumberValidator()
                    //    },
                    //},       
                    //new JQGridColumn()
                    //{
                    //    DataField="ShelfLifeType",
                    //    HeaderText="保质期单位",
                    //    DataType=typeof(int),
                    //    Editable=true,
                    //    Visible=false,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="ShelfLifeTypeName",
                    //    HeaderText="保质期单位",
                    //    DataType=typeof(string),
                    //    Editable=false,
                    //    Searchable=false,
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="InvalidDate",
                    //    HeaderText="失效日期",
                    //    DataType=typeof(DateTime),
                    //    Editable=false,
                    //    Searchable=false,
                    //    DataFormatString="{0:yyyy-MM-dd}"
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="Locator",
                    //    HeaderText="货位",
                    //    DataType=typeof(Guid),
                    //    Editable=true,
                    //    Visible=false,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="LocatorName",
                    //    HeaderText="货位",
                    //    DataType=typeof(string),
                    //    Editable=false,
                    //    Searchable=false,
                    //},     
                    new JQGridColumn()
                    {
                        DataField="SubMemo",
                        HeaderText="行备注",
                        DataType=typeof(string),
                    },
                }
            };
            MixVouchGrid.ToolBarSettings.ShowRefreshButton = true;
            MixVouchGrid.ToolBarSettings.ShowSearchButton = true;
            MixVouchGrid.ToolBarSettings.ShowColumnChooser = true;
            MixVouchGrid.ToolBarSettings.ShowExcelButton = true;
            MixVouchGrid.SearchDialogSettings.MultipleSearch = true;
            MixVouchGrid.AppearanceSettings.ShowRowNumbers = true;
            MixVouchGrid.AppearanceSettings.Caption = "搜索库存配料单";
            MixVouchGrid.ClientSideEvents.RowDoubleClick = "DblClick";
            MixVouchGrid.ClientSideEvents.BeforeRefresh = "beforeRefresh";
        }
    }
    #endregion

    public class MonthBalanceGridModel
    {
        public JQGrid MonthBalanceGrid { get; set; }
        public MonthBalanceGridModel()
        {
            MonthBalanceGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
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
                        DataField="MBDate",
                        HeaderText="单据日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new DateValidator()
                        },
                        EditType= EditType.DatePicker,
                        EditorControlID="DatePicker",
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}"
                    },
                    new JQGridColumn()
                    {
                        DataField="Period",
                        HeaderText="周期",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="PeriodCode",
                        HeaderText="周期",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="DeptId",
                    //    HeaderText="部门",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="DeptName",
                    //    HeaderText="部门",
                    //    DataType=typeof(string),
                    //    Editable=false,
                    //    Searchable=false,
                    //},
                    new JQGridColumn()
                    {
                        DataField="WhId",
                        HeaderText="仓库",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="WhName",
                        HeaderText="仓库",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },                                        
                    new JQGridColumn()
                    {
                        DataField="Salesman",
                        HeaderText="经手人",
                        DataType=typeof(Guid),
                        Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="SalesmanName",
                        HeaderText="经手人",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField ="IsVerify",
                        HeaderText="审核",
                        DataType=typeof(bool),
                        Editable=false,
                        Searchable=true,
                        Formatter = new CheckBoxFormatter(),
                        SearchType = SearchType.DropDown,
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
            MonthBalanceGrid.AppearanceSettings.Caption = "库存月结";
            MonthBalanceGrid.ToolBarSettings.ShowAddButton = true;
            MonthBalanceGrid.ToolBarSettings.ShowEditButton = true;
            MonthBalanceGrid.ToolBarSettings.ShowDeleteButton = true;
            MonthBalanceGrid.ToolBarSettings.ShowRefreshButton = true;
            MonthBalanceGrid.SearchDialogSettings.MultipleSearch = true;
            MonthBalanceGrid.ToolBarSettings.ShowSearchButton = true;
            MonthBalanceGrid.ToolBarSettings.CustomButtons = new List<JQGridToolBarButton>()
            {
                new JQGridToolBarButton()
                {
                    Position = ToolBarButtonPosition.Last,
                    ToolTip="审核",
                    Text="审核",
                    OnClick="customButtonClicked",
                    ButtonIcon="ui-icon-check"
                },
                new JQGridToolBarButton()
                {
                    Position = ToolBarButtonPosition.Last,
                    ToolTip="弃审",
                    Text="弃审",
                    OnClick="customButtonClicked2",
                    ButtonIcon="ui-icon-close"
                }
            };
        }
    }

    public class BatchStopGridModel
    {
        public JQGrid BatchStopGrid { get; set; }
        public BatchStopGridModel()
        {
            BatchStopGrid = new JQGrid()
            {
                AutoWidth = true,
                ShrinkToFit=true,
                Height = Unit.Percentage(100),
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
                        Width=50,
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
                        Formatter = new CheckBoxFormatter(),
                        Editable=true,
                        EditType = EditType.CheckBox,
                        Width=70,
                    },
                    new JQGridColumn()
                    {
                        DataField="WhId",
                        HeaderText="仓库",
                        DataType=typeof(Guid),
                        Visible=false,
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
                        DataField="InvId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Visible=false,
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
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
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
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}",
                        Width=70,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLife",
                        HeaderText="保质期",
                        DataType=typeof(int),
                        Searchable=false,
                        Width=70,
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
                        Width=70,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvalidDate",
                        HeaderText="失效日期",
                        DataType=typeof(DateTime),
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}",
                        Width=70,
                    },
                }
            };
            BatchStopGrid.EditDialogSettings.Caption = "库存批次冻结";
            BatchStopGrid.AppearanceSettings.AlternateRowBackground = true;
            BatchStopGrid.AppearanceSettings.Caption = "库存批次冻结";
            //BatchStopGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.Top;
            //BatchStopGrid.ToolBarSettings.ShowEditButton = true;       
            BatchStopGrid.ToolBarSettings.ShowRefreshButton = true;
            BatchStopGrid.ToolBarSettings.ShowSearchButton = true;
            BatchStopGrid.SearchDialogSettings.MultipleSearch = true;
            BatchStopGrid.AppearanceSettings.ShowRowNumbers = true;

        }
    }

    public class InvalidDateGridModel
    {
        public JQGrid InvalidDateGrid { get; set; }
        public InvalidDateGridModel()
        {
            InvalidDateGrid = new JQGrid()
            {
                AutoWidth = true,
                ShrinkToFit = true,
                Height = Unit.Percentage(100),
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
                        DataField="StopFlag",
                        HeaderText="是否冻结",
                        DataType=typeof(bool),
                        Formatter = new CheckBoxFormatter(),
                        Width=70,
                    },
                    new JQGridColumn()
                    {
                        DataField="WhId",
                        HeaderText="仓库",
                        DataType=typeof(Guid),
                        Visible=false,
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
                        DataField="InvId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Visible=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvName",
                        HeaderText="存货",
                        DataType=typeof(string),
                        Searchable=false,
                        Editable=true,
                        EditFieldAttributes = new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } },
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
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
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
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}",
                        Width=70,
                        Editable=true,
                        EditType = EditType.DatePicker,
                        EditorControlID="DatePicker",
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLife",
                        HeaderText="保质期",
                        DataType=typeof(int),
                        Editable=true,
                        Width=70,
                        Searchable=false,
                    },       
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeType",
                        HeaderText="保质期单位",
                        DataType=typeof(int),
                        Visible=false,
                        Editable=true,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeTypeName",
                        HeaderText="保质期单位",
                        DataType=typeof(string),
                        Searchable=false,
                        Width=70,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvalidDate",
                        HeaderText="失效日期",
                        DataType=typeof(DateTime),
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}",
                        Width=70,
                        //Editable=true,
                        //EditType = EditType.DatePicker,
                        //EditorControlID="DatePicker",
                    },
                }
            };
            InvalidDateGrid.EditDialogSettings.Caption = "库存失效日期维护";
            InvalidDateGrid.AppearanceSettings.Caption = "库存失效日期维护";
            //InvalidDateGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.Top;
            InvalidDateGrid.ToolBarSettings.ShowEditButton = true;       
            InvalidDateGrid.ToolBarSettings.ShowRefreshButton = true;
            InvalidDateGrid.ToolBarSettings.ShowSearchButton = true;
            InvalidDateGrid.SearchDialogSettings.MultipleSearch = true;
            InvalidDateGrid.AppearanceSettings.ShowRowNumbers = true;

        }
    }

    public class StockLocatorGridModel
    {
        public JQGrid StockLocatorGrid { get; set; }
        public StockLocatorGridModel()
        {
            StockLocatorGrid = new JQGrid()
            {
                AutoWidth = true,
                ShrinkToFit = true,
                Height = Unit.Percentage(100),
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
                        DataField="WhId",
                        HeaderText="仓库",
                        DataType=typeof(Guid),
                        Visible=false,
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
                        DataField="InvId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Visible=false,
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
                        HeaderText="台账结存数量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="LocatorNum",
                        HeaderText="货位结存数量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="NumDif",
                        HeaderText="结存数量差额",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
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
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}",
                        Width=70,
                        Editable=true,
                        EditType = EditType.DatePicker,
                        EditorControlID="DatePicker",
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLife",
                        HeaderText="保质期",
                        DataType=typeof(int),
                        Editable=true,
                        Width=70,
                        Searchable=false,
                    },       
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeType",
                        HeaderText="保质期单位",
                        DataType=typeof(int),
                        Visible=false,
                        Editable=true,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeTypeName",
                        HeaderText="保质期单位",
                        DataType=typeof(string),
                        Searchable=false,
                        Width=70,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvalidDate",
                        HeaderText="失效日期",
                        DataType=typeof(DateTime),
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}",
                        Width=70,
                        //Editable=true,
                        //EditType = EditType.DatePicker,
                        //EditorControlID="DatePicker",
                    },
                }
            };
            StockLocatorGrid.AppearanceSettings.Caption = "库存账与货位账对账";
            //StockLocatorGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.Top;
            //StockLocatorGrid.ToolBarSettings.ShowEditButton = true;
            StockLocatorGrid.ToolBarSettings.ShowRefreshButton = true;
            StockLocatorGrid.ToolBarSettings.ShowSearchButton = true;
            StockLocatorGrid.ToolBarSettings.ShowExcelButton = true;
            StockLocatorGrid.ToolBarSettings.ShowColumnChooser = true;
            StockLocatorGrid.SearchDialogSettings.MultipleSearch = true;
            StockLocatorGrid.AppearanceSettings.ShowRowNumbers = true;
        }
    }

    public class CurrentStockGridModel
    {
        public JQGrid CurrentStockGrid { get; set; }
        public CurrentStockGridModel()
        {
            CurrentStockGrid = new JQGrid()
            {
                AutoWidth = true,
                //ShrinkToFit = true,
                Height = Unit.Percentage(100),
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
                        DataField="WhId",
                        HeaderText="仓库",
                        DataType=typeof(Guid),
                        Visible=false,
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
                        DataField="Category",
                        HeaderText="分类",
                        DataType=typeof(Guid),
                        Visible=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="CategoryCode",
                        HeaderText="分类编码",
                        DataType=typeof(string),
                        //Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="CategoryName",
                        HeaderText="分类名称",
                        DataType=typeof(string),
                        //Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Visible=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvCode",
                        HeaderText="存货编码",
                        DataType=typeof(string),
                        //Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvName",
                        HeaderText="存货名称",
                        DataType=typeof(string),
                        //Searchable=false,
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
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="Price",
                        HeaderText="单价",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="Amount",
                        HeaderText="金额",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
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
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}",
                        Width=70,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLife",
                        HeaderText="保质期",
                        DataType=typeof(int),
                        Editable=true,
                        Width=70,
                        Searchable=false,
                    },       
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeType",
                        HeaderText="保质期单位",
                        DataType=typeof(int),
                        Visible=false,
                        Editable=true,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeTypeName",
                        HeaderText="保质期单位",
                        DataType=typeof(string),
                        Searchable=false,
                        Width=70,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvalidDate",
                        HeaderText="失效日期",
                        DataType=typeof(DateTime),
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}",
                        Width=70,
                    },
                    new JQGridColumn()
                    {
                        DataField="StopFlag",
                        HeaderText="是否冻结",
                        DataType=typeof(bool),
                        Formatter = new CheckBoxFormatter(),
                        Width=70,
                    },
                }
            };
            CurrentStockGrid.AppearanceSettings.Caption = "库存现存量";
            //CurrentStockGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.Top;
            CurrentStockGrid.ToolBarSettings.ShowRefreshButton = true;
            CurrentStockGrid.ToolBarSettings.ShowSearchButton = true;
            CurrentStockGrid.ToolBarSettings.ShowColumnChooser = true;
            CurrentStockGrid.ToolBarSettings.ShowExcelButton = true;
            CurrentStockGrid.SearchDialogSettings.MultipleSearch = true;
            CurrentStockGrid.AppearanceSettings.ShowRowNumbers = true;
        }
    }

    public class StockDayBookGridModel
    {
        public JQGrid StockDayBookGrid { get; set; }
        public StockDayBookGridModel()
        {
            StockDayBookGrid = new JQGrid()
            {
                AutoWidth = true,
                ShrinkToFit = true,
                Height = Unit.Percentage(100),
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
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}",
                        Width=70,
                    },
                    new JQGridColumn()
                    {
                        DataField="RdName",
                        HeaderText="收发类别",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="WhId",
                        HeaderText="仓库",
                        DataType=typeof(Guid),
                        Visible=false,
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
                        DataField="Salesman",
                        HeaderText="业务员",
                        DataType=typeof(Guid),
                        Visible=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="SalesmanName",
                        HeaderText="业务员",
                        DataType=typeof(string),
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="IsVerify",
                        HeaderText="是否审核",
                        DataType=typeof(bool),
                        Editable=false,
                        Formatter = new CheckBoxFormatter(),
                        SearchType = SearchType.DropDown,
                    },
                    new JQGridColumn()
                    {
                        DataField="VerifyDate",
                        HeaderText="审核日期",
                        DataType=typeof(DateTime),               
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}",
                        Width=70,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Visible=false,
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
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
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
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}",
                        Width=70,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLife",
                        HeaderText="保质期",
                        DataType=typeof(int),
                        Editable=true,
                        Width=70,
                        Searchable=false,
                    },       
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeType",
                        HeaderText="保质期单位",
                        DataType=typeof(int),
                        Visible=false,
                        Editable=true,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeTypeName",
                        HeaderText="保质期单位",
                        DataType=typeof(string),
                        Searchable=false,
                        Width=70,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvalidDate",
                        HeaderText="失效日期",
                        DataType=typeof(DateTime),
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}",
                        Width=70,
                    },
                }
            };
            StockDayBookGrid.AppearanceSettings.Caption = "库存流水账";
            //StockDayBookGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.Top;
            StockDayBookGrid.ToolBarSettings.ShowRefreshButton = true;
            StockDayBookGrid.ToolBarSettings.ShowSearchButton = true;
            StockDayBookGrid.ToolBarSettings.ShowExcelButton = true;
            StockDayBookGrid.ToolBarSettings.ShowColumnChooser = true;
            StockDayBookGrid.SearchDialogSettings.MultipleSearch = true;
            StockDayBookGrid.AppearanceSettings.ShowRowNumbers = true;
        }
    }

    public class StockBookGridModel
    {
        public JQGrid StockBookGrid { get; set; }
        public StockBookGridModel()
        {
            StockBookGrid = new JQGrid()
            {
                AutoWidth = true,
                ShrinkToFit = true,
                Height = Unit.Percentage(100),
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
                        DataField="WhId",
                        HeaderText="仓库",
                        DataType=typeof(Guid),
                        Visible=false,
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
                        DataField="InvId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Visible=false,
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
                        DataField="InNum",
                        HeaderText="收入数量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="OutNum",
                        HeaderText="发出数量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="Num",
                        HeaderText="结存数量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="Batch",
                    //    HeaderText="批号",
                    //    DataType=typeof(string),
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="MadeDate",
                    //    HeaderText="生产日期",
                    //    DataType=typeof(DateTime),               
                    //    SearchType = SearchType.DatePicker,
                    //    SearchControlID="DatePicker",
                    //    DataFormatString="{0:yyyy-MM-dd}",
                    //    Width=70,
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="ShelfLife",
                    //    HeaderText="保质期",
                    //    DataType=typeof(int),
                    //    Editable=true,
                    //    Width=70,
                    //    Searchable=false,
                    //},       
                    //new JQGridColumn()
                    //{
                    //    DataField="ShelfLifeType",
                    //    HeaderText="保质期单位",
                    //    DataType=typeof(int),
                    //    Visible=false,
                    //    Editable=true,
                    //    Searchable=false,
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="ShelfLifeTypeName",
                    //    HeaderText="保质期单位",
                    //    DataType=typeof(string),
                    //    Searchable=false,
                    //    Width=70,
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="InvalidDate",
                    //    HeaderText="失效日期",
                    //    DataType=typeof(DateTime),
                    //    SearchType = SearchType.DatePicker,
                    //    SearchControlID="DatePicker",
                    //    DataFormatString="{0:yyyy-MM-dd}",
                    //    Width=70,
                    //},
                }
            };
            StockBookGrid.AppearanceSettings.Caption = "库存台账";
            //StockBookGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.Top;
            StockBookGrid.ToolBarSettings.ShowRefreshButton = true;
            StockBookGrid.ToolBarSettings.ShowSearchButton = true;
            StockBookGrid.ToolBarSettings.ShowColumnChooser = true;
            StockBookGrid.ToolBarSettings.ShowExcelButton = true;
            StockBookGrid.SearchDialogSettings.MultipleSearch = true;
            StockBookGrid.AppearanceSettings.ShowRowNumbers = true;
        }
    }

    public class BatchBookGridModel
    {
        public JQGrid BatchBookGrid { get; set; }
        public BatchBookGridModel()
        {
            BatchBookGrid = new JQGrid()
            {
                AutoWidth = true,
                ShrinkToFit = true,
                Height = Unit.Percentage(100),
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
                        DataField="WhId",
                        HeaderText="仓库",
                        DataType=typeof(Guid),
                        Visible=false,
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
                        DataField="InvId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Visible=false,
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
                        DataField="InNum",
                        HeaderText="收入数量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="OutNum",
                        HeaderText="发出数量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="Num",
                        HeaderText="结存数量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
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
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}",
                        Width=70,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLife",
                        HeaderText="保质期",
                        DataType=typeof(int),
                        Editable=true,
                        Width=70,
                        Searchable=false,
                    },       
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeType",
                        HeaderText="保质期单位",
                        DataType=typeof(int),
                        Visible=false,
                        Editable=true,
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeTypeName",
                        HeaderText="保质期单位",
                        DataType=typeof(string),
                        Searchable=false,
                        Width=70,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvalidDate",
                        HeaderText="失效日期",
                        DataType=typeof(DateTime),
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}",
                        Width=70,
                    },
                }
            };
            BatchBookGrid.AppearanceSettings.Caption = "库存批次台账";
            //BatchBookGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.Top;
            BatchBookGrid.ToolBarSettings.ShowRefreshButton = true;
            BatchBookGrid.ToolBarSettings.ShowSearchButton = true;
            BatchBookGrid.ToolBarSettings.ShowExcelButton = true;
            BatchBookGrid.ToolBarSettings.ShowColumnChooser = true;
            BatchBookGrid.SearchDialogSettings.MultipleSearch = true;
            BatchBookGrid.AppearanceSettings.ShowRowNumbers = true;
        }
    }

    public class LocatorBookGridModel
    {
        public JQGrid LocatorBookGrid { get; set; }
        public LocatorBookGridModel()
        {
            LocatorBookGrid = new JQGrid()
            {
                AutoWidth = true,
                ShrinkToFit = true,
                Height = Unit.Percentage(100),
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
                        DataField="WhId",
                        HeaderText="仓库",
                        DataType=typeof(Guid),
                        Visible=false,
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
                        DataField="LocatorName",
                        HeaderText="货位",
                        DataType=typeof(string),
                        Editable=true,
                        Searchable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Visible=false,
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
                        DataField="InNum",
                        HeaderText="收入数量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="OutNum",
                        HeaderText="发出数量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="Num",
                        HeaderText="结存数量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="Batch",
                    //    HeaderText="批号",
                    //    DataType=typeof(string),
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="MadeDate",
                    //    HeaderText="生产日期",
                    //    DataType=typeof(DateTime),               
                    //    SearchType = SearchType.DatePicker,
                    //    SearchControlID="DatePicker",
                    //    DataFormatString="{0:yyyy-MM-dd}",
                    //    Width=70,
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="ShelfLife",
                    //    HeaderText="保质期",
                    //    DataType=typeof(int),
                    //    Editable=true,
                    //    Width=70,
                    //    Searchable=false,
                    //},       
                    //new JQGridColumn()
                    //{
                    //    DataField="ShelfLifeType",
                    //    HeaderText="保质期单位",
                    //    DataType=typeof(int),
                    //    Visible=false,
                    //    Editable=true,
                    //    Searchable=false,
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="ShelfLifeTypeName",
                    //    HeaderText="保质期单位",
                    //    DataType=typeof(string),
                    //    Searchable=false,
                    //    Width=70,
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="InvalidDate",
                    //    HeaderText="失效日期",
                    //    DataType=typeof(DateTime),
                    //    SearchType = SearchType.DatePicker,
                    //    SearchControlID="DatePicker",
                    //    DataFormatString="{0:yyyy-MM-dd}",
                    //    Width=70,
                    //},
                }
            };
            LocatorBookGrid.AppearanceSettings.Caption = "库存货位台账";
            //LocatorBookGrid.ToolBarSettings.ToolBarPosition = ToolBarPosition.Top;
            LocatorBookGrid.ToolBarSettings.ShowRefreshButton = true;
            LocatorBookGrid.ToolBarSettings.ShowSearchButton = true;
            LocatorBookGrid.ToolBarSettings.ShowColumnChooser = true;
            LocatorBookGrid.ToolBarSettings.ShowExcelButton = true;
            LocatorBookGrid.SearchDialogSettings.MultipleSearch = true;
            LocatorBookGrid.AppearanceSettings.ShowRowNumbers = true;
        }
    }
    public class SumamaryResult
    {
        public Guid Id { get; set; }
        public Guid DeptId { get; set; }
        public Guid WhId { get; set; }
        public string WhName { get; set; }
        public string InventoryCategoryCode { get; set; }
        public string InventoryCategoryName { get; set; }
        public string InvName { get; set; }
        public string Specs { get; set; }
        public string STUnitName { get; set; }
        public decimal InitNum { get; set; }
        public decimal InitAmount { get; set; }
        public decimal InNum { get; set; }
        public decimal InAmount { get; set; }
        public decimal OutNum { get; set; }
        public decimal OutAmount { get; set; }
        public decimal Num { get; set; }
        public decimal Amount { get; set; }
        public string Batch { get; set; }
        public DateTime? MadeDate { get; set; }
        public int? ShelfLife { get; set; }
        public string ShelfLifeTypeName { get; set; }
        public DateTime? InvalidDate { get; set; }
        public Guid? Locator { get; set; }
        public string LocatorName { get; set; }
    }

    public class RdSummaryGridModel
    {
        public JQGrid RdSummaryGrid { get; set; }
        [Display(Name = "开始日期"), Required(), DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        public DateTime BeginDate { get; set; }
        [Display(Name = "结束日期"), Required()]
        public DateTime EndDate { get; set; }
        [Display(Name = "仓库"), Required()]
        public Guid? WhId { get; set; }
        [Display(Name = "存货名称")]
        public string InvName { get; set; }
        public RdSummaryGridModel()
        {
            RdSummaryGrid = new JQGrid()
            {
                AutoWidth = true,
                ShrinkToFit = true,
                Height = Unit.Percentage(100),
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
                        DataField="InitNum",
                        HeaderText="期初数量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="InitAmount",
                        HeaderText="期初金额",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="InNum",
                        HeaderText="收入数量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="InAmount",
                        HeaderText="收入金额",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="OutNum",
                        HeaderText="发出数量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="OutAmount",
                        HeaderText="发出金额",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="Num",
                        HeaderText="结存数量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="Amount",
                        HeaderText="结存金额",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                }
            };
            //RdSummaryGrid.AppearanceSettings.Caption = "库存收发存汇总表";
            RdSummaryGrid.AppearanceSettings.ShowRowNumbers = true;
            //RdSummaryGrid.PagerSettings.PageSize = int.MaxValue;
            //RdSummaryGrid.PagerSettings.PageSizeOptions = "["+int.MaxValue+"]";

        }
    }

    public class RdSummaryByWhGridModel
    {
        public JQGrid RdSummaryByWhGrid { get; set; }
        [Display(Name = "开始日期"), Required(), DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        public DateTime BeginDate { get; set; }
        [Display(Name = "结束日期"), Required()]
        public DateTime EndDate { get; set; }
        [Display(Name = "仓库"), Required()]
        public Guid? WhId { get; set; }
        [Display(Name = "分类编码")]
        public string InventoryCategoryCode { get; set; }
        [Display(Name = "分类名称")]
        public string InventoryCategoryName { get; set; }
        public RdSummaryByWhGridModel()
        {
            RdSummaryByWhGrid = new JQGrid()
            {
                AutoWidth = true,
                ShrinkToFit = true,
                Height = Unit.Percentage(100),
                SortSettings = new SortSettings()
                {
                    InitialSortColumn = "InventoryCategoryCode"
                },

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
                        DataField="InventoryCategoryCode",
                        HeaderText="分类编码",
                        DataType=typeof(string),
                        Searchable=false,
                        //Sortable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="InventoryCategoryName",
                        HeaderText="分类名称",
                        DataType=typeof(string),
                        Searchable=false,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="WhName",
                    //    HeaderText="仓库",
                    //    DataType=typeof(string),
                    //    Searchable=false,
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="InvName",
                    //    HeaderText="存货",
                    //    DataType=typeof(string),
                    //    Searchable=false,
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="Specs",
                    //    HeaderText="规格型号",
                    //    DataType=typeof(string),
                    //    Searchable=false,
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="STUnitName",
                    //    HeaderText="计量单位",
                    //    DataType=typeof(string),
                    //    Searchable=false,
                    //},
                    new JQGridColumn()
                    {
                        DataField="InitNum",
                        HeaderText="期初数量",
                        DataType=typeof(decimal),
                        
                        //Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                        Formatter = new NumberFormatter(){DecimalPlaces=2},
                    },
                    new JQGridColumn()
                    {
                        DataField="InitAmount",
                        HeaderText="期初金额",
                        DataType=typeof(decimal),
                        //Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                        Formatter = new NumberFormatter(){DecimalPlaces=2},
                    },
                    new JQGridColumn()
                    {
                        DataField="InNum",
                        HeaderText="收入数量",
                        DataType=typeof(decimal),
                        //Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                        Formatter = new NumberFormatter(){DecimalPlaces=2},
                    },
                    new JQGridColumn()
                    {
                        DataField="InAmount",
                        HeaderText="收入金额",
                        DataType=typeof(decimal),
                        //Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                        Formatter = new NumberFormatter(){DecimalPlaces=2},
                    },
                    new JQGridColumn()
                    {
                        DataField="OutNum",
                        HeaderText="发出数量",
                        DataType=typeof(decimal),
                        //Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                        Formatter = new NumberFormatter(){DecimalPlaces=2},
                    },
                    new JQGridColumn()
                    {
                        DataField="OutAmount",
                        HeaderText="发出金额",
                        DataType=typeof(decimal),
                        //Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                        Formatter = new NumberFormatter(){DecimalPlaces=2},
                    },
                    new JQGridColumn()
                    {
                        DataField="Num",
                        HeaderText="结存数量",
                        DataType=typeof(decimal),
                        //Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                        Formatter = new NumberFormatter(){DecimalPlaces=2},
                    },
                    new JQGridColumn()
                    {
                        DataField="Amount",
                        HeaderText="结存金额",
                        DataType=typeof(decimal),
                        //Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                        Formatter = new NumberFormatter(){DecimalPlaces=2},
                    },
                }
            };

            //RdSummaryByWhGrid.AppearanceSettings.Caption = "库存收发存汇总表2";
            RdSummaryByWhGrid.AppearanceSettings.ShowRowNumbers = true;
        }
    }
    public class BatchSummaryGridModel
    {
        public JQGrid BatchSummaryGrid { get; set; }
        [Display(Name = "开始日期")]
        public DateTime BeginDate { get; set; }
        [Display(Name = "结束日期")]
        public DateTime EndDate { get; set; }
        [Display(Name = "仓库")]
        public Guid? WhId { get; set; }
        [Display(Name = "批次")]
        public string Batch { get; set; }
        [Display(Name = "存货名称")]
        public string InvName { get; set; }
        public BatchSummaryGridModel()
        {
            BatchSummaryGrid = new JQGrid()
            {
                AutoWidth = true,
                ShrinkToFit = true,
                Height = Unit.Percentage(100),
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
                        DataField="InitNum",
                        HeaderText="期初数量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="InitAmount",
                        HeaderText="期初金额",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="InNum",
                        HeaderText="收入数量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="InAmount",
                        HeaderText="收入金额",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="OutNum",
                        HeaderText="发出数量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="OutAmount",
                        HeaderText="发出金额",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="Num",
                        HeaderText="结存数量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="Amount",
                        HeaderText="结存金额",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
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
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}",
                        Width=70,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLife",
                        HeaderText="保质期",
                        DataType=typeof(int),
                        Editable=true,
                        Width=70,
                        Searchable=false,
                    },    
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeTypeName",
                        HeaderText="保质期单位",
                        DataType=typeof(string),
                        Searchable=false,
                        Width=70,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvalidDate",
                        HeaderText="失效日期",
                        DataType=typeof(DateTime),
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}",
                        Width=70,
                    },
                }
            };
            //BatchSummaryGrid.AppearanceSettings.Caption = "库存批次汇总表";
            BatchSummaryGrid.AppearanceSettings.ShowRowNumbers = true;
            //BatchSummaryGrid.PagerSettings.PageSize = int.MaxValue;
            //BatchSummaryGrid.PagerSettings.PageSizeOptions = "[" + int.MaxValue + "]";

        }
    }

    public class LocatorSummaryGridModel
    {
        public JQGrid LocatorSummaryGrid { get; set; }
        [Display(Name = "开始日期"), Required(), DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        public DateTime BeginDate { get; set; }
        [Display(Name = "结束日期"), Required()]
        public DateTime EndDate { get; set; }
        [Display(Name = "仓库"), Required()]
        public Guid? WhId { get; set; }
        [Display(Name = "货位"), Required()]
        public Guid? Locator { get; set; }
        [Display(Name = "存货名称")]
        public string InvName { get; set; }
        public LocatorSummaryGridModel()
        {
            LocatorSummaryGrid = new JQGrid()
            {
                AutoWidth = true,
                ShrinkToFit = true,
                Height = Unit.Percentage(100),
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
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="LocatorName",
                        HeaderText="货位",
                        DataType=typeof(string),
                        Editable=true,
                        Searchable=true,
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
                        DataField="InitNum",
                        HeaderText="期初数量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="InitAmount",
                        HeaderText="期初金额",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="InNum",
                        HeaderText="收入数量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="InAmount",
                        HeaderText="收入金额",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="OutNum",
                        HeaderText="发出数量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="OutAmount",
                        HeaderText="发出金额",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="Num",
                        HeaderText="结存数量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="Amount",
                        HeaderText="结存金额",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },         
                }
            };
            //LocatorSummaryGrid.AppearanceSettings.Caption = "库存货位汇总表";
            LocatorSummaryGrid.AppearanceSettings.ShowRowNumbers = true;
            //LocatorSummaryGrid.PagerSettings.PageSize = int.MaxValue;
            //LocatorSummaryGrid.PagerSettings.PageSizeOptions = "[" + int.MaxValue + "]";

        }
    }

    public class ShelfLifeWarningGridModel
    {
        public JQGrid ShelfLifeWarningGrid { get; set; }

        [Display(Name = "存货类别")]
        public int InvType { get; set; }
        //失效日期
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }

        [Display(Name = "过期天数")]
        public int? OutOfDays { get; set; }
        //[Display(Name = "临近天数")]
        public int? BeginCloseToDays { get; set; }
        public int? EndCloseToDays { get; set; }
        public ShelfLifeWarningGridModel()
        {
            ShelfLifeWarningGrid = new JQGrid()
            {
                AutoWidth = true,
                ShrinkToFit = true,
                Height = Unit.Percentage(100),
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
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
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
                        SearchType = SearchType.DatePicker,
                        SearchControlID="DatePicker",
                        DataFormatString="{0:yyyy-MM-dd}",
                        Width=70,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLife",
                        HeaderText="保质期",
                        DataType=typeof(int),
                        Editable=true,
                        Width=70,
                        Searchable=false,
                    },    
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeTypeName",
                        HeaderText="保质期单位",
                        DataType=typeof(string),
                        Searchable=false,
                        Width=70,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvalidDate",
                        HeaderText="失效日期",
                        DataType=typeof(DateTime),
                        DataFormatString="{0:yyyy-MM-dd}",
                        Width=70,
                    },
                    new JQGridColumn()
                    {
                        DataField="LocatorName",
                        HeaderText="货位",
                        DataType=typeof(string),
                        Editable=true,
                        Searchable=true,
                    },
                }
            };
            //ShelfLifeWarningGrid.AppearanceSettings.Caption = "库存保质期预警";
            ShelfLifeWarningGrid.AppearanceSettings.ShowRowNumbers = true;
            //ShelfLifeWarningGrid.PagerSettings.PageSize = int.MaxValue;
            //ShelfLifeWarningGrid.PagerSettings.PageSizeOptions = "[" + int.MaxValue + "]";

        }
    }

    public class SecurityStockGridModel
    {
        public JQGrid SecurityStockGrid { get; set; }
        [Display(Name = "查询类型")]
        public int QueryType { get; set; }
        public SecurityStockGridModel()
        {
            SecurityStockGrid = new JQGrid()
            {
                AutoWidth = true,
                ShrinkToFit = true,
                Height = Unit.Percentage(100),
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
                        DataField="SecurityStock",
                        HeaderText="安全库存量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="Num",
                        HeaderText="可用量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="DifNum",
                        HeaderText="差量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },                  
                }
            };
            //SecurityStockGrid.AppearanceSettings.Caption = "库存安全库存预警";
            SecurityStockGrid.AppearanceSettings.ShowRowNumbers = true;
            //SecurityStockGrid.PagerSettings.PageSize = int.MaxValue;
            //SecurityStockGrid.PagerSettings.PageSizeOptions = "[" + int.MaxValue + "]";

        }
    }

    public class AboveStockGridModel
    {
        public JQGrid AboveStockGrid { get; set; }
        [Display(Name = "查询类型")]
        public int QueryType { get; set; }
        public AboveStockGridModel()
        {
            AboveStockGrid = new JQGrid()
            {
                AutoWidth = true,
                ShrinkToFit = true,
                Height = Unit.Percentage(100),
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
                        DataField="HighStock",
                        HeaderText="最高库存量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="Num",
                        HeaderText="可用量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="DifNum",
                        HeaderText="超储量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },                  
                }
            };
            //AboveStockGrid.AppearanceSettings.Caption = "库存超储存货查询";
            AboveStockGrid.AppearanceSettings.ShowRowNumbers = true;
            //AboveStockGrid.PagerSettings.PageSize = int.MaxValue;
            //AboveStockGrid.PagerSettings.PageSizeOptions = "[" + int.MaxValue + "]";

        }
    }

    public class LowStockGridModel
    {
        public JQGrid LowStockGrid { get; set; }
        [Display(Name = "查询类型")]
        public int QueryType { get; set; }
        public LowStockGridModel()
        {
            LowStockGrid = new JQGrid()
            {
                AutoWidth = true,
                ShrinkToFit = true,
                Height = Unit.Percentage(100),
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
                        DataField="LowStock",
                        HeaderText="最低库存量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="Num",
                        HeaderText="可用量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },
                    new JQGridColumn()
                    {
                        DataField="DifNum",
                        HeaderText="短缺量",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}//new NumberFormatter(){ DecimalPlaces=0},
                    },                  
                }
            };
            //LowStockGrid.AppearanceSettings.Caption = "库存短缺存货查询";
            LowStockGrid.AppearanceSettings.ShowRowNumbers = true;
            //LowStockGrid.PagerSettings.PageSize = int.MaxValue;
            //LowStockGrid.PagerSettings.PageSizeOptions = "[" + int.MaxValue + "]";

        }
    }
    public class BillOfMaterialsGridModel
    {
        public JQGrid BillOfMaterialsGrid { get; set; }
        public BillOfMaterialsGridModel()
        {
            BillOfMaterialsGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType=typeof(Guid),
                        Visible=false,
                        PrimaryKey=true,
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="PartInvId",
                        DataType=typeof(Guid),
                        Visible=false,
                        Searchable=false,
                        Editable=true,
                        HeaderText="主件",
                    },
                    new JQGridColumn()
                    {
                        DataField="PartInvCode",
                        HeaderText="主件存货编码",
                        DataType=typeof(string),
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="PartInvName",
                        HeaderText="主件存货名称",
                        DataType=typeof(Guid),
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="PartSpecs",
                        HeaderText="主件规格型号",
                        DataType=typeof(string),
                        Searchable=false,
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="PartGroupName",
                        HeaderText="主件计量单位组",
                        DataType=typeof(string),
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="PartStockUnitName",
                        HeaderText="主件计量单位",
                        DataType=typeof(string),
                        Searchable=false,
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="BaseQtyD",
                        HeaderText="主件基础用量",
                        DataType=typeof(Decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator()
                        },
                        Searchable=false,
                        Formatter = new NumberFormatter(){ DecimalPlaces=2 },
                    },

                    //子件

                    new JQGridColumn()
                    {
                        DataField="ComponentInvId",
                        DataType=typeof(Guid),
                        Searchable=false,
                        Visible=false,
                        Editable=true,
                        HeaderText="子件",
                    },
                    new JQGridColumn()
                    {
                        DataField="ComponentInvCode",
                        HeaderText="子件存货编码",
                        DataType=typeof(string),
                    },    
                    
                    new JQGridColumn()
                    {
                        DataField="ComponentInvName",
                        HeaderText="子件存货名称",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="ComponentSpecs",
                        HeaderText="子件规格型号",
                        DataType=typeof(string),
                        Searchable=false,
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="ComponentGroupName",
                        HeaderText="子件计量单位组",
                        DataType=typeof(string),
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="ComponentStockUnitName",
                        HeaderText="子件计量单位",
                        DataType=typeof(string),
                        Searchable=false,
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="BaseQtyN",
                        HeaderText="子件用量",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator()
                        },
                        Searchable=false,
                        Formatter = new NumberFormatter(){ DecimalPlaces=2 },
                    },                    
                }
            };
            BillOfMaterialsGrid.AppearanceSettings.Caption = "配方";
            BillOfMaterialsGrid.ToolBarSettings.ShowSearchButton = true;
            BillOfMaterialsGrid.ToolBarSettings.ShowAddButton = true;
            BillOfMaterialsGrid.ToolBarSettings.ShowEditButton = true;
            BillOfMaterialsGrid.ToolBarSettings.ShowDeleteButton = true;
            BillOfMaterialsGrid.ToolBarSettings.ShowRefreshButton = true;
            BillOfMaterialsGrid.ToolBarSettings.ShowExcelButton = true;
            BillOfMaterialsGrid.ToolBarSettings.ShowColumnChooser = true;
            BillOfMaterialsGrid.SearchDialogSettings.MultipleSearch = true;
        }
    }

    public class ProduceEvaluationGridModel
    {
        public JQGrid ProduceEvaluationGrid { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Display(Name = "门店")]
        public Guid? DeptId { get; set; }
        public ProduceEvaluationGridModel()
        {
            ProduceEvaluationGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
                Columns = new List<JQGridColumn>()
                {
                    //new JQGridColumn()
                    //{
                    //    DataField="Id",
                    //    DataType=typeof(Guid),
                    //    Visible=false,
                    //    PrimaryKey=true,
                    //    Searchable=false
                    //},
                    new JQGridColumn()
                    {
                        DataField="PartInvId",
                        DataType=typeof(Guid),
                        Visible=false,
                        Searchable=false,
                        Editable=true,
                        HeaderText="主件",
                        PrimaryKey=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="PartInvCode",
                        HeaderText="主件存货编码",
                        DataType=typeof(string),
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="PartInvName",
                        HeaderText="主件存货名称",
                        DataType=typeof(Guid),
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="PartSpecs",
                        HeaderText="主件规格型号",
                        DataType=typeof(string),
                        Searchable=false,
                        Editable=true,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="PartGroupName",
                    //    HeaderText="主件计量单位组",
                    //    DataType=typeof(string),
                    //    Searchable=false
                    //},
                    new JQGridColumn()
                    {
                        DataField="PartStockUnitName",
                        HeaderText="主件计量单位",
                        DataType=typeof(string),
                        Searchable=false,
                        Editable=true,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="BaseQtyD",
                    //    HeaderText="主件基础用量",
                    //    DataType=typeof(Decimal),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator(),
                    //        new NumberValidator()
                    //    },
                    //    Searchable=false,
                    //    Formatter = new NumberFormatter(){ DecimalPlaces=2 },
                    //},

                    //子件

                    new JQGridColumn()
                    {
                        DataField="ComponentInvId",
                        DataType=typeof(Guid),
                        //Searchable=false,
                        Visible=false,
                        //Editable=true,
                        HeaderText="子件",
                        PrimaryKey=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="ComponentInvCode",
                        HeaderText="子件存货编码",
                        DataType=typeof(string),
                    },    
                    
                    new JQGridColumn()
                    {
                        DataField="ComponentInvName",
                        HeaderText="子件存货名称",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="ComponentSpecs",
                        HeaderText="子件规格型号",
                        DataType=typeof(string),
                        Searchable=false,
                        Editable=true,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="ComponentGroupName",
                    //    HeaderText="子件计量单位组",
                    //    DataType=typeof(string),
                    //    Searchable=false
                    //},
                    new JQGridColumn()
                    {
                        DataField="ComponentStockUnitName",
                        HeaderText="子件计量单位",
                        DataType=typeof(string),
                        Searchable=false,
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Num1",
                        HeaderText="配方用量",
                        DataType=typeof(decimal),
                        //Editable=true,
                        //EditDialogFieldSuffix="(*)",
                        //EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        //{
                        //    new RequiredValidator(),
                        //    new NumberValidator()
                        //},
                        Searchable=false,
                        //Formatter = new NumberFormatter(){ DecimalPlaces=2 },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}
                    },   
                    new JQGridColumn()
                    {
                        DataField="Num2",
                        HeaderText="实际用量",
                        DataType=typeof(decimal),
                        //Editable=true,
                        //EditDialogFieldSuffix="(*)",
                        //EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        //{
                        //    new RequiredValidator(),
                        //    new NumberValidator()
                        //},
                        Searchable=false,
                        //Formatter = new NumberFormatter(){ DecimalPlaces=2 },
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}
                    },                    
                }
            };
            ProduceEvaluationGrid.AppearanceSettings.Caption = "配方";
            //ProduceEvaluationGrid.ToolBarSettings.ShowSearchButton = true;
            //ProduceEvaluationGrid.ToolBarSettings.ShowAddButton = true;
            //ProduceEvaluationGrid.ToolBarSettings.ShowEditButton = true;
            //ProduceEvaluationGrid.ToolBarSettings.ShowDeleteButton = true;
            //ProduceEvaluationGrid.ToolBarSettings.ShowRefreshButton = true;
            //ProduceEvaluationGrid.ToolBarSettings.ShowExcelButton = true;
            //ProduceEvaluationGrid.ToolBarSettings.ShowColumnChooser = true;
            //ProduceEvaluationGrid.SearchDialogSettings.MultipleSearch = true;
        }
    }
}