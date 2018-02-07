using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace ynhnTransportManage.Models
{
    public class CardsModels
    {
    }
    public class IssueCardsGridModel
    {
        public JQGrid CardsGrid { get; set; }
        public IssueCardsGridModel()
        {
            CardsGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType = typeof(Guid),
                        PrimaryKey=true,
                        Visible=false
                    },
                    new JQGridColumn()
                    {
                        DataField="CardNo",
                        HeaderText="卡号",
                        DataType=typeof(string),
                        Editable=true,
                        //EditType = EditType.Custom,
                        //EditTypeCustomCreateElement="createCardElement",
                        //EditTypeCustomGetValue = "getCardElementValue",
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator(),
                            new Trirand.Web.Mvc.CustomValidator()
                            {
                                ValidationFunction="validateCard",                                
                            }                            
                        }                        
                    },
                    new JQGridColumn()
                    {
                        DataField = "PlateNo",
                        HeaderText="车牌号",
                        DataType=typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="BrandModel",
                        HeaderText="品牌型号",
                        DataType=typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="MotorNo",
                        HeaderText="发动机号",
                        DataType=typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="OwnerName",
                        HeaderText="车主",
                        DataType=typeof(string),
                        Editable=false
                    },                    
                    new JQGridColumn()
                    {
                        DataField="Comment",
                        HeaderText="描述",
                        DataType=typeof(string),
                        Editable=false
                    }
                }
            };
            CardsGrid.AppearanceSettings.Caption = "发卡";
            CardsGrid.ToolBarSettings.ShowSearchButton = true;
            CardsGrid.ToolBarSettings.ShowEditButton = true;
            CardsGrid.ToolBarSettings.ShowRefreshButton = true;
            CardsGrid.SearchDialogSettings.MultipleSearch = true;
        }
    }

    public class LossCardsGridModel
    {
        public JQGrid CardsGrid { get; set; }
        public LossCardsGridModel()
        {
            CardsGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType = typeof(Guid),
                        PrimaryKey=true,
                        Visible=false
                    },
                    new JQGridColumn()
                    {
                        DataField="IsInUser",
                        DataType=typeof(bool),
                        HeaderText="正常使用",
                        Editable=true,
                        EditType= EditType.CheckBox,
                        SearchType= SearchType.DropDown,
                        Formatter= new CheckBoxFormatter()
                    },
                    new JQGridColumn()
                    {
                        DataField="CardNo",
                        HeaderText="卡号",
                        DataType=typeof(string),
                        Editable=false                        
                    },
                    new JQGridColumn()
                    {
                        DataField = "PlateNo",
                        HeaderText="车牌号",
                        DataType=typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="BrandModel",
                        HeaderText="品牌型号",
                        DataType=typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="MotorNo",
                        HeaderText="发动机号",
                        DataType=typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="FullName",
                        HeaderText="操作员",
                        DataType=typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptName",
                        HeaderText="部门",
                        DataType = typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="CreateDate",
                        HeaderText="发卡日期",
                        DataType=typeof(DateTime),
                        Editable=false
                    }
                }
            };
            CardsGrid.AppearanceSettings.Caption = "挂失";
            CardsGrid.ToolBarSettings.ShowSearchButton = true;
            CardsGrid.ToolBarSettings.ShowEditButton = true;
            CardsGrid.ToolBarSettings.ShowRefreshButton = true;
            CardsGrid.SearchDialogSettings.MultipleSearch = true;
        }
    }

    public class FoundCardsGridModel
    {
        public JQGrid CardsGrid { get; set; }
        public FoundCardsGridModel()
        {
            CardsGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType = typeof(Guid),
                        PrimaryKey=true,
                        Visible=false
                    },
                    new JQGridColumn()
                    {
                        DataField="IsInUser",
                        DataType=typeof(bool),
                        HeaderText="已挂失",
                        Editable=true,
                        EditType= EditType.CheckBox,
                        SearchType= SearchType.DropDown,
                        Formatter= new CheckBoxFormatter()
                    },
                    new JQGridColumn()
                    {
                        DataField="CardNo",
                        HeaderText="卡号",
                        DataType=typeof(string),
                        Editable=false                        
                    },
                    new JQGridColumn()
                    {
                        DataField = "PlateNo",
                        HeaderText="车牌号",
                        DataType=typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="BrandModel",
                        HeaderText="品牌型号",
                        DataType=typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="MotorNo",
                        HeaderText="发动机号",
                        DataType=typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="FullName",
                        HeaderText="操作员",
                        DataType=typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptName",
                        HeaderText="部门",
                        DataType = typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="LossDate",
                        HeaderText="挂失日期",
                        DataType=typeof(DateTime),
                        Editable=false
                    }
                }
            };
            CardsGrid.AppearanceSettings.Caption = "解挂";
            CardsGrid.ToolBarSettings.ShowSearchButton = true;
            CardsGrid.ToolBarSettings.ShowEditButton = true;
            CardsGrid.ToolBarSettings.ShowRefreshButton = true;
            CardsGrid.SearchDialogSettings.MultipleSearch = true;
        }
    }

    public class AddCardsGridModel
    {
        public JQGrid CardsGrid { get; set; }
        public AddCardsGridModel()
        {
            CardsGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType = typeof(Guid),
                        PrimaryKey=true,
                        Visible=false
                    },
                    new JQGridColumn()
                    {
                        DataField="IsInUser",
                        DataType=typeof(bool),
                        HeaderText="已挂失",
                        Editable=false,
                        EditType= EditType.CheckBox,
                        SearchType= SearchType.DropDown,
                        Formatter= new CheckBoxFormatter()
                    },
                    new JQGridColumn()
                    {
                        DataField="CardNo",
                        HeaderText="卡号",
                        DataType=typeof(string),
                        Editable=false                        
                    },
                    new JQGridColumn()
                    {
                        DataField="SecondCardNo",
                        HeaderText="补卡卡号",
                        DataType=typeof(string),
                        Editable=true,
                        //EditType = EditType.Custom,
                        //EditTypeCustomCreateElement="createCardElement",
                        //EditTypeCustomGetValue = "getCardElementValue"
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator(),
                            new Trirand.Web.Mvc.CustomValidator()
                            {
                                ValidationFunction="validateCard",                                
                            }                            
                        }       
                    },
                    new JQGridColumn()
                    {
                        DataField = "AddReason",
                        HeaderText="补卡原因",
                        DataType=typeof(string),
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField = "PlateNo",
                        HeaderText="车牌号",
                        DataType=typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="BrandModel",
                        HeaderText="品牌型号",
                        DataType=typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="MotorNo",
                        HeaderText="发动机号",
                        DataType=typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="FullName",
                        HeaderText="操作员",
                        DataType=typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptName",
                        HeaderText="部门",
                        DataType = typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="LossDate",
                        HeaderText="挂失日期",
                        DataType=typeof(DateTime),
                        Editable=false
                    }
                }
            };
            CardsGrid.AppearanceSettings.Caption = "补卡";
            CardsGrid.ToolBarSettings.ShowSearchButton = true;
            CardsGrid.ToolBarSettings.ShowEditButton = true;
            CardsGrid.ToolBarSettings.ShowRefreshButton = true;
            CardsGrid.SearchDialogSettings.MultipleSearch = true;

        }
    }


    public class ManageCardsGridModel
    {
        public JQGrid CardsGrid { get; set; }
        public ManageCardsGridModel()
        {
            CardsGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
                Columns = new List<JQGridColumn>()
                {
                    //new JQGridColumn()
                    //{
                    //    DataField="Id",
                    //    DataType = typeof(Guid),
                    //    PrimaryKey=true,
                    //    Visible=false
                    //},                    
                    new JQGridColumn()
                    {
                        DataField="CardNo",
                        HeaderText="卡号",
                        PrimaryKey=true,
                        DataType=typeof(string),
                        Editable=false                        
                    },
                    new JQGridColumn()
                    {
                        DataField="Status",
                        HeaderText="状态",
                        DataType=typeof(string),
                        Editable=false                        
                    },
                    new JQGridColumn()
                    {
                        DataField = "PlateNo",
                        HeaderText="车牌号",
                        DataType=typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="BrandModel",
                        HeaderText="品牌型号",
                        DataType=typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="MotorNo",
                        HeaderText="发动机号",
                        DataType=typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="OwnerName",
                        HeaderText="车主",
                        DataType=typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptName",
                        HeaderText="部门",
                        DataType = typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="IssueOper",
                        HeaderText="发卡操作员",
                        DataType=typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="CreateDate",
                        HeaderText="发卡日期",
                        DataType=typeof(DateTime),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="LossOper",
                        HeaderText="挂失操作员",
                        DataType=typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="LossDate",
                        HeaderText="挂失日期",
                        DataType=typeof(DateTime),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="FoundOper",
                        HeaderText="解挂操作员",
                        DataType=typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="FoundDate",
                        HeaderText="解挂日期",
                        DataType=typeof(DateTime),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="SecondCardNo",
                        HeaderText="补卡卡号",
                        DataType=typeof(string),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="AddOper",
                        HeaderText="补卡操作员",
                        DataType=typeof(string),
                        Editable=false
                    },
                    
                    new JQGridColumn()
                    {
                        DataField="AddDate",
                        HeaderText="补卡日期",
                        DataType=typeof(DateTime),
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="AddReason",
                        HeaderText="补卡原因",
                        DataType=typeof(string),
                        Editable=false
                    }
                    
                }
            };
            CardsGrid.AppearanceSettings.Caption = "IC卡清单";
            CardsGrid.ToolBarSettings.ShowSearchButton = true;
            CardsGrid.ToolBarSettings.ShowEditButton = false;
            CardsGrid.ToolBarSettings.ShowRefreshButton = true;
            CardsGrid.SearchDialogSettings.MultipleSearch = true;
            CardsGrid.ToolBarSettings.CustomButtons = new List<JQGridToolBarButton>()
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
}