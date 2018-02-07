using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace DXInfo.Web.Models
{
    public class CardDonateInventoryModels
    {
    }

    public class CardDonateInventoryGrid
    {
        public JQGrid CardGrid { get; set; }
        public JQGrid InventoryGrid { get; set; }
        public JQGrid InventoryGrid2 { get; set; }
        public CardDonateInventoryGrid()
        {
            CardGrid = new JQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField = "Id",
                        PrimaryKey = true,
                        DataType = typeof(Guid),
                        Visible = false,
                        Searchable=false,
                        Hidedlg=true,
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
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="Balance",
                        HeaderText="余额",
                        DataType = typeof(decimal),
                        Formatter = new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="Recharge",
                        HeaderText="充值",
                        DataType = typeof(decimal),
                        Formatter = new DigitFormatter(),
                    },
                    new JQGridColumn()
                    {
                        DataField="Points",
                        HeaderText="积分",
                        DataType = typeof(decimal),
                        Formatter = new DigitFormatter(),
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
                        DataType = typeof(string),                        
                        Editable=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptName",
                        HeaderText="发卡门店",
                        DataType = typeof(string)     
                    }
                }
            };
            CardGrid.ToolBarSettings.ShowSearchButton = true;
            CardGrid.SearchDialogSettings.MultipleSearch = true;
            CardGrid.HierarchySettings.HierarchyMode = HierarchyMode.Parent;            
            CardGrid.MultiSelect = true;
            CardGrid.MultiSelectKey = MultiSelectKey.None;
            CardGrid.MultiSelectMode = MultiSelectMode.SelectOnCheckBoxClickOnly;
            CardGrid.ToolBarSettings.CustomButtons = new List<JQGridToolBarButton>()
            {
                new JQGridToolBarButton()
                {
                    Position = ToolBarButtonPosition.Last,
                    ToolTip="批量设置商品赠送",
                    Text="批量设置商品赠送",
                    OnClick="customButtonClicked",
                    ButtonIcon="ui-icon-extlink"
                }
            };

            InventoryGrid = new JQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        EditActionIconsColumn=true,
                        EditActionIconsSettings = new EditActionIconsSettings(){ ShowEditIcon=true,ShowDeleteIcon=false},
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
                        DataField="IsValidate",
                        HeaderText="是否赠送",
                        DataType=typeof(bool),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="InvalideDate",
                        HeaderText="有效期",
                        DataType=typeof(DateTime),
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="Code",
                        HeaderText="编码",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="Name",
                        HeaderText="名称",
                        DataType=typeof(string),
                    },
                    
                    new JQGridColumn()
                    {
                        DataField="CategoryName",
                        HeaderText="存货分类",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="UnitName",
                        HeaderText="计量单位",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice",
                        HeaderText="默认单价",
                        DataType=typeof(decimal),
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice0",
                        HeaderText="大杯单价",
                        DataType=typeof(decimal),
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice1",
                        HeaderText="中杯单价",
                        DataType=typeof(decimal),
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice2",
                        HeaderText="小杯单价",
                        DataType=typeof(decimal),
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePoint",
                        HeaderText="默认积分",
                        DataType=typeof(decimal),
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePoint0",
                        HeaderText="大杯积分",
                        DataType=typeof(decimal),
                    },
                     new JQGridColumn()
                    {
                        DataField="SalePoint1",
                        HeaderText="中杯积分",
                        DataType=typeof(decimal),
                    },
                     new JQGridColumn()
                    {
                        DataField="SalePoint2",
                        HeaderText="小杯积分",
                        DataType=typeof(decimal),
                    },                                        
                }
            };
            InventoryGrid.AppearanceSettings.Caption = "存货档案";
            InventoryGrid.ToolBarSettings.ShowSearchButton = true;
            InventoryGrid.ToolBarSettings.ShowEditButton = true;
            InventoryGrid.ToolBarSettings.ShowRefreshButton = true;
            InventoryGrid.SearchDialogSettings.MultipleSearch = true;
            InventoryGrid.HierarchySettings.HierarchyMode = HierarchyMode.Child;
            InventoryGrid.AutoWidth = false;

            InventoryGrid2 = new JQGrid()
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
                        DataField="Code",
                        HeaderText="编码",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="Name",
                        HeaderText="名称",
                        DataType=typeof(string),
                    },
                    
                    new JQGridColumn()
                    {
                        DataField="CategoryName",
                        HeaderText="存货分类",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="UnitName",
                        HeaderText="计量单位",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice",
                        HeaderText="默认单价",
                        DataType=typeof(decimal),
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice0",
                        HeaderText="大杯单价",
                        DataType=typeof(decimal),
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice1",
                        HeaderText="中杯单价",
                        DataType=typeof(decimal),
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePrice2",
                        HeaderText="小杯单价",
                        DataType=typeof(decimal),
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePoint",
                        HeaderText="默认积分",
                        DataType=typeof(decimal),
                    },
                    new JQGridColumn()
                    {
                        DataField="SalePoint0",
                        HeaderText="大杯积分",
                        DataType=typeof(decimal),
                    },
                     new JQGridColumn()
                    {
                        DataField="SalePoint1",
                        HeaderText="中杯积分",
                        DataType=typeof(decimal),
                    },
                     new JQGridColumn()
                    {
                        DataField="SalePoint2",
                        HeaderText="小杯积分",
                        DataType=typeof(decimal),
                    },
                }
            };
            InventoryGrid2.SearchDialogSettings.MultipleSearch = true;
            InventoryGrid2.MultiSelect = true;
            InventoryGrid2.MultiSelectKey = MultiSelectKey.None;
            InventoryGrid2.MultiSelectMode = MultiSelectMode.SelectOnRowClick;
            
        }
    }
}