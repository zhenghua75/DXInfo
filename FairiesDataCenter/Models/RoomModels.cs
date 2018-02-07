using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace ynhnTransportManage.Models
{
    public class RoomModels
    {
    }
    public class RoomGridModel
    {
        public EntityJQGrid RoomGrid { get; set; }
        public RoomGridModel()
        {
            RoomGrid = new EntityJQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        EditActionIconsColumn=true,
                        EditActionIconsSettings= new EditActionIconsSettings(),
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
                        DataField="DeptId",
                        HeaderText="部门",
                        DataType=typeof(Guid),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptName",
                        HeaderText="部门",
                        DataType=typeof(string),
                        Visible = false,
                        Searchable=false,
                        Exportable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Code",
                        HeaderText="编码",
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
                        DataField="Name",
                        HeaderText="名称",
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
                        DataField="Status",
                        HeaderText="状态",
                        DataType=typeof(int),
                        Editable=true,
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()
                        },
                        EditDialogFieldSuffix="(*)",
                    },    
                    new JQGridColumn()
                    {
                        DataField="StatusName",
                        HeaderText="状态",
                        DataType=typeof(string),
                        Visible=false,
                        Searchable=false,
                        Exportable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Comment",
                        HeaderText="描述",
                        DataType=typeof(string),
                        Editable=true
                    }
                }
            };
        }
    }
}