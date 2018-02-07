using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class ConsKindQueryModels
    {
    }
    public class ConsKindQueryGridModel
    {
        public JQGrid ConsKindQueryGrid { get; set; }
        public ConsKindQueryGridModel()
        {
            ConsKindQueryGrid = new JQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="vcDeptID",
                        HeaderText="门店",
                        DataType=typeof(string),
                        PrimaryKey=true,
                    }, 
                    new JQGridColumn()
                    {
                        DataField="vcAssType",
                        HeaderText="会员类型",
                        DataType=typeof(string),
                        PrimaryKey=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="vcGoodsType",
                        HeaderText="商品类型",
                        DataType=typeof(string),
                        PrimaryKey=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="vcGoodsName",
                        HeaderText="商品名称",
                        DataType=typeof(string),
                        PrimaryKey=true,
                    },
                    
                    new JQGridColumn()
                    {
                        DataField="tolcount",
                        HeaderText="数量合计",
                        DataType=typeof(int),
                    },                    
                    new JQGridColumn()
                    {
                        DataField="tolfee",
                        HeaderText="金额合计",
                        DataType=typeof(decimal),
                    },                    
                }
            };
        }
    }
}