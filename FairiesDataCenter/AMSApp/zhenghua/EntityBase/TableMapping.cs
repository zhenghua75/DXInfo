using System;
using System.Diagnostics;

namespace AMSApp.zhenghua.EntityBase
{
    /// <summary>
    /// ����ʵ��������������������ӳ���ϵ
    /// fightop@create 2006.6.20
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TableMapping : System.Attribute
    {
        #region ˽���ֶ�

        // ӳ�������
        private string strTableName = string.Empty;

        #endregion

        #region ������

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="strTableName">ӳ�������</param>
        public TableMapping(string strTableName)
        {
            Debug.Assert(strTableName != null && strTableName.Trim().Length > 0,
                                              "-- ���ṩ��ӳ�����������Ч��!");
            this.strTableName = strTableName;
        }

		#endregion

        #region ��������

        /// <summary>
        /// ӳ�����ݿ������
        /// </summary>
        public string TableName
        {
            get { return strTableName; }
        }

        #endregion
    }
}
