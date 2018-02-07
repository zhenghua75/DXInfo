using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class BalanceQueryModels
    {
    }
    public class BalanceQueryModel
    {
        public int Id { get; set; }
        public string vcLocalDeptName { get; set; }
        public string vcRemoteDeptName { get; set; }
        public decimal? nFillFee_Pay { get; set; }
        public decimal? nFillProm_Pay { get; set; }
        public decimal? nFee_Pay { get; set; }
        public decimal? sumFee_Pay { get; set; }
        public decimal? nFillFee_Income { get; set; }
        public decimal? nFillProm_Income { get; set; }
        public decimal? nFee_Income { get; set; }
        public decimal? sumFee_Income { get; set; }
        public decimal? nFillFee_Dif { get; set; }
        public decimal? nFillProm_Dif { get; set; }
        public decimal? nFee_Dif { get; set; }
        public decimal? sumFee_Dif { get; set; }
     
    }
    public class BalanceQueryGridModel
    {
        public JQGrid BalanceQueryGrid { get; set; }
        public BalanceQueryGridModel()
        {
            BalanceQueryGrid = new JQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType=typeof(int),
                        PrimaryKey=true,
                        Visible=false,
                    }, 
                    new JQGridColumn()
                    {
                        DataField="vcLocalDeptName",
                        HeaderText="-",//"发卡门店",
                        DataType=typeof(string),
                        Sortable=false,
                    }, 
                    new JQGridColumn()
                    {
                        DataField="vcRemoteDeptName",
                        HeaderText="-",//"门店",
                        DataType=typeof(string),
                        Sortable=false,
                    },                                 
                    new JQGridColumn()
                    {
                        DataField="nFillFee_Pay",
                        HeaderText="充值金额",//(支出)",
                        DataType=typeof(decimal),
                        Sortable=false,
                    },    
                    new JQGridColumn()
                    {
                        DataField="nFillProm_Pay",
                        HeaderText="充值赠送金额",//(支出)",
                        DataType=typeof(decimal),
                        Sortable=false,
                    },  
                    new JQGridColumn()
                    {
                        DataField="nFee_Pay",
                        HeaderText="消费金额",//(支出)",
                        DataType=typeof(decimal),
                        Sortable=false,
                    },  
                    new JQGridColumn()
                    {
                        DataField="sumFee_Pay",
                        HeaderText="小计",//(支出)",
                        DataType=typeof(decimal),
                        Sortable=false,
                    },  
                    new JQGridColumn()
                    {
                        DataField="nFillFee_Income",
                        HeaderText="充值金额",//(收入)",
                        DataType=typeof(decimal),
                        Sortable=false,
                    },  
                    new JQGridColumn()
                    {
                        DataField="nFillProm_Income",
                        HeaderText="充值赠送金额",//(收入)",
                        DataType=typeof(decimal),
                        Sortable=false,
                    },  
                    new JQGridColumn()
                    {
                        DataField="nFee_Income",
                        HeaderText="消费金额",//(收入)",
                        DataType=typeof(decimal),
                        Sortable=false,
                    },  
                    new JQGridColumn()
                    {
                        DataField="sumFee_Income",
                        HeaderText="小计",//(收入)",
                        DataType=typeof(decimal),
                        Sortable=false,
                    },  
                    new JQGridColumn()
                    {
                        DataField="nFillFee_Dif",
                        HeaderText="充值金额",//(差额)",
                        DataType=typeof(decimal),
                        Sortable=false,
                    },  
                    new JQGridColumn()
                    {
                        DataField="nFillProm_Dif",
                        HeaderText="充值赠送金额",//(差额)",
                        DataType=typeof(decimal),
                        Sortable=false,
                    },  
                    new JQGridColumn()
                    {
                        DataField="nFee_Dif",
                        HeaderText="消费金额",//(差额)",
                        DataType=typeof(decimal),
                        Sortable=false,
                    },  
                    new JQGridColumn()
                    {
                        DataField="sumFee_Dif",
                        HeaderText="小计",//(差额)",
                        DataType=typeof(decimal),
                        Sortable=false,
                    },  
                }
            };
        }
    }
}