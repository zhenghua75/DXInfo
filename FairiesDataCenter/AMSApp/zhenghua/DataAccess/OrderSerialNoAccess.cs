
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	OrderSerialNoAccess.cs
* ����:	     ֣��
* ��������:    2008-10-4
* ��������:    ������ˮ���ɱ�

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ImportNameSpace
using System;
using System.Data;
using System.Data.SqlClient;

using AMSApp.zhenghua.EntityBase;
using AMSApp.zhenghua.Entity;
using AMSApp.zhenghua.QueryArgs;
using AMSApp.zhenghua.Common;
#endregion

namespace AMSApp.zhenghua.DataAccess
{
	/// <summary>
	/// **�������ƣ�������ˮ���ɱ����ݷ�����
	/// </summary>
	public class OrderSerialNoAccess
	{
		// ����д��Ĵ���



		#region ���������ɴ���



		/// <summary>
		/// ȡ��ǰ��ѯ����ʵ��


		/// </summary>
		/// <returns>��ѯ����</returns>
		private static OrderSerialNoArgs GetCurrentArgs()
		{
			return new OrderSerialNoArgs();
		}
		
		#endregion
	}
}

