using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace DXInfo.Web.Models
{
    public class PayTypeModels
    {
    }
    public class PayTypeGridModel
    {
        public EntityJQGrid PayTypeGrid { get; set; }
        public PayTypeGridModel()
        {
            PayTypeGrid = new EntityJQGrid()
            {
                Width = Unit.Pixel(1100),
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
                        DataField="Comment",
                        HeaderText="描述",
                        DataType=typeof(string),
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="PayType",
                        HeaderText="类型",
                        DataType=typeof(int),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="PayTypeName",
                        HeaderText="类型",
                        DataType=typeof(string),
                        Visible=false,
                        Searchable=false,
                        Exportable=true,
                        Hidedlg=true,
                    }
                }
            };
        }
    }
}