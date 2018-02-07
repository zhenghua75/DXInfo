using System;
using System.Diagnostics;

namespace AMSApp.zhenghua.EntityBase
{
	/// <summary>
	/// ����ʵ��������Ե������������ӳ���ϵ
	/// fightop@create 2006.6.20
	/// </summary>
	[System.AttributeUsage(AttributeTargets.Property,AllowMultiple = false)]
	public class ColumnMapping : System.Attribute
	{
		#region ˽���ֶ�

        // ӳ���ֶ�����
		private string strColumnName  = string.Empty;

        // �Ƿ�Ϊ����
        private bool bIsPrimaryKey = false;

        // �Ƿ�Ϊ�����ֶ�
        private bool bIsIdentity = false;

        // �Ƿ�Ϊ�汾��
        private bool bIsVersionNumber = false;

		#endregion

        #region ������

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="strColumnName">ӳ���ֶ�����</param>
        public ColumnMapping(string strColumnName)
        {
            Debug.Assert(strColumnName != null && strColumnName.Trim().Length > 0,
                                                      "-- ���ṩ��ӳ���ֶ���������Ч��!");
            this.strColumnName = strColumnName;
        }

		#endregion

		#region ��������

		/// <summary>
		/// ӳ�����ݿ��ֶ�����
		/// </summary>
        public string ColumnName
		{
			get { return strColumnName; }
		}

		/// <summary>
		/// �Ƿ�Ϊ����
		/// </summary>
        public bool IsPrimaryKey
		{
			get { return bIsPrimaryKey; }
            set { bIsPrimaryKey = value; }
		}

        /// <summary>
        /// �Ƿ�Ϊ�����ֶ�
        /// </summary>
        public bool IsIdentity
        {
            get { return bIsIdentity; }
            set { bIsIdentity = value; }

        }

        /// <summary>
        /// �Ƿ�Ϊ�汾��
        /// </summary>
        public bool IsVersionNumber
        {
            get { return bIsVersionNumber; }
            set { bIsVersionNumber = value; }

        }

		#endregion
	}
}
