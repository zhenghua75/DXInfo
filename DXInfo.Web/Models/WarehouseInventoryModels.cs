using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class WarehouseInventoryModels
    {
    }
    public class WarehouseInventoryGridModel
    {
        public EntityJQGrid WarehouseInventoryGrid { get; set; }
        public WarehouseInventoryGridModel()
        {
            WarehouseInventoryGrid = new EntityJQGrid()
            {
                Width=1100,
                Columns = new List<JQGridColumn>()
                {
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
                        DataField="Warehouse",
                        HeaderText="仓库",
                        DataType=typeof(Guid),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="WarehouseName",
                        HeaderText="仓库",
                        DataType=typeof(string),
                        Visible=false,
                        Searchable=false,
                        Exportable=true,
                    },    
                    new JQGridColumn()
                    {
                        DataField="Inventory",
                        HeaderText="存货",
                        DataType=typeof(Guid),
                        Editable=true,
                        DataInit = "InventoryColumnDataInit",
                    },
                    new JQGridColumn()
                    {
                        DataField="InventoryName",
                        HeaderText="存货",
                        DataType=typeof(string),
                        Visible=false,
                        Searchable=false,
                        Exportable=true,
                    },        
                }
            };
            WarehouseInventoryGrid.EditDialogSettings.Width = 450;
            WarehouseInventoryGrid.AddDialogSettings.Width = 450;
        }
    }
}