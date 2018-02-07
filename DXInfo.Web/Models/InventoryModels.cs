using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace DXInfo.Web.Models
{
    public class InventoryModels
    {
        public int InvType { get; set; }
        public EntityJQGrid InventoryGrid { get; set; }
    }
    public class CDInventoryGridModel : InventoryModels
    {
        public CDInventoryGridModel()
        {
            InventoryGrid = new EntityJQGrid()
            {
                Width = Unit.Pixel(1400),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType=typeof(Guid),
                        PrimaryKey=true,
                        Visible=false,
                        Searchable=false,
                        Hidedlg=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Code",
                        HeaderText="编码",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Name",
                        HeaderText="名称",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Category",
                        HeaderText="存货分类",
                        DataType=typeof(Guid),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="CategoryName",
                        HeaderText="存货分类",
                        DataType=typeof(string),
                        Visible=false,                        
                        Searchable=false,
                        Hidedlg=true,  
                        Exportable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="UnitOfMeasure",
                        HeaderText="计量单位",
                        DataType=typeof(Guid),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="UnitOfMeasureName",
                        HeaderText="计量单位",
                        DataType=typeof(string),
                        Visible=false,                        
                        Searchable=false,
                        Hidedlg=true,
                        Exportable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice",
                        HeaderText="默认单价",
                        DataType=typeof(decimal),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice0",
                        HeaderText="大杯单价",
                        DataType=typeof(decimal),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice1",
                        HeaderText="中杯单价",
                        DataType=typeof(decimal),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice2",
                        HeaderText="小杯单价",
                        DataType=typeof(decimal),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePoint",
                        HeaderText="默认积分",
                        DataType=typeof(decimal),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePoint0",
                        HeaderText="大杯积分",
                        DataType=typeof(decimal),
                        Editable=true,
                    },
                     new JQGridColumn()
                    {
                        DataField="SalePoint1",
                        HeaderText="中杯积分",
                        DataType=typeof(decimal),
                        Editable=true,
                    },
                     new JQGridColumn()
                    {
                        DataField="SalePoint2",
                        HeaderText="小杯积分",
                        DataType=typeof(decimal),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Specs",
                        HeaderText="规格型号",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="Printer",
                    //    HeaderText="打印机",
                    //    DataType=typeof(string),
                    //    Editable=true,
                    //},
                    new JQGridColumn()
                    {
                        DataField="IsDonate",
                        HeaderText="是否赠送商品",
                        DataType=typeof(bool),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Comment",
                        HeaderText="描述",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="IsInvalid",
                        HeaderText="是否失效",
                        DataType=typeof(bool),
                        Editable=true,
                    },            
                    new JQGridColumn()
                    {
                        DataField="InvType",
                        DataType=typeof(int),
                        Visible=false,
                        Searchable = false,
                        Hidedlg=true,
                        SearchToolBarOperation = SearchOperation.IsEqualTo,
                    },
                }
            };
            InventoryGrid.ToolBarSettings.CustomButtons.Add(new JQGridToolBarButton()
            {
                Text = "正常存货",
                ToolTip = "正常存货",
                ButtonIcon = "ui-icon-search",
                OnClick = "normalInv",
                Position = ToolBarButtonPosition.Last,
            });
            InventoryGrid.ToolBarSettings.CustomButtons.Add(new JQGridToolBarButton()
            {
                Text = "失效存货",
                ToolTip = "失效存货",
                ButtonIcon = "ui-icon-search",
                OnClick = "invalidInv",
                Position = ToolBarButtonPosition.Last,
            });
        }
    }    
    public class WRInventoryGridModel : InventoryModels
    {
        public WRInventoryGridModel()
        {
            InventoryGrid = new EntityJQGrid()
            {
                Width = Unit.Pixel(1600),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType=typeof(Guid),
                        PrimaryKey=true,
                        Visible=false,
                        Searchable=false, 
                        Hidedlg=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Code",
                        HeaderText="编码",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Name",
                        HeaderText="名称",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="EnglishName",
                        HeaderText="英文名",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Category",
                        HeaderText="存货分类",
                        DataType=typeof(Guid),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="CategoryName",
                        HeaderText="存货分类",
                        DataType=typeof(string),
                        Visible=false,
                        Searchable=false,
                        Exportable=true,
                        Hidedlg=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="UnitOfMeasure",
                        HeaderText="计量单位",
                        DataType=typeof(string),
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="UnitOfMeasureName",
                        HeaderText="计量单位",
                        DataType=typeof(string),
                        Visible=false,
                        Searchable=false,
                        Hidedlg=true,
                        Exportable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice",
                        HeaderText="默认单价",
                        DataType=typeof(decimal),
                        Editable=true,
                    },
                    
                    new JQGridColumn()
                    {
                        DataField="SalePoint",
                        HeaderText="默认积分",
                        DataType=typeof(decimal),
                        Editable=true,
                    },
                   new JQGridColumn()
                    {
                        DataField="IsRecommend",
                        HeaderText="是否推荐",
                        DataType=typeof(bool),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="ImageFileName",
                        HeaderText="图片文件名",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Specs",
                        HeaderText="规格型号",
                        DataType=typeof(string),
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="IsDonate",
                        HeaderText="是否赠送商品",
                        DataType=typeof(bool),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Comment",
                        HeaderText="描述",
                        DataType=typeof(string),
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="Stars",
                        HeaderText="评分",
                        DataType=typeof(int),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Feature",
                        HeaderText="产品特点",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Dosage",
                        HeaderText="产品配料",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Palette",
                        HeaderText="建议搭配",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Printer",
                        HeaderText="打印机",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="EnglishIntroduce",
                        HeaderText="英文介绍",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="EnglishDosage",
                        HeaderText="英文配料",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="IsPackage",
                        HeaderText="是否套餐",
                        DataType=typeof(bool),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="IsInvalid",
                        HeaderText="是否失效",
                        DataType=typeof(bool),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Sort",
                        HeaderText="排序",
                        DataType=typeof(int),
                        Editable=true,
                        Formatter = new IntegerFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="InvType",
                        DataType=typeof(int),
                        Visible=false,
                        Searchable = false,
                        SearchToolBarOperation = SearchOperation.IsEqualTo,
                        Hidedlg=true,
                    },
                }
            };            

            InventoryGrid.ToolBarSettings.CustomButtons.Add(new JQGridToolBarButton() { 
                Text="正常存货",
                ToolTip = "正常存货",
                ButtonIcon = "ui-icon-search",
                OnClick="normalInv",
                Position= ToolBarButtonPosition.Last,
            });
            InventoryGrid.ToolBarSettings.CustomButtons.Add(new JQGridToolBarButton()
            {
                Text = "失效存货",
                ToolTip = "失效存货",
                ButtonIcon = "ui-icon-search",
                OnClick = "invalidInv",
                Position = ToolBarButtonPosition.Last,
            });
            InventoryGrid.ToolBarSettings.CustomButtons.Add(new JQGridToolBarButton()
            {
                Text = "批量删除失效图片",
                ToolTip = "批量删除失效图片",
                ButtonIcon = "ui-icon-trash",
                OnClick = "deleteImg",
                Position = ToolBarButtonPosition.Last,
            });
        }
    }
    public class STInventoryGridModel:InventoryModels
    {
        public STInventoryGridModel()
        {
            InventoryGrid = new EntityJQGrid()
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
                        Hidedlg=true,
                    },  
                    new JQGridColumn()
                    {
                        DataField="Code",
                        HeaderText="编码",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogColumnPosition=1,
                        EditDialogRowPosition=1,

                    },
                    new JQGridColumn()
                    {
                        DataField="Name",
                        HeaderText="名称",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogColumnPosition=1,
                        EditDialogRowPosition=2,
                    },
                    new JQGridColumn()
                    {
                        DataField="Category",
                        HeaderText="存货分类",
                        DataType=typeof(Guid),
                        Editable=true,
                        EditDialogColumnPosition=1,
                        EditDialogRowPosition=3,
                        Visible=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="CategoryName",
                        HeaderText="存货分类",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                        Exportable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="MeasurementUnitGroup",
                        HeaderText="计量单位组",
                        DataType=typeof(Guid),
                        Editable=true,
                        EditDialogColumnPosition=1,
                        EditDialogRowPosition=4,
                        Visible=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="MeasurementUnitGroupName",
                        HeaderText="计量单位组",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                        Exportable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="MainUnit",
                        HeaderText="主计量单位",
                        DataType=typeof(Guid),
                        Editable=true,
                        EditDialogColumnPosition=1,
                        EditDialogRowPosition=5,
                    },
                    new JQGridColumn()
                    {
                        DataField="MainUnitName",
                        HeaderText="主计量单位",
                        DataType=typeof(string),
                        Visible=false,
                        Searchable=false,
                        Exportable=true,
                        Hidedlg=true,
                    },                                        
                    new JQGridColumn()
                    {
                        DataField="StockUnit",
                        HeaderText="库存单位",
                        DataType=typeof(Guid),
                        Editable=true,
                        EditDialogColumnPosition=1,
                        EditDialogRowPosition=6,
                    },
                    new JQGridColumn()
                    {
                        DataField="StockUnitName",
                        HeaderText="库存单位",
                        DataType=typeof(string),
                        Visible=false,
                        Searchable=false,
                        Exportable=true,
                        Hidedlg=true,
                    },
                    
                    new JQGridColumn()
                    {
                        DataField="Specs",
                        HeaderText="规格型号",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogColumnPosition=1,
                        EditDialogRowPosition=7,
                    },
                    
                    new JQGridColumn()
                    {
                        DataField="IsInvalid",
                        HeaderText="是否失效",
                        DataType=typeof(bool),
                        Editable=true,
                        EditDialogColumnPosition=1,
                        EditDialogRowPosition=8,
                    },
                    
                    new JQGridColumn()
                    {
                        DataField="Comment",
                        HeaderText="描述",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogColumnPosition=1,
                        EditDialogRowPosition=9,
                    },
                    new JQGridColumn()
                    {
                        DataField="IsSale",
                        HeaderText="是否零售",
                        DataType=typeof(bool),
                        Editable=true,
                        EditDialogColumnPosition=1,
                        EditDialogRowPosition=10,
                    },
                    new JQGridColumn()
                    {
                        DataField="ImageFileName",
                        HeaderText="图片文件名",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogColumnPosition=1,
                        EditDialogRowPosition=11,
                    },

                    new JQGridColumn()
                    {
                        DataField="HighStock",
                        HeaderText="最高库存",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new NumberValidator()
                        },
                        Formatter = new DigitFormatter(),
                        EditDialogColumnPosition=2,
                        EditDialogRowPosition=1,
                    },
                    new JQGridColumn()
                    {
                        DataField="LowStock",
                        HeaderText="最低库存",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new NumberValidator()
                        },
                        Formatter = new DigitFormatter(),
                        EditDialogColumnPosition=2,
                        EditDialogRowPosition=2,
                    },
                    new JQGridColumn()
                    {
                        DataField="SecurityStock",
                        HeaderText="安全库存",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new NumberValidator()
                        },
                        Formatter = new DigitFormatter(),
                        EditDialogColumnPosition=2,
                        EditDialogRowPosition=3,
                    },
                    
                    //new JQGridColumn()
                    //{
                    //    DataField="LastCheckDate",
                    //    HeaderText="上次盘点日期",
                    //    DataType=typeof(DateTime),
                    //    Editable=true,  
                    //    EditDialogColumnPosition=2,
                    //    EditDialogRowPosition=4,
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="CheckCycle",
                    //    HeaderText="盘点周期单位",
                    //    DataType=typeof(int),
                    //    Editable=true,
                    //    EditDialogColumnPosition=2,
                    //    EditDialogRowPosition=5,
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="CheckCycleName",
                    //    HeaderText="盘点周期单位",
                    //    DataType=typeof(string),
                    //    Visible=false,
                    //    Searchable=false,
                    //    Exportable=true,
                    //    Hidedlg=true,
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="SomeDay",
                    //    HeaderText="每天/周/月第（）天",
                    //    DataType=typeof(int),
                    //    Editable=true,
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>(){new NumberValidator()},
                    //    EditDialogColumnPosition=2,
                    //    EditDialogRowPosition=6,
                    //},
                    new JQGridColumn()
                    {
                        DataField="EarlyWarningDay",
                        HeaderText="预警天数",
                        DataType=typeof(int),
                        Editable=true,
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>(){new NumberValidator()},
                        EditDialogColumnPosition=2,
                        EditDialogRowPosition=4,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLife",
                        HeaderText="保质期",
                        DataType=typeof(int),
                        Editable=true,
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>(){new NumberValidator()},
                        EditDialogColumnPosition=2,
                        EditDialogRowPosition=5,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeType",
                        HeaderText="保质期单位",
                        DataType=typeof(int),
                        Editable=true,
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>(){new NumberValidator()},
                        EditDialogColumnPosition=2,
                        EditDialogRowPosition=6,
                    },
                    new JQGridColumn()
                    {
                        DataField="ShelfLifeTypeName",
                        HeaderText="保质期单位",
                        DataType=typeof(string),
                        Visible=false,
                        Searchable=false,
                        Exportable=true,
                        Hidedlg=true,
                    },
                    
                    new JQGridColumn()
                    {
                        DataField="UnitOfMeasure",
                        HeaderText="销售单位",
                        DataType=typeof(Guid),
                        Editable=true,
                        EditDialogColumnPosition=3,
                        EditDialogRowPosition=1,
                    },
                    new JQGridColumn()
                    {
                        DataField="UnitOfMeasureName",
                        HeaderText="销售单位",
                        DataType=typeof(string),
                        Visible=false,
                        Searchable=false,
                        Exportable=true,
                        Hidedlg=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice",
                        HeaderText="零售单价",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogColumnPosition=3,
                        EditDialogRowPosition=2,
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePoint",
                        HeaderText="零售积分",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogColumnPosition=3,
                        EditDialogRowPosition=3,
                    },
                    new JQGridColumn()
                    {
                        DataField="Printer",
                        HeaderText="打印机",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogColumnPosition=3,
                        EditDialogRowPosition=4,
                    },
                    new JQGridColumn()
                    {
                        DataField="IsDonate",
                        HeaderText="是否赠送商品",
                        DataType=typeof(bool),
                        Editable=true,
                        EditDialogColumnPosition=3,
                        EditDialogRowPosition=5,
                    },

                    new JQGridColumn()
                    {
                        DataField="IsPackage",
                        HeaderText="是否套餐",
                        DataType=typeof(bool),
                        Editable=true,
                        EditDialogColumnPosition=3,
                        EditDialogRowPosition=6,
                    },

                    new JQGridColumn()
                    {
                        DataField="IsRecommend",
                        HeaderText="是否推荐",
                        DataType=typeof(bool),
                        Editable=true,
                        EditDialogColumnPosition=4,
                        EditDialogRowPosition=1,
                    },
                    
                    new JQGridColumn()
                    {
                        DataField="Sort",
                        HeaderText="排序",
                        DataType=typeof(int),
                        Editable=true,
                        Formatter = new IntegerFormatter(),  
                        EditDialogColumnPosition=4,
                        EditDialogRowPosition=2,
                    },
                    new JQGridColumn()
                    {
                        DataField="Stars",
                        HeaderText="评分",
                        DataType=typeof(int),
                        Editable=true,
                        EditDialogColumnPosition=4,
                        EditDialogRowPosition=3,
                    },
                    new JQGridColumn()
                    {
                        DataField="Feature",
                        HeaderText="产品特点",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogColumnPosition=4,
                        EditDialogRowPosition=4,
                    },
                    new JQGridColumn()
                    {
                        DataField="Dosage",
                        HeaderText="产品配料",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogColumnPosition=4,
                        EditDialogRowPosition=5,
                    },
                    new JQGridColumn()
                    {
                        DataField="Palette",
                        HeaderText="建议搭配",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogColumnPosition=4,
                        EditDialogRowPosition=6,
                    },
                    
                    new JQGridColumn()
                    {
                        DataField="EnglishName",
                        HeaderText="英文名",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogColumnPosition=4,
                        EditDialogRowPosition=7,
                    },
                    new JQGridColumn()
                    {
                        DataField="EnglishIntroduce",
                        HeaderText="英文介绍",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogColumnPosition=4,
                        EditDialogRowPosition=8,
                    },
                    new JQGridColumn()
                    {
                        DataField="EnglishDosage",
                        HeaderText="英文配料",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogColumnPosition=4,
                        EditDialogRowPosition=9,
                    },

                    new JQGridColumn()
                    {
                        DataField="Karat",
                        HeaderText="含量",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogColumnPosition=5,
                        EditDialogRowPosition=1,
                    },
                    new JQGridColumn()
                    {
                        DataField="MetalWeight",
                        HeaderText="金属重量",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogColumnPosition=5,
                        EditDialogRowPosition=2,
                    },
                    new JQGridColumn()
                    {
                        DataField="Jewelweight",
                        HeaderText="宝石重量",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogColumnPosition=5,
                        EditDialogRowPosition=3,
                    },
                    new JQGridColumn()
                    {
                        DataField="TotalWeight",
                        HeaderText="总重量",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogColumnPosition=5,
                        EditDialogRowPosition=4,
                    },
                    new JQGridColumn()
                    {
                        DataField="QTY",
                        HeaderText="数量",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogColumnPosition=5,
                        EditDialogRowPosition=5,
                    },
                    new JQGridColumn()
                    {
                        DataField="OrderNo",
                        HeaderText="订货号",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogColumnPosition=5,
                        EditDialogRowPosition=6,
                    },
                    new JQGridColumn()
                    {
                        DataField="VendorSpec",
                        HeaderText="供应商型号",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogColumnPosition=5,
                        EditDialogRowPosition=7,
                    },
                    new JQGridColumn()
                    {
                        DataField="Length",
                        HeaderText="长度",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogColumnPosition=5,
                        EditDialogRowPosition=8,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvType",
                        DataType=typeof(int),
                        Visible=false,
                        Searchable = false,
                        Hidedlg=true,
                        SearchToolBarOperation = SearchOperation.IsEqualTo,
                    },
                }
            };

            InventoryGrid.ToolBarSettings.CustomButtons.Add(new JQGridToolBarButton()
            {
                Text = "<span class='ui-pg-button-text'>正常存货</span>",
                ToolTip = "正常存货",
                ButtonIcon = "ui-icon-search",
                OnClick = "normalInv",
                Position = ToolBarButtonPosition.Last,
            });
            InventoryGrid.ToolBarSettings.CustomButtons.Add(new JQGridToolBarButton()
            {
                Text = "<span class='ui-pg-button-text'>失效存货</span>",
                ToolTip = "失效存货",
                ButtonIcon = "ui-icon-search",
                OnClick = "invalidInv",
                Position = ToolBarButtonPosition.Last,
            });

            InventoryGrid.ToolBarSettings.CustomButtons.Add(new JQGridToolBarButton()
            {
                Text = "<span class='ui-pg-button-text'>批量删除失效图片</span>",
                ToolTip = "批量删除失效图片",
                ButtonIcon = "ui-icon-trash",
                OnClick = "deleteImg",
                Position = ToolBarButtonPosition.Last,
            });
            //InventoryGrid.ToolBarSettings.CustomButtons.Add(new JQGridToolBarButton()
            //{
            //    Text = "<span class='ui-pg-button-text'>导入EXCEL</span>",
            //    ToolTip = "导入EXCEL",
            //    ButtonIcon = "ui-icon-extlink",
            //    OnClick = "LoadExcel",
            //    Position = ToolBarButtonPosition.Last,
            //});
        }
    }

    public class InvPriceGridModel
    {
        public int InvType { get; set; }
        public EntityJQGrid InvPriceGrid { get; set; }
        public InvPriceGridModel()
        {
            InvPriceGrid = new EntityJQGrid()
            {
                Height = Unit.Percentage(100),
                Width=Unit.Pixel(1200),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        EditActionIconsColumn=true,
                        Viewable=false,
                        Hidedlg=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType=typeof(Guid),
                        PrimaryKey=true,
                        Visible=false,
                        Searchable=false,
                        Hidedlg=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Code",
                        HeaderText="批号",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Name",
                        HeaderText="年份",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvId",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvName",
                        HeaderText="存货名称",
                        DataType=typeof(string),                       
                        Searchable=false,
                        Visible=false,
                        Exportable=true,
                        Hidedlg=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice",
                        HeaderText="单价",
                        DataType=typeof(decimal),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePoint",
                        HeaderText="积分",
                        DataType=typeof(decimal),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="IsInvalid",
                        HeaderText="是否失效",
                        DataType=typeof(bool),
                        Editable=true,
                    },  
                    new JQGridColumn()
                    {
                        DataField="Comment",
                        HeaderText="描述",
                        DataType=typeof(string),
                        Editable=true,
                    }, 
                    new JQGridColumn()
                    {
                        DataField="InvType",
                        DataType=typeof(int),
                        Visible=false,
                        Searchable=false,
                        SearchToolBarOperation= SearchOperation.IsEqualTo,
                        Hidedlg=true,
                    }, 
                }
            };
            
        }
    }
}