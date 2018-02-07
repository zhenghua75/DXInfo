using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class WarehouseDeptModels
    {
    }
    public class WarehouseDeptGridModel
    {
        public EntityJQGrid WarehouseDeptGrid { get; set; }
        public WarehouseDeptGridModel()
        {
            WarehouseDeptGrid = new EntityJQGrid()
            {
                Width=1100,
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        EditActionIconsColumn = true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType=typeof(Guid),
                        PrimaryKey=true,
                        Visible=false,
                        Searchable=false,
                        Hidedlg=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Dept",
                        HeaderText="门店",
                        DataType=typeof(Guid),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptName",
                        HeaderText="门店",
                        DataType=typeof(string),
                        Visible=false,
                        Searchable=false,
                        Exportable=true,
                        Hidedlg = true,
                    },        
                    new JQGridColumn()
                    {
                        DataField="Warehouse",
                        HeaderText="配料仓",
                        DataType=typeof(Guid),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="WarehouseName",
                        HeaderText="配料仓",
                        DataType=typeof(string),
                        Visible=false,
                        Searchable=false,
                        Exportable=true,
                        Hidedlg = true,                        
                    },                    
                }
            };
        }
    }
}