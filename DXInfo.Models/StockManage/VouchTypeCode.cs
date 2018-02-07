using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXInfo.Models
{
    public class VouchTypeCode
    {
        /// <summary>
        /// 采购入库单
        /// </summary>
        public const string PurchaseInStock = "001";
        /// <summary>
        /// 销售出库单
        /// </summary>
        public const string SaleOutStock = "002";
        /// <summary>
        /// 产成品入库单
        /// </summary>
        public const string ProductInStock = "006";
        /// <summary>
        /// 材料出库单
        /// </summary>
        public const string MaterialOutStock = "005";
        /// <summary>
        /// 其它入库单
        /// </summary>
        public const string OtherInStock = "003";
        public const string OtherOutStock = "004";
        public const string InitStock = "007";
        public const string ScrapVouch = "008";
        public const string TransVouch = "009";
        public const string CheckVouch = "010";
        public const string AdjustLocatorVouch = "011";
        public const string MixVouch = "012";
        /// <summary>
        /// 零售出库单
        /// </summary>
        public const string RetailOutStock = "013";
    }
}
