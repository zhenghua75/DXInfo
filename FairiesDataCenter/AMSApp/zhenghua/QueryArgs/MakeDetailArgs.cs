
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	MakeDetailArgs.cs
* ����:	     ֣��
* ��������:    2008-10-10
* ��������:    ���������Ʒϸ�ڱ�

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ����������Ʒϸ�ڱ��ѯ������
	/// </summary>
	public class MakeDetailArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbMakeDetail";
				
		/// <summary>
		/// ������ˮ
		/// </summary>
		public QueryConditionField cnnMakeSerialNo = new QueryConditionField("cnnMakeSerialNo");

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public QueryConditionField cnvcCode = new QueryConditionField("cnvcCode");

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public QueryConditionField cnvcName = new QueryConditionField("cnvcName");

		/// <summary>
		/// ��λ
		/// </summary>
		public QueryConditionField cnvcUnit = new QueryConditionField("cnvcUnit");

		/// <summary>
		/// ����
		/// </summary>
		public QueryConditionField cnnCount = new QueryConditionField("cnnCount");
	}	
}
