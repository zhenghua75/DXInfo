using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class FillMdQueryModels
    {
    }
    public class FillMdQueryGridModel
    {
        public JQGrid FillMdQueryGrid { get; set; }
        public FillMdQueryGridModel()
        {
            FillMdQueryGrid = new JQGrid()
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
                        DataField="nFillFee",
                        HeaderText="充值金额",
                        DataType=typeof(decimal),
                    }, 
                    new JQGridColumn()
                    {
                        DataField="nFillProm",
                        HeaderText="赠款金额",
                        DataType=typeof(decimal),
                    }, 
                    new JQGridColumn()
                    {
                        DataField="nFeeLast",
                        HeaderText="上次余额",
                        DataType=typeof(decimal),
                    }, 
                    new JQGridColumn()
                    {
                        DataField="nFeeCur",
                        HeaderText="当前余额",
                        DataType=typeof(decimal),
                    }, 
                    new JQGridColumn()
                    {
                        DataField="vcComments",
                        HeaderText="备注",
                        DataType=typeof(string),
                    }, 
                    new JQGridColumn()
                    {
                        DataField="dtFillDate",
                        HeaderText="充值日期",
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
                        HeaderText="操作员门店",
                        DataType=typeof(string),
                    }, 
                }
            };
        }
    }
}