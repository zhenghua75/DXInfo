
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	CheckSerialLogAccess.cs
* ����:	     ֣��
* ��������:    2009-7-25
* ��������:    �̵������ˮ����־

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
	/// **�������ƣ��̵������ˮ����־���ݷ�����
	/// </summary>
	public class CheckSerialLogAccess
	{
		// ����д��Ĵ���



		#region ���������ɴ���



		/// <summary>
		/// ȡ��ǰ��ѯ����ʵ��


		/// </summary>
		/// <returns>��ѯ����</returns>
		private static CheckSerialLogArgs GetCurrentArgs()
		{
			return new CheckSerialLogArgs();
		}
		
		#endregion
	}
}

