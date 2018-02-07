using System;
using System.Diagnostics;

namespace AMSApp.zhenghua.EntityBase
{
    /// <summary>
    /// 定义实体对像类名到物理表名的映射关系
    /// fightop@create 2006.6.20
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TableMapping : System.Attribute
    {
        #region 私有字段

        // 映射表名称
        private string strTableName = string.Empty;

        #endregion

        #region 构造器

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="strTableName">映射表名称</param>
        public TableMapping(string strTableName)
        {
            Debug.Assert(strTableName != null && strTableName.Trim().Length > 0,
                                              "-- 所提供的映射表名称是无效的!");
            this.strTableName = strTableName;
        }

		#endregion

        #region 公开属性

        /// <summary>
        /// 映射数据库表名称
        /// </summary>
        public string TableName
        {
            get { return strTableName; }
        }

        #endregion
    }
}
