using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class BusiLogQueryModels
    {
    }
    public class BusiLogQueryGridModel
    {
        public JQGrid BusiLogQueryGrid { get; set; }
        public BusiLogQueryGridModel()
        {
            BusiLogQueryGrid = new JQGrid()
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
                        DataField="vcOperType",
                        HeaderText="操作类型",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="vcOperName",
                        HeaderText="操作员",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="dtOperDate",
                        HeaderText="操作日期",
                        DataType=typeof(DateTime),
                    },                    
                    new JQGridColumn()
                    {
                        DataField="vcDeptID",
                        HeaderText="操作员门店",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="vcComments",
                        HeaderText="备注",
                        DataType=typeof(string),
                    },                    
                }
            };
        }
    }
}