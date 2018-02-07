using System;
using System.Text;

namespace CommCenter
{
	/// <summary>
	/// Summary description for SructToString.
	/// </summary>
	public class StructToString
	{
		public StructToString()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public string ToAssString(CMSMStruct.AssociatorStruct asstmp)
		{
			/*
			iAssID--DU001
			vcCardID--DU002
			vcAssName--DU003
			vcSpell--DU004
			vcAssNbr--DU005
			vcLinkPhone--DU006
			vcLinkAddress--DU007
			vcEmail--DU008
			vcAssType--DU009
			vcAssState--DU010
			nCharge--DU011
			iIgValue--DU012
			vcCardFlag--DU013
			vcComments--DU014
			dtCreateDate--DU015
			dtOperDate--DU016
			vcDeptID--DU017
			DU018--数据类型：1--会员资料;2--消费明细;3--小票数据;4--积分日志;5--充值日志;6--营业日志
			*/
			StringBuilder sb = new StringBuilder(1024);
			sb.Append(asstmp.strAssID);
			sb.Append(",");
			sb.Append(asstmp.strCardID);
			sb.Append(",");
			sb.Append(asstmp.strAssName);
			sb.Append(",");
			sb.Append(asstmp.strSpell);
			sb.Append(",");
			sb.Append(asstmp.strAssNbr);
			sb.Append(",");
			sb.Append(asstmp.strLinkPhone);
			sb.Append(",");
			sb.Append(asstmp.strLinkAddress);
			sb.Append(",");
			sb.Append(asstmp.strEmail);
			sb.Append(",");
			sb.Append(asstmp.strAssType);
			sb.Append(",");
			sb.Append(asstmp.strAssState);
			sb.Append(",");
			sb.Append(asstmp.dCharge.ToString());
			sb.Append(",");
			sb.Append(asstmp.iIgValue.ToString());
			sb.Append(",");
			sb.Append(asstmp.strCardFlag);
			sb.Append(",");
			sb.Append(asstmp.strComments);
			sb.Append(",");
			sb.Append(asstmp.strCreateDate);
			sb.Append(",");
			sb.Append(asstmp.strOperDate);
			sb.Append(",");
			sb.Append(asstmp.strDeptID);
//			sb.Append(",1");
			return sb.ToString();
		}

		public string ToAssChangeString(CMSMStruct.AssChangeStruct asstmp)
		{
			StringBuilder sb = new StringBuilder(1024);
			sb.Append(asstmp.strCardID);
			sb.Append(",");
			sb.Append(asstmp.strChangeField);
			sb.Append(",");
			sb.Append(asstmp.strChangeValue);
			sb.Append(",");
			sb.Append(asstmp.strOperDate);
			return sb.ToString();
		}

		public string ToCommCodeString(CMSMStruct.CommStruct comtmp)
		{
			StringBuilder sb = new StringBuilder(1024);
			sb.Append(comtmp.strCommName);
			sb.Append(",");
			sb.Append(comtmp.strCommCode);
			sb.Append(",");
			sb.Append(comtmp.strCommSign);
			sb.Append(",");
			sb.Append(comtmp.strComments);
			return sb.ToString();
		}

		public string ToGoodsString(CMSMStruct.GoodsStruct asstmp)
		{
			StringBuilder sb = new StringBuilder(1024);
			sb.Append(asstmp.strGoodsID);
			sb.Append(",");
			sb.Append(asstmp.strGoodsName);
			sb.Append(",");
			sb.Append(asstmp.strSpell);
			sb.Append(",");
			sb.Append(asstmp.dPrice);
			sb.Append(",");
			sb.Append(asstmp.dRate);
			sb.Append(",");
			sb.Append(asstmp.iIgValue);
			sb.Append(",");
			sb.Append(asstmp.strNewFlag);
			sb.Append(",");
			sb.Append(asstmp.strComments);
			return sb.ToString();
		}

