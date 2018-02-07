using System;
using DataAccess;
using System.Data;
using CommCenter;
using System.Collections;
using System.Web.Security;
using ynhnTransportManage.Models;

using System.Linq;
using ynhnTransportManage;
using Ninject;
using DXInfo.Data.Contracts;
using System.Web.Mvc;
namespace BusiComm
{
	/// <summary>
	/// Summary description for Manager.
	/// </summary>
	public class Manager
	{
        //[Inject]
        //public IFairiesMemberManageUow Uow { get; set; }
        //[Inject]
        //public IAMSCMUow AmscmUow { get; set; }
		string strcon="";
		OperAcc opa=null;
		public Manager(string strcons)
		{
			//
			// TODO: Add constructor logic here
			//
			strcon=strcons;
			opa=new OperAcc(strcon);
		}

        public CMSMStruct.LoginStruct GetLoginInfo(string strLoginid)
        {
            DataTable dtout = opa.GetLoginInfo(strLoginid);
            CMSMStruct.LoginStruct ls1 = null;
            if (dtout != null && dtout.Rows.Count > 0)
            {
                ls1 = new CommCenter.CMSMStruct.LoginStruct();

                ls1.strLoginID = dtout.Rows[0]["vcLoginID"].ToString();
                ls1.strOperName = dtout.Rows[0]["vcOperName"].ToString();
                ls1.strLimit = dtout.Rows[0]["vcLimit"].ToString();
                ls1.strPwd = dtout.Rows[0]["vcPwd"].ToString();
                ls1.strDeptID = dtout.Rows[0]["vcDeptID"].ToString();
            }
            return ls1;
        }

		public DataTable GetGoods(string strGoodsid,string strGoodsName)
		{
			DataTable dtout=opa.GetGoods(strGoodsid,strGoodsName);
			if(dtout!=null)
			{
				dtout.Columns["vcGoodsID"].ColumnName="商品编号";
				dtout.Columns["vcGoodsName"].ColumnName="商品名称";
				dtout.Columns["vcSpell"].ColumnName="拼音简写";
				dtout.Columns["nPrice"].ColumnName="单价";
				dtout.Columns["iIgValue"].ColumnName="兑换分值";
				dtout.Columns["vcComments"].ColumnName="备注";
				dtout.Columns.Add("操作");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					dtout.Rows[i]["操作"]="<a href='wfmGoodsDetail.aspx?id=" + dtout.Rows[i]["商品编号"].ToString() + "'>编辑</a>";
				}
			}
			return dtout;
		}

		public bool InsertOperLog(CMSMStruct.OperStruct OperNew)
		{
			int recount=opa.InsertOperLog(OperNew);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public DataTable GetGoods(string strGoodsid,string strGoodsName,string strSpell)
		{
			DataTable dtout=opa.GetGoods(strGoodsid,strGoodsName,strSpell);
			if(dtout!=null)
			{
				dtout.Columns["vcGoodsID"].ColumnName="商品编号";
				dtout.Columns["vcGoodsName"].ColumnName="商品名称";
				dtout.Columns["vcSpell"].ColumnName="拼音简写";
				dtout.Columns["nPrice"].ColumnName="单价";
				dtout.Columns["iIgValue"].ColumnName="兑换分值";
				dtout.Columns["vcComments"].ColumnName="备注";
				dtout.Columns.Add("操作");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					dtout.Rows[i]["操作"]="<a href='wfmGoodsDetail.aspx?id=" + dtout.Rows[i]["商品编号"].ToString() + "'>编辑</a>";
				}
			}
			return dtout;
		}

		public bool ChkGoodsIDDup(string strGoodsID)
		{
			string strid=opa.getGoodsID(strGoodsID);
			if(strid!="0")
			{
				return false;
			}

			return true;
		}

		public string GetGoodsMaxID(string strmask)
		{
			string strid=opa.getMaxGoodsID(strmask);

			return strid;
		}

		public bool ChkGoodsNameDup(string strGoodsName)
		{
			string strname=opa.getGoodsName(strGoodsName);
			if(strname!="0")
			{
				return false;
			}

			return true;
		}

		public bool ChkNewGoodsNameDup(string strnewGoodsName,string strGoodsID)
		{
			string strname=opa.getGoodsNamebyID(strnewGoodsName,strGoodsID);
			if(strname!="0")
			{
				return false;
			}

			return true;
		}

