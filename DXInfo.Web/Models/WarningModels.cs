using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DXInfo.Web.Models
{
    public class WarningModels
    {
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
        public int? BeginCloseToDays { get; set; }
        public int? EndCloseToDays { get; set; }
        public ShelfLifeWarningGridModel()
        {
            ShelfLifeWarningGrid = new JQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType=typeof(Guid),
                        PrimaryKey=true,
                        Visible=false,
                        Searchable=false,                        
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
                        DataField="LocatorName",
                        HeaderText="货位",
                        DataType=typeof(string),
                    },
                }
            };
            ShelfLifeWarningGrid.AppearanceSettings.ShowRowNumbers = true;

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
                        DataField="SecurityStock",
                        HeaderText="安全库存量",
                        DataType=typeof(decimal),
                        Formatter=new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="Num",
                        HeaderText="可用量",
                        DataType=typeof(decimal),
                        Formatter=new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="DifNum",
                        HeaderText="差量",
                        DataType=typeof(decimal),
                        Formatter=new DigitFormatter(),
                    },                  
                }
            };
            SecurityStockGrid.AppearanceSettings.ShowRowNumbers = true;

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
                        DataField="HighStock",
                        HeaderText="最高库存量",
                        DataType=typeof(decimal),
                        Formatter=new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="Num",
                        HeaderText="可用量",
                        DataType=typeof(decimal),
                        Formatter=new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="DifNum",
                        HeaderText="超储量",
                        DataType=typeof(decimal),
                        Formatter=new DigitFormatter(),
                    },                  
                }
            };
            AboveStockGrid.AppearanceSettings.ShowRowNumbers = true;

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
                        DataField="LowStock",
                        HeaderText="最低库存量",
                        DataType=typeof(decimal),
                        Formatter=new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="Num",
                        HeaderText="可用量",
                        DataType=typeof(decimal),
                        Formatter=new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="DifNum",
                        HeaderText="短缺量",
                        DataType=typeof(decimal),
                        Formatter=new DigitFormatter(),
                    },                  
                }
            };
            LowStockGrid.AppearanceSettings.ShowRowNumbers = true;

        }
    }
}