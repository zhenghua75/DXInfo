using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace AMSApp.zhenghua.Common
{
    /// <summary>
    /// ���ƣ���ѯ�ֶ�ӳ���ࡣ
    /// �汾��V1.0
    /// ������Fightop Lin
    /// ���ڣ�2006-06-25
    /// ������ӳ���ѯ�����ϵ��߼�����
    ///
    /// Log ��1
    /// �汾��
    /// �޸ģ�
    /// ���ڣ�
    /// ������
    ///       
    /// </summary>
	public class QueryConditionField : QueryConditionDecorator
	{
        private string strFieldName                     = String.Empty;
        private QueryOperationSign.LogicOperation logic = QueryOperationSign.LogicOperation.Equal;
        private object objValue                         = null;

        // �������������
        private QueryOperationSign.ConditionOperation condition = QueryOperationSign.ConditionOperation.And;


        /// <summary>
        /// ������
        /// </summary>
        /// <param name="strFieldName">ӳ���ֶ�����</param>
        public QueryConditionField(string strFieldName) : base(null)
        {
            this.strFieldName = strFieldName;
        }

        /// <summary>
        /// �ֶ�����
        /// </summary>
        public string FieldName
        {
            get{ return strFieldName; }
        }


        /// <summary>
        /// �����߼�
        /// </summary>
        public QueryOperationSign.LogicOperation Logic
        {
            get{ return logic; }
            set{ logic = value;}
        }

        /// <summary>
        /// ����ֵ
        /// </summary>
        public object Value
        {
            get{ return objValue;}
            set{objValue = value;}
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

        /// <summary>
        /// ��������ʵ��
        /// </summary>
        /// <returns>�¸���</returns>
        public QueryConditionField Copy()
        {
            QueryConditionField field = new QueryConditionField(this.FieldName);
            field.Logic     = this.Logic;
            field.objValue  = this.objValue;

            return field;
        }

        public override string MakeCondition(ArrayList lstSqlParamete)
        {
            string strAllCondition =  base.MakeCondition(lstSqlParamete);

			string strCondition = String.Empty;
			// ȡ�߼������ַ���
			string strLogic = QueryOperationSign.GetLogicOperationString(logic);
			switch(logic)
			{
				// NULL �߼�
				case QueryOperationSign.LogicOperation.IsNull:
				case QueryOperationSign.LogicOperation.IsNotNull:
					strCondition += strFieldName + strLogic;
					break;

				// LIKE �߼�
				case QueryOperationSign.LogicOperation.Like:
					strCondition += strFieldName + strLogic + "'%' + " + "@" + strFieldName + " + '%'";
					lstSqlParamete.Add(new SqlParameter("@" + strFieldName,this.Value));
					break;

				case QueryOperationSign.LogicOperation.LeftLike:
					strCondition += strFieldName + strLogic + "'%' + " + "@" + strFieldName;
					lstSqlParamete.Add(new SqlParameter("@" + strFieldName,this.Value));
					break;

				case QueryOperationSign.LogicOperation.RightLike:
					strCondition += strFieldName + strLogic +  "@" + strFieldName + " + '%'";
					lstSqlParamete.Add(new SqlParameter("@" + strFieldName,this.Value));
					break;

				// �����߼�
				default:
					strCondition += strFieldName + strLogic + "@" + strFieldName;
					lstSqlParamete.Add(new SqlParameter("@" + strFieldName,this.Value));
					break;
			}

            if( null != base.queryCondition)
            {// �������������
                strCondition += QueryOperationSign.GetConditionOperationString(this.condition);
            }

            return strCondition + strAllCondition;
        }

	}
}
