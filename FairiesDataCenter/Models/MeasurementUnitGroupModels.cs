using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace ynhnTransportManage.Models
{
    public class MeasurementUnitGroupModels
    {
    }
    public class MeasurementUnitGroupGridModel
    {
        public EntityJQGrid MeasurementUnitGroupGrid { get; set; }
        public MeasurementUnitGroupGridModel()
        {
            MeasurementUnitGroupGrid = new EntityJQGrid()
            {
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        DataField="Id",
                        DataType=typeof(Guid),
                        PrimaryKey=true,
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
                        DataField="Category",
                        HeaderText="类别",
                        DataType=typeof(int),
                        Editable=true,
                        EditDialogFieldSuffix="(*)",
                        EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                        {
                            new RequiredValidator()                            
                        },
                    },
                    new JQGridColumn()
                    {
                        DataField="CategoryName",
                        HeaderText="类别",
                        DataType=typeof(string),
                        Editable=false,
                        Searchable=false,
                        Exportable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="Comment",
                        HeaderText="描述",
                        DataType=typeof(string),
                        Editable=true                        
                    },
                }
            };
        }
    }
}