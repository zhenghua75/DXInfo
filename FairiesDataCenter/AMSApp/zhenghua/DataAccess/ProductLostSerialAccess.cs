
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	ProductLostSerialAccess.cs
* ����:	     ֣��
* ��������:    2009-7-24
* ��������:    ����������ˮ��

*                                                           Copyright(C) 2009 zhenghua
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
	/// **�������ƣ�����������ˮ�����ݷ�����
	/// </summary>
	public class ProductLostSerialAccess
	{
		// ����д��Ĵ���



		#region ���������ɴ���



		/// <summary>
		/// ȡ��ǰ��ѯ����ʵ��


		/// </summary>
		/// <returns>��ѯ����</returns>
		private static ProductLostSerialArgs GetCurrentArgs()
		{
			return new ProductLostSerialArgs();
		}
		
		#endregion
	}
}

