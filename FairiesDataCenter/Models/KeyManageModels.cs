using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace ynhnTransportManage.Models
{
    public class KeyManageModels
    {
    }
    public class KeyManageGridModel
    {
        public EntityJQGrid KeyManageGrid { get; set; }
        public KeyManageGridModel()
        {
            KeyManageGrid = new EntityJQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        EditActionIconsColumn=true,
                        EditActionIconsSettings = new EditActionIconsSettings(){ ShowEditIcon=true,ShowDeleteIcon=false},
                    },
                    new JQGridColumn()
                    {
                        PrimaryKey=true,
                        DataField="HardwareID",
                        HeaderText = "硬件序列号",
                        DataType = typeof(string),
                        Width=250,
                    },
                    new JQGridColumn()
                    {
                        DataField = "CardNo",
                        HeaderText = "ekey编号",
                        DataType = typeof(string),
                        Width=100,
                    },
                    new JQGridColumn()
                    {
                        DataField = "CreateDate",
                        HeaderText = "启用日期",
                        DataType = typeof(string),
                        Editable=false,
                        Width=150,
                    },
                    new JQGridColumn()
                    {
                        DataField = "IsUse",
                        HeaderText = "是否启用",
                        DataType = typeof(bool),
                        Editable = true,
                    },
                    new JQGridColumn()
                    {
                        DataField = "UserId",
                        HeaderText = "用户",
                        DataType = typeof(Guid),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField = "FullName",
                        HeaderText = "用户",
                        DataType = typeof(string),
                        Visible=false,
                        Searchable=false,
                        Exportable=true,
                    }
                }
            };
        }
    }
}