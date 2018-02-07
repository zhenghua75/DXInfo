using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace ynhnTransportManage.Models
{
    public class VehicleListModels
    {
    }
    public class VehicleListGridModel
    {
        public JQGrid VehicleListGrid { get; set; }
        public VehicleListGridModel()
        {
            VehicleListGrid = new JQGrid()
            {
                AutoWidth=true,
                Height=Unit.Percentage(100),
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
                        DataType = typeof(string),                        
                        Editable=false
                    },
                     new JQGridColumn()
                    {
                        DataField="Status",
                        HeaderText="状态",
                        DataType = typeof(string),                        
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="PlateNo",
                        HeaderText="车牌号",
                        DataType = typeof(string),                        
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="BrandModel",
                        HeaderText="品牌型号",
                        DataType = typeof(string),                        
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="MotorNo",
                        HeaderText="发动机号",
                        DataType = typeof(string),                        
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="OwnName",
                        HeaderText="车主",
                        DataType = typeof(string),                        
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="DriverName",
                        HeaderText="驾驶员",
                        DataType = typeof(string),                        
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="BalanceType",
                        HeaderText="结算方式",
                        DataType = typeof(Guid),                        
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="BalanceTypeName",
                        HeaderText="结算方式",
                        DataType = typeof(string), 
                        Visible=false,
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="AgreeFreightRate",
                        HeaderText="约定运价",
                        DataType = typeof(decimal),                        
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="FreightRate",
                        HeaderText="实际运价",
                        DataType = typeof(decimal),                        
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="Shipper",
                        HeaderText="托运人",
                        DataType = typeof(string),                        
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="Shipper_Telephone",
                        HeaderText="托运人电话",
                        DataType = typeof(string),                        
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="Carrier",
                        HeaderText="承运人",
                        DataType = typeof(string),                        
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="Carrier_Telephone",
                        HeaderText="承运人电话",
                        DataType = typeof(string),                        
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="Lines",
                        HeaderText="运输路线",
                        DataType = typeof(Guid),                        
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="LinesName",
                        HeaderText="运输路线名称",
                        DataType = typeof(string),  
                        Visible =false,
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="Mileage",
                        HeaderText="里程(公里)",
                        DataType = typeof(decimal),                        
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="InFactory_Dept",
                        HeaderText="进厂部门",
                        DataType = typeof(string),                        
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="InFactory_Date",
                        HeaderText="进厂时间",
                        DataType = typeof(DateTime),                        
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="InFactory_Oper",
                        HeaderText="进厂操作员",
                        DataType = typeof(string),                        
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="Load_Dept",
                        HeaderText="装车部门",
                        DataType = typeof(string),                        
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="Load_Date",
                        HeaderText="装车时间",
                        DataType = typeof(DateTime),                        
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="Load_Oper",
                        HeaderText="装车操作员",
                        DataType = typeof(string),                        
                        Editable=false
                    },

                    new JQGridColumn()
                    {
                        DataField="Load_Inventory",
                        HeaderText="存货",
                        DataType = typeof(string),                        
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="UnitName",
                        HeaderText="计量单位",
                        DataType = typeof(string),                        
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="Specs",
                        HeaderText="规格型号",
                        DataType = typeof(string),                        
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="Load_Quantity",
                        HeaderText="装车数量",
                        DataType = typeof(decimal),                        
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator()                       
                        }       
                    },
                    new JQGridColumn()
                    {
                        DataField="Shipment_Dept",
                        HeaderText="卸货部门",
                        DataType = typeof(string),                        
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="Shipment_Date",
                        HeaderText="卸货时间",
                        DataType = typeof(DateTime),                        
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="Shipment_Oper",
                        HeaderText="卸货操作员",
                        DataType = typeof(string),                        
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="Shipment_Quantity",
                        HeaderText="卸货数量",
                        DataType = typeof(decimal),                        
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator()                       
                        }     
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="Shipment_CheckUserId",
                    //    HeaderText="签收",
                    //    DataType = typeof(Guid),                        
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                     
                    //    }     
                    //},
                    new JQGridColumn()
                    {
                        DataField="Shipment_CheckUser",
                        HeaderText="签收人姓名",
                        DataType = typeof(string),   
                        //Visible=false,
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                     
                        }   
                    },

                    new JQGridColumn()
                    {
                        DataField="OutFactory_Dept",
                        HeaderText="出厂部门",
                        DataType = typeof(string),                        
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="OutFactory_Date",
                        HeaderText="出厂时间",
                        DataType = typeof(DateTime),                        
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="OutFactory_Oper",
                        HeaderText="出厂操作员",
                        DataType = typeof(string),                        
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="Modify_Dept",
                        HeaderText="修改部门",
                        DataType = typeof(string),                        
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="ModifyDate",
                        HeaderText="修改时间",
                        DataType = typeof(DateTime),                        
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="Modify_Oper",
                        HeaderText="修改操作员",
                        DataType = typeof(string),                        
                        Editable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="Comment",
                        HeaderText="备注",
                        DataType = typeof(string),                        
                        Editable=true
                    }
                }
            };
            VehicleListGrid.ToolBarSettings.ShowEditButton = true;
            VehicleListGrid.ToolBarSettings.ShowSearchButton = true;
            VehicleListGrid.SearchDialogSettings.MultipleSearch = true;
            VehicleListGrid.AppearanceSettings.Caption = "车辆清单";
            VehicleListGrid.ToolBarSettings.CustomButtons = new List<JQGridToolBarButton>()
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