		public bool InsertGoods(CMSMStruct.GoodsStruct gs)
		{
			int recount=opa.InsertGoods(gs);
			if(recount<=0)
			{
				return false;
			}
            //zhh 20121104
            var uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
            var amsuow = DependencyResolver.Current.GetService<IAMSCMUow>();
            DXInfo.Business.Common businessCommon = new DXInfo.Business.Common(uow);
            businessCommon.SyncGoods(amsuow);
			return true;
		}
        public bool InsertGoods(CMSMStruct.GoodsStruct gs, string strGoodsType)
        {
            int recount = opa.InsertGoods(gs, strGoodsType);
            if (recount <= 0)
            {
                return false;
            }
            //zhh 20121104
            //Helpers.SyncGoods();
            return true;
        }
        public bool InsertGoods(CMSMStruct.GoodsStruct gs, string strGoodsType,out string strGoodsId)
        {
            int recount = opa.InsertGoods(gs, strGoodsType,out strGoodsId);
            if (recount <= 0)
            {
                return false;
            }
            return true;
        }
		public CMSMStruct.GoodsStruct GetGoodsInfo(string strGoodsid)
		{
			DataTable dtout=opa.GetGoodsInfo(strGoodsid);
			CMSMStruct.GoodsStruct gs=new CommCenter.CMSMStruct.GoodsStruct();
			if(dtout!=null)
			{
				gs.strGoodsID=dtout.Rows[0]["vcGoodsID"].ToString();
				gs.strGoodsName=dtout.Rows[0]["vcGoodsName"].ToString();
				gs.strSpell=dtout.Rows[0]["vcSpell"].ToString();
				gs.dPrice=double.Parse(dtout.Rows[0]["nPrice"].ToString());
				gs.iIgValue=int.Parse(dtout.Rows[0]["iIgValue"].ToString());
				gs.strComments=dtout.Rows[0]["vcComments"].ToString();
			}
			return gs;
		}

		public bool UpdateGoods(CMSMStruct.GoodsStruct gsnew,CMSMStruct.GoodsStruct gsold)
		{
			string sqlset="";
			if(gsnew.strGoodsName!=gsold.strGoodsName)
			{
				sqlset+="vcGoodsName='" + gsnew.strGoodsName + "',";
			}
			if(gsnew.strSpell!=gsold.strSpell)
			{
				sqlset+="vcSpell='" + gsnew.strSpell + "',";
			}
			if(gsnew.dPrice!=gsold.dPrice)
			{
				sqlset+="nPrice=" + gsnew.dPrice.ToString() + ",";
			}
			if(gsnew.iIgValue!=gsold.iIgValue)
			{
				sqlset+="iIgValue=" + gsnew.iIgValue.ToString() + ",";
			}
			if(gsnew.strComments!=gsold.strComments)
			{
				sqlset+="vcComments='" + gsnew.strComments + "',";
			}

			if(sqlset!="")
			{
				sqlset=sqlset.Substring(0,sqlset.Length-1);
				int recount=opa.UpdateGoods(gsnew.strGoodsID,sqlset);
				if(recount<=0)
				{
					return false;
				}
			}
			return true;
		}

		public string getGoodsSerial(string strCreateDate)
		{
			string strserial=opa.getGoodsSerial(strCreateDate);
			return strserial;
		}

		public string getNotiSerial(string strCreateDate)
		{
			string strserial=opa.getNotiSerial(strCreateDate);
			return strserial;
		}

		public string getAssSerial(string strCreateDate)
		{
			string strserial=opa.getAssSerial(strCreateDate);
			return strserial;
		}

