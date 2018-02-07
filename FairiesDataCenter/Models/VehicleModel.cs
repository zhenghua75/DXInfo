using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace ynhnTransportManage.Models
{
    public class VehicleModel
    {
    }
    public class VehicleGridModel
    {
        public JQGrid VehicleGrid { get; set; }
        public VehicleGridModel()
        {
            VehicleGrid = new JQGrid()
            {
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
                        DataField = "PlateNo",
                        HeaderText="车牌号",
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
                        DataField="BrandModel",
                        HeaderText="品牌型号",
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
                        DataField="MotorNo",
                        HeaderText="发动机号",
                        DataType=typeof(string),
                        Editable=true
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="OwnerIdCardNo",
                    //    HeaderText="车主身份证号",
                    //    DataType=typeof(string),
                    //    Editable=true
                    //},
                    new JQGridColumn()
                    {
                        DataField="OwnerName",
                        HeaderText="车主",
                        DataType=typeof(string),
                        Editable=true
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="OwnerDriverNo",
                    //    HeaderText="车主驾驶证号",
                    //    DataType=typeof(string),
                    //    Editable=true
                    //},
                    //new JQGridColumn()
                    //{
                    //    DataField="Driver",
                    //    HeaderText="车主",
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
                    //    DataField="DriverName",
                    //    DataType = typeof(string),
                    //    Visible=false
                    //},
                    new JQGridColumn()
                    {
                        DataField="Comment",
                        HeaderText="描述",
                        DataType=typeof(string),
                        Editable=true
                    }
                }
            };
            VehicleGrid.AppearanceSettings.Caption = "车辆信息";
            VehicleGrid.ToolBarSettings.ShowSearchButton = true;
            VehicleGrid.ToolBarSettings.ShowAddButton = true;
            //VehicleGrid.ToolBarSettings.ShowDeleteButton = true;
            VehicleGrid.ToolBarSettings.ShowEditButton = true;
            VehicleGrid.ToolBarSettings.ShowRefreshButton = true;
            VehicleGrid.AutoWidth = true;
            VehicleGrid.Height = Unit.Percentage(100);

        }
    }
}