		public string ToNoticeString(CMSMStruct.NoticeStruct notitmp)
		{
			StringBuilder sb = new StringBuilder(1024);
			sb.Append(notitmp.strid);
			sb.Append(",");
			sb.Append(notitmp.strComments);
			sb.Append(",");
			sb.Append(notitmp.strCreateDate);
			sb.Append(",");
			sb.Append(notitmp.strActiveFlag);
			sb.Append(",");
			sb.Append(notitmp.strDeptFlag);
			return sb.ToString();
		}

		public string ToConsString(CMSMStruct.ConsDownStruct asstmp)
		{
			/*
			 iSerial--DU001
			 vcGoodsID--DU002
			 iAssID--DU003
			 vcCardID--DU004
			 nPrice--DU005
			 iCount--DU006
			 nTRate--DU007
			 nFee--DU008
			 vcComments--DU009
			 cFlag--DU010
			 dtConsDate--DU011
			 vcOperName--DU012
			 vcDeptID--DU013
			 DU018--数据类型：1--会员资料;2--消费明细;3--小票数据;4--积分日志;5--充值日志;6--营业日志
			 */
			StringBuilder sb = new StringBuilder(1024);
			sb.Append(asstmp.strSerial);
			sb.Append(",");
			sb.Append(asstmp.strGoodsID);
			sb.Append(",");
			sb.Append(asstmp.strAssID);
			sb.Append(",");
			sb.Append(asstmp.strCardID);
			sb.Append(",");
			sb.Append(asstmp.dPrice.ToString());
			sb.Append(",");
			sb.Append(asstmp.iCount.ToString());
			sb.Append(",");
			sb.Append(asstmp.dTRate.ToString());
			sb.Append(",");
			sb.Append(asstmp.dFee.ToString());
			sb.Append(",");
			sb.Append(asstmp.strComments);
			sb.Append(",");
			sb.Append(asstmp.strFlag);
			sb.Append(",");
			sb.Append(asstmp.strConsDate);
			sb.Append(",");
			sb.Append(asstmp.strOperName);
			sb.Append(",");
			sb.Append(asstmp.strDeptID);
//			sb.Append(",null,null,null,null,2");
			return sb.ToString();
		}

		public string ToBillString(CMSMStruct.BillStruct asstmp)
		{
			/*
			 iSerial--DU001
			 iAssID--DU002
			 vcCardID--DU003
			 nTRate--DU004
			 nFee--DU005
			 nPay--DU006
			 nBalance--DU007
			 iIgValue--DU008
			 vcConsType--DU009
			 vcOperName--DU010
			 dtConsDate--DU011
			 vcDeptID--DU012
			 DU018--数据类型：1--会员资料;2--消费明细;3--小票数据;4--积分日志;5--充值日志;6--营业日志
			 */
			StringBuilder sb = new StringBuilder(1024);
			sb.Append(asstmp.strSerial);
			sb.Append(",");
			sb.Append(asstmp.strAssID);
			sb.Append(",");
			sb.Append(asstmp.strCardID);
			sb.Append(",");
			sb.Append(asstmp.dTRate.ToString());
			sb.Append(",");
			sb.Append(asstmp.dFee.ToString());
			sb.Append(",");
			sb.Append(asstmp.dPay.ToString());
			sb.Append(",");
			sb.Append(asstmp.dBalance.ToString());
			sb.Append(",");
			sb.Append(asstmp.iIgValue.ToString());
			sb.Append(",");
			sb.Append(asstmp.strConsType);
			sb.Append(",");
			sb.Append(asstmp.strOperName);
			sb.Append(",");
			sb.Append(asstmp.strConsDate);
			sb.Append(",");
			sb.Append(asstmp.strDeptID);
//			sb.Append(",null,null,null,null,null,3");
			return sb.ToString();
		}

