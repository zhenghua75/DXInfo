using System;
using System.Collections;

namespace AMSApp.zhenghua.Common
{
    /// <summary>
    /// ���ƣ���ѯ����ʵ���ࡣ
    /// �汾��V1.0
    /// ������Fightop Lin
    /// ���ڣ�2006-06-25
    /// ������
    ///
    /// Log ��1
    /// �汾��
    /// �޸ģ�
    /// ���ڣ�
    /// ������
    ///       
    /// </summary>
    public class QueryCondition : QueryConditionDecorator
    {
        public QueryCondition() : base( null ){ }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="queryCondition">����������</param>
        public void ConnectTo(QueryConditionDecorator queryCondition)
        {
            base.queryCondition = queryCondition;
        }

        public override string MakeCondition(ArrayList lstSqlParamete)
        {
            return " WHERE " + base.MakeCondition(lstSqlParamete);
        }
    }
}
