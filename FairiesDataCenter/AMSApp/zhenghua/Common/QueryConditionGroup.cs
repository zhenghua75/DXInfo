using System;
using System.Collections;

namespace AMSApp.zhenghua.Common
{
    /// <summary>
    /// ���ƣ���ѯ��������ӳ���ࡣ
    /// �汾��V1.0
    /// ������Fightop Lin
    /// ���ڣ�2006-06-25
    /// ������ӳ�����
    ///
    /// Log ��1
    /// �汾��
    /// �޸ģ�
    /// ���ڣ�
    /// ������
    ///       
    /// </summary>
	public class QueryConditionGroup : QueryConditionDecorator
	{
        // �������������
        private new QueryConditionDecorator queryCondition = null;
        // ���ӷ���ʱ�����������
        private QueryOperationSign.ConditionOperation condition = QueryOperationSign.ConditionOperation.And;

        public QueryConditionDecorator QueryCondition
        {
            get{ return queryCondition; }
            set{ queryCondition = value;}
        }


        /// <summary>
        /// ������
        /// </summary>
        /// <param name="queryCondition">�������������</param>
		public QueryConditionGroup(QueryConditionDecorator queryCondition):base(null)
		{
            this.queryCondition = queryCondition;
		}

		/// <summary>
		/// �������� ʹ��Ĭ����������� AND
		/// </summary>
		/// <param name="queryCondition">����������</param>
		public void ConnectTo(QueryConditionDecorator queryCondition)
		{
			base.queryCondition = queryCondition;
		}
    
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="queryCondition">����������</param>
        /// <param name="condition">���������</param>
        public void ConnectTo(QueryConditionDecorator queryCondition,QueryOperationSign.ConditionOperation condition)
        {
            base.queryCondition = queryCondition;
            this.condition      = condition;
        }

        public override string MakeCondition(ArrayList lstSqlParamete)
        {
            string strAllCondition = base.MakeCondition(lstSqlParamete);

            string strGroup = "(" + queryCondition.MakeCondition(lstSqlParamete) + ")";

            if(null != base.queryCondition)
            {
                strGroup += QueryOperationSign.GetConditionOperationString(condition);
            }

            return strGroup + strAllCondition;
        }
	}
}
