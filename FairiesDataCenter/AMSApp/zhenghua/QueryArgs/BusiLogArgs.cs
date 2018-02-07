
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	BusiLogArgs.cs
* ����:	     ֣��
* ��������:    2008-9-29
* ��������:    ҵ����־

*                                                           Copyright(C) 2008 fightop
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ�ҵ����־��ѯ������
	/// </summary>
	public class BusiLogArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbBusiLog";
				
		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField iSerial = new QueryConditionField("iSerial");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField iAssID = new QueryConditionField("iAssID");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField vcCardID = new QueryConditionField("vcCardID");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField vcOperType = new QueryConditionField("vcOperType");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField vcOperName = new QueryConditionField("vcOperName");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField dtOperDate = new QueryConditionField("dtOperDate");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField vcComments = new QueryConditionField("vcComments");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField vcDeptID = new QueryConditionField("vcDeptID");
	}	
}