		public bool writeDataLog(string strsqlset)
		{
			int recount=opa.InsertDataLog(strsqlset);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public ArrayList DownSysPara()
		{
			return opa.DownSysPara();
		}

		public ArrayList DownGoodsData()
		{
			return opa.DownGoodsData();
		}

		public ArrayList DownNotice(string strid)
		{
			return opa.DownNotice(strid);
		}

		public ArrayList DownAssData(string strBeginDate)
		{
			return opa.DownAssData(strBeginDate);
		}

		public DataTable GetLoginOper(Hashtable htpara)
		{
			DataTable dtout=opa.GetLoginOper(htpara);
			if(dtout!=null)
			{
				dtout.Columns["vcLoginID"].ColumnName="登录ID";
				dtout.Columns["vcOperName"].ColumnName="操作员名称";
				dtout.Columns["vcLimit"].ColumnName="查看权限";
				dtout.Columns["vcDeptID"].ColumnName="门店";
				dtout.Columns.Add("功能权限");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					dtout.Rows[i]["功能权限"]="<a href='wfmOperPurview.aspx?FuncType=BS&id=" + dtout.Rows[i]["登录ID"].ToString() + "&name="+dtout.Rows[i]["操作员名称"].ToString()+"'>修改权限</a>";
				}
				dtout.Columns.Add("操作");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					dtout.Rows[i]["操作"]="<a href='wfmOperDetail.aspx?id=" + dtout.Rows[i]["登录ID"].ToString() + "'>编辑</a>";
				}
			}
			return dtout;
		}

		public bool ChkLoginIDDup(string strLoginID)
		{
			string strid=opa.getLoginID(strLoginID);
			if(strid!="0")
			{
				return false;
			}

			return true;
		}

		public int getLoginIDcount(string strLoginID)
		{
			string strid=opa.getLoginID(strLoginID);
			return int.Parse(strid);
		}

		public bool ChkOperNameDup(string strOperID,string strOperName)
		{
			string strname=opa.getOperName(strOperID,strOperName);
			if(strname!="0")
			{
				return false;
			}

			return true;
		}

		public bool InsertLogin(CMSMStruct.LoginStruct lsnew)
		{
			int recount=opa.InsertLogin(lsnew);
			if(recount<=0)
			{
				return false;
			}
            //zhh 20120912
            var uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
            var amsuow = DependencyResolver.Current.GetService<IAMSCMUow>();
            Common centerCommon = new Common(uow);
            centerCommon.SyncOper(amsuow);
			return true;
		}

		public bool UpdateLogin(CMSMStruct.LoginStruct lsnew,CMSMStruct.LoginStruct lsold)
		{
			string sqlset="";
			if(lsnew.strOperName!=lsold.strOperName)
			{
				sqlset+="vcOperName='" + lsnew.strOperName + "',";
			}
			if(lsnew.strLimit!=lsold.strLimit)
			{
				sqlset+="vcLimit='" + lsnew.strLimit + "',";
			}
			if(lsnew.strDeptID!=lsold.strDeptID)
			{
				sqlset+="vcDeptID='" + lsnew.strDeptID + "',";
			}

			if(sqlset!="")
			{
				sqlset=sqlset.Substring(0,sqlset.Length-1);
				int recount=opa.UpdateLogin(lsold.strLoginID,sqlset);
				if(recount<=0)
				{
					return false;
				}
			}
            //zhh 20120912
            var uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
            var amsuow = DependencyResolver.Current.GetService<IAMSCMUow>();
            Common centerCommon = new Common(uow);
            centerCommon.UpdateOper(lsold.strLoginID, lsnew.strOperName, lsnew.strDeptID);
			return true;
		}

		public bool DeleteLogin(string strLoginID)
		{
			int recount=opa.DeleteLogin(strLoginID);
			if(recount<=0)
			{
				return false;
			}
            //zhh 20120912
            var uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
            Common centerCommon = new Common(uow);
            centerCommon.DeleteOper(strLoginID);
			return true;
		}

		public DataTable GetNotice(Hashtable htpara)
		{
			DataTable dtout=opa.GetNotice(htpara);
			if(dtout!=null)
			{
				dtout.Columns["id"].ColumnName="通知编号";
				dtout.Columns["vcComments"].ColumnName="通知内容";
				dtout.Columns["dtCreateDate"].ColumnName="创建时间";
				dtout.Columns["vcActiveFlag"].ColumnName="发送标志";
				dtout.Columns["vcDeptFlag"].ColumnName="发往门店";
				dtout.Columns.Add("操作");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					if(dtout.Rows[i]["发送标志"].ToString()=="0")
					{
						dtout.Rows[i]["操作"]="<a href='wfmNoticeDetail.aspx?id=" + dtout.Rows[i]["通知编号"].ToString() + "'>发送</a>";
					}
					else
					{
						dtout.Rows[i]["操作"]="";
					}
				}
			}
			return dtout;
		}

		public bool InsertNotice(string strDept,string strContent)
		{
			int recount=opa.InsertNotice(strDept,strContent);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public bool UpdateNotice(string strid)
		{
			int recount=opa.UpdateNotice(strid);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public string getNoticeActiveFlag(string strid)
		{
			string strreturnid=opa.getNoticeActiveFlag(strid);
			return strreturnid;
		}

		public bool UpdateOperPwd(string strid,string strpwd)
		{
			int recount=opa.UpdateOperPwd(strid,strpwd);
			if(recount<=0)
			{
				return false;
			}
            var uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
            Common centerCommon = new Common(uow);
            centerCommon.UpdatePwd(strid, strpwd);
			return true;
		}

		public DataTable GetAllGoodsName()
		{
			DataTable dtout=opa.GetAllGoodsName();
			return dtout;
		}

		public DataTable GetIgPara()
		{
			DataTable dtout=opa.GetIgPara();
			return dtout;
		}

		public Hashtable GetPromRate()
		{
			DataTable dtout=opa.GetPromRate();
			Hashtable htPromRate=new Hashtable();
			if(dtout!=null)
			{
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					htPromRate.Add(dtout.Rows[i]["vcCommSign"].ToString(),dtout.Rows[i]["vcCommCode"].ToString());
				}
			}
			return htPromRate;
		}
        public string GetSPF()
        {
            return opa.GetSPF();
        }
        public bool UpdateSPF(string strSPF)
        {
            int recount = opa.UpdateSPF(strSPF);
            if (recount <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public Hashtable GetCP()
        {
            DataTable dtout = opa.GetCP();
            Hashtable htCP = new Hashtable();
            string strimg = "";
            string strcompanyName = "";
            if (dtout != null && dtout.Rows.Count>0)
            {
                strimg = dtout.Rows[0]["vcCommCode"].ToString();
                strcompanyName = dtout.Rows[0]["vcCommName"].ToString();
            }
            htCP.Add("strimg", strimg);
            htCP.Add("strcompanyName", strcompanyName);
            return htCP;
        }
        public bool UpdateCP_Img(string strimg)
        {
            int recount = opa.UpdateCP_Img(strimg);
            if (recount <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool UpdateCP_CompnayName(string strcompanyName)
        {
            int recount = opa.UpdateCP_CompanyName(strcompanyName);
            if (recount <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
		public DataTable GetIOTime()
		{
			DataTable dtout=opa.GetIOTime();
			if(dtout!=null)
			{
				dtout.Columns["vcOfficer"].ColumnName="职务类型";
				dtout.Columns["vcClass"].ColumnName="班次";
				dtout.Columns["vcCommCode"].ColumnName="上班时间";
				dtout.Columns["vcCommSign"].ColumnName="下班时间";
			}
			return dtout;
		}

		public DataSet GetFuncList(string strLogionID,string strFuncType)
		{
			DataSet dsout=opa.GetFuncList(strLogionID,strFuncType);
			return dsout;
		}

		public bool UpdateGoodsNewFlag(ArrayList al)
		{
			int recount=opa.UpdateGoodsNewFlag(al);
			if(recount<=0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public bool UpdateIgComm(CMSMStruct.CommStruct cos)
		{
			int recount=opa.UpdateIgComm(cos);
			if(recount<=0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public bool UpdateFillPromComm(Hashtable htfp)
		{
			int recount=opa.UpdateFillPromComm(htfp);
			if(recount<=0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public bool UpdateIOTimeComm(CMSMStruct.CommStruct cos)
		{
			int recount=opa.UpdateIOTimeComm(cos);
			if(recount<=0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public bool UpdateOperPurview(string strOperID,ArrayList alfunc,string strFuncType)
		{
			int recount=opa.UpdateOperPurview(strOperID,alfunc,strFuncType);
			if(recount<=0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public DataTable GetDeptManageInfo()
		{
			DataTable dtout=opa.GetDeptManageInfo();
			return dtout;
		}

		public bool InsertDeptManageInfo(Hashtable htpara)
		{
			int recount=opa.InsertDeptManageInfo(htpara);
			if(recount<=0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public bool UpdateDeptManageInfo(Hashtable htpara)
		{
			int recount=opa.UpdateDeptManageInfo(htpara);
			if(recount<=0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public int IsExsitDeptInfo(string strClientID)//,string strNewID)
		{
            int DeptCount = opa.IsExsitDeptInfo(strClientID);//,strNewID);
			return DeptCount;
		}

		public DataTable GetClientOper(Hashtable htpara)
		{
			DataTable dtout=opa.GetClientOper(htpara);
			if(dtout!=null)
			{
				dtout.Columns["vcOperID"].ColumnName="客户端操作员编号";
				dtout.Columns["vcOperName"].ColumnName="客户端操作员名称";
				dtout.Columns["vcLimit"].ColumnName="权限";
				dtout.Columns["vcDeptID"].ColumnName="门店";
				dtout.Columns["vcActiveFlag"].ColumnName="激活标志";
				dtout.Columns["vcPwdBeginFlag"].ColumnName="密码初始化标志";
				dtout.Columns.Add("功能权限");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					dtout.Rows[i]["功能权限"]="<a href='wfmOperPurview.aspx?FuncType=CS&id=" + dtout.Rows[i]["客户端操作员编号"].ToString() + "&name="+dtout.Rows[i]["客户端操作员名称"].ToString()+"'>修改权限</a>";
				}
				dtout.Columns.Add("操作");
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					dtout.Rows[i]["操作"]="<a href='wfmClientOperDetail.aspx?id=" + dtout.Rows[i]["客户端操作员编号"].ToString() + "'>编辑</a>";
				}
			}
			return dtout;
		}

		public CMSMStruct.ClientOperStruct GetClientOperInfo(string strOperid)
		{
			DataTable dtout=opa.GetClientOperInfo(strOperid);
			CMSMStruct.ClientOperStruct coper1=new CommCenter.CMSMStruct.ClientOperStruct();
			if(dtout!=null)
			{
				coper1.strOperID=dtout.Rows[0]["vcOperID"].ToString();
				coper1.strOperName=dtout.Rows[0]["vcOperName"].ToString();
				coper1.strLimit=dtout.Rows[0]["vcLimit"].ToString();
				coper1.strPwd=dtout.Rows[0]["vcPwd"].ToString();
				coper1.strDeptID=dtout.Rows[0]["vcDeptID"].ToString();
				coper1.strActiveFlag=dtout.Rows[0]["vcActiveFlag"].ToString();
				coper1.strPwdBeginFlag=dtout.Rows[0]["vcPwdBeginFlag"].ToString();
			}
			else
			{
				coper1=null;
			}
			return coper1;
		}

		public bool ChkClientOperIDDup(string strOperID)
		{
			string strid=opa.ChkClientOperIDDup(strOperID);
			if(strid!="0")
			{
				return false;
			}

			return true;
		}

		public bool ChkClientOperNameDup(string strOperID,string strOperName,string strDeptID)
		{
			string strname=opa.ChkClientOperNameDup(strOperID,strOperName,strDeptID);
			if(strname!="0")
			{
				return false;
			}

			return true;
		}

		public bool InsertClientOper(CMSMStruct.ClientOperStruct copernew)
		{
			int recount=opa.InsertClientOper(copernew);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public bool UpdateClientOper(CMSMStruct.ClientOperStruct copernew,CMSMStruct.ClientOperStruct coperold)
		{
			string sqlset="";
			if(copernew.strOperName!=coperold.strOperName)
			{
				sqlset+="vcOperName='" + copernew.strOperName + "',";
			}
			if(copernew.strLimit!=coperold.strLimit)
			{
				sqlset+="vcLimit='" + copernew.strLimit + "',";
			}
			if(copernew.strDeptID!=coperold.strDeptID)
			{
				sqlset+="vcDeptID='" + copernew.strDeptID + "',";
			}

			if(sqlset!="")
			{
				sqlset=sqlset.Substring(0,sqlset.Length-1);
				int recount=opa.UpdateClientOper(coperold.strOperID,sqlset);
				if(recount<=0)
				{
					return false;
				}
			}
			
			return true;
		}

		public bool UpdateClientOperPwdBegin(string strOperID)
		{
			int recount=opa.UpdateClientOperPwdBegin(strOperID);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

		public bool UpdateClientOperFreeze(string strOperID)
		{
			int recount=opa.UpdateClientOperFreeze(strOperID);
			if(recount<=0)
			{
				return false;
			}

			return true;
		}

        public DataTable GetAssType()
        {
            DataTable dtout = opa.GetAssType();
            return dtout;
        }

        public bool GetAssTypeExist(string strAt)
        {
            int existcnt = opa.GetAssTypeExist(strAt);
            if (existcnt > 0)
                return true;
            else
                return false;
        }

        public bool ModifyAssType(string strATCode, string strATName, string strrate, string stropertype)
        {
            int existcnt = opa.ModifyAssType(strATCode, strATName, strrate, stropertype);
            if (existcnt > 0)
                return true;
            else
                return false;
        }
	}
}
