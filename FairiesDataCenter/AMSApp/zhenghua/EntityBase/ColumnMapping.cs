using System;
using System.Diagnostics;

namespace AMSApp.zhenghua.EntityBase
{
	/// <summary>
	/// 定义实体对像属性到物理表例名的映射关系
	/// fightop@create 2006.6.20
	/// </summary>
	[System.AttributeUsage(AttributeTargets.Property,AllowMultiple = false)]
	public class ColumnMapping : System.Attribute
	{
		#region 私有字段

        // 映射字段名称
		private string strColumnName  = string.Empty;

        // 是否为主键
        private bool bIsPrimaryKey = false;

        // 是否为自增字段
        private bool bIsIdentity = false;

        // 是否为版本号
        private bool bIsVersionNumber = false;

		#endregion

        #region 构造器

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="strColumnName">映射字段名称</param>
        public ColumnMapping(string strColumnName)
        {
            Debug.Assert(strColumnName != null && strColumnName.Trim().Length > 0,
                                                      "-- 所提供的映射字段名称是无效的!");
            this.strColumnName = strColumnName;
        }

		#endregion

		#region 公开属性

		/// <summary>
		/// 映射数据库字段名称
		/// </summary>
        public string ColumnName
		{
			get { return strColumnName; }
		}

		/// <summary>
		/// 是否为主键
		/// </summary>
        public bool IsPrimaryKey
		{
			get { return bIsPrimaryKey; }
            set { bIsPrimaryKey = value; }
		}

        /// <summary>
        /// 是否为自增字段
        /// </summary>
        public bool IsIdentity
        {
            get { return bIsIdentity; }
            set { bIsIdentity = value; }

        }

        /// <summary>
        /// 是否为版本号
        /// </summary>
        public bool IsVersionNumber
        {
            get { return bIsVersionNumber; }
            set { bIsVersionNumber = value; }

        }

		#endregion
	}
}
