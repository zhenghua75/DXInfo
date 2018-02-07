using System;
using System.Collections;

namespace AMSApp.zhenghua.Common
{
    /// <summary>
    /// 名称：查询条件分组映射类。
    /// 版本：V1.0
    /// 创建：Fightop Lin
    /// 日期：2006-06-25
    /// 描述：映射分组
    ///
    /// Log ：1
    /// 版本：
    /// 修改：
    /// 日期：
    /// 描述：
    ///       
    /// </summary>
	public class QueryConditionGroup : QueryConditionDecorator
	{
        // 分组包含的条件
        private new QueryConditionDecorator queryCondition = null;
        // 连接分组时的条件运算符
        private QueryOperationSign.ConditionOperation condition = QueryOperationSign.ConditionOperation.And;

        public QueryConditionDecorator QueryCondition
        {
            get{ return queryCondition; }
            set{ queryCondition = value;}
        }


        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="queryCondition">分组包含的条件</param>
		public QueryConditionGroup(QueryConditionDecorator queryCondition):base(null)
		{
            this.queryCondition = queryCondition;
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
