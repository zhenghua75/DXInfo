using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class SpecConsQueryModels
    {
    }
    public class SpecConsQueryGridModel
    {
        public JQGrid SpecConsQueryGrid { get; set; }
        public SpecConsQueryGridModel()
        {
            SpecConsQueryGrid = new JQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="vcConsType",
                        HeaderText="特殊消费类型",
                        DataType=typeof(string),
                        PrimaryKey=true,
                    }, 
                    new JQGridColumn()
                    {
                        DataField="vcGoodsName",
                        HeaderText="商品名称",
                        DataType=typeof(string),
                        PrimaryKey = true,
                    },                                 
                    new JQGridColumn()
                    {
                        DataField="tolCount",
                        HeaderText="数量",
                        DataType=typeof(int),
                    },    
                    new JQGridColumn()
                    {
                        DataField="tolfee",
                        HeaderText="销售金额",
                        DataType=typeof(decimal),
                    },  
                    new JQGridColumn()
                    {
                        DataField="tolcash",
                        HeaderText="现金",
                        DataType=typeof(decimal),
                    },  
                }
            };
        }
    }
}