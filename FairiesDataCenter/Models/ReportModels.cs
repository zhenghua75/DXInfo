using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ynhnTransportManage.Models
{
    public class ReportModels
    {
    }
    #region Report1
    public class Report1Grid
    {
        public JQGrid ReportGrid { get; set; }
        public Report1Grid()
        {
            ReportGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="CardNo",
                        HeaderText="卡号",
                        DataType = typeof(string)
                    },                    
                    new JQGridColumn()
                    {
                        DataField="MemberName",
                        HeaderText="会员名",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="Balance",
                        HeaderText="余额",
                        DataType = typeof(decimal),
                        Formatter = new CurrencyFormatter
                        {
                            DecimalPlaces = 2,
                            DecimalSeparator = ".",
                            Prefix = "￥",
                            ThousandsSeparator = ","
                        },          
                    },
                    new JQGridColumn()
                    {
                        DataField="Points",
                        HeaderText="积分",
                        DataType = typeof(decimal)
                    },
                    new JQGridColumn()
                    {
                        DataField="IdCard",
                        HeaderText="证件号",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="LinkPhone",
                        HeaderText="联系电话",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="LinkAddress",
                        HeaderText="联系地址",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="Email",
                        HeaderText="Email",
                        DataType = typeof(DateTime)
                    },
                    new JQGridColumn()
                    {
                        DataField="CardType",
                        HeaderText="卡类型",
                        DataType = typeof(string)
                    },

                    new JQGridColumn()
                    {
                        DataField="CardLevel",
                        HeaderText="卡级别",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="Status",
                        HeaderText="状态",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="CreateDate",
                        HeaderText="发卡日期",
                        DataType = typeof(DateTime),                        
                        Editable=false,
                        SearchType= SearchType.DatePicker,
                        SearchControlID = "DatePicker",
                        DataFormatString = "{0:yyyy-MM-dd HH:mm}",
                    },
                    new JQGridColumn()
                    {
                        DataField="FullName",
                        HeaderText="发卡操作员",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptName",
                        HeaderText="发卡部门",
                        DataType = typeof(string)     
                    },
                    new JQGridColumn()
                    {
                        DataField="LossDate",
                        HeaderText="挂失日期",
                        DataType = typeof(DateTime),                        
                        Editable=false,
                        SearchType= SearchType.DatePicker,
                        SearchControlID = "DatePicker",
                        DataFormatString = "{0:yyyy-MM-dd HH:mm}",
                    },
                    new JQGridColumn()
                    {
                        DataField="LossFullName",
                        HeaderText="挂失操作员",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="LossDeptName",
                        HeaderText="挂失部门",
                        DataType = typeof(string)     
                    },
                    new JQGridColumn()
                    {
                        DataField="FoundDate",
                        HeaderText="解挂日期",
                        DataType = typeof(DateTime),                        
                        Editable=false,
                        SearchType= SearchType.DatePicker,
                        SearchControlID = "DatePicker",
                        DataFormatString = "{0:yyyy-MM-dd HH:mm}",
                    },
                    new JQGridColumn()
                    {
                        DataField="FoundFullName",
                        HeaderText="解挂操作员",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="FoundDeptName",
                        HeaderText="解挂部门",
                        DataType = typeof(string)     
                    },
                    new JQGridColumn()
                    {
                        DataField="AddDate",
                        HeaderText="补卡日期",
                        DataType = typeof(DateTime),                        
                        Editable=false,
                        SearchType= SearchType.DatePicker,
                        SearchControlID = "DatePicker",
                        DataFormatString = "{0:yyyy-MM-dd HH:mm}",
                    },
                    new JQGridColumn()
                    {
                        DataField="SecondCardNo",
                        HeaderText="补卡卡号",
                        DataType = typeof(string),     
                    },
                    new JQGridColumn()
                    {
                        DataField="AddFullName",
                        HeaderText="补卡操作员",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="AddDeptName",
                        HeaderText="补卡部门",
                        DataType = typeof(string)     
                    },
                    new JQGridColumn()
                    {
                        DataField="AddReason",
                        HeaderText="补卡原因",
                        DataType = typeof(string)     
                    }
                }
            };
            ReportGrid.ToolBarSettings.ShowSearchButton = true;
            ReportGrid.SearchDialogSettings.MultipleSearch = true;
            ReportGrid.AppearanceSettings.Caption = "会员资料查询";
            ReportGrid.ToolBarSettings.CustomButtons = new List<JQGridToolBarButton>()
            {
                new JQGridToolBarButton()
                {
                    Position = ToolBarButtonPosition.Last,
                    ToolTip="导出EXCEL",
                    Text="导出",
                    OnClick="customButtonClicked",
                    ButtonIcon="ui-icon-extlink"
                }
            };
        }
    }
    public class Report1Model
    {
        [Display(Name = "卡级别：")]
        public Guid? CardLevel { get; set; }
        [Display(Name = "卡类型：")]
        public Guid? CardType { get; set; }
        [Display(Name = "门店：")]
        public Guid? DeptId { get; set; }
        [Display(Name = "卡号：")]
        public string CardNo { get; set; }
        [Display(Name = "会员名：")]
        public string MemberName { get; set; }
        [Display(Name = "卡状态：")]
        public int? Status { get; set; }
        [Display(Name = "开始日期：")]
        public string BeginDate { get; set; }
        [Display(Name = "结束日期：")]
        public string EndDate { get; set; }

        public List<Report1Result> result { get; set; }

        [Display(Name = "积分：")]
        public decimal Points { get; set; }
        [Display(Name = "余额：")]
        public decimal Balance { get; set; }
        [Display(Name = "充值金额：")]
        public decimal Recharge { get; set; }
    }
    public class Report1Result
    {
        public string CardType { get; set; }
        public string CardLevel { get; set; }
        public string DeptName { get; set; }
        public string CardNo { get; set; }
        public string MemberName { get; set; }
        
        public decimal Balance { get; set; }
        public decimal Recharge { get; set; }
        public decimal Points { get; set; }

        public DateTime CreateDate { get; set; }
        public string FullName { get; set; }        
        public string Status { get; set; }        
        public string Email { get; set; }
        public string IdCard { get; set; }
        public string LinkAddress { get; set; }
        public string LinkPhone { get; set; }
        public string Sex { get; set; }
        public string Birthday { get; set; }
        
        public DateTime? LossDate { get; set; }
        public string LossFullName { get; set; }
        public string LossDeptName { get; set; }

        public DateTime? FoundDate { get; set; }
        public string FoundFullName { get; set; }
        public string FoundDeptName { get; set; }

        public string SecondCardNo { get; set; }
        public DateTime? AddDate { get; set; }
        public string AddReason { get; set; }
        public string AddFullName { get; set; }
        public string AddDeptName { get; set; }
    }
    #endregion

    #region Report2
    public class Report2GridModel
    {
        public int InvType { get; set; }
        public JQGrid Report2Grid { get; set; }
        public Report2GridModel()
        {
            Report2Grid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
                SortSettings = new SortSettings()
                {
                    InitialSortColumn = "CreateDate",
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
                        DataField="LocalDeptId",
                        HeaderText="发卡门店",
                        DataType = typeof(Guid),
                        Visible=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="LocalDeptName",
                        HeaderText="发卡门店",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptId",
                        HeaderText="门店",
                        DataType = typeof(Guid),
                        Visible=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptName",
                        HeaderText="门店",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="UserId",
                        HeaderText="操作员",
                        DataType = typeof(Guid),
                        Visible=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="FullName",
                        HeaderText="操作员",
                        DataType = typeof(string)
                    },   
                    new JQGridColumn()
                    {
                        DataField="OperatorsOnDuty",
                        HeaderText="当班操作员",
                        DataType = typeof(string)
                    },  
                    new JQGridColumn()
                    {
                        DataField="ConsumeType",
                        HeaderText="消费类型",
                        DataType = typeof(int),    
                        Visible=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="ConsumeTypeName",
                        HeaderText="消费类型",
                        DataType = typeof(string),     
                    },
                    new JQGridColumn()
                    {
                        DataField="PayType",
                        HeaderText="支付方式",
                        DataType = typeof(Guid),    
                        Visible=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="PayTypeName",
                        HeaderText="支付方式",
                        DataType = typeof(string),     
                    },
                    new JQGridColumn()
                    {
                        DataField="CardNo",
                        HeaderText="卡号",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="MemberName",
                        HeaderText="会员名",
                        DataType = typeof(string),     
                    },   
                    new JQGridColumn()
                    {
                        DataField="Category",
                        HeaderText="分类",
                        DataType = typeof(Guid),
                        Visible=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="CategoryName",
                        HeaderText="分类",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="InventoryId",
                        HeaderText="商品",
                        DataType = typeof(Guid),
                        Visible=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="InventoryName",
                        HeaderText="商品",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="Cup",
                        HeaderText="杯型",
                        DataType = typeof(int),
                        Visible=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="CupName",
                        HeaderText="杯型",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="UnitOfMeasureName",
                        HeaderText="单位",
                        DataType = typeof(string),
                        Visible=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="Price",
                        HeaderText="单价",
                        DataType = typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}
                    },
                    new JQGridColumn()
                    {
                        DataField="Quantity",
                        HeaderText="数量",
                        DataType = typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}
                    },
                    new JQGridColumn()
                    {
                        DataField="Sum",
                        HeaderText="合计",
                        DataType = typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}         
                    },
                    new JQGridColumn()
                    {
                        DataField="Discount",
                        HeaderText="折扣%",
                        DataType = typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}
                    },                    
                    new JQGridColumn()
                    {
                        DataField="Amount",
                        HeaderText="金额",
                        DataType = typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}  
                    },
                    new JQGridColumn()
                    {
                        DataField="CreateDate",
                        HeaderText="消费时间",
                        DataType = typeof(DateTime),
                        DataFormatString="{0:yyyy-MM-dd HH:mm:ss}",
                        Width=140,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvType",
                        DataType=typeof(int),
                        Visible=false,
                        Searchable=false
                    },
                }
            };
        }
    }
    #endregion

    #region Report3
    public class Report3Grid
    {
        public JQGrid ReportGrid { get; set; }
        public Report3Grid()
        {
            ReportGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="CardNo",
                        HeaderText="卡号",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="MemberName",
                        HeaderText="会员名",
                        DataType = typeof(string),     
                    },
                    new JQGridColumn()
                    {
                        DataField="RechargeType",
                        HeaderText="充值类型",
                        DataType = typeof(string),     
                    },
                    new JQGridColumn()
                    {
                        DataField="LastBalance",
                        HeaderText="上次余额",
                        DataType = typeof(decimal),
                        Formatter = new CurrencyFormatter
                        {
                            DecimalPlaces = 2,
                            DecimalSeparator = ".",
                            Prefix = "￥",
                            ThousandsSeparator = ","
                        },      
                    },
                    new JQGridColumn()
                    {
                        DataField="Amount",
                        HeaderText="充值金额",
                        DataType = typeof(decimal),
                        Formatter = new CurrencyFormatter
                        {
                            DecimalPlaces = 2,
                            DecimalSeparator = ".",
                            Prefix = "￥",
                            ThousandsSeparator = ","
                        },      
                    },
                    new JQGridColumn()
                    {
                        DataField="Donate",
                        HeaderText="赠送",
                        DataType = typeof(decimal),
                        Formatter = new CurrencyFormatter
                        {
                            DecimalPlaces = 2,
                            DecimalSeparator = ".",
                            Prefix = "￥",
                            ThousandsSeparator = ","
                        },      
                    },
                    new JQGridColumn()
                    {
                        DataField="Balance",
                        HeaderText="余额",
                        DataType = typeof(decimal),
                        Formatter = new CurrencyFormatter
                        {
                            DecimalPlaces = 2,
                            DecimalSeparator = ".",
                            Prefix = "￥",
                            ThousandsSeparator = ","
                        },      
                    },                    
                    new JQGridColumn()
                    {
                        DataField="CreateDate",
                        HeaderText="充值日期",
                        DataType = typeof(DateTime),
                        SearchType= SearchType.DatePicker,
                        SearchControlID = "DatePicker",
                        DataFormatString = "{0:yyyy-MM-dd HH:mm}",
                    },
                    new JQGridColumn()
                    {
                        DataField="FullName",
                        HeaderText="操作员",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptName",
                        HeaderText="门店",
                        DataType = typeof(string)
                    }
                }
            };
            ReportGrid.ToolBarSettings.ShowSearchButton = true;
            ReportGrid.SearchDialogSettings.MultipleSearch = true;
            ReportGrid.AppearanceSettings.Caption = "充值明细查询";
            ReportGrid.ToolBarSettings.CustomButtons = new List<JQGridToolBarButton>()
            {
                new JQGridToolBarButton()
                {
                    Position = ToolBarButtonPosition.Last,
                    ToolTip="导出EXCEL",
                    Text="导出",
                    OnClick="customButtonClicked",
                    ButtonIcon="ui-icon-extlink"
                }
            };
        }
    }
    public class Report3Model
    {
        [Display(Name = "门店：")]
        public Guid? DeptId { get; set; }
        [Display(Name = "操作员：")]
        public Guid? UserId { get; set; }

        [Display(Name = "充值类型：")]
        public int? RechargeType { get; set; }

        [Display(Name = "支付方式：")]
        public Guid? PayType { get; set; }

        [Display(Name = "卡号：")]
        public string CardNo { get; set; }
        [Display(Name = "会员名：")]
        public string MemberName { get; set; }

        [Display(Name = "开始日期：")]
        public string BeginDate { get; set; }
        [Display(Name = "结束日期：")]
        public string EndDate { get; set; }

        public List<Report3Result> result { get; set; }

        [Display(Name = "充值金额：")]
        public decimal Amount { get; set; }
        [Display(Name = "赠送金额：")]
        public decimal Donate { get; set; }
    }
    public class Report3Result
    {
        public string DeptName { get; set; }
        public string FullName { get; set; }
        public string RechargeType { get; set; }
        public string PayType { get; set; }
        public string CardNo { get; set; }
        public string MemberName { get; set; }
        
        public decimal LastBalance { get; set; }
        public decimal Amount { get; set; }       
        public decimal Donate { get; set; }
        public decimal Balance { get; set; }        
        public DateTime CreateDate { get; set; } 
    }
    #endregion

    #region Report4
    public class Report4Grid
    {
        
        public JQGrid ReportGrid { get; set; }
        [Display(Name = "开始日期")]
        public DateTime? BeginDate { get; set; }
        [Display(Name = "结束日期")]
        public DateTime? EndDate { get; set; }
        public Report4Grid()
        {
            ReportGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="ConsumeType",
                        HeaderText="消费类型",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="CategoryName",
                        HeaderText="分类",
                        DataType = typeof(string),     
                    },
                    new JQGridColumn()
                    {
                        DataField="InventoryName",
                        HeaderText="商品",
                        DataType = typeof(string),     
                    },
                    new JQGridColumn()
                    {
                        DataField="CupType",
                        HeaderText="杯型",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="Amount",
                        HeaderText="金额",
                        DataType = typeof(decimal),
                        Formatter = new CurrencyFormatter
                        {
                            DecimalPlaces = 2,
                            DecimalSeparator = ".",
                            Prefix = "￥",
                            ThousandsSeparator = ","
                        },    
                    },
                    new JQGridColumn()
                    {
                        DataField="Quantity",
                        HeaderText="数量",
                        DataType = typeof(decimal)
                    },                    
                    new JQGridColumn()
                    {
                        DataField="CreateDate",                        
                        HeaderText="消费日期",
                        Visible=false,
                        Searchable=true,
                        DataType = typeof(DateTime),
                        SearchType= SearchType.DatePicker,
                        SearchControlID = "DatePicker",
                        DataFormatString = "{0:yyyy-MM-dd HH:mm}",                        
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptName",
                        HeaderText="门店",
                        DataType = typeof(string)
                    }
                }
            };
            ReportGrid.ToolBarSettings.ShowSearchButton = true;
            ReportGrid.SearchDialogSettings.MultipleSearch = true;
            ReportGrid.AppearanceSettings.Caption = "消费分类统计";
            ReportGrid.ToolBarSettings.CustomButtons = new List<JQGridToolBarButton>()
            {
                new JQGridToolBarButton()
                {
                    Position = ToolBarButtonPosition.Last,
                    ToolTip="导出EXCEL",
                    Text="导出",
                    OnClick="customButtonClicked",
                    ButtonIcon="ui-icon-extlink"
                }
            };
            //ReportGrid.AppearanceSettings.ShowRowNumbers = true;
            
            
        }
    }
    public class Report4Model
    {
        [Display(Name = "门店：")]
        public Guid? DeptId { get; set; }
        public bool IsDept { get; set; }

        [Display(Name = "消费类型：")]
        public int? ConsumeType { get; set; }
        public bool IsConsumeType { get; set; }

        [Display(Name = "支付方式：")]
        public Guid? PayType { get; set; }
        public bool IsPayType { get; set; }

        [Display(Name = "分类：")]
        public Guid? Category { get; set; }
        public bool IsCategory { get; set; }

        [Display(Name = "商品：")]
        public Guid? Inventory { get; set; }
        public bool IsInventory { get; set; }

        [Display(Name = "杯型：")]
        public int? CupType { get; set; }
        public bool IsCupType { get; set; }

        [Display(Name = "开始日期：")]
        public string BeginDate { get; set; }
        [Display(Name = "结束日期：")]
        public string EndDate { get; set; }

        public IQueryable<Report4Result> result { get; set; }

        [Display(Name = "金额：")]
        public decimal Amount { get; set; }
        [Display(Name = "数量：")]
        public decimal Quantity { get; set; }
        public int InvType { get; set; }
        public int CategoryType { get; set; }
        public int DeptType { get; set; }
    }
    public class Report4Result
    {
        public Guid Id { get; set; }
        public string DeptName { get; set; }
        public string CategoryName { get; set; }
        public string InventoryName { get; set; }
        public string ConsumeType { get; set; }
        public string PayType { get; set; }
        public string CupType { get; set; }
        public decimal Amount { get; set; }
        public decimal Quantity { get; set; }
        public string UnitOfMeasureName { get; set; }
    }
    #endregion

    #region Report5
    public class Report5Grid
    {
        public JQGrid ReportGrid { get; set; }
        public Report5Grid()
        {
            ReportGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="ConsumeType",
                        HeaderText="消费类型",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="CategoryName",
                        HeaderText="分类",
                        DataType = typeof(string),     
                    },
                    new JQGridColumn()
                    {
                        DataField="InventoryName",
                        HeaderText="商品",
                        DataType = typeof(string),     
                    },
                    new JQGridColumn()
                    {
                        DataField="CardNo",
                        HeaderText="卡号",
                        DataType = typeof(string),     
                    },
                    new JQGridColumn()
                    {
                        DataField="MemberName",
                        HeaderText="会员",
                        DataType = typeof(string),     
                    },
                    new JQGridColumn()
                    {
                        DataField="CupType",
                        HeaderText="杯型",
                        DataType = typeof(decimal)
                    },
                    new JQGridColumn()
                    {
                        DataField="Amount",
                        HeaderText="金额",
                        DataType = typeof(decimal)
                    },
                    new JQGridColumn()
                    {
                        DataField="Quantity",
                        HeaderText="数量",
                        DataType = typeof(decimal)
                    },                    
                    new JQGridColumn()
                    {
                        DataField="CreateDate",
                        HeaderText="消费日期",
                        DataType = typeof(string),
                        SearchType= SearchType.DatePicker,
                        SearchControlID = "DatePicker",
                        DataFormatString = "{0:yyyy-MM-dd HH:mm}",
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptName",
                        HeaderText="门店",
                        DataType = typeof(string)
                    }
                }
            };
            ReportGrid.ToolBarSettings.ShowSearchButton = true;
            ReportGrid.SearchDialogSettings.MultipleSearch = true;
            ReportGrid.AppearanceSettings.Caption = "销售排名统计";
            ReportGrid.ToolBarSettings.CustomButtons = new List<JQGridToolBarButton>()
            {
                new JQGridToolBarButton()
                {
                    Position = ToolBarButtonPosition.Last,
                    ToolTip="导出EXCEL",
                    Text="导出",
                    OnClick="customButtonClicked",
                    ButtonIcon="ui-icon-extlink"
                }
            };
        }
    }
    public class Report5Model
    {
        [Display(Name = "门店：")]
        public Guid? DeptId { get; set; }
        public bool IsDept { get; set; }

        [Display(Name = "消费类型：")]
        public int? ConsumeType { get; set; }
        public bool IsConsumeType { get; set; }

        [Display(Name = "支付方式：")]
        public Guid? PayType { get; set; }
        public bool IsPayType { get; set; }

        [Display(Name = "分类：")]
        public Guid? Category { get; set; }
        public bool IsCategory { get; set; }

        [Display(Name = "商品：")]
        public Guid? Inventory { get; set; }
        public bool IsInventory { get; set; }

        [Display(Name = "杯型：")]
        public int? CupType { get; set; }
        public bool IsCupType { get; set; }

        [Display(Name = "卡号：")]
        public string CardNo { get; set; }
        [Display(Name = "会员名：")]
        public string MemberName { get; set; }

        [Display(Name = "会员卡排名：")]
        public bool IsCard { get; set; }
        [Display(Name = "开始日期：")]
        public string BeginDate { get; set; }
        [Display(Name = "结束日期：")]
        public string EndDate { get; set; }

        public IQueryable<Report5Result> result { get; set; }

        [Display(Name = "金额：")]
        public decimal Amount { get; set; }
        [Display(Name = "数量：")]
        public decimal Quantity { get; set; }
        public int InvType { get; set; }
        public int CategoryType { get; set; }
        public int DeptType { get; set; }
    }
    public class Report5Result
    {
        public Guid Id { get; set; }
        public string DeptName { get; set; }
        public string CategoryName { get; set; }
        public string InventoryName { get; set; }
        public string ConsumeType { get; set; }
        public string PayType { get; set; }
        public string CardNo { get; set; }
        public string MemberName { get; set; }
        public string CupType { get; set; }
        public decimal Amount { get; set; }
        public decimal Quantity { get; set; }
        public string UnitOfMeasureName { get; set; }
    }
    #endregion

    #region Report6
    public class Report6Model
    {
        [Display(Name = "门店：")]
        public Guid? DeptId { get; set; }

        [Display(Name = "开始日期：")]        
        public string BeginDate { get; set; }

        [Display(Name = "结束日期：")]
        public string EndDate { get; set; }

        public Report6Result result { get; set; }
    }
    public class Report6Result
    {
        public List<Report6Recharge> Recharge { get; set; }
        public List<Report6Consume> MemberConsume{get;set;}
        public List<Report6Consume> NoMemberConsume{get;set;}
        public List<Report6Card> Card { get; set; }
    }
    public class Report6Recharge
    {
        public string PayType { get; set; }
        public decimal Amount { get; set; }
        public decimal Donate { get; set; }
        public int Count { get; set; }
    }
    public class Report6Consume
    {
        public string PayType { get; set; }
        public decimal Amount { get; set; }
        public decimal Quantity { get; set; }
        public int Count { get; set; }
    }
    public class Report6Card
    {
        public string Status { get; set; }
        public decimal Balance { get; set; }
        public decimal Point { get; set; }
        public decimal Fee { get; set; }
        public int Count { get; set; }
    }
    #endregion
    #region Report7
    public class Report7Grid
    {
        public JQGrid ReportGrid { get; set; }
        public Report7Grid()
        {
            ReportGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="ConsumeType",
                        HeaderText="消费类型",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptName",
                        HeaderText="门店",
                        DataType = typeof(string),     
                    },
                    new JQGridColumn()
                    {
                        DataField="FullName",
                        HeaderText="操作员",
                        DataType = typeof(string),     
                    },
                    
                    new JQGridColumn()
                    {
                        DataField="Amount",
                        HeaderText="金额",
                        DataType = typeof(decimal)
                    },                
                    new JQGridColumn()
                    {
                        DataField="CreateDate",
                        HeaderText="消费日期",
                        DataType = typeof(string),
                        SearchType= SearchType.DatePicker,
                        SearchControlID = "DatePicker",
                        DataFormatString = "{0:yyyy-MM-dd HH:mm}",
                    }
                }
            };
            ReportGrid.ToolBarSettings.ShowSearchButton = true;
            ReportGrid.SearchDialogSettings.MultipleSearch = true;
            ReportGrid.AppearanceSettings.Caption = "寻仙记各分店日常结账情况";
            ReportGrid.ToolBarSettings.CustomButtons = new List<JQGridToolBarButton>()
            {
                new JQGridToolBarButton()
                {
                    Position = ToolBarButtonPosition.Last,
                    ToolTip="导出EXCEL",
                    Text="导出",
                    OnClick="customButtonClicked",
                    ButtonIcon="ui-icon-extlink"
                }
            };
        }
    }

    public class Report7Model
    {
        [Display(Name="开始日期：")]
        [Required]
        public string BeginDate { get; set; }

        [Display(Name = "结束日期：")]
        [Required]
        public string EndDate { get; set; }

        [Display(Name = "门店：")]
        public Guid? DeptId { get; set; }

        [Display(Name = "收银员：")]
        public Guid? UserId { get; set; }

        public List<Report7Result> result { get; set; }

        [Display(Name = "总现金收入：")]
        public decimal SumCash { get; set; }

        [Display(Name = "实际消费：")]
        public decimal SumConsume { get; set; }

        [Display(Name = "刷卡消费：")]
        public decimal CardConsume { get; set; }

        [Display(Name = "办卡（张）：")]
        public int CardCount { get; set; }
        [Display(Name = "办卡（元）：")]
        public decimal CardRecharge { get; set; }

        [Display(Name = "充值（张）：")]
        public int RechargeCount { get; set; }
        [Display(Name = "充值（元）：")]
        public decimal RechargeSum { get; set; }

        [Display(Name = "总杯数：")]
        public decimal SumCup { get; set; }

        [Display(Name = "平均单价：")]
        public decimal AvgPrice { get; set; }

        [Display(Name = "补卡（张）：")]
        public decimal AddCardCount { get; set; }
        
        [Display(Name = "补卡（元）：")]
        public decimal AddCardSum { get; set; }

    }
    public class Report7Result
    {
        public Guid Id { get; set; }
        public string CreateDate { get; set; }        
        public string DeptName { get; set; }        
        public string FullName { get; set; }
        public decimal SumCash { get; set; }
        public decimal SumConsume { get; set; }
        public decimal CardConsume { get; set; }
        public int CardCount { get; set; }
        public decimal CardRecharge { get; set; }
        public int RechargeCount { get; set; }
        public decimal RechargeSum { get; set; }
        public decimal SumCup { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal AddCardCount { get; set; }
        public decimal AddCardSum { get; set; }
    }
    #endregion
    #region Report8
    public class Report8Model
    {
        [Display(Name = "开始日期：")]
        [Required]
        public string BeginDate { get; set; }
        [Display(Name = "终止日期：")]
        [Required]
        public string EndDate { get; set; }

        [Display(Name = "门店：")]
        public Guid? DeptId { get; set; }

        [Display(Name = "收银员：")]
        public Guid? UserId { get; set; }

        public List<Report8Result> result { get; set; }

        [Display(Name = "应收总金额：")]
        public decimal Amount { get; set; }

        [Display(Name = "长款金额：")]
        public decimal More { get; set; }
        [Display(Name = "长款天数：")]
        public int MoreDays { get; set; }

        [Display(Name = "短款金额：")]
        public decimal Less { get; set; }
        [Display(Name = "短款天数：")]
        public int LessDays { get; set; }

    }
    public class Report8Result
    {
        [Display(Name = "收银员：")]
        public string FullName { get; set; }
        [Display(Name = "应收总金额：")]
        public decimal Amount { get; set; }
        [Display(Name = "长款金额：")]
        public decimal More { get; set; }
        [Display(Name = "长款天数：")]        
        public int MoreDays { get; set; }
        public decimal MoreRatio { get; set; }
        [Display(Name = "短款金额：")]
        public decimal Less { get; set; }
        [Display(Name = "短款天数：")] 
        public int LessDays { get; set; }
        public decimal LessRatio { get; set; }
        public int NormalDays { get; set; }
        public decimal NormalRatio { get; set; }
        [Display(Name = "情况说明：")]
        public string Comment { get; set; }
    }
    #endregion
    #region Report9
    public class Report9Model
    {
        [Display(Name = "年度")]
        public string year { get; set; }
        [Display(Name = "月份")]
        public string month { get; set; }
        [Display(Name = "杯数")]
        public bool IsCup { get; set; }
    }
    #endregion

    #region Report10
    public class Report10Model
    {
        [Display(Name = "开始日期：")]
        [Required]
        public string BeginDate { get; set; }

        [Display(Name = "结束日期：")]
        [Required]
        public string EndDate { get; set; }

        [Display(Name = "门店：")]
        public Guid? DeptId { get; set; }

        [Display(Name = "收银员：")]
        public Guid? UserId { get; set; }

        [Display(Name = "分类：")]
        public Guid? Category { get; set; }

        public List<Report10Result> result { get; set; }
        [Display(Name = "销售数量：")]
        public decimal Cups { get; set; }
        [Display(Name = "销售金额：")]
        public decimal Amount { get; set; }
        [Display(Name = "日均销量：")]
        public decimal AmountOfDayAvg { get; set; }
        [Display(Name = "数量比例：")]
        public decimal CupsRatio { get; set; }
        [Display(Name = "金额比例：")]
        public decimal AmountRatio { get; set; }
        public int InvType { get; set; }
        public int CategoryType { get; set; }
        public int DeptType { get; set; }
    }
    public class Report10Result
    {
        public Guid Id { get; set; }
        public string CreateDate { get; set; }
        public string DeptName { get; set; }
        public string FullName { get; set; }
        public Guid Category { get; set; }
        public string CategoryName { get; set; }
        public decimal Cups { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountOfDayAvg { get; set; }
        public decimal CupsRatio { get; set; }
        public decimal AmountRatio { get; set; }
    }
    public class Report101Result
    {
        public Guid Id { get; set; }
        public Guid DeptId { get; set; }
        public string DeptName { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public Guid Category { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Quantity { get; set; }
    }
    #endregion
    #region Report11
    public class Report11Model
    {
        [Display(Name = "开始日期：")]
        [Required]
        public string BeginDate { get; set; }

        [Display(Name = "结束日期：")]
        [Required]
        public string EndDate { get; set; }

        [Display(Name = "门店：")]
        public Guid? DeptId { get; set; }

        [Display(Name = "收银员：")]
        public Guid? UserId { get; set; }

        [Display(Name = "分类：")]
        public Guid? Category { get; set; }

        [Display(Name = "商品：")]
        public Guid? Inventory { get; set; }

        public List<Report11Result> result { get; set; }

        [Display(Name = "销售数量：")]
        public decimal Cups { get; set; }
        [Display(Name = "销售金额：")]
        public decimal Amount { get; set; }
        [Display(Name = "日均销量：")]
        public decimal AmountOfDayAvg { get; set; }

        [Display(Name = "销售数量比例：")]
        public decimal CupsRatioOfCategory { get; set; }
        [Display(Name = "数量总比例：")]
        public decimal CupsRatioOfAll { get; set; }
        [Display(Name = "销售金额比例：")]
        public decimal AmountRatioOfCategory { get; set; }
        [Display(Name = "金额总比例：")]
        public decimal AmountRatioOfAll { get; set; }
        public int InvType { get; set; }
        public int CategoryType { get; set; }
        public int DeptType { get; set; }
    }
    public class Report11Result
    {
        public Guid Id { get; set; }
        public string CreateDate { get; set; }
        public string DeptName { get; set; }
        public string FullName { get; set; }
        public string OperatorsOnDuty { get; set; }
        public Guid Category { get; set; }
        public string CategoryName { get; set; }
        public Guid Inventory { get; set; }
        public string InventoryName { get; set; }
        public decimal Cups { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountOfDayAvg { get; set; }
        public decimal CupsRatioOfCategory { get; set; }
        public decimal CupsRatioOfAll { get; set; }
        public decimal AmountRatioOfCategory { get; set; }
        public decimal AmountRatioOfAll { get; set; }
    }
    public class Report111Result
    {
        public Guid Id { get; set; }
        public Guid DeptId { get; set; }
        public string DeptName { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string OperatorsOnDuty { get; set; }
        public Guid Category { get; set; }
        public string CategoryName { get; set; }
        public Guid Inventory { get; set; }
        public string InventoryName { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Quantity { get; set; }
    }
    #endregion

    #region Report12
    public class Report12Model
    {
        [Display(Name = "开始日期：")]
        [Required]
        public string BeginDate { get; set; }

        [Display(Name = "结束日期：")]
        [Required]
        public string EndDate { get; set; }

        [Display(Name = "门店：")]
        public Guid? DeptId { get; set; }

        [Display(Name = "收银员：")]
        public Guid? UserId { get; set; }

        public List<Report12Result> result { get; set; }
        public Report12Result curResult { get; set; }
    }
    public class Report121Result
    {
        public Guid Id { get; set; }
        public Guid DeptId { get; set; }
        public string DeptName { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Amount { get; set; }
    }
    public class Report12Result
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [Display(Name = "收银员：")]
        public string FullName { get; set; }
        public Guid DeptId { get; set; }
        [Display(Name = "店名：")]
        public string DeptName { get; set; }
        [Display(Name = "日期：")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DifDate { get; set; }
        [Display(Name="应收款：")]
        public decimal Amount { get; set; }
        [Display(Name = "长款金额：")]
        public decimal More { get; set; }
        [Display(Name = "短款金额：")]
        public decimal Less { get; set; }
        [Display(Name = "情况说明：")]
        public string Comment { get; set; }
        public int IsIn { get; set; }
    }
    #endregion

    #region Report13
    public class Report13Model
    {
        [Display(Name = "门店")]
        public Guid? deptId { get; set; }
        [Display(Name = "收银员")]
        public Guid? userId { get; set; }
        [Display(Name = "分类")]
        public Guid? category { get; set; }
        [Display(Name = "单品")]
        public Guid? inventory { get; set; }
        [Display(Name = "开始时间")]
        public DateTime beginDate { get; set; }
        [Display(Name = "结束时间")]
        public DateTime endDate { get; set; }
    }
    #endregion

    #region Report14
    public class Report14Model
    {
        [Display(Name = "门店：")]
        public Guid? DeptId { get; set; }
        [Display(Name = "操作员：")]
        public Guid? UserId { get; set; }

        [Display(Name = "消费类型：")]
        public int? ConsumeType { get; set; }

        [Display(Name = "支付方式：")]
        public Guid? PayType { get; set; }

        [Display(Name = "卡号：")]
        public string CardNo { get; set; }
        [Display(Name = "会员名：")]
        public string MemberName { get; set; }

        [Display(Name = "开始日期：")]
        public string BeginDate { get; set; }
        [Display(Name = "结束日期：")]
        public string EndDate { get; set; }

        public List<Report14Result> result { get; set; }

        [Display(Name = "单数：")]
        public int Count { get; set; }

        [Display(Name = "数量：")]
        public decimal Quantity { get; set; }
        [Display(Name = "合计：")]
        public decimal Sum { get; set; }
        [Display(Name = "金额：")]
        public decimal Amount { get; set; }
        public int InvType { get; set; }
        public int CategoryType { get; set; }
        public int DeptType { get; set; }
    }
    public class Report14Result
    {
        public Guid Consume { get; set; }
        public int Id { get; set; }
        public string DeptName { get; set; }
        public string FullName { get; set; }
        public string OperatorsOnDuty { get; set; }
        public string ConsumeType { get; set; }
        public string PayType { get; set; }

        public string CardNo { get; set; }
        public string MemberName { get; set; }
        //public string CategoryName { get; set; }
        //public string InventoryName { get; set; }


        //public string Cup { get; set; }
        //public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal Sum { get; set; }
        public decimal Discount { get; set; }
        public decimal Amount { get; set; }

        public DateTime CreateDate { get; set; }

    }
    #endregion
}