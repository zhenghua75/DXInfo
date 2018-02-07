using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class VendorModels
    {
    }
    public class VendorGridModel
    {
        public int VendorType { get; set; }
        public EntityJQGrid VendorGrid { get; set; }
        public VendorGridModel()
        {
            VendorGrid = new EntityJQGrid()
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
                        DataField="VendorType",
                        DataType=typeof(int),
                        Visible=false,
                        Searchable=false,
                        Hidedlg=true,
                        SearchToolBarOperation= SearchOperation.IsEqualTo,
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
                        DataField="Tel",
                        HeaderText="电话",
                        DataType=typeof(string),
                        Editable=true,
                    },   
                    new JQGridColumn()
                    {
                        DataField="Fax",
                        HeaderText="传真",
                        DataType=typeof(string),
                        Editable=true,
                    }, 
                    new JQGridColumn()
                    {
                        DataField="Phone",
                        HeaderText="手机",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Zip",
                        HeaderText="邮编",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Linkman",
                        HeaderText="联系人",
                        DataType=typeof(string),
                        Editable=true,
                    },                    
                    new JQGridColumn()
                    {
                        DataField="Address",
                        HeaderText="地址",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Email",
                        HeaderText="Email",
                        DataType=typeof(string),
                        Editable=true,
                    },
                }
            };
        }
    }
}