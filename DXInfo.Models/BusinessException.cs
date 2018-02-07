using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXInfo.Models
{
    public class BusinessException : Exception
    {
        private string sMessage = String.Empty;
        public override string Message
        {
            get
            {
                return this.sMessage;
            }
        }
        public BusinessException(string sMessage)
        {
            this.sMessage = sMessage;
        }
        public override string ToString()
        {
            return "业务规则异常: " + sMessage;
        }
    }
}
