using System;
using System.Collections;

namespace AMSApp.zhenghua.Common
{
    /// <summary>
    /// ���ƣ���ѯ����װ�λ��ࡣ
    /// �汾��V1.0
    /// ������Fightop Lin
    /// ���ڣ�2006-06-25
    /// ����������ʵ�ֹ���
    ///
    /// Log ��1
    /// �汾��
    /// �޸ģ�
    /// ���ڣ�
    /// ������
    ///       
    /// </summary>
	abstract public class QueryConditionDecorator
	{
        protected QueryConditionDecorator queryCondition = null;

		public QueryConditionDecorator(QueryConditionDecorator queryCondition)
		{
            this.queryCondition = queryCondition;
		}

        public virtual string MakeCondition(ArrayList lstSqlParamete)
        {
            if(null != queryCondition)
            {
                return queryCondition.MakeCondition(lstSqlParamete);
            }
            else
            {
                return String.Empty;
            }
        }

	}
}
