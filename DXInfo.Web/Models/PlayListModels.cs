using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace DXInfo.Web.Models
{
    public class PlayListModels
    {
    }
    public class PlayListGridModel
    {
        public EntityJQGrid PlayListGrid { get; set; }
        public PlayListGridModel()
        {
            PlayListGrid = new EntityJQGrid()
            {
                Width=1100,
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        EditActionIconsColumn=true,
                        Viewable=false,
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
                        DataField="DeptId",
                        HeaderText="门店",
                        DataType=typeof(Guid),
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="DeptName",
                        HeaderText="门店",
                        DataType=typeof(string),
                        Visible=false,
                        Searchable=false,
                        Exportable=true,
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
                        DataField="Name",
                        HeaderText="MP3文件名",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="BeginTime",
                        HeaderText="开始时间",
                        DataType=typeof(TimeSpan),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="EndTime",
                        HeaderText="结束时间",
                        DataType=typeof(TimeSpan),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="IsEnabled",
                        HeaderText="是否启用",
                        DataType=typeof(bool),
                        Editable=true,
                    }
                }
            };
        }
    }
}