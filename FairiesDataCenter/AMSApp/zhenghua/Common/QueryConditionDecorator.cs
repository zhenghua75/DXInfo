using System;
using System.Collections;

namespace AMSApp.zhenghua.Common
{
    /// <summary>
    /// 名称：查询条件装饰基类。
    /// 版本：V1.0
    /// 创建：Fightop Lin
    /// 日期：2006-06-25
    /// 描述：辅助实现功能
    ///
    /// Log ：1
    /// 版本：
    /// 修改：
    /// 日期：
    /// 描述：
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
