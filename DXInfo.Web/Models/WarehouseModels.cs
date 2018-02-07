using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class WarehouseModels
    {
    }
    public class WarehouseGridModel
    {
        public EntityJQGrid WarehouseGrid { get; set; }
        public WarehouseGridModel()
        {
            WarehouseGrid = new EntityJQGrid()
            {
                Width=1100,
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        EditActionIconsColumn=true,
                        Viewable=false,
                        Hidedlg=true,
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
                        DataField="Code",
                        HeaderText="编码",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Name",
                        HeaderText="名称",
                        DataType=typeof(string),
                        Editable=true,
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
                        Hidedlg=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Principal",
                        HeaderText="负责人",
                        DataType=typeof(string),
                        Editable=true                        
                    },
                    new JQGridColumn()
                    {
                        DataField="Tele",
                        HeaderText="电话",
                        DataType=typeof(string),
                        Editable=true                        
                    },
                    new JQGridColumn()
                    {
                        DataField="Address",
                        HeaderText="地址",
                        DataType=typeof(string),
                        Editable=true                        
                    },
                    new JQGridColumn()
                    {
                        DataField="Comment",
                        HeaderText="描述",
                        DataType=typeof(string),
                        Editable=true                        
                    },
                }
            };            
        }
    }
}