		public string ToIntegralString(CMSMStruct.IntegralStruct asstmp)
		{
			/*
			 iSerial--DU001
			 iAssID--DU002
			 vcCardID--DU003
			 vcIgType--DU004
			 iIgLast--DU005
			 iIgGet--DU006
			 iIgArrival--DU007
			 iLinkCons--DU008
			 dtIgDate--DU009
			 vcOperName--DU010
			 vcComments--DU011
			 vcDeptID--DU012
			 DU018--数据类型：1--会员资料;2--消费明细;3--小票数据;4--积分日志;5--充值日志;6--营业日志
			 */
			StringBuilder sb = new StringBuilder(1024);
			sb.Append(asstmp.strSerial);
			sb.Append(",");
			sb.Append(asstmp.strAssID);
			sb.Append(",");
			sb.Append(asstmp.strCardID);
			sb.Append(",");
			sb.Append(asstmp.strIgType);
			sb.Append(",");
			sb.Append(asstmp.iIgLast.ToString());
			sb.Append(",");
			sb.Append(asstmp.iIgGet.ToString());
			sb.Append(",");
			sb.Append(asstmp.iIgArrival.ToString());
			sb.Append(",");
			sb.Append(asstmp.iLinkCons.ToString());
			sb.Append(",");
			sb.Append(asstmp.strIgDate);
			sb.Append(",");
			sb.Append(asstmp.strOperName);
			sb.Append(",");
			sb.Append(asstmp.strComments);
			sb.Append(",");
			sb.Append(asstmp.strDeptID);
//			sb.Append(",null,null,null,null,null,4");
			return sb.ToString();
		}

		public string ToFillString(CMSMStruct.FillFeeStruct asstmp)
		{
			/*
			 iSerial--DU001
			 iAssID--DU002
			 vcCardID--DU003
			 nFillFee--DU004
			 nFillProm--DU005
			 nFeeLast--DU006
			 nFeeCur--DU007
			 dtFillDate--DU008
			 vcComments--DU009
			 vcOperName--DU010
			 vcDeptID--DU011
			 DU018--数据类型：1--会员资料;2--消费明细;3--小票数据;4--积分日志;5--充值日志;6--营业日志
			 */
			StringBuilder sb = new StringBuilder(1024);
			sb.Append(asstmp.strSerial);
			sb.Append(",");
			sb.Append(asstmp.strAssID);
			sb.Append(",");
			sb.Append(asstmp.strCardID);
			sb.Append(",");
			sb.Append(asstmp.dFillFee.ToString());
			sb.Append(",");
			sb.Append(asstmp.dFillProm.ToString());
			sb.Append(",");
			sb.Append(asstmp.dFeeLast.ToString());
			sb.Append(",");
			sb.Append(asstmp.dFeeCur.ToString());
			sb.Append(",");
			sb.Append(asstmp.strFillDate);
			sb.Append(",");
			sb.Append(asstmp.strComments);
			sb.Append(",");
			sb.Append(asstmp.strOperName);
			sb.Append(",");
			sb.Append(asstmp.strDeptID);
//			sb.Append(",null,null,null,null,null,null,5");
			return sb.ToString();
		}

		public string ToBusiLogString(CMSMStruct.BusiLogStruct asstmp)
		{
			/*
			 iSerial--DU001
			 iAssID--DU002
			 vcCardID--DU003
			 vcOperType--DU004
			 vcOperName--DU005
			 dtOperDate--DU006
			 vcComments--DU007
			 vcDeptID--DU008
			 DU018--数据类型：1--会员资料;2--消费明细;3--小票数据;4--积分日志;5--充值日志;6--营业日志
			 */
			StringBuilder sb = new StringBuilder(1024);
			sb.Append(asstmp.strSerial);
			sb.Append(",");
			sb.Append(asstmp.strAssID);
			sb.Append(",");
			sb.Append(asstmp.strCardID);
			sb.Append(",");
			sb.Append(asstmp.strOperType);
			sb.Append(",");
			sb.Append(asstmp.strOperName);
			sb.Append(",");
			sb.Append(asstmp.strOperDate);
			sb.Append(",");
			sb.Append(asstmp.strComments);
			sb.Append(",");
			sb.Append(asstmp.strDeptID);
//			sb.Append(",null,null,null,null,null,null,null,null,null,6");
			return sb.ToString();
		}

