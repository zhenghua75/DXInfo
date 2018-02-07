
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	OrderSerialNoArgs.cs
* ����:	     ֣��
* ��������:    2008-10-4
* ��������:    ������ˮ���ɱ�

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ�������ˮ���ɱ��ѯ������
	/// </summary>
	public class OrderSerialNoArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbOrderSerialNo";
				
		/// <summary>
		/// ������ˮ
		/// </summary>
		public QueryConditionField cnnSerialNo = new QueryConditionField("cnnSerialNo");

		/// <summary>
		/// ���
		/// </summary>
		public QueryConditionField cnvcFill = new QueryConditionField("cnvcFill");
	}	
}
