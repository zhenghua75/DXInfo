using System;

namespace AMSApp.zhenghua.Common
{
    /// <summary>
    /// ���ƣ���ѯ�����������װ�ࡣ
    /// �汾��V1.0
    /// ������Fightop Lin
    /// ���ڣ�2006-06-25
    /// ������������ɹ���
    ///
    /// Log ��1
    /// �汾��
    /// �޸ģ�
    /// ���ڣ�
    /// ������
    ///       
    /// </summary>
	public class QueryOperationSign
	{  
        /// <summary>
        /// ֧�ֵ��߼�����
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
        /// ֧�ֵ���������
        /// </summary>
        public enum ConditionOperation
        {
            And,      // AND
            Or,       // OR
            Not,      // NOT
        }

        /// <summary>
        /// ֧�ֵ���������
        /// </summary>
        public enum OrderOperation
        {
            ASC,        // ASC
            DESC       // DESC
        }

        /// <summary>
        /// ȡ�߼������ַ���
        /// </summary>
        /// <param name="logic">����enum</param>
        /// <returns>�߼������ַ���</returns>
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
        /// ȡ���������ַ���
        /// </summary>
        /// <param name="condition">����enum</param>
        /// <returns>���������ַ���</returns>
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
        /// ȡ���������ַ���
        /// </summary>
        /// <param name="order">����enum</param>
        /// <returns>���������ַ���</returns>
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
