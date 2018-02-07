using System;

namespace AMSApp.zhenghua.Common
{
    /// <summary>
    /// 名称：查询条件运算符包装类。
    /// 版本：V1.0
    /// 创建：Fightop Lin
    /// 日期：2006-06-25
    /// 描述：辅助完成功能
    ///
    /// Log ：1
    /// 版本：
    /// 修改：
    /// 日期：
    /// 描述：
    ///       
    /// </summary>
	public class QueryOperationSign
	{  
        /// <summary>
        /// 支持的逻辑运算
        /// </summary>
        public enum LogicOperation
        {
            Equal,    // =
            NotEqual, // <>
            IsNull,   // IS NULL
            IsNotNull,// IS NOT NULL
			Like,     // LIKE '%Content%'
			LeftLike, // LIKE '%Content'
			RightLike // LIKE 'Content%'
        }

        /// <summary>
        /// 支持的条件运算
        /// </summary>
        public enum ConditionOperation
        {
            And,      // AND
            Or,       // OR
            Not,      // NOT
        }

        /// <summary>
        /// 支持的排序运算
        /// </summary>
        public enum OrderOperation
        {
            ASC,        // ASC
            DESC       // DESC
        }

        /// <summary>
        /// 取逻辑运算字符串
        /// </summary>
        /// <param name="logic">运算enum</param>
        /// <returns>逻辑运算字符串</returns>
        public static string GetLogicOperationString(LogicOperation logic)
        {
            string strLogicOperation = ""; 

            switch(logic)
            {
                case LogicOperation.Equal :
                    strLogicOperation = " = ";
                    break;
                case LogicOperation.NotEqual :
                    strLogicOperation = " <> ";
                    break;
                case LogicOperation.IsNull :
                    strLogicOperation = " IS NULL ";
                    break;
                case LogicOperation.IsNotNull :
                    strLogicOperation = " IS NOT NULL ";
                    break;
				case LogicOperation.Like:
				case LogicOperation.LeftLike:
				case LogicOperation.RightLike:
					strLogicOperation = " LIKE ";
					break;
                default:
                    strLogicOperation = " OnKonw ";
                    break;
            }

            return strLogicOperation;
        }


        /// <summary>
        /// 取条件运算字符串
        /// </summary>
        /// <param name="condition">运算enum</param>
        /// <returns>条件运算字符串</returns>
        public static string GetConditionOperationString(ConditionOperation condition)
        {
            string strConditionOperation = ""; 

            switch(condition)
            {
                case ConditionOperation.And :
                    strConditionOperation = " AND ";
                    break;
                case ConditionOperation.Or  :
                    strConditionOperation = " OR ";
                    break;
                case ConditionOperation.Not :
                    strConditionOperation = " NOT ";
                    break;
                default:
                    strConditionOperation = " OnKonw ";
                    break;
            }

            return strConditionOperation;
        }


        /// <summary>
        /// 取排序运算字符串
        /// </summary>
        /// <param name="order">运算enum</param>
        /// <returns>排序运算字符串</returns>
        public static string GetOrderOperationString(OrderOperation order)
        {
            string strOrderOperation = ""; 

            switch(order)
            {
                case OrderOperation.ASC:
                    strOrderOperation = " ASC ";
                    break;
                case OrderOperation.DESC  :
                    strOrderOperation = " DESC ";
                    break;
                default:
                    strOrderOperation = " OnKonw ";
                    break;
            }

            return strOrderOperation;
        }
	}
}
