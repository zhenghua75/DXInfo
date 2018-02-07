using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace DXInfo.Web.Models
{
    public class OrganizationsModels
    {
    }
    public class OrganizationsGridModel
    {
        public EntityJQGrid OrganizationsGrid { get; set; }
        public OrganizationsGridModel()
        {
            OrganizationsGrid = new EntityJQGrid()
            {
                Width = Unit.Pixel(1100),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        Viewable=false,
                        Hidedlg=true,
                        EditActionIconsColumn=true,
                        //EditActionIconsSettings = new EditActionIconsSettings(){ ShowEditIcon=true,ShowDeleteIcon=false},
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
                        DataField="Code",
                        HeaderText = "编码",
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                        DataType=typeof(string)
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
                        DataType=typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="Comment",
                        HeaderText="描述",
                        Editable=true,
                        DataType=typeof(string)
                    }

                },
            };
            OrganizationsGrid.SortSettings.InitialSortColumn = "Code";
        }
    }
}