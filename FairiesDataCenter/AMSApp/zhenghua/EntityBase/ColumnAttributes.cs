using System;

namespace AMSApp.zhenghua.EntityBase
{
	/// <summary>
	/// 字段映射属性存储类.
	/// fightop@create 2006.6.20
	/// </summary>
	public class ColumnAttributes
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

        // 字段值
        private object objValue = null;

        // 是否被修改
        private bool bIsModify = false;

        // 原始值
        private object objOriginalValue = null;

        #endregion

        #region 公开属性

        /// <summary>
        /// 映射数据库字段名称
        /// </summary>
        public string ColumnName
        {
            get { return strColumnName; }
            set { strColumnName = value; }
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

        /// <summary>
        /// 字段值
        /// </summary>
        public object Value
        {
            get { return objValue; }
            set { objValue = value;}
        }

        /// <summary>
        /// 是否被修改
        /// </summary>
        public bool IsModify
        {
            get { return bIsModify; }
            set { bIsModify = value; }
        }

        /// <summary>
        /// 原始值
        /// </summary>
        public object OriginalValue
        {
            get { return objOriginalValue; }
            set { objOriginalValue = value; }
        }

        #endregion
	}
}
