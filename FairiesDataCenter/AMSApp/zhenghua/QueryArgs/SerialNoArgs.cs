
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	SerialNoArgs.cs
* ����:	     ֣��
* ��������:    2009-7-24
* ��������:    ��Ʒ��������ˮ��

*                                                           Copyright(C) 2009 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ���Ʒ��������ˮ���ѯ������
	/// </summary>
	public class SerialNoArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbSerialNo";
				
		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField cnnSerialNo = new QueryConditionField("cnnSerialNo");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField cnvcFill = new QueryConditionField("cnvcFill");
	}	
}
