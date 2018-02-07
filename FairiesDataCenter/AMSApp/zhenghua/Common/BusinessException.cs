using System;

namespace AMSApp.zhenghua.Common
{
    /// <summary>
    /// ���ƣ�ҵ������쳣�ࡣ
    /// �汾��V1.0
    /// ������Fightop Lin
    /// ���ڣ�2006-1-12
    /// �������������߼�������ҵ�����ʱ�׳����쳣
    ///
    /// Log ��1
    /// �汾��
    /// �޸ģ�
    /// ���ڣ�
    /// ������
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
            return "ҵ������쳣: "  + "[" + sType + "]" + sMessage;
        }

    }
}
