using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXInfo.Models
{
    public class EnumHelper
    {
        public static string GetUnitGroupCategory(UnitGroupCategory category)
        {
            string ret = "";
            switch (category)
            {
                case UnitGroupCategory.No:
                    ret = "无换算";
                    break;
                case UnitGroupCategory.Fixed:
                    ret = "固定换算";
                    break;
                case UnitGroupCategory.Float:
                    ret = "浮动换算";
                    break;
            }
            return ret;
        }
        public static string GetValueType(ValueType valueType)
        {
            string ret = "";
            switch (valueType)
            {
                case DXInfo.Models.ValueType.Separately:
                    ret = "个别计价";
                    break;
            }
            return ret;
        }
        /// <summary>
        /// 计量单位组类别
        /// UnitGroupCategory
        /// </summary>
        public const string MeasurementUnitGroupCategory = "MeasurementUnitGroupCategory";
        /// <summary>
        /// 计价方式
        /// </summary>
        public const string ValueType = "ValueType";
        /// <summary>
        /// 盘点周期
        /// </summary>
        public const string CheckCycle = "CheckCycle";
        public const string ShelfLifeType = "ShelfLifeType";
        /// <summary>
        /// 采购入库单类型
        /// </summary>
        public const string PurchaseInStockVouchType = "001";
        public const string ExceptionPolicy = "Policy";
    }
}
