
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:    	InventoryArgs.cs
* ����:	     ֣��
* ��������:    2010-3-6
* ��������:    �������

*                                                           Copyright(C) 2010 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ����������ѯ������
	/// </summary>
	public class InventoryArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbInventory";
				
		/// <summary>
		/// ������������
		/// </summary>
		public QueryConditionField cnbProductBill = new QueryConditionField("cnbProductBill");

		/// <summary>
		/// �������
		/// </summary>
		public QueryConditionField cnvcInvCode = new QueryConditionField("cnvcInvCode");

		/// <summary>
		/// �������
		/// </summary>
		public QueryConditionField cnvcInvName = new QueryConditionField("cnvcInvName");

		/// <summary>
		/// ����ͺ�
		/// </summary>
		public QueryConditionField cnvcInvStd = new QueryConditionField("cnvcInvStd");

		/// <summary>
		/// ����������
		/// </summary>
		public QueryConditionField cnvcInvCCode = new QueryConditionField("cnvcInvCCode");

		/// <summary>
		/// �Ƿ�����
		/// </summary>
		public QueryConditionField cnbSale = new QueryConditionField("cnbSale");

		/// <summary>
		/// �Ƿ��⹺
		/// </summary>
		public QueryConditionField cnbPurchase = new QueryConditionField("cnbPurchase");

		/// <summary>
		/// �Ƿ�����
		/// </summary>
		public QueryConditionField cnbSelf = new QueryConditionField("cnbSelf");

		/// <summary>
		/// �Ƿ���������
		/// </summary>
		public QueryConditionField cnbComsume = new QueryConditionField("cnbComsume");

		/// <summary>
		/// �ο��ɱ�
		/// </summary>
		public QueryConditionField cniInvCCost = new QueryConditionField("cniInvCCost");

		/// <summary>
		/// ���³ɱ�
		/// </summary>
		public QueryConditionField cniInvNCost = new QueryConditionField("cniInvNCost");

		/// <summary>
		/// ��ȫ�����
		/// </summary>
		public QueryConditionField cniSafeNum = new QueryConditionField("cniSafeNum");

		/// <summary>
		/// ��Ϳ��
		/// </summary>
		public QueryConditionField cniLowSum = new QueryConditionField("cniLowSum");

		/// <summary>
		/// ��������
		/// </summary>
		public QueryConditionField cndSDate = new QueryConditionField("cndSDate");

		/// <summary>
		/// ͣ������
		/// </summary>
		public QueryConditionField cndEDate = new QueryConditionField("cndEDate");

		/// <summary>
		/// ������
		/// </summary>
		public QueryConditionField cnvcCreatePerson = new QueryConditionField("cnvcCreatePerson");

		/// <summary>
		/// �����
		/// </summary>
		public QueryConditionField cnvcModifyPerson = new QueryConditionField("cnvcModifyPerson");

		/// <summary>
		/// �������
		/// </summary>
		public QueryConditionField cndModifyDate = new QueryConditionField("cndModifyDate");

		/// <summary>
		/// �Ƽ۷�ʽ
		/// </summary>
		public QueryConditionField cnvcValueType = new QueryConditionField("cnvcValueType");

		/// <summary>
		/// ������λ�����
		/// </summary>
		public QueryConditionField cnvcGroupCode = new QueryConditionField("cnvcGroupCode");

		/// <summary>
		/// ��������λ����
		/// </summary>
		public QueryConditionField cnvcComUnitCode = new QueryConditionField("cnvcComUnitCode");

		/// <summary>
		/// ����Ĭ�ϼ�����λ
		/// </summary>
		public QueryConditionField cnvcSAComUnitCode = new QueryConditionField("cnvcSAComUnitCode");

		/// <summary>
		/// �ɹ�Ĭ�ϼ�����λ
		/// </summary>
		public QueryConditionField cnvcPUComUnitCode = new QueryConditionField("cnvcPUComUnitCode");

		/// <summary>
		/// ���Ĭ�ϼ�����λ
		/// </summary>
		public QueryConditionField cnvcSTComUnitCode = new QueryConditionField("cnvcSTComUnitCode");

		/// <summary>
		/// ����������λ
		/// </summary>
		public QueryConditionField cnvcProduceUnitCode = new QueryConditionField("cnvcProduceUnitCode");

		/// <summary>
		/// ���ۼ۸�
		/// </summary>
		public QueryConditionField cnfRetailPrice = new QueryConditionField("cnfRetailPrice");

		/// <summary>
		/// ���ۼ�����λ
		/// </summary>
		public QueryConditionField cnvcShopUnitCode = new QueryConditionField("cnvcShopUnitCode");

		/// <summary>
		/// �ڸ�
		/// </summary>
		public QueryConditionField cnvcFeel = new QueryConditionField("cnvcFeel");

		/// <summary>
		/// ��֯
		/// </summary>
		public QueryConditionField cnvcOrganise = new QueryConditionField("cnvcOrganise");

		/// <summary>
		/// ��ɫ
		/// </summary>
		public QueryConditionField cnvcColor = new QueryConditionField("cnvcColor");

		/// <summary>
		/// ��ζ
		/// </summary>
		public QueryConditionField cnvcTaste = new QueryConditionField("cnvcTaste");
	}	
}
