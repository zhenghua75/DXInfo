using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace DXInfo.Web.Models
{
    public class ConsumePointModels
    {
    }
    public class ConsumePointGridModel
    {
        public EntityJQGrid ConsumePointGrid { get; set; }
        public ConsumePointGridModel()
        {
            ConsumePointGrid = new EntityJQGrid()
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
                        DataField="DeptId",
                        HeaderText="门店",
                        DataType=typeof(Guid),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptName",
                        HeaderText="门店",
                        DataType=typeof(string),
                        Searchable=false,
                        Visible=false,
                        Exportable=true,
                        Hidedlg=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Category",
                        HeaderText="分类",
                        DataType=typeof(Guid),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="CategoryName",
                        HeaderText="分类",
                        DataType=typeof(string),
                        Searchable=false,
                        Visible=false,
                        Exportable=true,
                        Hidedlg=true,
                    },                    
                    new JQGridColumn()
                    {
                        DataField="Amount",
                        HeaderText="金额",
                        DataType=typeof(decimal),
                        Editable=true,   
                        Formatter = new DigitFormatter(),
                    },        
                    new JQGridColumn()
                    {
                        DataField="Point",
                        HeaderText="积分",
                        DataType=typeof(decimal),
                        Editable=true,
                        Formatter = new DigitFormatter(),
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