		public bool UserParseLine(string strValue,string strSplit,out CMSMStruct.AssociatorStruct asstmp,out Exception err)
		{
			int i = 0;
			asstmp=new CMSMStruct.AssociatorStruct();
			err=null;
			try
			{
				string[] strFields = strValue.Split(strSplit.ToCharArray());
				asstmp.strAssID = strFields[i++];
				asstmp.strCardID = strFields[i++];
				asstmp.strAssName = strFields[i++];
				asstmp.strSpell = strFields[i++];
				asstmp.strAssNbr = strFields[i++];
				asstmp.strLinkPhone = strFields[i++];
				asstmp.strLinkAddress = strFields[i++];
				asstmp.strEmail = strFields[i++];
				asstmp.strAssType = strFields[i++];
				asstmp.strAssState = strFields[i++];
				asstmp.dCharge = double.Parse(strFields[i++]);
				asstmp.iIgValue = int.Parse(strFields[i++]);
				asstmp.strCardFlag = strFields[i++];
				asstmp.strComments = strFields[i++];
				asstmp.strCreateDate = strFields[i++];
				asstmp.strOperDate = strFields[i++];
				asstmp.strDeptID = strFields[i++];
			}
			catch(Exception e)
			{
				err=e;
				return false;
			}
			return true;
		}

		public bool CommParseLine(string strValue,string strSplit,out CMSMStruct.CommStruct comtmp,out Exception err)
		{
			int i = 0;
			comtmp=new CMSMStruct.CommStruct();
			err=null;
			try
			{
				string[] strFields = strValue.Split(strSplit.ToCharArray());
				comtmp.strCommName = strFields[i++];
				comtmp.strCommCode = strFields[i++];
				comtmp.strCommSign = strFields[i++];
				comtmp.strComments = strFields[i++];
			}
			catch(Exception e)
			{
				err=e;
				return false;
			}
			return true;
		}

		public bool UserAlterParseLine(string strValue,string strSplit,out CMSMStruct.AssChangeStruct asschange,out Exception err)
		{
			int i = 0;
			asschange=new CMSMStruct.AssChangeStruct();
			err=null;
			try
			{
				string[] strFields = strValue.Split(strSplit.ToCharArray());
				asschange.strCardID = strFields[i++];
				asschange.strChangeField = strFields[i++];
				asschange.strChangeValue = strFields[i++];
				asschange.strOperDate = strFields[i++];
			}
			catch(Exception e)
			{
				err=e;
				return false;
			}
			return true;
		}

		public bool GoodsParseLine(string strValue,string strSplit,out CMSMStruct.GoodsStruct asstmp,out Exception err)
		{
			int i = 0;
			asstmp=new CMSMStruct.GoodsStruct();
			err=null;
			try
			{
				string[] strFields = strValue.Split(strSplit.ToCharArray());
				asstmp.strGoodsID = strFields[i++];
				asstmp.strGoodsName = strFields[i++];
				asstmp.strSpell = strFields[i++];
				asstmp.dPrice = double.Parse(strFields[i++]);
				asstmp.dRate = double.Parse(strFields[i++]);
				asstmp.iIgValue = int.Parse(strFields[i++]);
				asstmp.strNewFlag = strFields[i++];
				asstmp.strComments = strFields[i++];
			}
			catch(Exception e)
			{
				err=e;
				return false;
			}
			return true;
		}

		public bool ConsParseLine(string strValue,string strSplit,out CMSMStruct.ConsDownStruct consd,out Exception err)
		{
			int i = 0;
			consd=new CMSMStruct.ConsDownStruct();
			err=null;
			try
			{
				string[] strFields = strValue.Split(strSplit.ToCharArray());
				consd.strSerial = strFields[i++];
				consd.strGoodsID = strFields[i++];
				consd.strAssID = strFields[i++];
				consd.strCardID = strFields[i++];
				consd.dPrice = double.Parse(strFields[i++]);
				consd.iCount = int.Parse(strFields[i++]);
				consd.dTRate = double.Parse(strFields[i++]);
				consd.dFee = double.Parse(strFields[i++]);
				consd.strComments = strFields[i++];
				consd.strFlag = strFields[i++];
				consd.strConsDate = strFields[i++];
				consd.strOperName = strFields[i++];
				consd.strDeptID = strFields[i++];
			}
			catch(Exception e)
			{
				err=e;
				return false;
			}
			return true;
		}

