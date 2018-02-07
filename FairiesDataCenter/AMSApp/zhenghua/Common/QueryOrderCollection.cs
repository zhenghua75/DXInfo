using System;
using System.Collections;

namespace AMSApp.zhenghua.Common
{
    /// <summary>
    /// ���ƣ���ѯ�����װ�ࡣ
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
