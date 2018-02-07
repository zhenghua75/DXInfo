using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace DXInfo.Web.Models
{
    public class InventoryCategoryModels
    {
        
    }
    public class InventoryCategoryGridModel
    {
        public int CategoryType { get; set; }
        public int DeptType { get; set; }
        public EntityJQGrid InventoryCategoryGrid { get; set; }
        public InventoryCategoryGridModel()
        {
            InventoryCategoryGrid = new EntityJQGrid()
            {
                Width=Unit.Pixel(1100),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        Viewable=false,
                        Hidedlg=true,
                        EditActionIconsColumn=true,
                        EditActionIconsSettings = new EditActionIconsSettings(),
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
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="Name",
                        HeaderText="名称",
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
                        DataField="IsDiscount",
                        HeaderText="是否折扣",
                        DataType=typeof(bool),
                        Editable=true,
                    },
                    //new JQGridColumn()
                    //{
                    //    DataField="ProductType",
                    //    HeaderText="产品类型",
                    //    DataType=typeof(int),
                    //    Editable=true,
                    //    EditDialogFieldSuffix="(*)",
                    //    EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                    //    {
                    //        new RequiredValidator()                            
                    //    },
                    //}, 
                    //new JQGridColumn()
                    //{
                    //    DataField="ProductTypeName",
                    //    HeaderText="产品类型",
                    //    DataType=typeof(string),
                    //    Visible=false,
                    //    Searchable=false,
                    //    Exportable=true,
                    //    Hidedlg=true,
                    //},
                    new JQGridColumn()
                    {
                        DataField="SectionType",
                        HeaderText="位置",
                        DataType=typeof(int),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="SectionTypeName",
                        HeaderText="位置",
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
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="CategoryType",
                        DataType=typeof(int),
                        Visible=false,
                        Searchable=false,
                        SearchToolBarOperation = SearchOperation.IsEqualTo,
                        Hidedlg=true,
                    },
                }

            };
        }
    }    
}