using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class SaleBalanceModels
    {
    }
    public class SaleBalanceGridModel
    {
        public JQGrid SaleBalanceGrid { get; set; }
        public SaleBalanceGridModel()
        {
            SaleBalanceGrid = new JQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="BalanceDate",
                        HeaderText="日期",
                        DataType=typeof(DateTime),
                    },
                    new JQGridColumn()
                    {
                        DataField="vcDeptName",
                        HeaderText="门店",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="vcGoodsTypeName",
                        HeaderText="商品类型",
                        DataType=typeof(string),
                        PrimaryKey=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="vcGoodsName",
                        HeaderText="商品",
                        DataType=typeof(string),
                        PrimaryKey=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="nPrice",
                        HeaderText="单价",
                        DataType=typeof(decimal),
                        PrimaryKey=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="LastCheckQuantity",
                        HeaderText="上次盘点数量",
                        DataType=typeof(int),
                    },
                    new JQGridColumn()
                    {
                        DataField="InQuantity",
                        HeaderText="入库数量",
                        DataType=typeof(int),
                    },
                    new JQGridColumn()
                    {
                        DataField="SaleQuantity",
                        HeaderText="销售数量",
                        DataType=typeof(int),
                    },
                    new JQGridColumn()
                    {
                        DataField="CheckQuantity",
                        HeaderText="本次盘点数量",
                        DataType=typeof(int),
                    },
                    
                    new JQGridColumn()
                    {
                        DataField="Differences",
                        HeaderText="差异金额",
                        DataType=typeof(decimal),
                        PrimaryKey=true,
                    },

                    new JQGridColumn()
                    {
                        DataField="IsBalance",
                        HeaderText="是否平衡",
                        DataType=typeof(bool),
                    },
                }
            };
        }
    }
}