
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	StorageLogAccess.cs
* ����:	     ֣��
* ��������:    2008-10-20
* ��������:    �����־��

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
	/// **�������ƣ������־�����ݷ�����
	/// </summary>
	public class StorageLogAccess
	{
		// ����д��Ĵ���



		#region ���������ɴ���



		/// <summary>
		/// ȡ��ǰ��ѯ����ʵ��


		/// </summary>
		/// <returns>��ѯ����</returns>
		private static StorageLogArgs GetCurrentArgs()
		{
			return new StorageLogArgs();
		}
		
		#endregion
	}
}

