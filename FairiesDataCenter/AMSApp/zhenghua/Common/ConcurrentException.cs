using System;
using System.Data.Common;

namespace AMSApp.zhenghua.Common
{
    /// <summary>
    /// 名称：数据并发访问异常。

    /// 版本：V1.0
    /// 创建：Fightop Lin
    /// 日期：2006-7-28
    /// 描述：当并发操作数据时抛出该异常
    ///
    /// Log ：1
    /// 版本：

    /// 修改：

    /// 日期：

    /// 描述：

    ///       
    /// </summary>
    public class ConcurrentException : BusinessException
    {
        private string strTableName = String.Empty;

        private string strKey  = string.Empty;

        public override string Message
        {
            get
            {
                return "[Table]" + strTableName + "    " + "[Key]" + strKey;
            }
        }

        public ConcurrentException(string strTableName, string strKey):base("CONCURRENT",String.Empty)
        {
            this.strTableName = strTableName;
            this.strKey = strKey;
        }

        public override string ToString()
        {
            return "数据并发访问异常:\n " +  Message + "\n" + base.ToString();
        }
    }
}
