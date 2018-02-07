using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace ynhnTransportManage.Models
{
    public class ReceiptModel
    {
    }
    public class ReceiptGridModel
    {
        public int ReceiptType { get; set; }
        public JQGrid ReceiptGrid { get; set; }
        public ReceiptGridModel()
        {
            ReceiptGrid = new JQGrid()
            {
                Columns = new List<JQGridColumn>()
                {   
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType = typeof(Guid),
                        PrimaryKey = true,
                        Visible=false,
                        Searchable=false,
                    }, 
                    new JQGridColumn()
                    {
                        DataField="ReceiptType",
                        DataType = typeof(int),
                        Visible=false,
                        Searchable=false,
                        SearchToolBarOperation = SearchOperation.IsEqualTo,
                    },                     
                    new JQGridColumn()
                    {
                        DataField="MemberName",
                        HeaderText="姓名",
                        DataType = typeof(string)
                    },                    
                    new JQGridColumn()
                    {
                        DataField="IdCard",
                        HeaderText="证件号",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="LinkPhone",
                        HeaderText="联系电话",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="LinkAddress",
                        HeaderText="联系地址",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="Email",
                        HeaderText="Email",
                        DataType = typeof(string)
                    },  
                    new JQGridColumn()
                    {
                        DataField="Content",
                        HeaderText="内容",
                        DataType = typeof(string)
                    },  
                    new JQGridColumn()
                    {
                        DataField="Comment",
                        HeaderText="备注",
                        DataType = typeof(string)
                    },  
                    new JQGridColumn()
                    {
                        DataField="Status",
                        HeaderText="状态",
                        DataType = typeof(int),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="StatusName",
                        HeaderText="状态",
                        DataType = typeof(string),   
                        Visible=false,
                        Searchable=false,
                        Exportable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="CreateDate",
                        HeaderText="创建日期",
                        DataType = typeof(DateTime),    
                    },
                    new JQGridColumn()
                    {
                        DataField="FullName",
                        HeaderText="创建操作员",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptName",
                        HeaderText="创建部门",
                        DataType = typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="ModifyDate",
                        HeaderText="修改日期",
                        DataType = typeof(DateTime),       
                    },
                    new JQGridColumn()
                    {
                        DataField="ModifyFullName",
                        HeaderText="修改操作员",
                        DataType = typeof(string)
                    },
                    new JQGridColumn()
                    {
                        DataField="ModifyDeptName",
                        HeaderText="修改部门",
                        DataType = typeof(string)     
                    },
                    
                }
            };
        }
    }
}