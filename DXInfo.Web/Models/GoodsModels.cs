using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trirand.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class GoodsModels
    {
    }
    public class GoodsGridModel
    {
        public EntityJQGrid GoodsGrid { get; set; }
        public GoodsGridModel()
        {
            GoodsGrid = new EntityJQGrid()
            {
                Width=1100,
                Columns = new List<JQGridColumn>()
                {
                    new JQGridColumn()
                    {
                        EditActionIconsColumn=true,
                        Viewable=false,
                        Hidedlg=true,
                        EditActionIconsSettings = new EditActionIconsSettings(){ onEdit="beforeInlineEdit"},
                    },
                    new JQGridColumn()
                    {
                        DataField="vcGoodsID",
                        HeaderText="商品编号",
                        DataType=typeof(string),
                        PrimaryKey=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="vcGoodsName",
                        HeaderText="商品名称",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="vcGoodsType",
                        HeaderText="商品类别",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="vcSpell",
                        HeaderText="拼音简写",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="nPrice",
                        HeaderText="单价",
                        DataType=typeof(decimal),
                        Editable=true,
                    },  
                    new JQGridColumn()
                    {
                        DataField="cNewFlag",
                        HeaderText="是否推荐新品",
                        DataType=typeof(string),
                        Editable=true,
                    },
                    new JQGridColumn()
                    {
                        DataField="iIgValue",
                        HeaderText="兑换分值",
                        DataType=typeof(decimal),
                        Editable=true,
                        DefaultValue="-1",
                    }, 
                    //new JQGridColumn()
                    //{
                    //    DataField="vcComments",
                    //    HeaderText="是否折扣",
                    //    DataType=typeof(string),
                    //    Editable=true,
                    //}
                }
            };
        }
    }
}