using System;
using System.Collections;

namespace AMSApp.zhenghua.EntityBase
{
	/// <summary>
    /// ��ӳ�����Դ洢��.
    /// fightop@create 2006.6.20.
	/// </summary>
	public class TableAttributes
	{
        // ������
        private string           strTableName = String.Empty;
        // �ֶ�
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
