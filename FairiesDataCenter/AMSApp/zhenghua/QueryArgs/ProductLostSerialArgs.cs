
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	ProductLostSerialArgs.cs
* ����:	     ֣��
* ��������:    2009-7-24
* ��������:    ��Ʒ������ˮ��

*                                                           Copyright(C) 2009 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ���Ʒ������ˮ���ѯ������
	/// </summary>
	public class ProductLostSerialArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbProductLostSerial";
				
		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField cnnSerialNo = new QueryConditionField("cnnSerialNo");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField cnvcName = new QueryConditionField("cnvcName");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField cnvcCode = new QueryConditionField("cnvcCode");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField cnnCount = new QueryConditionField("cnnCount");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField cnnAddCount = new QueryConditionField("cnnAddCount");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField cnnReduceCount = new QueryConditionField("cnnReduceCount");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField cndLostDate = new QueryConditionField("cndLostDate");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField cnvcDeptID = new QueryConditionField("cnvcDeptID");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField cnvcOperID = new QueryConditionField("cnvcOperID");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField cndOperDate = new QueryConditionField("cndOperDate");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField cnvcComments = new QueryConditionField("cnvcComments");
	}	
}
