using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class ConsItemMdModels
    {
    }
    public class ConsItemMdGridModel
    {
        public JQGrid ConsItemMdGrid { get; set; }
        public ConsItemMdGridModel()
        {
            ConsItemMdGrid = new JQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="iSerial",
                        HeaderText="流水",
                        DataType=typeof(Int64),
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
                        DataField="vcAssType",
                        HeaderText="会员类型",
                        DataType=typeof(string),
                    }, 
                    new JQGridColumn()
                    {
                        DataField="vcCardID",
                        HeaderText="会员卡号",
                        DataType=typeof(string),
                    }, 
                    new JQGridColumn()
                    {
                        DataField="vcLocalDeptId",
                        HeaderText="发卡门店",
                        DataType=typeof(string),
                    }, 
                    new JQGridColumn()
                    {
                        DataField="vcGoodsID",
                        HeaderText="商品编号",
                        DataType=typeof(string),
                    }, 
                    new JQGridColumn()
                    {
                        DataField="nPrice",
                        HeaderText="单价",
                        DataType=typeof(decimal),
                    }, 
                    new JQGridColumn()
                    {
                        DataField="iCount",
                        HeaderText="数量",
                        DataType=typeof(int),
                    }, 
                    new JQGridColumn()
                    {
                        DataField="nFee",
                        HeaderText="合计",
                        DataType=typeof(decimal),
                    }, 
                    new JQGridColumn()
                    {
                        DataField="vcConsType",
                        HeaderText="付款类型",
                        DataType=typeof(string),
                    }, 
                    new JQGridColumn()
                    {
                        DataField="vcComments",
                        HeaderText="备注",
                        DataType=typeof(string),
                    }, 
                    new JQGridColumn()
                    {
                        DataField="cFlag",
                        HeaderText="有效状态",
                        DataType=typeof(string),
                    }, 
                    new JQGridColumn()
                    {
                        DataField="dtConsDate",
                        HeaderText="消费日期",
                        DataType=typeof(DateTime),
                    }, 
                    new JQGridColumn()
                    {
                        DataField="vcOperName",
                        HeaderText="操作员",
                        DataType=typeof(string),
                    }, 
                    new JQGridColumn()
                    {
                        DataField="vcDeptID",
                        HeaderText="门店",
                        DataType=typeof(string),
                    }, 
                }
            };
        }
    }
}