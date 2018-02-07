using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace ynhnTransportManage.Models
{
    public class SitemapModels
    {
    }
    public class SitemapGridModel
    {
        public EntityJQGrid SitemapGrid { get; set; }
        public SitemapGridModel()
        {
            SitemapGrid = new EntityJQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        EditActionIconsColumn=true,
                        EditActionIconsSettings = new EditActionIconsSettings(){ ShowEditIcon=true,ShowDeleteIcon=true},
                    },
                    new JQGridColumn()
                    {
                        DataField="Code",
                        HeaderText="编码",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()
                        },
                        PrimaryKey=true
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
                        DataField="Title",
                        HeaderText="标题",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",        
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()
                        },
                    },    
                    new JQGridColumn()
                    {
                        DataField="Description",
                        HeaderText="描述",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",        
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()
                        },
                    }, 
                    new JQGridColumn()
                    {
                        DataField="Controller",
                        HeaderText="控制器",
                        DataType=typeof(string),
                        Editable=true,
                    }, 
                    new JQGridColumn()
                    {
                        DataField="Action",
                        HeaderText="方法",
                        DataType=typeof(string),
                        Editable=true,
                    }, 
                    new JQGridColumn()
                    {
                        DataField="ParaId",
                        HeaderText="参数",
                        DataType=typeof(string),
                        Editable=true,
                    }, 
                    new JQGridColumn()
                    {
                        DataField="Url",
                        HeaderText="路径",
                        DataType=typeof(string),
                        Editable=true,
                    }, 
                    new JQGridColumn()
                    {
                        DataField="ParentCode",
                        HeaderText="上级编码",
                        DataType=typeof(string),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",        
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()
                        },
                    }, 
                    new JQGridColumn()
                    {
                        DataField="IsAuthorize",
                        HeaderText="是否控制",
                        DataType=typeof(bool),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="IsMenu",
                        HeaderText="是否菜单",
                        DataType=typeof(bool),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="IsClient",
                        HeaderText="是否客户端",
                        DataType=typeof(bool),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Sort",
                        HeaderText="排序",
                        DataType=typeof(bool),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",        
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator(),
                        },
                    },
                }
            };
        }
    }
}