using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DXInfo.Web.Models
{
    public class ProduceEvaluationModels
    {
    }
    public class ProduceEvaluationGridModel
    {
        public JQGrid ProduceEvaluationGrid { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Display(Name = "门店")]
        public Guid? DeptId { get; set; }
        public ProduceEvaluationGridModel()
        {
            ProduceEvaluationGrid = new JQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="PartInvId",
                        DataType=typeof(Guid),
                        Visible=false,
                        Searchable=false,
                        Editable=true,
                        HeaderText="主件",
                        PrimaryKey=true,
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
                        DataField="PartStockUnitName",
                        HeaderText="主件计量单位",
                        DataType=typeof(string),
                        Searchable=false,
                        Editable=true,
                    },

                    //子件

                    new JQGridColumn()
                    {
                        DataField="ComponentInvId",
                        DataType=typeof(Guid),
                        Visible=false,
                        HeaderText="子件",
                        PrimaryKey=true,
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
                        DataField="ComponentStockUnitName",
                        HeaderText="子件计量单位",
                        DataType=typeof(string),
                        Searchable=false,
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Num1",
                        HeaderText="配方用量",
                        DataType=typeof(decimal),
                        Searchable=false,
                        Formatter=new DigitFormatter()
                    },   
                    new JQGridColumn()
                    {
                        DataField="Num2",
                        HeaderText="实际用量",
                        DataType=typeof(decimal),
                        Searchable=false,
                        Formatter=new DigitFormatter()
                    },                    
                }
            };
        }
    }
}