		public bool BillParseLine(string strValue,string strSplit,out CMSMStruct.BillStruct bis,out Exception err)
		{
			int i = 0;
			bis=new CMSMStruct.BillStruct();
			err=null;
			try
			{
				string[] strFields = strValue.Split(strSplit.ToCharArray());
				bis.strSerial = strFields[i++];
				bis.strAssID = strFields[i++];
				bis.strCardID = strFields[i++];
				bis.dTRate = double.Parse(strFields[i++]);
				bis.dFee = double.Parse(strFields[i++]);
				bis.dPay = double.Parse(strFields[i++]);
				bis.dBalance = double.Parse(strFields[i++]);
				bis.iIgValue = int.Parse(strFields[i++]);
				bis.strConsType = strFields[i++];
				bis.strOperName = strFields[i++];
				bis.strConsDate = strFields[i++];
				bis.strDeptID = strFields[i++];
			}
			catch(Exception e)
			{
				err=e;
				return false;
			}
			return true;
		}

		public bool IntegralParseLine(string strValue,string strSplit,out CMSMStruct.IntegralStruct its,out Exception err)
		{
			int i = 0;
			its=new CMSMStruct.IntegralStruct();
			err=null;
			try
			{
				string[] strFields = strValue.Split(strSplit.ToCharArray());
				its.strSerial = strFields[i++];
				its.strAssID = strFields[i++];
				its.strCardID = strFields[i++];
				its.strIgType = strFields[i++];
				its.iIgLast = int.Parse(strFields[i++]);
				its.iIgGet = int.Parse(strFields[i++]);
				its.iIgArrival = int.Parse(strFields[i++]);
				its.iLinkCons = int.Parse(strFields[i++]);
				its.strIgDate = strFields[i++];
				its.strOperName = strFields[i++];
				its.strComments = strFields[i++];
				its.strDeptID = strFields[i++];
			}
			catch(Exception e)
			{
				err=e;
				return false;
			}
			return true;
		}

		public bool FillParseLine(string strValue,string strSplit,out CMSMStruct.FillFeeStruct ffs,out Exception err)
		{
			int i = 0;
			ffs=new CMSMStruct.FillFeeStruct();
			err=null;
			try
			{
				string[] strFields = strValue.Split(strSplit.ToCharArray());
				ffs.strSerial = strFields[i++];
				ffs.strAssID = strFields[i++];
				ffs.strCardID = strFields[i++];
				ffs.dFillFee = double.Parse(strFields[i++]);
				ffs.dFillProm = double.Parse(strFields[i++]);
				ffs.dFeeLast = double.Parse(strFields[i++]);
				ffs.dFeeCur = double.Parse(strFields[i++]);
				ffs.strFillDate = strFields[i++];
				ffs.strComments = strFields[i++];
				ffs.strOperName = strFields[i++];
				ffs.strDeptID = strFields[i++];
			}
			catch(Exception e)
			{
				err=e;
				return false;
			}
			return true;
		}

		public bool BusiLogParseLine(string strValue,string strSplit,out CMSMStruct.BusiLogStruct blogs,out Exception err)
		{
			int i = 0;
			blogs=new CMSMStruct.BusiLogStruct();
			err=null;
			try
			{
				string[] strFields = strValue.Split(strSplit.ToCharArray());
				blogs.strSerial = strFields[i++];
				blogs.strAssID = strFields[i++];
				blogs.strCardID = strFields[i++];
				blogs.strOperType = strFields[i++];
				blogs.strOperName = strFields[i++];
				blogs.strOperDate = strFields[i++];
				blogs.strComments = strFields[i++];
				blogs.strDeptID = strFields[i++];
			}
			catch(Exception e)
			{
				err=e;
				return false;
			}
			return true;
		}
	}
}
