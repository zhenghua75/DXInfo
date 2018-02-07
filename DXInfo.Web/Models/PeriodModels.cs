using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class PeriodModels
    {
    }
    public class PeriodGridModel
    {
        public EntityJQGrid PeriodGrid { get; set; }
        public PeriodGridModel()
        {
            PeriodGrid = new EntityJQGrid()
            {
                Width=1100,
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        EditActionIconsColumn=true,
                        Hidedlg=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType=typeof(Guid),
                        PrimaryKey=true,
                        Visible=false,
                        Searchable=false,
                        Hidedlg=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Code",
                        HeaderText="编码",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="BeginDate",
                        HeaderText="开始日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="EndDate",
                        HeaderText="结束日期",
                        DataType=typeof(DateTime),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Memo",
                        HeaderText="描述",
                        DataType=typeof(string),
                        Editable=true                        
                    },
                }
            };
        }
    }
}