using System;
using System.Collections;

namespace AMSApp.zhenghua.EntityBase
{
	/// <summary>
    /// 表映射属性存储类.
    /// fightop@create 2006.6.20.
	/// </summary>
	public class TableAttributes
	{
        // 表名称
        private string           strTableName = String.Empty;
        // 字段
        private ArrayList caColumns = new ArrayList();

        public string TableName
        {
            get{return strTableName;}
            set{strTableName = value;}
        }

        public ArrayList Columns
        {
            get{return caColumns;}
            set{caColumns = value;}
        }
	}
}
