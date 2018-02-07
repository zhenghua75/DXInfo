using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXInfo.Models
{
    /// <summary>
    /// 生产入库
    /// </summary>
    public class ProductionInStorage
    {
        public int Id { get; set; }
        /// <summary>
        /// 门店
        /// </summary>
        public string vcDeptId { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime InDate { get; set; }
        /// <summary>
        /// 商品
        /// </summary>
        public string vcGoodsId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string vcOperId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
