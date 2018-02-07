using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace ynhnTransportManage.Models
{
    public class DriversModel
    {
    }
    public class DriversGridModel
    {
        public JQGrid DriversGrid { get; set; }
        public DriversGridModel()
        {
            DriversGrid = new JQGrid()
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
                        PrimaryKey=true
                    },
                    new JQGridColumn()
                    {
                        DataField="Code",
                        DataType=typeof(string),
                        HeaderText="编码",
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
                        DataType=typeof(string),
                        HeaderText="姓名",
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="DriverNo",
                        DataType=typeof(string),
                        HeaderText="驾驶证号",
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="Telephone",
                        DataType=typeof(string),
                        HeaderText="电话",
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="Address",
                        DataType=typeof(string),
                        HeaderText="地址",
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="IdCardNo",
                        DataType=typeof(string),
                        HeaderText="身份证号",
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="Comment",
                        DataType=typeof(string),
                        HeaderText="描述",
                        Editable=true
                    }
                }
            };
            DriversGrid.AppearanceSettings.Caption = "驾驶员信息";
            DriversGrid.ToolBarSettings.ShowSearchButton = true;
            DriversGrid.ToolBarSettings.ShowAddButton = true;
            //DriversGrid.ToolBarSettings.ShowDeleteButton = true;
            DriversGrid.ToolBarSettings.ShowEditButton = true;
            DriversGrid.ToolBarSettings.ShowRefreshButton = true;
        }
    }
}