using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class SysParaSetModels
    {
    }
    public class SysParaSetGridModel
    {
        public EntityJQGrid SysParaSetGrid { get; set; }
        public SysParaSetGridModel()
        {            
            SysParaSetGrid = new EntityJQGrid()
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
                        DataType=typeof(int),
                        PrimaryKey=true,
                        Visible=false,
                    },
                    new JQGridColumn()
                    {
                        DataField="vcCommCode",
                        HeaderText="参数值",
                        DataType=typeof(string),
                        Editable=true,
                        //EditFieldAttributes=new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } },
                    },
                    new JQGridColumn()
                    {
                        DataField="vcCommName",
                        HeaderText="参数名称",
                        DataType=typeof(string),
                        Editable=true,
                        //EditFieldAttributes=new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } },
                    },
                    new JQGridColumn()
                    {
                        DataField="vcCommSign",
                        HeaderText="参数类型",
                        DataType=typeof(string),
                        Editable=true,
                        //EditFieldAttributes=new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } },
                    },
                    new JQGridColumn()
                    {
                        DataField="vcComments",
                        HeaderText="描述",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    
                }
            };
        }
    }
}