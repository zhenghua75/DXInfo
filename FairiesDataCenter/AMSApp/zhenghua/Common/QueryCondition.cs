using System;
using System.Collections;

namespace AMSApp.zhenghua.Common
{
    /// <summary>
    /// 名称：查询条件实现类。
    /// 版本：V1.0
    /// 创建：Fightop Lin
    /// 日期：2006-06-25
    /// 描述：
    ///
    /// Log ：1
    /// 版本：
    /// 修改：
    /// 日期：
    /// 描述：
    ///       
    /// </summary>
    public class QueryCondition : QueryConditionDecorator
    {
        public QueryCondition() : base( null ){ }

        /// <summary>
        /// 条件连接
        /// </summary>
        /// <param name="queryCondition">被连接条件</param>
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
