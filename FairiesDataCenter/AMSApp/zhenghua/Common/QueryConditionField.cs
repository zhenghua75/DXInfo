using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace AMSApp.zhenghua.Common
{
    /// <summary>
    /// 名称：查询字段映射类。
    /// 版本：V1.0
    /// 创建：Fightop Lin
    /// 日期：2006-06-25
    /// 描述：映射查询条件上的逻辑运算
    ///
    /// Log ：1
    /// 版本：
    /// 修改：
    /// 日期：
    /// 描述：
    ///       
    /// </summary>
	public class QueryConditionField : QueryConditionDecorator
	{
        private string strFieldName                     = String.Empty;
        private QueryOperationSign.LogicOperation logic = QueryOperationSign.LogicOperation.Equal;
        private object objValue                         = null;

        // 连接条件运算符
        private QueryOperationSign.ConditionOperation condition = QueryOperationSign.ConditionOperation.And;


        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="strFieldName">映射字段名字</param>
        public QueryConditionField(string strFieldName) : base(null)
        {
            this.strFieldName = strFieldName;
        }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName
        {
            get{ return strFieldName; }
        }


        /// <summary>
        /// 运算逻辑
        /// </summary>
        public QueryOperationSign.LogicOperation Logic
        {
            get{ return logic; }
            set{ logic = value;}
        }

        /// <summary>
        /// 运算值
        /// </summary>
        public object Value
        {
            get{ return objValue;}
            set{objValue = value;}
        }

		/// <summary>
		/// 条件连接 使用默认条件运算符 AND
		/// </summary>
		/// <param name="queryCondition">被连接条件</param>
		public void ConnectTo(QueryConditionDecorator queryCondition)
		{
			base.queryCondition = queryCondition;
		}

        /// <summary>
        /// 条件连接
        /// </summary>
        /// <param name="queryCondition">被连接条件</param>
        /// <param name="condition">条件运算符</param>
        public void ConnectTo(QueryConditionDecorator queryCondition,QueryOperationSign.ConditionOperation condition)
        {
            base.queryCondition = queryCondition;
            this.condition      = condition;
        }

        /// <summary>
        /// 拷贝副本实例
        /// </summary>
        /// <returns>新副本</returns>
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
			// 取逻辑运算字符串
			string strLogic = QueryOperationSign.GetLogicOperationString(logic);
			switch(logic)
			{
				// NULL 逻辑
				case QueryOperationSign.LogicOperation.IsNull:
				case QueryOperationSign.LogicOperation.IsNotNull:
					strCondition += strFieldName + strLogic;
					break;

				// LIKE 逻辑
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

				// 其它逻辑
				default:
					strCondition += strFieldName + strLogic + "@" + strFieldName;
					lstSqlParamete.Add(new SqlParameter("@" + strFieldName,this.Value));
					break;
			}

            if( null != base.queryCondition)
            {// 加上条件运算符
                strCondition += QueryOperationSign.GetConditionOperationString(this.condition);
            }

            return strCondition + strAllCondition;
        }

	}
}
