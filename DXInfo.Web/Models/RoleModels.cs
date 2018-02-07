using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace DXInfo.Web.Models
{
    public class RoleModels
    {
    }
    public class RoleGridModel
    {
        public EntityJQGrid RoleGrid { get; set; }
        public RoleGridModel()
        {
            RoleGrid = new EntityJQGrid();
            List<JQGridColumn> columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        Viewable=false,
                        Hidedlg=true,
                        EditActionIconsColumn=true,
                        EditActionIconsSettings = new EditActionIconsSettings(){ ShowEditIcon=true,ShowDeleteIcon=true},
                    },
                    new JQGridColumn()
                    {
                        DataField="RoleId",                    
                        PrimaryKey=true,
                        Visible=false,
                        Searchable=false,
                        Hidedlg=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="RoleName",
                        HeaderText="角色名",
                        DataType=typeof(string),
                        Editable = true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        }
                    },
                    new JQGridColumn()
                    {
                        DataField="Description",
                        HeaderText="描述",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        }
                    }
                };
            RoleGrid.Columns = columns;
            RoleGrid.Width = Unit.Pixel(1100);
        }
    }
}