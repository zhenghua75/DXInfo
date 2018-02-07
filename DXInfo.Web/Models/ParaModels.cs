using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace DXInfo.Web.Models
{
    public class ParaModels
    {
    }
    public class ParaGridModel
    {
        public EntityJQGrid ParasGrid { get; set; }
        public ParaGridModel()
        {
            ParasGrid = new EntityJQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        Viewable=false,
                        Hidedlg=true,
                        EditActionIconsColumn = true,
                        //EditActionIconsSettings = new EditActionIconsSettings(){ ShowEditIcon=true, ShowDeleteIcon=false},
                    },
                    new JQGridColumn()
                    {
                        DataField="ID",
                        PrimaryKey = true,
                        Visible = false,
                        DataType=typeof(string),
                        Searchable=false,
                        Hidedlg=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Type",
                        HeaderText = "类型",
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        DataType=typeof(string),
                        Width=200,
                    },
                    new JQGridColumn()
                    {
                        DataField="Code",
                        HeaderText = "编码",
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        DataType=typeof(string),
                        Width=200,
                    },
                    new JQGridColumn()
                    {
                        DataField="Name",
                        HeaderText="名称",
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        DataType=typeof(string),
                        Width=200,
                    },
                    new JQGridColumn()
                    {
                        DataField="Value",
                        HeaderText="值",
                        Editable=true,
                        DataType=typeof(string),
                        Width=200,
                    },
                    new JQGridColumn()
                    {
                        DataField="Comment",
                        HeaderText="描述",
                        Editable=true,
                        DataType=typeof(string),
                        Width=200,
                    }

                },
            };
            ParasGrid.SortSettings.InitialSortColumn = "Type,Code";
        }
    }
}