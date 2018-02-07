using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DXInfo.Web.Models
{
    public class SummaryModels
    {
    }
    public class SumamaryResult
    {
        public Guid Id { get; set; }
        public Guid DeptId { get; set; }
        public Guid? OrganizationId { get; set; }
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
                        Formatter=new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="InitAmount",
                        HeaderText="期初金额",
                        DataType=typeof(decimal),
                        Formatter=new DigitFormatter(),
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
                        DataField="InAmount",
                        HeaderText="收入金额",
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
                        DataField="OutAmount",
                        HeaderText="发出金额",
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
                        DataField="Amount",
                        HeaderText="结存金额",
                        DataType=typeof(decimal),
                        Formatter=new DigitFormatter(),
                    },
                }
            };
            RdSummaryGrid.AppearanceSettings.ShowRowNumbers = true;

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
                    },
                    new JQGridColumn()
                    {
                        DataField="InventoryCategoryName",
                        HeaderText="分类名称",
                        DataType=typeof(string),
                    },                    
                    new JQGridColumn()
                    {
                        DataField="InitNum",
                        HeaderText="期初数量",
                        DataType=typeof(decimal),
                        Formatter = new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="InitAmount",
                        HeaderText="期初金额",
                        DataType=typeof(decimal),
                        Formatter = new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="InNum",
                        HeaderText="收入数量",
                        DataType=typeof(decimal),
                        Formatter = new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="InAmount",
                        HeaderText="收入金额",
                        DataType=typeof(decimal),
                        Formatter = new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="OutNum",
                        HeaderText="发出数量",
                        DataType=typeof(decimal),
                        Formatter = new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="OutAmount",
                        HeaderText="发出金额",
                        DataType=typeof(decimal),
                        Formatter = new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="Num",
                        HeaderText="结存数量",
                        DataType=typeof(decimal),
                        Formatter = new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="Amount",
                        HeaderText="结存金额",
                        DataType=typeof(decimal),
                        Formatter = new DigitFormatter(),
                    },
                }
            };
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
                        DataField="InitNum",
                        HeaderText="期初数量",
                        DataType=typeof(decimal),
                        Formatter=new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="InitAmount",
                        HeaderText="期初金额",
                        DataType=typeof(decimal),
                        Formatter=new DigitFormatter(),
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
                        DataField="InAmount",
                        HeaderText="收入金额",
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
                        DataField="OutAmount",
                        HeaderText="发出金额",
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
                        DataField="Amount",
                        HeaderText="结存金额",
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
            BatchSummaryGrid.AppearanceSettings.ShowRowNumbers = true;
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
                        DataField="InitNum",
                        HeaderText="期初数量",
                        DataType=typeof(decimal),
                        Formatter=new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="InitAmount",
                        HeaderText="期初金额",
                        DataType=typeof(decimal),
                        Formatter=new DigitFormatter(),
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
                        DataField="InAmount",
                        HeaderText="收入金额",
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
                        DataField="OutAmount",
                        HeaderText="发出金额",
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
                        DataField="Amount",
                        HeaderText="结存金额",
                        DataType=typeof(decimal),
                        Formatter=new DigitFormatter(),
                    },         
                }
            };
            LocatorSummaryGrid.AppearanceSettings.ShowRowNumbers = true;
        }
    }
}