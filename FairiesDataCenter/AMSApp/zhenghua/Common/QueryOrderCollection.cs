using System;
using System.Collections;

namespace AMSApp.zhenghua.Common
{
    /// <summary>
    /// 名称：查询排序包装类。
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
	public class QueryOrderCollection
	{
        private Hashtable hsOrder = new Hashtable();

        public void Add(string strOrderField,QueryOperationSign.OrderOperation orderSign)
        {

            hsOrder.Add(strOrderField,orderSign);
        }

        public string MakeOrder()
        {
            string strOrder = " ORDER BY ";

            int iCount=0;
            int iOrderCount = hsOrder.Count;
            if(0 == iOrderCount)
            {
                return "";
            }

            foreach(DictionaryEntry de in hsOrder)
            {
                strOrder += de.Key.ToString() + " " + 
                    QueryOperationSign.GetOrderOperationString((QueryOperationSign.OrderOperation)de.Value);
                
                iCount++;
                if(iCount != iOrderCount)
                {
                    strOrder += ",";
                }
            }

            return strOrder;
        }
	}
}
