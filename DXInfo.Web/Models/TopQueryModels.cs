using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class TopQueryModels
    {
    }
    public class GoodsTopQueryGridModel
    {
        public JQGrid GoodsTopQueryGrid { get; set; }
        public GoodsTopQueryGridModel()
        {
            GoodsTopQueryGrid = new JQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="vcGoodsID",
                        HeaderText="商品ID",
                        DataType=typeof(string),
                        PrimaryKey=true,
                    }, 
                    new JQGridColumn()
                    {
                        DataField="vcGoodsType",
                        HeaderText="商品类型",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="vcGoodsName",
                        HeaderText="商品名称",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="SaleCount",
                        HeaderText="销售数量",
                        DataType=typeof(int),
                    },                    
                    new JQGridColumn()
                    {
                        DataField="nFee",
                        HeaderText="销售金额",
                        DataType=typeof(decimal),
                    },                    
                }
            };
        }
    }
    public class CardTopQueryGridModel
    {
        public JQGrid CardTopQueryGrid { get; set; }
        public CardTopQueryGridModel()
        {
            CardTopQueryGrid = new JQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="vcCardID",
                        HeaderText="会员卡号",
                        DataType=typeof(string),
                        PrimaryKey=true,
                    }, 
                    new JQGridColumn()
                    {
                        DataField="vcAssName",
                        HeaderText="会员名称",
                        DataType=typeof(string),
                    },                                 
                    new JQGridColumn()
                    {
                        DataField="SaleFee",
                        HeaderText="消费额",
                        DataType=typeof(decimal),
                    },                    
                }
             };
        }
    }
}