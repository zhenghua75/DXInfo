using System;

namespace AMSApp.zhenghua.Common
{
    /// <summary>
    /// 名称：业务规则异常类。
    /// 版本：V1.0
    /// 创建：Fightop Lin
    /// 日期：2006-1-12
    /// 描述：当操作逻辑不符合业务规则时抛出该异常
    ///
    /// Log ：1
    /// 版本：
    /// 修改：
    /// 日期：
    /// 描述：
    ///       
    /// </summary>
    public class BusinessException : Exception
    {
        private string sType;
        private string sMessage = String.Empty;

        public string Type
        {
            get
            {
                return sType;
            }
        }

        public override string Message
        {
            get
            {
                return sMessage;
            }
        }


        public BusinessException(string sType,string sMessage)
        {
            this.sType    = sType;
            this.sMessage = sMessage;
        }

        public override string ToString()
        {
            return "业务规则异常: "  + "[" + sType + "]" + sMessage;
        }

    }
}
