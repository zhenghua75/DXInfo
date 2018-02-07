using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class BillOfMaterialsModels
    {
    }
    public class BillOfMaterialsGridModel
    {
        public EntityJQGrid BillOfMaterialsGrid { get; set; }
        public BillOfMaterialsGridModel()
        {
            BillOfMaterialsGrid = new EntityJQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType=typeof(Guid),
                        Visible=false,
                        PrimaryKey=true,
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="PartInvId",
                        DataType=typeof(Guid),
                        Visible=false,
                        Searchable=false,
                        Editable=true,
                        HeaderText="主件",
                    },
                    new JQGridColumn()
                    {
                        DataField="PartInvCode",
                        HeaderText="主件存货编码",
                        DataType=typeof(string),
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="PartInvName",
                        HeaderText="主件存货名称",
                        DataType=typeof(Guid),
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="PartSpecs",
                        HeaderText="主件规格型号",
                        DataType=typeof(string),
                        Searchable=false,
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="PartGroupName",
                        HeaderText="主件计量单位组",
                        DataType=typeof(string),
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="PartStockUnitName",
                        HeaderText="主件计量单位",
                        DataType=typeof(string),
                        Searchable=false,
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="BaseQtyD",
                        HeaderText="主件基础用量",
                        DataType=typeof(Decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator()
                        },
                        Searchable=false,
                        Formatter = new DigitFormatter(),
                    },

                    //子件

                    new JQGridColumn()
                    {
                        DataField="ComponentInvId",
                        DataType=typeof(Guid),
                        Searchable=false,
                        Visible=false,
                        Editable=true,
                        HeaderText="子件",
                    },
                    new JQGridColumn()
                    {
                        DataField="ComponentInvCode",
                        HeaderText="子件存货编码",
                        DataType=typeof(string),
                    },    
                    
                    new JQGridColumn()
                    {
                        DataField="ComponentInvName",
                        HeaderText="子件存货名称",
                        DataType=typeof(string),
                    },
                    new JQGridColumn()
                    {
                        DataField="ComponentSpecs",
                        HeaderText="子件规格型号",
                        DataType=typeof(string),
                        Searchable=false,
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="ComponentGroupName",
                        HeaderText="子件计量单位组",
                        DataType=typeof(string),
                        Searchable=false
                    },
                    new JQGridColumn()
                    {
                        DataField="ComponentStockUnitName",
                        HeaderText="子件计量单位",
                        DataType=typeof(string),
                        Searchable=false,
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="BaseQtyN",
                        HeaderText="子件用量",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator(),
                            new NumberValidator()
                        },
                        Searchable=false,
                        Formatter = new DigitFormatter(),
                    },                    
                }
            };
        }
    }
}