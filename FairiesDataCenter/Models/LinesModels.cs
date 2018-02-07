using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace ynhnTransportManage.Models
{
    public class LinesModels
    {
    }
    public class LinesGridModel
    {
        public JQGrid LinesGrid { get; set; }
        public LinesGridModel()
        {
            LinesGrid = new JQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="Id",
                        PrimaryKey=true,
                        Visible=false
                    },
                    new JQGridColumn()
                    {
                        DataField = "Code",
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
                        DataField = "Name",
                        DataType=typeof(string),
                        HeaderText="名称",
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    
                    new JQGridColumn()
                    {
                        DataField = "Mileage",
                        DataType=typeof(decimal),
                        HeaderText="里程(公里)",
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new Trirand.Web.Mvc.NumberValidator()
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField = "Comment",
                        DataType=typeof(string),
                        Editable=true,
                        HeaderText="描述",
                        EditType= EditType.TextArea
                    },
                }
            };

            LinesGrid.ToolBarSettings.ShowAddButton = true;
            LinesGrid.ToolBarSettings.ShowEditButton = true;
            LinesGrid.ToolBarSettings.ShowRefreshButton = true;
            LinesGrid.ToolBarSettings.ShowSearchButton = true;
            LinesGrid.AppearanceSettings.Caption = "运输线路";
            LinesGrid.SearchDialogSettings.MultipleSearch = true;
            LinesGrid.SortSettings.InitialSortColumn = "Code";
        }
    }
}