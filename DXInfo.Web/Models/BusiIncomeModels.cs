using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class BusiIncomeModels
    {
    }
    public class BusiIncomeGridModel
    {
        public JQGrid BusiIncomeGrid { get; set; }
        public BusiIncomeGridModel()
        {
            BusiIncomeGrid = new JQGrid()
            {
                Columns = new List<JQGridColumn>()
                {                    
                    new JQGridColumn()
                    {
                        DataField="GroupType",
                        HeaderText="-",
                        DataType=typeof(string),
                        PrimaryKey=true,
                    }, 
                    new JQGridColumn()
                    {
                        DataField="Type",
                        HeaderText="-",
                        DataType=typeof(string),
                        PrimaryKey=true,
                    },                                 
                    new JQGridColumn()
                    {
                        DataField="REP1",
                        HeaderText="会员数",
                        DataType=typeof(int),  
                        Formatter = new CustomFormatter(){ FormatFunction="MyFormatNumber", UnFormatFunction="MyUnFormatNumber"},
                    },
                    new JQGridColumn()
                    {
                        DataField="REP2",
                        HeaderText="可用积分",
                        DataType=typeof(int),
                        Formatter = new CustomFormatter(){ FormatFunction="MyFormatNumber", UnFormatFunction="MyUnFormatNumber"},
                    }, 
                    new JQGridColumn()
                    {
                        DataField="REP3",
                        HeaderText="使用积分",
                        DataType=typeof(decimal),
                        Formatter = new CustomFormatter(){ FormatFunction="MyFormatNumber", UnFormatFunction="MyUnFormatNumber"},
                    }, 
                    new JQGridColumn()
                    {
                        DataField="REP4",
                        HeaderText="金额",
                        DataType=typeof(decimal),
                        Formatter = new CustomFormatter(){ FormatFunction="MyFormatNumber", UnFormatFunction="MyUnFormatNumber"},
                    },
                    new JQGridColumn()
                    {
                        DataField="REP5",
                        HeaderText="附赠情况",
                        DataType=typeof(decimal),
                        Formatter = new CustomFormatter(){ FormatFunction="MyFormatNumber", UnFormatFunction="MyUnFormatNumber"},
                    },
                    new JQGridColumn()
                    {
                        DataField="REP6",
                        HeaderText="次数",
                        DataType=typeof(decimal),
                        Formatter = new CustomFormatter(){ FormatFunction="MyFormatNumber", UnFormatFunction="MyUnFormatNumber"},
                    },
                    new JQGridColumn()
                    {
                        DataField="REP7",
                        HeaderText="商品数",
                        DataType=typeof(int),
                        Formatter = new CustomFormatter(){ FormatFunction="MyFormatNumber", UnFormatFunction="MyUnFormatNumber"},
                    }, 
                }
             };
        }
    }
}