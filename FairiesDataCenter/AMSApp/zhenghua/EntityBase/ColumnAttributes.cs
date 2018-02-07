using System;

namespace AMSApp.zhenghua.EntityBase
{
	/// <summary>
	/// �ֶ�ӳ�����Դ洢��.
	/// fightop@create 2006.6.20
	/// </summary>
	public class ColumnAttributes
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

        // �ֶ�ֵ
        private object objValue = null;

        // �Ƿ��޸�
        private bool bIsModify = false;

        // ԭʼֵ
        private object objOriginalValue = null;

        #endregion

        #region ��������

        /// <summary>
        /// ӳ�����ݿ��ֶ�����
        /// </summary>
        public string ColumnName
        {
            get { return strColumnName; }
            set { strColumnName = value; }
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

        /// <summary>
        /// �ֶ�ֵ
        /// </summary>
        public object Value
        {
            get { return objValue; }
            set { objValue = value;}
        }

        /// <summary>
        /// �Ƿ��޸�
        /// </summary>
        public bool IsModify
        {
            get { return bIsModify; }
            set { bIsModify = value; }
        }

        /// <summary>
        /// ԭʼֵ
        /// </summary>
        public object OriginalValue
        {
            get { return objOriginalValue; }
            set { objOriginalValue = value; }
        }

        #endregion
	}
}
