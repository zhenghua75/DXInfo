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
    public class WRReportModels
    {
    }
    #region WRReport1
    public class WRReport1GridModel
    {
        public JQGrid WRReportGrid { get; set; }
        public WRReport1GridModel()
        {
            WRReportGrid = new JQGrid()
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
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="CardType",
                        HeaderText="卡类型",
                        DataType = typeof(Guid),
                        Visible=false
                    },
                    new JQGridColumn()
                    {
                        DataField="CardTypeName",
                        HeaderText="卡类型",
                        DataType = typeof(string),
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="CardLevel",
                        HeaderText="卡级别",
                        DataType = typeof(Guid),
                        Visible=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="CardLevelName",
                        HeaderText="卡级别",
                        DataType = typeof(string),                        
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="Status",
                        HeaderText="状态",
                        DataType = typeof(int),
                        Visible=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="StatusName",
                        HeaderText="状态",
                        DataType = typeof(string),                        
                        Searchable=false
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
                        DataField="DeptId",
                        HeaderText="发卡部门",
                        DataType = typeof(Guid),
                        Visible=false
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptName",
                        HeaderText="发卡部门",
                        DataType = typeof(string),
                        Searchable=false
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
            //WRReportGrid.SearchDialogSettings.MultipleSearch = true;
            //WRReportGrid.ToolBarSettings.ShowRefreshButton = true;
            //WRReportGrid.AppearanceSettings.Caption = "会员资料查询";
            //WRReportGrid.ShrinkToFit = true;
            //WRReportGrid.ToolBarSettings.CustomButtons = new List<JQGridToolBarButton>()
            //{
            //    new JQGridToolBarButton()
            //    {
            //        Position = ToolBarButtonPosition.Last,
            //        ToolTip="导出EXCEL",
            //        Text="导出",
            //        OnClick="customButtonClicked",
            //        ButtonIcon="ui-icon-extlink"
            //    }
            //};
        }
    }
    public class WRReport1Model
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

        public List<WRReport1Result> result { get; set; }

        [Display(Name = "积分：")]
        public decimal Points { get; set; }
        [Display(Name = "余额：")]
        public decimal Balance { get; set; }
        [Display(Name = "充值金额：")]
        public decimal Recharge { get; set; }
    }
    public class WRReport1Result
    {
        public Guid CardType { get; set; }
        public string CardTypeName { get; set; }
        public Guid CardLevel { get; set; }
        public string CardLevelName { get; set; }
        public Guid DeptId { get; set; }
        public string DeptName { get; set; }
        public string CardNo { get; set; }
        public string MemberName { get; set; }
        
        public decimal Balance { get; set; }
        public decimal Recharge { get; set; }
        public decimal Points { get; set; }

        public DateTime CreateDate { get; set; }
        public string FullName { get; set; }        
        public int Status { get; set; }
        public string StatusName { get; set; }
        public string Email { get; set; }
        public string IdCard { get; set; }
        public string LinkAddress { get; set; }
        public string LinkPhone { get; set; }
        public string Sex { get; set; }
        public DateTime? Birthday { get; set; }
        
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

    #region WRReport2
    public class WRReport2GridModel
    {
        public int InvType { get; set; }
        public int CategoryType { get; set; }
        public JQGrid WRReport2Grid { get; set; }
        public WRReport2GridModel()
        {
            WRReport2Grid = new JQGrid()
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
                        DataField="SectionId",
                        HeaderText="部门",
                        DataType = typeof(int),
                        Visible=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="SectionName",
                        HeaderText="部门",
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
                        DataType = typeof(string)
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
                        DataField="IsPackage",
                        HeaderText="套餐",
                        DataType = typeof(bool),
                        Visible=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="PackageId",
                        HeaderText="套餐",
                        DataType = typeof(Guid),
                        Visible=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="PackageName",
                        HeaderText="套餐",
                        DataType = typeof(string)
                    },     
                    new JQGridColumn()
                    {
                        DataField="Inventory",
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
                        HeaderText="消费日期",
                        DataType = typeof(DateTime),
                        Width=140,
                        DataFormatString = "{0:yyyy-MM-dd HH:mm}",
                    },
                    new JQGridColumn()
                    {
                        DataField="InvType",
                        DataType=typeof(int),
                        Visible=false,
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="CategoryType",
                        DataType=typeof(int),
                        Visible=false,
                        Searchable=false
                    },
                }
            };
        }
    }
    public class WRReport2Model
    {
        [Display(Name = "门店：")]
        public Guid? DeptId { get; set; }

        [Display(Name = "部门：")]
        public int? Section { get; set; }

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
        [Display(Name = "是否套餐：")]
        public bool? IsPackage { get; set; }
        public List<WRReport2Result> result { get; set; }

        [Display(Name = "数量：")]
        public decimal Quantity { get; set; }
        [Display(Name = "合计：")]
        public decimal Sum { get; set; }
        [Display(Name = "金额：")]
        public decimal Amount { get; set; }
    }
    public class WRReport2Result
    {
        public string DeptName { get; set; }
        public string SectionName { get; set; }
        public string FullName { get; set; }
        public string ConsumeType { get; set; }
        public string PayType { get; set; }

        public string CardNo { get; set; }
        public string MemberName { get; set; }
        public string CategoryName { get; set; }
        public string PackageName { get; set; }

        public string InventoryName { get; set; }
        
       
        //public string Cup { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal Sum { get; set; }
        public decimal Discount { get; set; }
        public decimal Amount { get; set; }
                
        public DateTime CreateDate { get; set; }

    }
    #endregion

    #region WRReport3
    public class WRReport3GridModel
    {
        public JQGrid WRReportGrid { get; set; }
        public WRReport3GridModel()
        {
            WRReportGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
                Columns = new List<JQGridColumn>()
                {
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
                        DataType = typeof(string),
                        Searchable=false
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
                        DataType = typeof(string),
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="UserId",
                        HeaderText="操作员",
                        DataType = typeof(Guid),
                        Visible=false
                    },
                    new JQGridColumn()
                    {
                        DataField="FullName",
                        HeaderText="操作员",
                        DataType = typeof(string),
                        Searchable=false
                    },
                     new JQGridColumn()
                    {
                        DataField="OperatorsOnDuty",
                        HeaderText="当班操作员",
                        DataType = typeof(string),
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="RechargeType",
                        HeaderText="充值类型",
                        DataType = typeof(int), 
                        Visible=false
                    },
                    new JQGridColumn()
                    {
                        DataField="RechargeTypeName",
                        HeaderText="充值类型",
                        DataType = typeof(string),  
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="PayType",
                        HeaderText="支付方式",
                        DataType = typeof(int),   
                        Visible=false
                    },
                    new JQGridColumn()
                    {
                        DataField="PayTypeName",
                        HeaderText="支付方式",
                        DataType = typeof(string),  
                        Searchable=false
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
                        SearchType= SearchType.DateTimePicker,
                        SearchControlID = "DateTimePicker",
                        DataFormatString = "{0:yyyy-MM-dd HH:mm}",
                    },
                }
            };
            //WRReportGrid.ToolBarSettings.ShowRefreshButton = true;
            //WRReportGrid.SearchDialogSettings.MultipleSearch = true;
            //WRReportGrid.AppearanceSettings.Caption = "充值明细查询";
            //WRReportGrid.ToolBarSettings.CustomButtons = new List<JQGridToolBarButton>()
            //{
            //    new JQGridToolBarButton()
            //    {
            //        Position = ToolBarButtonPosition.Last,
            //        ToolTip="导出EXCEL",
            //        Text="导出",
            //        OnClick="customButtonClicked",
            //        ButtonIcon="ui-icon-extlink"
            //    }
            //};
        }
    }
    public class WRReport3Model
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

        public List<WRReport3Result> result { get; set; }

        [Display(Name = "充值金额：")]
        public decimal Amount { get; set; }
        [Display(Name = "赠送金额：")]
        public decimal Donate { get; set; }
    }
    public class WRReport3Result
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

    #region WRReport4
    public class WRReport4Grid
    {
        
        public JQGrid WRReportGrid { get; set; }
        [Display(Name = "开始日期")]
        public DateTime? BeginDate { get; set; }
        [Display(Name = "结束日期")]
        public DateTime? EndDate { get; set; }
        public WRReport4Grid()
        {
            WRReportGrid = new JQGrid()
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
                    //new JQGridColumn()
                    //{
                    //    DataField="CupType",
                    //    HeaderText="杯型",
                    //    DataType = typeof(string)
                    //},
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
            WRReportGrid.ToolBarSettings.ShowSearchButton = true;
            WRReportGrid.SearchDialogSettings.MultipleSearch = true;
            WRReportGrid.AppearanceSettings.Caption = "消费分类统计";
            WRReportGrid.ToolBarSettings.CustomButtons = new List<JQGridToolBarButton>()
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
            //WRReportGrid.AppearanceSettings.ShowRowNumbers = true;
            
            
        }
    }
    public class WRReport4Model
    {
        [Display(Name = "门店：")]
        public Guid? DeptId { get; set; }
        public bool IsDept { get; set; }

        [Display(Name = "部门：")]
        public int? Section { get; set; }
        public bool IsSection { get; set; }

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

        //[Display(Name = "杯型：")]
        //public int? CupType { get; set; }
        //public bool IsCupType { get; set; }

        [Display(Name = "开始日期：")]
        public string BeginDate { get; set; }
        [Display(Name = "结束日期：")]
        public string EndDate { get; set; }

        public IQueryable<WRReport4Result> result { get; set; }

        [Display(Name = "金额：")]
        public decimal Amount { get; set; }
        [Display(Name = "数量：")]
        public decimal Quantity { get; set; }
        public int InvType { get; set; }
        public int CategoryType { get; set; }
    }
    public class WRReport4Result
    {
        public Guid Id { get; set; }
        public string DeptName { get; set; }
        public string SectionName { get; set; }
        public string CategoryName { get; set; }
        public string InventoryName { get; set; }
        public string ConsumeType { get; set; }
        public string PayType { get; set; }
        //public string CupType { get; set; }
        public decimal Amount { get; set; }
        public decimal Quantity { get; set; }
    }
    #endregion

    #region WRReport5
    public class WRReport5Grid
    {
        public JQGrid WRReportGrid { get; set; }
        public WRReport5Grid()
        {
            WRReportGrid = new JQGrid()
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
                    //new JQGridColumn()
                    //{
                    //    DataField="CupType",
                    //    HeaderText="杯型",
                    //    DataType = typeof(decimal)
                    //},
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
            WRReportGrid.ToolBarSettings.ShowSearchButton = true;
            WRReportGrid.SearchDialogSettings.MultipleSearch = true;
            WRReportGrid.AppearanceSettings.Caption = "销售排名统计";
            WRReportGrid.ToolBarSettings.CustomButtons = new List<JQGridToolBarButton>()
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
    public class WRReport5Model
    {
        [Display(Name = "门店：")]
        public Guid? DeptId { get; set; }
        public bool IsDept { get; set; }

        [Display(Name = "部门：")]
        public int? Section { get; set; }
        public bool IsSection { get; set; }

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

        //[Display(Name = "杯型：")]
        //public int? CupType { get; set; }
        //public bool IsCupType { get; set; }

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

        public IQueryable<WRReport5Result> result { get; set; }

        [Display(Name = "金额：")]
        public decimal Amount { get; set; }
        [Display(Name = "数量：")]
        public decimal Quantity { get; set; }
        public int InvType { get; set; }
        public int CategoryType { get; set; }
    }
    public class WRReport5Result
    {
        public Guid Id { get; set; }
        public string DeptName { get; set; }
        public string SectionName { get; set; }
        public string CategoryName { get; set; }
        public string InventoryName { get; set; }
        public string ConsumeType { get; set; }
        public string PayType { get; set; }
        public string CardNo { get; set; }
        public string MemberName { get; set; }
        //public string CupType { get; set; }
        public decimal Amount { get; set; }
        public decimal Quantity { get; set; }
    }
    #endregion

    #region WRReport6
    public class WRReport6Model
    {
        [Display(Name = "门店：")]
        public Guid? DeptId { get; set; }

        [Display(Name = "开始日期：")]        
        public string BeginDate { get; set; }

        [Display(Name = "结束日期：")]
        public string EndDate { get; set; }

        public WRReport6Result result { get; set; }
    }
    public class WRReport6Result
    {
        public List<WRReport6Recharge> Recharge { get; set; }
        public List<WRReport6Consume> MemberConsume{get;set;}
        public List<WRReport6Consume> NoMemberConsume{get;set;}
        public List<WRReport6Card> Card { get; set; }
    }
    public class WRReport6Recharge
    {
        public string PayType { get; set; }
        public decimal Amount { get; set; }
        public decimal Donate { get; set; }
        public int Count { get; set; }
    }
    public class WRReport6Consume
    {
        public string PayType { get; set; }
        public decimal Amount { get; set; }
        public decimal Quantity { get; set; }
        public int Count { get; set; }
    }
    public class WRReport6Card
    {
        public string Status { get; set; }
        public decimal Balance { get; set; }
        public decimal Point { get; set; }
        public decimal Fee { get; set; }
        public int Count { get; set; }
    }
    #endregion
    #region WRReport7
    public class WRReport7Grid
    {
        public JQGrid WRReportGrid { get; set; }
        public WRReport7Grid()
        {
            WRReportGrid = new JQGrid()
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
            WRReportGrid.ToolBarSettings.ShowSearchButton = true;
            WRReportGrid.SearchDialogSettings.MultipleSearch = true;
            WRReportGrid.AppearanceSettings.Caption = "寻仙记各分店日常结账情况";
            WRReportGrid.ToolBarSettings.CustomButtons = new List<JQGridToolBarButton>()
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

    public class WRReport7Model
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

        public int rowCount { get; set; }
        public List<WRReport7Result> result { get; set; }
        public List<WRReport7Result> result2 { get; set; }

        [Display(Name = "资金总收入：")]
        public decimal? Sum { get; set; }

        [Display(Name = "实收现金：")]
        public decimal? Cash { get; set; }

        [Display(Name = "实收银行卡：")]
        public decimal? Bank { get; set; }

        [Display(Name = "实收其它：")]
        public decimal? Other { get; set; }

        [Display(Name = "销售收入：")]
        public decimal? SumConsume { get; set; }

        [Display(Name = "会员卡消费：")]
        public decimal? CardConsume { get; set; }
        [Display(Name = "现金消费：")]
        public decimal? CashConsume { get; set; }
        [Display(Name = "银行卡消费：")]
        public decimal? BankConsume { get; set; }
        [Display(Name = "代金券消费：")]
        public decimal? VoucherConsume { get; set; }
        [Display(Name = "其它消费：")]
        public decimal? OtherConsume { get; set; }

        [Display(Name = "打折卡消费：")]
        public decimal? DiscountCardConsume { get; set; }

        [Display(Name = "办卡（张）：")]
        public int? CardQuantity { get; set; }

        [Display(Name = "现金办卡（张）：")]
        public int? CashCardQuantity { get; set; }
        [Display(Name = "银行卡办卡（张）：")]
        public int? BankCardQuantity { get; set; }
        [Display(Name = "其它办卡（张）：")]
        public int? OtherCardQuantity { get; set; }

        [Display(Name = "办卡（元）：")]
        public decimal? CardAmount { get; set; }

        [Display(Name = "现金办卡（元）：")]
        public decimal? CashCardAmount { get; set; }
        [Display(Name = "银行卡办卡（元）：")]
        public decimal? BankCardAmount { get; set; }
        [Display(Name = "其它办卡（元）：")]
        public decimal? OtherCardAmount { get; set; }

        [Display(Name = "充值（张）：")]
        public int? RechargeQuantity { get; set; }
        [Display(Name = "充值（元）：")]
        public decimal? RechargeAmount { get; set; }

        [Display(Name = "现金充值（张）：")]
        public int? CashRechargeQuantity { get; set; }
        [Display(Name = "现金充值（元）：")]
        public decimal? CashRechargeAmount { get; set; }

        [Display(Name = "银行卡充值（张）：")]
        public int? BankRechargeQuantity { get; set; }
        [Display(Name = "银行卡充值（元）：")]
        public decimal? BankRechargeAmount { get; set; }

        [Display(Name = "其它充值（张）：")]
        public int? OtherRechargeQuantity { get; set; }
        [Display(Name = "其它充值（元）：")]
        public decimal? OtherRechargeAmount { get; set; }

        [Display(Name = "总数量：")]
        public decimal? SumQuantity { get; set; }

        [Display(Name = "平均单价：")]
        public decimal? AvgPrice { get; set; }

        [Display(Name = "补卡（张）：")]
        public int? AddCardQuantity { get; set; }
        
        [Display(Name = "补卡（元）：")]
        public decimal? AddCardAmount { get; set; }

        [Display(Name = "到店人数：")]
        public decimal? ComeQuantity { get; set; }

        [Display(Name = "合计储值（元）：")]
        public decimal? Sum1 { get; set; }
        [Display(Name = "合计消费（元）：")]
        public decimal? Sum2 { get; set; }
        [Display(Name = "占用会员（元）：")]
        public decimal? Sum3 { get; set; }
    }
    public class WRReport7Result
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }        
        public string DeptName { get; set; }        
        public string FullName { get; set; }
        public string OperatorsOnDuty { get; set; }
        public decimal Sum { get; set; }//资金总收入
        public decimal Cash { get; set; }//实收现金
        public decimal Bank { get; set; }//实收银行卡
        //public decimal Voucher { get; set; }//实收代金券
        public decimal Other { get; set; }//实收其它

        public decimal SumConsume { get; set; }//销售收入

        public decimal CardConsume { get; set; }//会员卡消费
        public decimal CashConsume { get; set; }//现金消费
        public decimal BankConsume { get; set; }//银行卡消费
        public decimal VoucherConsume { get; set; }//代金券消费
        public decimal OtherConsume { get; set; }//其它消费

        public decimal DiscountCardConsume { get; set; }//打折卡消费       
        //办卡张     
        public int CardQuantity { get; set; }//办卡（张）
        public int CashCardQuantity { get; set; }//办卡（张）
        public int BankCardQuantity { get; set; }//办卡（张）
        public int OtherCardQuantity { get; set; }//办卡（张）
        //办卡元
        public decimal CardAmount { get; set; }//办卡（元）
        public decimal CashCardAmount { get; set; }//办卡（元）
        public decimal BankCardAmount { get; set; }//办卡（元）
        public decimal OtherCardAmount { get; set; }//办卡（元）
        //充值张
        public int RechargeQuantity { get; set; }//充值（张）
        public int CashRechargeQuantity { get; set; }//充值（张）
        public int BankRechargeQuantity { get; set; }//充值（张）        
        public int OtherRechargeQuantity { get; set; }//充值（张）
        //充值元
        public decimal RechargeAmount { get; set; }//充值（元） 
        public decimal CashRechargeAmount { get; set; }//充值（元）
        public decimal BankRechargeAmount { get; set; }//充值（元）             
        public decimal OtherRechargeAmount { get; set; }//充值（元）

        //数量
        public decimal SumQuantity { get; set; }
        //平均单价
        public decimal AvgPrice { get; set; }
        //补卡
        public int AddCardQuantity { get; set; }
        public decimal AddCardAmount { get; set; }

        public decimal ExchangeCardCount { get; set; }//换卡(张）
        //到店人数
        public decimal ComeQuantity { get; set; }
    }
    #endregion
    #region WRReport8
    public class WRReport8Model
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

        public List<WRReport8Result> result { get; set; }

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
    public class WRReport8Result
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
    #region WRReport9
    public class WRReport9Model
    {
        [Display(Name = "年度")]
        public string year { get; set; }
        [Display(Name = "月份")]
        public string month { get; set; }
        //[Display(Name = "杯数")]
        //public bool IsCup { get; set; }
    }
    #endregion

    #region WRReport10
    public class WRReport10Model
    {
        [Display(Name = "开始日期：")]
        [Required]
        public string BeginDate { get; set; }

        [Display(Name = "结束日期：")]
        [Required]
        public string EndDate { get; set; }

        [Display(Name = "门店：")]
        public Guid? DeptId { get; set; }

        [Display(Name = "部门：")]
        public int? Section { get; set; }

        [Display(Name = "收银员：")]
        public Guid? UserId { get; set; }

        [Display(Name = "分类：")]
        public Guid? Category { get; set; }

        public List<WRReport10Result> result { get; set; }
        //[Display(Name = "销售杯数：")]
        //public decimal Cups { get; set; }
        [Display(Name = "销售金额：")]
        public decimal Amount { get; set; }
        [Display(Name = "日均销量：")]
        public decimal AmountOfDayAvg { get; set; }
        //[Display(Name = "杯数比例：")]
        //public decimal CupsRatio { get; set; }
        [Display(Name = "金额比例：")]
        public decimal AmountRatio { get; set; }
        public int InvType { get; set; }
        public int CategoryType { get; set; }
    }
    public class WRReport10Result
    {
        public Guid Id { get; set; }
        public string CreateDate { get; set; }
        public string DeptName { get; set; }
        public string SectionName { get; set; }
        public string FullName { get; set; }
        public Guid Category { get; set; }
        public string CategoryName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountOfDayAvg { get; set; }
        public decimal QuantityRatio { get; set; }
        public decimal AmountRatio { get; set; }
    }
    public class WRReport101Result
    {
        public Guid Id { get; set; }
        public Guid DeptId { get; set; }
        public string DeptName { get; set; }
        public int? Section { get; set; }
        public string SectionName { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public Guid Category { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Quantity { get; set; }
    }
    #endregion
    #region WRReport11
    public class WRReport11Model
    {
        [Display(Name = "开始日期：")]
        [Required]
        public string BeginDate { get; set; }

        [Display(Name = "结束日期：")]
        [Required]
        public string EndDate { get; set; }

        [Display(Name = "门店：")]
        public Guid? DeptId { get; set; }

        [Display(Name = "部门：")]
        public int? Section { get; set; }

        [Display(Name = "收银员：")]
        public Guid? UserId { get; set; }

        [Display(Name = "分类：")]
        public Guid? Category { get; set; }

        [Display(Name = "商品：")]
        public Guid? Inventory { get; set; }

        public List<WRReport11Result> result { get; set; }

        //[Display(Name = "销售杯数：")]
        //public decimal Cups { get; set; }
        [Display(Name = "销售金额：")]
        public decimal Amount { get; set; }
        [Display(Name = "日均销量：")]
        public decimal AmountOfDayAvg { get; set; }

        //[Display(Name = "销售杯数比例：")]
        //public decimal CupsRatioOfCategory { get; set; }
        //[Display(Name = "杯数总比例：")]
        public decimal CupsRatioOfAll { get; set; }
        [Display(Name = "销售金额比例：")]
        public decimal AmountRatioOfCategory { get; set; }
        [Display(Name = "金额总比例：")]
        public decimal AmountRatioOfAll { get; set; }
        public int InvType { get; set; }
        public int CategoryType { get; set; }
    }
    public class WRReport11Result
    {
        public Guid Id { get; set; }
        public string CreateDate { get; set; }
        public string DeptName { get; set; }
        public string FullName { get; set; }
        public Guid Category { get; set; }
        public string CategoryName { get; set; }
        public Guid Inventory { get; set; }
        public string InventoryName { get; set; }
        //public decimal Cups { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountOfDayAvg { get; set; }
        //public decimal CupsRatioOfCategory { get; set; }
        //public decimal CupsRatioOfAll { get; set; }
        public decimal AmountRatioOfCategory { get; set; }
        public decimal AmountRatioOfAll { get; set; }
        public int Section { get; set; }
        public string SectionName { get; set; }
    }
    public class WRReport111Result
    {
        public Guid Id { get; set; }
        public Guid DeptId { get; set; }
        public string DeptName { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public Guid Category { get; set; }
        public string CategoryName { get; set; }
        public Guid Inventory { get; set; }
        public string InventoryName { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Quantity { get; set; }
        public int? Section { get; set; }
        public string SectionName { get; set; }
    }
    #endregion

    #region WRReport12
    public class WRReport12Model
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

        public List<WRReport12Result> result { get; set; }
        public WRReport12Result curResult { get; set; }
    }
    public class WRReport121Result
    {
        public Guid Id { get; set; }
        public Guid DeptId { get; set; }
        public string DeptName { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Amount { get; set; }
    }
    public class WRReport12Result
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [Display(Name = "收银员：")]
        public string FullName { get; set; }
        public Guid DeptId { get; set; }
        [Display(Name = "店名：")]
        public string DeptName { get; set; }
        [Display(Name = "日期：")]
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

    #region WRReport13
    public class WRReport13Model
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
        public string beginDate { get; set; }
        [Display(Name = "结束时间")]
        public string endDate { get; set; }
    }
    #endregion

    #region WRReport14
    public class WRReport14Model
    {
        [Display(Name = "门店：")]
        public Guid? DeptId { get; set; }

        [Display(Name = "部门：")]
        public int? Section { get; set; }

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

        public List<WRReport14Result> result { get; set; }

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
    }
    public class WRReport14Result
    {
        public int Consume { get; set; }
        public int Id { get; set; }
        public string DeptName { get; set; }
        public string FullName { get; set; }
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
        public string SectionName { get; set; }

    }
    #endregion


    #region WRReport15
    public class WRReport15Model
    {
        [Display(Name = "开始日期：")]
        [Required]
        public string BeginDate { get; set; }

        [Display(Name = "结束日期：")]
        [Required]
        public string EndDate { get; set; }

        [Display(Name = "门店：")]
        public Guid? DeptId { get; set; }

        //[Display(Name = "收银员：")]
        //public Guid? UserId { get; set; }

        //[Display(Name = "分类：")]
        //public Guid? Category { get; set; }

        public List<WRReport15Result> result { get; set; }
        [Display(Name = "销售数量：")]
        public decimal Quantity { get; set; }
        [Display(Name = "销售金额：")]
        public decimal Amount { get; set; }
        [Display(Name = "日均销量：")]
        public decimal AmountOfDayAvg { get; set; }
        [Display(Name = "数量比例：")]
        public decimal QuantityRatio { get; set; }
        [Display(Name = "金额比例：")]
        public decimal AmountRatio { get; set; }
        public int InvType { get; set; }
        public int CategoryType { get; set; }
    }
    public class WRReport15Result
    {
        public Guid Id { get; set; }
        public string CreateDate { get; set; }
        public string DeptName { get; set; }
        public int? Section { get; set; }
        public string SectionName { get; set; }
        //public Guid Category { get; set; }
        //public string CategoryName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountOfDayAvg { get; set; }
        public decimal QuantityRatio { get; set; }
        public decimal AmountRatio { get; set; }
    }
    public class WRReport151Result
    {
        public Guid Id { get; set; }
        public Guid DeptId { get; set; }
        public string DeptName { get; set; }
        public Guid UserId { get; set; }
        public int? Section { get; set; }
        public string SectionName { get; set; }
        //public Guid Category { get; set; }
        //public string CategoryName { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Quantity { get; set; }
    }
    #endregion

#region WRReport16
    public class WRReport16Model
    {
        [Display(Name = "开始日期：")]
        [Required]
        public string BeginDate { get; set; }

        [Display(Name = "结束日期：")]
        [Required]
        public string EndDate { get; set; }

        [Display(Name = "门店：")]
        public Guid? DeptId { get; set; }

        [Display(Name = "菜品操作员：")]
        public Guid? UserId { get; set; }

        [Display(Name = "菜品状态：")]
        public int? OrderMenuStatus { get; set; }
        [Display(Name = "订单状态：")]
        public int? OrderDishStatus { get; set; }

        [Display(Name = "桌台：")]
        public string DeskNo { get; set; }
        public int rowCount { get; set; }
        public List<WRReport16Result> result { get; set; }
        public List<WRReport16Result> result2 { get; set; }
    }
    public class WRReport16Result
    {
        public string DeptName { get; set; }
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }
        public string OrderDishStatus { get; set; }
        public string OrderDishFullName { get; set; }
        public DateTime OrderDishCreateDate { get; set; }
        public string DeskNo { get; set; }
        public string OrderDeskFullName { get; set; }
        public DateTime OrderDeskCreateDate { get; set; }
        public string OrderMenuInvName { get; set; }
        public string OrderMenuStatus { get; set; }
        public decimal OrderMenuInvPrice { get; set; }
        public decimal OrderMenuInvQuantity { get; set; }
        public decimal OrderMenuInvAmount { get; set; }
        public string OrderMenuFullName { get; set; }
        public DateTime OrderMenuCreateDate { get; set; }
    }
#endregion

    #region WRReport17
    public class WRReport17Grid
    {
        public JQGrid WRReportGrid { get; set; }
        public WRReport17Grid()
        {
            WRReportGrid = new JQGrid()
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
            WRReportGrid.ToolBarSettings.ShowSearchButton = true;
            WRReportGrid.SearchDialogSettings.MultipleSearch = true;
            WRReportGrid.AppearanceSettings.Caption = "寻仙记各分店日常结账情况";
            WRReportGrid.ToolBarSettings.CustomButtons = new List<JQGridToolBarButton>()
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

    public class WRReport17Model
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

        public int rowCount { get; set; }
        public List<WRReport17Result> result { get; set; }
        public List<WRReport17Result> result2 { get; set; }

        [Display(Name = "资金总收入：")]
        public decimal? SumAmount { get; set; }
        [Display(Name = "销售收入：")]
        public decimal? SumConsume { get; set; }

        [Display(Name = "办卡（张）：")]
        public int? CardCount { get; set; }
        [Display(Name = "办卡（元）：")]
        public decimal? CardRecharge { get; set; }

        [Display(Name = "充值（张）：")]
        public int? RechargeCount { get; set; }
        [Display(Name = "充值（元）：")]
        public decimal? RechargeSum { get; set; }

        [Display(Name = "总数量：")]
        public decimal? SumCup { get; set; }

        [Display(Name = "平均单价：")]
        public decimal? AvgPrice { get; set; }

        [Display(Name = "补卡（张）：")]
        public int? AddCardCount { get; set; }

        [Display(Name = "补卡（元）：")]
        public decimal? AddCardSum { get; set; }
    }
    public class WRReport17Result
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string DeptName { get; set; }
        public string FullName { get; set; }
        public string OperatorsOnDuty { get; set; }
        public string PayTypeName { get; set; }

        public decimal SumAmount { get; set; }//资金总收入
        public decimal SumConsume { get; set; }//销售收入

        public int CardCount { get; set; }//办卡（张）
        public decimal CardRecharge { get; set; }//办卡（元）

        public int RechargeCount { get; set; }//充值（张）
        public decimal RechargeSum { get; set; }//充值（元）

        public decimal SumCup { get; set; }
        public decimal AvgPrice { get; set; }

        public int AddCardCount { get; set; }
        public decimal AddCardSum { get; set; }

        public decimal ExchangeCardCount { get; set; }//换卡(张）
    }
    #endregion

    #region WRReport18
    public class WRReport18Grid
    {
        public JQGrid WRReportGrid { get; set; }
        public WRReport18Grid()
        {
            WRReportGrid = new JQGrid()
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
            WRReportGrid.ToolBarSettings.ShowSearchButton = true;
            WRReportGrid.SearchDialogSettings.MultipleSearch = true;
            WRReportGrid.AppearanceSettings.Caption = "寻仙记各分店日常结账情况";
            WRReportGrid.ToolBarSettings.CustomButtons = new List<JQGridToolBarButton>()
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

    public class WRReport18Model
    {
        [Display(Name = "开始日期：")]
        [Required]
        public string BeginDate { get; set; }

        [Display(Name = "结束日期：")]
        [Required]
        public string EndDate { get; set; }

        [Display(Name = "门店：")]
        public Guid? DeptId { get; set; }

        //[Display(Name = "收银员：")]
        //public Guid? UserId { get; set; }

        public int rowCount { get; set; }
        public List<WRReport18Result> result { get; set; }
        //public List<WRReport18Result> result2 { get; set; }

        //[Display(Name = "资金总收入：")]
        //public decimal? SumAmount { get; set; }
        //[Display(Name = "销售收入：")]
        //public decimal? SumConsume { get; set; }

        //[Display(Name = "办卡（张）：")]
        //public int? CardCount { get; set; }
        //[Display(Name = "办卡（元）：")]
        //public decimal? CardRecharge { get; set; }

        //[Display(Name = "充值（张）：")]
        //public int? RechargeCount { get; set; }
        //[Display(Name = "充值（元）：")]
        //public decimal? RechargeSum { get; set; }

        //[Display(Name = "总数量：")]
        //public decimal? SumCup { get; set; }

        //[Display(Name = "平均单价：")]
        //public decimal? AvgPrice { get; set; }

        //[Display(Name = "补卡（张）：")]
        //public int? AddCardCount { get; set; }

        //[Display(Name = "补卡（元）：")]
        //public decimal? AddCardSum { get; set; }
    }
    public class WRReport18Result
    {
        public string LocalDeptName { get; set; }
        public string RemoteDeptName { get; set; }
        public decimal? FillFee_Pay { get; set; }
        public decimal? FillProm_Pay { get; set; }
        public decimal? Fee_Pay { get; set; }
        public decimal? sumFee_Pay { get; set; }
        public decimal? FillFee_Income { get; set; }
        public decimal? FillProm_Income { get; set; }
        public decimal? Fee_Income { get; set; }
        public decimal? sumFee_Income { get; set; }
        public decimal? FillFee_Dif { get; set; }
        public decimal? FillProm_Dif { get; set; }
        public decimal? Fee_Dif { get; set; }
        public decimal? sumFee_Dif { get; set; }
    }
    #endregion

#region WRReport19
    public class WRReport19GridModel
    {
        public JQGrid WRReport19Grid { get; set; }
        public WRReport19GridModel()
        {
            WRReport19Grid = new JQGrid()
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
                        DataField="CardNo",
                        HeaderText="卡号",
                        DataType=typeof(string),
                        Searchable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="MemberName",
                        HeaderText="会员名",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="BillTypeName",
                        HeaderText="消费类型",
                        DataType=typeof(string),
                    },
                    
                    new JQGridColumn()
                    {
                        DataField="LastBalance",
                        HeaderText="上次余额",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}
                    },
                    new JQGridColumn()
                    {
                        DataField="Sum",
                        HeaderText="本次应收",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}
                    },
                    new JQGridColumn()
                    {
                        DataField="Discount",
                        HeaderText="折扣%",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}
                    },
                    new JQGridColumn()
                    {
                        DataField="Amount",
                        HeaderText="实收金额",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}
                    },
                    new JQGridColumn()
                    {
                        DataField="Balance",
                        HeaderText="余额",
                        DataType=typeof(decimal),
                        Formatter=new CustomFormatter(){ FormatFunction="FormatNumber",UnFormatFunction="UnFormatNumber"}
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptName",
                        HeaderText="门店",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="FullName",
                        HeaderText="操作员",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="CreateDate",
                        HeaderText="操作时间",
                        DataType=typeof(DateTime),    
                        DataFormatString="{0:yyyy-MM-dd hh:mm:ss}",
                        Width=140,
                    },
                    new JQGridColumn()
                    {
                        DataField="PayTypeName",
                        HeaderText="支付方式",
                        DataType=typeof(string),
                    },
                }
            };
            
        }
    }
#endregion
}