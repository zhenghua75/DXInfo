﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace DXInfo.Web.Models
{
    public class IPadModels
    {
    }
    public class IPadGridModel
    {
        public EntityJQGrid IPadGrid { get; set; }
        public IPadGridModel()
        {
            IPadGrid = new EntityJQGrid()
            {
                Width=Unit.Pixel(1100),
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
                        DataField="Code",
                        HeaderText="编码",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Name",
                        HeaderText="名称",
                        DataType=typeof(string),
                        Editable=true,
                    },      
                    new JQGridColumn()
                    {
                        DataField="SN",
                        HeaderText="序列号",
                        DataType=typeof(string),
                        Editable=true,
                    },      
                    new JQGridColumn()
                    {
                        DataField="Status",
                        HeaderText="状态",
                        DataType=typeof(int),
                        Editable=true,  
                    },  
                    new JQGridColumn()
                    {
                        DataField="StatusName",
                        HeaderText="状态",
                        DataType=typeof(string),
                        Visible=false,
                        Searchable=false,
                        Exportable=true,
                        Hidedlg=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Comment",
                        HeaderText="描述",
                        DataType=typeof(string),
                        Editable=true
                    }
                }
            };
        }
    }
}