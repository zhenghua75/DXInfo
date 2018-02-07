using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace ynhnTransportManage.Models
{
    public class UnitOfMeasureModels
    {
    }
    public class UnitOfMeasureGridModel
    {
        public int UOMType { get; set; }
        public EntityJQGrid UnitOfMeasuresGrid { get; set; }
        public UnitOfMeasureGridModel()
        {
            UnitOfMeasuresGrid = new EntityJQGrid()
            {
                AutoWidth = true,
                Height = Unit.Percentage(100),
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType=typeof(Guid),
                        PrimaryKey = true,
                        Visible=false,
                        Searchable=false
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
                        DataField="Group",
                        HeaderText="计量单位组",
                        DataType=typeof(Guid),
                        Editable=true,
                        Visible=false,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="GroupName",
                        HeaderText="计量单位组",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                        Width=100,
                    },
                    new JQGridColumn()
                    {
                        DataField="Rate",
                        HeaderText="换算率",
                        DataType=typeof(decimal),
                        Editable=true,
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new NumberValidator(),
                        },
                        Formatter = new NumberFormatter(){ DecimalPlaces=3 }
                    },
                    new JQGridColumn()
                    {
                        DataField="IsMain",
                        HeaderText="是否主计量单位",
                        DataType=typeof(bool),
                        Editable=true,
                        Width=100,
                    },
                    new JQGridColumn()
                    {
                        DataField="Comment",
                        HeaderText="描述",
                        DataType=typeof(string),
                        Editable=true
                    },
                    new JQGridColumn()
                    {
                        DataField="UOMType",
                        DataType=typeof(int),
                        Visible=false,
                        Searchable=false,
                        SearchToolBarOperation = SearchOperation.IsEqualTo,
                    }
                }
            };
        }
    }
}