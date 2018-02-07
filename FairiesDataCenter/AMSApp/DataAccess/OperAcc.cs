using System;
using System.Data;
using System.Data.SqlClient;
using CommCenter;
using System.Collections;

namespace DataAccess
{
	/// <summary>
	/// Summary description for OperAcc.
	/// </summary>
	public class OperAcc
	{

		SqlDataReader drr;
		SqlConnection con;
		AMSLog clog=new AMSLog();

		public OperAcc(string strcons)
		{
			//
			// TODO: Add constructor logic here
			//
			con=new SqlConnection(strcons);
		}

		public DataTable GetLoginInfo(string strLoginid)
		{
			string strsql="select * from tbLogin where vcLoginID='" + strLoginid + "'";
			DataTable dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,strsql);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return dtout;
		}

		public int InsertOperLog(CMSMStruct.OperStruct OperNew)
		{
			string sql1="insert into tbOperLog values('登录中心','" + OperNew.strOperID+"','"+OperNew.strDeptID+"',getdate(),'"+OperNew.strMacAddress+"')";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public DataTable GetGoods(string strGoodsID,string strGoodsName)
		{
			DataTable dtGoods=new DataTable();
			try
			{
				string strCondition="";
				if(strGoodsID!=""&&strGoodsID!="*")
				{
					strCondition=" vcGoodsID like '" + strGoodsID + "%'";
				}
				if(strGoodsName!=""&&strGoodsName!="*")
				{
					if(strCondition=="")
					{
						strCondition=" vcGoodsName like '%" + strGoodsName + "%'";
					}
					else
					{
						strCondition=strCondition + " and vcGoodsName='" + strGoodsName + "'";
					}
				}
				string sql1="select vcGoodsID,vcGoodsName,vcSpell,nPrice,iIgValue,vcComments from tbGoods";
				if(strCondition!="")
				{
					sql1=sql1 + " where " + strCondition + " order by vcGoodsID";
				}
				dtGoods=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtGoods;
		}

		public DataTable GetGoods(string strGoodsID,string strGoodsName,string strSpell)
		{
			DataTable dtGoods=new DataTable();
			try
			{
				string strCondition="";
				if(strGoodsID!=""&&strGoodsID!="*")
				{
					strCondition=" vcGoodsID like '" + strGoodsID + "%'";
				}
				if(strGoodsName!=""&&strGoodsName!="*")
				{
					if(strCondition=="")
					{
						strCondition=" vcGoodsName like '%" + strGoodsName + "%'";
					}
					else
					{
						strCondition=strCondition + " and vcGoodsName='" + strGoodsName + "'";
					}
				}
				if(strSpell!="")
				{
					if(strCondition=="")
					{
						strCondition=" vcSpell like '%" + strSpell + "%'";
					}
					else
					{
						strCondition=strCondition + " and vcSpell='" + strSpell + "'";
					}
				}
				string sql1="select vcGoodsID,vcGoodsName,vcSpell,nPrice,iIgValue,vcComments from tbGoods";
				if(strCondition!="")
				{
					sql1=sql1 + " where " + strCondition + " order by vcGoodsID";
				}
				dtGoods=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtGoods;
		}

		public DataTable GetGoodsInfo(string strGoodsID)
		{
			DataTable dtGoods=new DataTable();
			try
			{
				string sql1="select vcGoodsID,vcGoodsName,vcSpell,nPrice,iIgValue,vcComments from tbGoods where vcGoodsID='" + strGoodsID + "'";
				dtGoods=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtGoods;
		}

		public string getGoodsID(string strGoodsID)
		{
			string strsql="select count(*) from tbGoods where vcGoodsID='" + strGoodsID + "'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			string strid=drr[0].ToString();
			drr.Close();
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return strid;
		}

		public string getMaxGoodsID(string mask)
		{
			string strsql="select isnull(max(cast(vcGoodsID as numeric(10,0))),0) from tbGoods where vcGoodsID like '"+mask+"%'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			string strid=drr[0].ToString();
			drr.Close();
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return strid;
		}

		public string getGoodsName(string strGoodsName)
		{
			string strsql="select count(*) from tbGoods where vcGoodsName='" + strGoodsName + "'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			string strname=drr[0].ToString();
			drr.Close();
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return strname;
		}

		public string getGoodsNamebyID(string strGoodsName,string strGoodsID)
		{
			string strsql="select count(*) from tbGoods where vcGoodsName='" + strGoodsName + "' and vcGoodsID<>'" + strGoodsID + "'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			string strname=drr[0].ToString();
			drr.Close();
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return strname;
		}

		public int InsertGoods(CMSMStruct.GoodsStruct gs)
		{
			string strType="";
			if (gs.strGoodsType=="甜面类")
			{
				strType="20','21','22','23','24','25','26";
			}
			if (gs.strGoodsType=="欧包类")
			{
				strType="27";
			}
			if (gs.strGoodsType=="硬质面包类")
			{
				strType="28";
			}
			if (gs.strGoodsType=="丹麦类")
			{
				strType="29";
			}
			if (gs.strGoodsType=="吐司类")
			{
				strType="30','31','32";
			}
			if (gs.strGoodsType=="调理类及其它杂类")
			{
				strType="33','34','35','36','37','38','39";
			}
			if (gs.strGoodsType=="常温蛋糕类")
			{
				strType="40','41','42','43','44";
			}
			if (gs.strGoodsType=="月饼类")
			{
                strType = "45','46','47','48','49";
			}
			if (gs.strGoodsType=="西点类")
			{
				strType="50','51','52','53','54','55','56','57','58','59";
			}
			if (gs.strGoodsType=="生日蛋糕类")
			{
				strType="60','61','62','63','64','65','66','67','68','69";
			}
			if (gs.strGoodsType=="饼干类")
			{
				strType="70','71','72','73','74','75','76','77','78','79";
			}
			if (gs.strGoodsType=="外卖产品")
			{
				strType="80','81','82','83','84','85','86','87','88','89";
			}
			if (gs.strGoodsType=="咖啡类")
			{
				strType="90','91','92','93";
			}
			int recount=0;
			string strGoodsID="";
			string strsql="select isnull(max(cast(vcGoodsID as numeric(10,0))),0) as maxid from tbGoods where substring (vcGoodsID,1,2) in ('"+ strType +"')";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			int strmaxid=int.Parse(drr[0].ToString());
			drr.Close();
			if (gs.strGoodsType=="甜面类")
			{
				if(strmaxid==0)
				{
					strmaxid=2000;
				}
				else if(strmaxid>=2699)
				{
					return 0;
				}
			}
			if (gs.strGoodsType=="欧包类")
			{
				if(strmaxid==0)
				{
					strmaxid=2700;
				}
				else if(strmaxid>=2799)
				{
					return 0;
				}
			}
			if (gs.strGoodsType=="硬质面包类")
			{
				if(strmaxid==0)
				{
					strmaxid=2800;
				}
				else if(strmaxid>=2899)
				{
					return 0;
				}
			}
			if (gs.strGoodsType=="丹麦类")
			{
				if(strmaxid==0)
				{
					strmaxid=2900;
				}
				else if(strmaxid>=2999)
				{
					return 0;
				}
			}
			if (gs.strGoodsType=="吐司类")
			{
				if(strmaxid==0)
				{
					strmaxid=3000;
				}
				else if(strmaxid>=3299)
				{
					return 0;
				}
			}
			if (gs.strGoodsType=="调理类及其它杂类")
			{
				if(strmaxid==0)
				{
					strmaxid=3300;
				}
				else if(strmaxid>=3999)
				{
					return 0;
				}
			}
			if (gs.strGoodsType=="常温蛋糕类")
			{
				if(strmaxid==0)
				{
					strmaxid=4000;
				}
				else if(strmaxid>=4499)
				{
					return 0;
				}
			}
			if (gs.strGoodsType=="月饼类")
			{
				if(strmaxid==0)
				{
					strmaxid=4500;
				}
				else if(strmaxid>=4999)
				{
					return 0;
				}
			}
			if (gs.strGoodsType=="西点类")
			{
				if(strmaxid==0)
				{
					strmaxid=5000;
				}
				else if(strmaxid>=5999)
				{
					return 0;
				}
			}
			if (gs.strGoodsType=="生日蛋糕类")
			{
				if(strmaxid==0)
				{
					strmaxid=6000;
				}
				else if(strmaxid>=6999)
				{
					return 0;
				}
			}
			if (gs.strGoodsType=="饼干类")
			{
				if(strmaxid==0)
				{
					strmaxid=7000;
				}
				else if(strmaxid>=7999)
				{
					return 0;
				}
			}
			if (gs.strGoodsType=="外卖产品")
			{
				if(strmaxid==0)
				{
					strmaxid=8000;
				}
				else if(strmaxid>=8999)
				{
					return 0;
				}
			}
			if (gs.strGoodsType=="咖啡类")
			{
				if(strmaxid==0)
				{
					strmaxid=9000;
				}
				else if(strmaxid>=9399)
				{
					return 0;
				}
			}
			
			strGoodsID=(strmaxid+1).ToString();
			string sql1="insert into tbGoods values('" + strGoodsID + "','" + gs.strGoodsName + "','" + gs.strSpell + "'," + gs.dPrice.ToString() + ",0," + gs.iIgValue.ToString() + ",'0','" + gs.strComments + "')";
			recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);

			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

        public int InsertGoods(CMSMStruct.GoodsStruct gs, string strGoodsType)
        {
            string strGoodsIDTmp = "";
            if (gs.strGoodsType == "月饼类")
            {
                strGoodsIDTmp = "'10','11','12','13','14','15','16'";
            }
            if (gs.strGoodsType == "甜面类")
            {
                strGoodsIDTmp = "'20','21','22','23','24','25','26'";
            }
            if (gs.strGoodsType == "欧包类")
            {
                strGoodsIDTmp = "'27'";
            }
            if (gs.strGoodsType == "硬质面包类")
            {
                strGoodsIDTmp = "'28'";
            }
            if (gs.strGoodsType == "丹麦类")
            {
                strGoodsIDTmp = "'29'";
            }
            if (gs.strGoodsType == "吐司类")
            {
                strGoodsIDTmp = "'30','31','32'";
            }
            if (gs.strGoodsType == "调理类及其它杂类")
            {
                strGoodsIDTmp = "'33','34','35','36','37','38','39'";
            }
            if (gs.strGoodsType == "常温蛋糕类")
            {
                strGoodsIDTmp = "'40','41','42','43','44'";
            }
            if (gs.strGoodsType == "蛋糕胚")
            {
                strGoodsIDTmp = "'45'";
            }
            if (gs.strGoodsType == "西点类")
            {
                strGoodsIDTmp = "'50','51','52','53','54','55','56','57','58','59'";
            }
            if (gs.strGoodsType == "生日蛋糕类")
            {
                strGoodsIDTmp = "'60','61','62','63','64','65','66','67','68','69'";
            }
            if (gs.strGoodsType == "饼干类")
            {
                strGoodsIDTmp = "'70','71','72','73','74','75','76','77','78','79'";
            }
            if (gs.strGoodsType == "外卖产品")
            {
                strGoodsIDTmp = "'80','81','82','83','84','85','86','87','88','89'";
            }
            if (gs.strGoodsType == "咖啡类")
            {
                strGoodsIDTmp = "'90','91','92','93'";
            }
            string strsql = "select isnull(max(cast(vcGoodsID as numeric(10,0))),0) as maxid from tbGoods  where substring(vcGoodsID,1,2) in(" + strGoodsIDTmp + ")";
            drr = SqlHelper.ExecuteReader(con, CommandType.Text, strsql);
            drr.Read();
            int strmaxid = int.Parse(drr[0].ToString());
            drr.Close();
            if (gs.strGoodsType == "月饼类")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 1000;
                }
                else if (strmaxid >= 1699)
                {
                    return 0;
                }
            }
            if (gs.strGoodsType == "甜面类")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 2000;
                }
                else if (strmaxid >= 2699)
                {
                    return 0;
                }
            }
            if (gs.strGoodsType == "欧包类")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 2700;
                }
                else if (strmaxid >= 2799)
                {
                    return 0;
                }
            }
            if (gs.strGoodsType == "硬质面包类")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 2800;
                }
                else if (strmaxid >= 2899)
                {
                    return 0;
                }
            }
            if (gs.strGoodsType == "丹麦类")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 2900;
                }
                else if (strmaxid >= 2999)
                {
                    return 0;
                }
            }
            if (gs.strGoodsType == "吐司类")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 3000;
                }
                else if (strmaxid >= 3299)
                {
                    return 0;
                }
            }
            if (gs.strGoodsType == "调理类及其它杂类")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 3300;
                }
                else if (strmaxid >= 3399)
                {
                    return 0;
                }
            }
            if (gs.strGoodsType == "常温蛋糕类")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 4000;
                }
                else if (strmaxid >= 4499)
                {
                    return 0;
                }
            }
            if (gs.strGoodsType == "蛋糕胚")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 4500;
                }
                else if (strmaxid >= 4599)
                {
                    return 0;
                }
            }
            if (gs.strGoodsType == "西点类")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 5000;
                }
                else if (strmaxid >= 5999)
                {
                    return 0;
                }
            }
            if (gs.strGoodsType == "生日蛋糕类")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 6000;
                }
                else if (strmaxid >= 6999)
                {
                    return 0;
                }
            }
            if (gs.strGoodsType == "饼干类")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 7000;
                }
                else if (strmaxid >= 7999)
                {
                    return 0;
                }
            }
            if (gs.strGoodsType == "外卖产品")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 8000;
                }
                else if (strmaxid >= 8999)
                {
                    return 0;
                }
            }


            int recount = 0;
            string strGoodsID = "";
            //			string strsql="select isnull(max(cast(vcGoodsID as numeric(10,0))),0) as maxid from tbGoods where vcGoodsID like '8%'";
            //			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
            //			drr.Read();
            //			int strmaxid=int.Parse(drr[0].ToString());
            //			drr.Close();
            //			if(strmaxid==0)
            //			{
            //				strGoodsID="8001";
            //			}
            //			else if(strmaxid>=8999)
            //			{
            //				return 0;
            //			}
            //			else
            //			{
            strGoodsID = (strmaxid + 1).ToString();
            string sql1 = "insert into tbGoods values('" + strGoodsID + "','" + gs.strGoodsName + "','" + gs.strSpell + "'," + gs.dPrice.ToString() + ",0," + gs.iIgValue.ToString() + ",'0','" + gs.strComments + "','" + strGoodsType + "')";
            recount = SqlHelper.ExecuteNonQuery(con, CommandType.Text, sql1);
            //}

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return recount;
        }
        public int InsertGoods(CMSMStruct.GoodsStruct gs, string strGoodsType, out string strGoodsID)
        {
            string strGoodsIDTmp = "";
            strGoodsID = "";
            if (gs.strGoodsType == "月饼类")
            {
                strGoodsIDTmp = "'10','11','12','13','14','15','16'";
            }
            if (gs.strGoodsType == "甜面类")
            {
                strGoodsIDTmp = "'20','21','22','23','24','25','26'";
            }
            if (gs.strGoodsType == "欧包类")
            {
                strGoodsIDTmp = "'27'";
            }
            if (gs.strGoodsType == "硬质面包类")
            {
                strGoodsIDTmp = "'28'";
            }
            if (gs.strGoodsType == "丹麦类")
            {
                strGoodsIDTmp = "'29'";
            }
            if (gs.strGoodsType == "吐司类")
            {
                strGoodsIDTmp = "'30','31','32'";
            }
            if (gs.strGoodsType == "调理类及其它杂类")
            {
                strGoodsIDTmp = "'33','34','35','36','37','38','39'";
            }
            if (gs.strGoodsType == "常温蛋糕类")
            {
                strGoodsIDTmp = "'40','41','42','43','44'";
            }
            if (gs.strGoodsType == "蛋糕胚")
            {
                strGoodsIDTmp = "'45'";
            }
            if (gs.strGoodsType == "西点类")
            {
                strGoodsIDTmp = "'50','51','52','53','54','55','56','57','58','59'";
            }
            if (gs.strGoodsType == "生日蛋糕类")
            {
                strGoodsIDTmp = "'60','61','62','63','64','65','66','67','68','69'";
            }
            if (gs.strGoodsType == "饼干类")
            {
                strGoodsIDTmp = "'70','71','72','73','74','75','76','77','78','79'";
            }
            if (gs.strGoodsType == "外卖产品")
            {
                strGoodsIDTmp = "'80','81','82','83','84','85','86','87','88','89'";
            }
            string strsql = "select isnull(max(cast(vcGoodsID as numeric(10,0))),0) as maxid from tbGoods  where substring(vcGoodsID,1,2) in(" + strGoodsIDTmp + ")";
            drr = SqlHelper.ExecuteReader(con, CommandType.Text, strsql);
            drr.Read();
            int strmaxid = int.Parse(drr[0].ToString());
            drr.Close();
            if (gs.strGoodsType == "月饼类")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 1000;
                }
                else if (strmaxid >= 1699)
                {
                    return 0;
                }
            }
            if (gs.strGoodsType == "甜面类")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 2000;
                }
                else if (strmaxid >= 2699)
                {
                    return 0;
                }
            }
            if (gs.strGoodsType == "欧包类")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 2700;
                }
                else if (strmaxid >= 2799)
                {
                    return 0;
                }
            }
            if (gs.strGoodsType == "硬质面包类")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 2800;
                }
                else if (strmaxid >= 2899)
                {
                    return 0;
                }
            }
            if (gs.strGoodsType == "丹麦类")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 2900;
                }
                else if (strmaxid >= 2999)
                {
                    return 0;
                }
            }
            if (gs.strGoodsType == "吐司类")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 3000;
                }
                else if (strmaxid >= 3299)
                {
                    return 0;
                }
            }
            if (gs.strGoodsType == "调理类及其它杂类")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 3300;
                }
                else if (strmaxid >= 3399)
                {
                    return 0;
                }
            }
            if (gs.strGoodsType == "常温蛋糕类")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 4000;
                }
                else if (strmaxid >= 4499)
                {
                    return 0;
                }
            }
            if (gs.strGoodsType == "蛋糕胚")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 4500;
                }
                else if (strmaxid >= 4599)
                {
                    return 0;
                }
            }
            if (gs.strGoodsType == "西点类")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 5000;
                }
                else if (strmaxid >= 5999)
                {
                    return 0;
                }
            }
            if (gs.strGoodsType == "生日蛋糕类")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 6000;
                }
                else if (strmaxid >= 6999)
                {
                    return 0;
                }
            }
            if (gs.strGoodsType == "饼干类")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 7000;
                }
                else if (strmaxid >= 7999)
                {
                    return 0;
                }
            }
            if (gs.strGoodsType == "外卖产品")
            {
                if (strmaxid == 0)
                {
                    strmaxid = 8000;
                }
                else if (strmaxid >= 8999)
                {
                    return 0;
                }
            }


            int recount = 0;
            //string strGoodsID = "";
            //			string strsql="select isnull(max(cast(vcGoodsID as numeric(10,0))),0) as maxid from tbGoods where vcGoodsID like '8%'";
            //			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
            //			drr.Read();
            //			int strmaxid=int.Parse(drr[0].ToString());
            //			drr.Close();
            //			if(strmaxid==0)
            //			{
            //				strGoodsID="8001";
            //			}
            //			else if(strmaxid>=8999)
            //			{
            //				return 0;
            //			}
            //			else
            //			{
            strGoodsID = (strmaxid + 1).ToString();
            string sql1 = "insert into tbGoods values('" + strGoodsID + "','" + gs.strGoodsName + "','" + gs.strSpell + "'," + gs.dPrice.ToString() + ",0," + gs.iIgValue.ToString() + ",'0','" + gs.strComments + "','" + strGoodsType + "')";
            recount = SqlHelper.ExecuteNonQuery(con, CommandType.Text, sql1);
            //}

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return recount;
        }
		public int UpdateGoods(string strGoodsID,string strsqlset)
		{
			string sql1="update tbGoods set " + strsqlset + " where vcGoodsID='" + strGoodsID + "'";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public string getGoodsSerial(string strCreateDate)
		{
			string strsql="select isnull(max(cast(substring(vcFileName,14,2) as int)),0) from tbDataSoftUpdateLog where Type='GOODS' and vcFileName like 'CEN00" + strCreateDate + "%'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			string strserial=drr[0].ToString();
			drr.Close();
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return strserial;
		}

		public string getNotiSerial(string strCreateDate)
		{
			string strsql="select isnull(max(cast(substring(vcFileName,14,2) as int)),0) from tbDataSoftUpdateLog where Type='NOTI' and vcFileName like 'CEN00" + strCreateDate + "%'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			string strserial=drr[0].ToString();
			drr.Close();
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return strserial;
		}

		public string getAssSerial(string strCreateDate)
		{
			string strsql="select isnull(max(cast(substring(vcFileName,14,2) as int)),0) from tbDataSoftUpdateLog where Type='CEN' and vcFileName like '%CEN00" + strCreateDate + "%'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			string strserial=drr[0].ToString();
			drr.Close();
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return strserial;
		}

		public int InsertDataLog(string strsqlset)
		{
			string sql1="insert into tbDataSoftUpdateLog values(" + strsqlset + ")";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public ArrayList DownSysPara()
		{
			DataTable dtPara=new DataTable();
			ArrayList alPara=new ArrayList();
			string sql1="select * from tbCommCode where vcCommSign<>'LOCAL' order by vcCommSign,vcCommCode";
			dtPara=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			for(int i=0;i<dtPara.Rows.Count;i++)
			{
				CMSMStruct.CommStruct paratmp=new CMSMStruct.CommStruct();
				paratmp.strCommName=dtPara.Rows[i]["vcCommName"].ToString();
				paratmp.strCommCode=dtPara.Rows[i]["vcCommCode"].ToString();
				paratmp.strCommSign=dtPara.Rows[i]["vcCommSign"].ToString();
				paratmp.strComments=dtPara.Rows[i]["vcComments"].ToString();
				alPara.Add(paratmp);
			}
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}

			return alPara;
		}

		public ArrayList DownGoodsData()
		{
			DataTable dtAss=new DataTable();
			ArrayList alAss=new ArrayList();
			string sql1="select * from tbGoods order by vcGoodsID";
			dtAss=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);

			for(int i=0;i<dtAss.Rows.Count;i++)
			{
				CMSMStruct.GoodsStruct asstmp=new CMSMStruct.GoodsStruct();
				asstmp.strGoodsID=dtAss.Rows[i]["vcGoodsID"].ToString();
				asstmp.strGoodsName=dtAss.Rows[i]["vcGoodsName"].ToString();
				asstmp.strSpell=dtAss.Rows[i]["vcSpell"].ToString();
				asstmp.dPrice=double.Parse(dtAss.Rows[i]["nPrice"].ToString());
				asstmp.dRate=double.Parse(dtAss.Rows[i]["nRate"].ToString());
				asstmp.iIgValue=int.Parse(dtAss.Rows[i]["iIgValue"].ToString());
				asstmp.strNewFlag=dtAss.Rows[i]["cNewFlag"].ToString();
				asstmp.strComments=dtAss.Rows[i]["vcComments"].ToString();
				alAss.Add(asstmp);
			}

			return alAss;
		}

		public ArrayList DownNotice(string strid)
		{
			DataTable dtnoti=new DataTable();
			ArrayList alnoti=new ArrayList();
			string sql1="select * from tbNotice where id='"+strid+"'";
			dtnoti=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);

			for(int i=0;i<dtnoti.Rows.Count;i++)
			{
				CMSMStruct.NoticeStruct notitmp=new CMSMStruct.NoticeStruct();
				notitmp.strid=dtnoti.Rows[i]["id"].ToString();
				notitmp.strComments=dtnoti.Rows[i]["vcComments"].ToString();
				notitmp.strCreateDate=dtnoti.Rows[i]["dtCreateDate"].ToString();
				notitmp.strActiveFlag=dtnoti.Rows[i]["vcActiveFlag"].ToString();
				notitmp.strDeptFlag=dtnoti.Rows[i]["vcDeptFlag"].ToString();
				alnoti.Add(notitmp);
			}

			return alnoti;
		}

		public ArrayList DownAssData(string strBeginDate)
		{
			DataTable dtAss=new DataTable();
			ArrayList alAss=new ArrayList();
			try
			{
				string sql1="select * from tbAssociator where vcCardID<>'V9999' and vcAssType<>'AT999' and dtOperDate>='" + strBeginDate + "' order by iAssID,vcCardID";
				dtAss=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);

				for(int i=0;i<dtAss.Rows.Count;i++)
				{
					CMSMStruct.AssociatorStruct asstmp=new CMSMStruct.AssociatorStruct();
					asstmp.strAssID=dtAss.Rows[i]["iAssID"].ToString();
					asstmp.strCardID=dtAss.Rows[i]["vcCardID"].ToString();
					asstmp.strAssName=dtAss.Rows[i]["vcAssName"].ToString();
					asstmp.strSpell=dtAss.Rows[i]["vcSpell"].ToString();
					asstmp.strAssNbr=dtAss.Rows[i]["vcAssNbr"].ToString();
					asstmp.strLinkPhone=dtAss.Rows[i]["vcLinkPhone"].ToString();
					asstmp.strLinkAddress=dtAss.Rows[i]["vcLinkAddress"].ToString();
					asstmp.strEmail=dtAss.Rows[i]["vcEmail"].ToString();
					asstmp.strAssType=dtAss.Rows[i]["vcAssType"].ToString();
					asstmp.strAssState=dtAss.Rows[i]["vcAssState"].ToString();
					asstmp.dCharge=Double.Parse(dtAss.Rows[i]["nCharge"].ToString());
					asstmp.iIgValue=int.Parse(dtAss.Rows[i]["iIgValue"].ToString());
					asstmp.strCardFlag=dtAss.Rows[i]["vcCardFlag"].ToString();
					asstmp.strComments=dtAss.Rows[i]["vcComments"].ToString();
					asstmp.strCreateDate=dtAss.Rows[i]["dtCreateDate"].ToString();
					asstmp.strOperDate=dtAss.Rows[i]["dtOperDate"].ToString();
					asstmp.strDeptID=dtAss.Rows[i]["vcDeptID"].ToString();
					alAss.Add(asstmp);
				}
			}
			catch(Exception e)
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				con.Close();
			}
			return alAss;
		}

		public DataTable GetLoginOper(Hashtable htPara)
		{
			DataTable dtLogin=new DataTable();
			try
			{
				string strCondition="";
				if(htPara["strLoginName"].ToString()!=""&&htPara["strLoginName"].ToString()!="*")
				{
					strCondition=" vcOperName like '%" + htPara["strLoginName"].ToString() + "%'";
				}
				if(htPara["strDeptID"].ToString()!=""&&htPara["strDeptID"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" vcDeptID like '" + htPara["strDeptID"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and vcDeptID='" + htPara["strDeptID"].ToString() + "'";
					}
				}
				string sql1="SELECT [vcLoginID], [vcOperName],[vcLimit],[vcDeptID] FROM [tbLogin]";
				if(strCondition!="")
				{
					sql1=sql1 + " where " + strCondition + " order by vcDeptID,vcOperName";
				}
				dtLogin=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtLogin;
		}

		public string getLoginID(string strLoginID)
		{
			string strsql="select count(*) from tbLogin where vcLoginID='" + strLoginID + "'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			string strid=drr[0].ToString();
			drr.Close();
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return strid;
		}

		public string getOperName(string strOperID,string strOperName)
		{
			string strsql="select count(*) from tbLogin where vcLoginID<>'"+strOperID+"' and vcOperName='" + strOperName + "'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			string strname=drr[0].ToString();
			drr.Close();
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return strname;
		}

		public int InsertLogin(CMSMStruct.LoginStruct lsnew)
		{
			string sql1="insert into tbLogin values('" + lsnew.strLoginID+"','"+lsnew.strOperName+"','"+lsnew.strLimit+"','123456','"+lsnew.strDeptID+"')";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public int UpdateLogin(string strLoginID,string strsqlset)
		{
			string sql1="update tbLogin set " + strsqlset + " where vcLoginID='" + strLoginID + "'";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public int DeleteLogin(string strLoginID)
		{
			string sql1="delete from tbLogin where vcLoginID='" + strLoginID + "'";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public DataTable GetNotice(Hashtable htPara)
		{
			DataTable dtNotice=new DataTable();
			try
			{
				string strCondition="";
				if(htPara["strDeptID"].ToString()!=""&&htPara["strDeptID"].ToString()!="*")
				{
					strCondition=" vcDeptFlag in('" + htPara["strDeptID"].ToString() + "')";
				}
				if(htPara["strContent"].ToString()!=""&&htPara["strContent"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" vcComments like '%" + htPara["strContent"].ToString() + "%'";
					}
					else
					{
						strCondition=strCondition + " and vcComments='" + htPara["strContent"].ToString() + "'";
					}
				}
				if(strCondition!="")
				{
					strCondition=strCondition+" and ";
				}
				string sql1="select id,vcComments,dtCreateDate,vcActiveFlag,(case vcDeptFlag when 'all' then '全部门店' else vcDeptFlag end) as vcDeptFlag from tbNotice where "+strCondition+" dtCreateDate between '"+htPara["strBegin"].ToString()+"' and '"+htPara["strEnd"].ToString()+" 23:59:59' order by id desc";
				dtNotice=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtNotice;
		}

		public int InsertNotice(string strDept,string strContent)
		{
			string sql1="insert into tbNotice values('" + strContent+"',getdate(),'0','"+strDept+"')";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public int UpdateNotice(string strid)
		{
			string sql1="update tbNotice set vcActiveFlag='2' where id='"+strid+"'";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public string getNoticeActiveFlag(string strid)
		{
			string strsql="select vcActiveFlag from tbNotice where id='" + strid + "'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			string strreturnid=drr[0].ToString();
			drr.Close();
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return strreturnid;
		}

		public int UpdateOperPwd(string strid,string strpwd)
		{
			string sql1="update tbLogin set vcPwd='"+strpwd+"' where vcLoginID='"+strid+"'";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public DataTable GetAllGoodsName()
		{
			DataTable dtGoods=new DataTable();
			try
			{
				string sql1="select vcGoodsName,cNewFlag from tbGoods";
				dtGoods=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtGoods;
		}

		public DataTable GetIgPara()
		{
			DataTable dtIg=new DataTable();
			try
			{
				string sql1="select vcCommName,vcCommCode from tbCommCode where vcCommSign='IG'";
				dtIg=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtIg;
		}

		public DataTable GetPromRate()
		{
			DataTable dtProm=new DataTable();
			try
			{
				string sql1="select * from tbCommCode where vcComments='充值赠款'";
				dtProm=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtProm;
		}

        public string GetSPF()
        {
            string strSPF = "";
            try
            {
                string sql1 = "select vcCommName from tbCommcode where vcCommSign='SPF' and vcCommCOde='Q1'";
                strSPF = SqlHelper.ExecuteScalar(con, CommandType.Text, sql1).ToString();
            }
            catch (Exception e)
            {
                clog.WriteLine(e);
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return strSPF;
        }
        public int UpdateSPF(string strSPF)
        {
            con.Open();
            using (SqlTransaction tran = con.BeginTransaction())
            {
                try
                {
                    string sql1 = "";
                    string sql2 = "";
                    sql1 = "select count(*) from tbCommcode where vcCommSign='SPF' and vcCommCOde='Q1'";
                    SqlDataReader drr = SqlHelper.ExecuteReader(con, tran, CommandType.Text, sql1);
                    drr.Read();
                    string strCount = drr[0].ToString();
                    drr.Close();

                    if (strCount == "0")
                    {
                        sql2 = "insert into tbCommCode values('" + strSPF + "','Q1','SPF','积分清零日期')";
                    }
                    else
                    {
                        sql2 = "update tbCommCode set vcCommName='" + strSPF + "' where vcCommSign='SPF' and vcCommCOde='Q1'";
                    }
                    SqlHelper.ExecuteNonQuery(con, tran, CommandType.Text, sql2);

                    tran.Commit();
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    clog.WriteLine(e);
                    return 0;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }

                return 1;
            }
        }
        public DataTable GetCP()
        {
            DataTable dtCP = new DataTable();
            try
            {
                string sql1 = "select * from tbCommcode where vcCommSign='CP'";
                dtCP = SqlHelper.ExecuteDataTable(con, CommandType.Text, sql1);
            }
            catch (Exception e)
            {
                clog.WriteLine(e);
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return dtCP;
        }
        public int UpdateCP_Img(string strimg)
        {
            con.Open();
            using (SqlTransaction tran = con.BeginTransaction())
            {
                try
                {
                    string sql1 = "";
                    string sql2 = "";
                    sql1 = "select count(*) from tbCommcode where vcCommSign='CP'";
                    SqlDataReader drr = SqlHelper.ExecuteReader(con, tran, CommandType.Text, sql1);
                    drr.Read();
                    string strCount = drr[0].ToString();
                    drr.Close();

                    if (strCount == "0")
                    {
                        sql2 = "insert into tbCommCode values('公司名称','" + strimg + "','CP','公司名称')";
                    }
                    else
                    {
                        sql2 = "update tbCommCode set vcCommCode='" + strimg + "' where vcCommSign='CP'";
                    }
                    SqlHelper.ExecuteNonQuery(con, tran, CommandType.Text, sql2);

                    tran.Commit();
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    clog.WriteLine(e);
                    return 0;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }

                return 1;
            }
        }
        public int UpdateCP_CompanyName(string strcompanyName)
        {
            con.Open();
            using (SqlTransaction tran = con.BeginTransaction())
            {
                try
                {
                    string sql1 = "";
                    string sql2 = "";
                    sql1 = "select count(*) from tbCommcode where vcCommSign='CP'";
                    SqlDataReader drr = SqlHelper.ExecuteReader(con, tran, CommandType.Text, sql1);
                    drr.Read();
                    string strCount = drr[0].ToString();
                    drr.Close();

                    if (strCount == "0")
                    {
                        sql2 = "insert into tbCommCode values('" + strcompanyName + "','2.JPG','CP','公司名称')";
                    }
                    else
                    {
                        sql2 = "update tbCommCode set vcCommName='" + strcompanyName + "' where vcCommSign='CP'";
                    }
                    SqlHelper.ExecuteNonQuery(con, tran, CommandType.Text, sql2);

                    tran.Commit();
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    clog.WriteLine(e);
                    return 0;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }

                return 1;
            }
        }
		public DataTable GetIOTime()
		{
			DataTable dttime=new DataTable();
			try
			{
				string sql1="select substring(vcCommName,1,5) as vcOfficer,substring(vcCommName,6,1) as vcClass,vcCommCode,vcCommSign from tbCommCode where vcComments='IOTime' order by vcOfficer,vcClass";
				dttime=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dttime;
		}

		public DataSet GetFuncList(string strLogionID,string strFuncType)
		{
			DataTable dttmp=new DataTable();
			DataSet dsout=new DataSet();
			try
			{
				string sql1 = "select cnvcFuncName,cnvcFuncAddress from tbFunc where cnvcFuncType='"+strFuncType+"' order by cnvcFuncParentName,cnvcFuncName";				
				dttmp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				dttmp.TableName="funclist";
				dsout.Tables.Add(dttmp);

				dttmp=null;
				//string sql2="select vcFuncName from tbOperFunc where vcOperID='"+strLogionID+"' order by vcFuncName";
				string sql2 = "SELECT a.vcFuncName FROM tbOperFunc a "
					+" LEFT JOIN tbFunc b ON a.vcfuncname = b.cnvcfuncname AND a.vcfuncaddress=b.cnvcfuncaddress "
					+" WHERE vcOperID='"+strLogionID+"' AND cnvcFuncType='"+strFuncType+"' "
					+" ORDER BY a.vcFuncName ";
				dttmp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql2);
				dttmp.TableName="operfunc";
				dsout.Tables.Add(dttmp);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dsout;
		}

		public int UpdateGoodsNewFlag(ArrayList al)
		{
			string sqlset="";
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					if(al.Count==0)
					{
						string sql1="update tbGoods set cNewFlag='0' where cNewFlag<>'0'";
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

						tran.Commit();
					}
					else
					{
						for(int i=0;i<al.Count;i++)
						{
							sqlset+="'" + al[i].ToString() + "',";
						}
						sqlset=sqlset.Substring(0,sqlset.Length-1);

						string sql2="update tbGoods set cNewFlag='0' where cNewFlag<>'0'";
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

						string sql3="update tbGoods set cNewFlag='1' where vcGoodsName in(" + sqlset + ")";
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql3);

						tran.Commit();
					}
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}

		public int UpdateIgComm(CMSMStruct.CommStruct cos)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="select count(*) from tbCommCode where vcCommSign='IG'";
					SqlDataReader dr=SqlHelper.ExecuteReader(con,tran,CommandType.Text,sql1);
					dr.Read();
					string strCount=dr[0].ToString();
					dr.Close();

					string sql2="";
					if(strCount=="0")
					{
						sql2="insert into tbCommCode values('" + cos.strCommName + "','" + cos.strCommCode + "','" + cos.strCommSign + "','" + cos.strComments + "')";
					}
					else
					{
						sql2="update tbCommCode set vcCommName='" + cos.strCommName + "',vcCommCode='" + cos.strCommCode + "' where vcCommSign='IG'";
					}
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}

		public int UpdateFillPromComm(Hashtable htp)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="";
					string sql2="";
					for(int i=1;i<=htp.Count;i++)
					{
						CMSMStruct.CommStruct cos=(CMSMStruct.CommStruct)htp["FP"+i];
						sql1="select count(*) from tbCommCode where vcCommSign='" + cos.strCommSign + "'";
						SqlDataReader drr=SqlHelper.ExecuteReader(con,tran,CommandType.Text,sql1);
						drr.Read();
						string strCount=drr[0].ToString();
						drr.Close();

						if(strCount=="0")
						{
							sql2="insert into tbCommCode values('" + cos.strCommName + "','" + cos.strCommCode + "','" + cos.strCommSign + "','" + cos.strComments + "')";
						}
						else
						{
							sql2="update tbCommCode set vcCommCode='" + cos.strCommCode + "' where vcCommSign='" + cos.strCommSign + "'";
						}
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);
					}
					
					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}

		public int UpdateIOTimeComm(CMSMStruct.CommStruct cos)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="select count(*) from tbCommCode where vcCommName='" + cos.strCommName + "'";
					SqlDataReader drr=SqlHelper.ExecuteReader(con,tran,CommandType.Text,sql1);
					drr.Read();
					string strCount=drr[0].ToString();
					drr.Close();

					string sql2="";
					if(strCount=="0")
					{
						sql2="insert into tbCommCode values('" + cos.strCommName + "','" + cos.strCommCode + "','" + cos.strCommSign + "','" + cos.strComments + "')";
					}
					else
					{
						sql2="update tbCommCode set vcCommCode='" + cos.strCommCode + "',vcCommSign='" + cos.strCommSign + "' where vcCommName='" + cos.strCommName + "'";
					}
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);
					
					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}

		public int UpdateOperPurview(string strOperID,ArrayList alfunc,string strFuncType)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="delete from tbOperFunc where vcOperID='"+strOperID+"'";
					sql1 += " and vcFuncAddress IN(SELECT cnvcFuncAddress FROM tbFunc WHERE cnvcFuncType = '"+strFuncType+"')";																 
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

					string sql2="";
					for(int i=0;i<alfunc.Count;i++)
					{
						CMSMStruct.MenuStruct ms=(CMSMStruct.MenuStruct)alfunc[i];
						sql2="insert into tbOperFunc values('"+strOperID+"','"+ms.strFuncName+"','"+ms.strFuncAddress+"')";
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);
					}

					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}

		public DataTable GetDeptManageInfo()
		{
			DataTable dttmp=new DataTable();
			try
			{
                string sql1 = "select a.vcCommName,a.vcCommCode,c.cnvcDeptName,c.cnvcDeptID,c.cnnPriority,a.vcComments from tbCommCode a,tbDeptMapInfo b,tbDept c where a.vcCommSign='MD' and a.vcCommCode=b.cnvcOldDeptID and b.cnvcNewDeptID=c.cnvcDeptID order by c.cnnPriority";
				dttmp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dttmp;
		}

		public int InsertDeptManageInfo(Hashtable htpara)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
                    string sql1 = "insert into tbCommCode(vcCommName,vcCommCode,vcCommSign,vcComments) values('" + htpara["strClientName"].ToString() + "','" + htpara["strClientID"].ToString() + "','MD','" + htpara["strComments"].ToString() + "')";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

                    //string sql2="insert into tbDept(cnvcDeptName,cnvcDeptID,cnvcDeptType,cnvcParentDeptID,cnvcComments,cnnPriority) values('"+htpara["strNewName"].ToString()+"','"+htpara["strNewID"].ToString()+"','SalesRoom','BreadWorkShop','门店',"+htpara["strSortNum"].ToString()+")";
                    //SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

                    //string sql3="insert into tbDeptMapInfo(cnvcOldDeptID,cnvcNewDeptID) values('"+htpara["strClientID"].ToString()+"','"+htpara["strNewID"].ToString()+"')";
                    //SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql3);

					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}

		public int UpdateDeptManageInfo(Hashtable htpara)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
                    string sql1 = "update tbCommCode set vcCommName='" + htpara["strClientName"].ToString() + "',vcComments='" + htpara["strComments"].ToString() + "' where vcCommSign='MD' and vcCommCode='" + htpara["strClientID"].ToString() + "'";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

                    //string sql2="update tbDept set cnvcDeptName='"+htpara["strNewName"].ToString()+"',cnnPriority="+htpara["strSortNum"].ToString()+" where cnvcDeptID='"+htpara["strNewID"].ToString()+"'";
                    //SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return 0;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}

				return 1;
			}
		}

		public int IsExsitDeptInfo(string strClientID)//,string strNewID)
		{
			int deptFlag=2;
			try
			{
				string strsql="select count(*) from tbCommCode where vcCommSign='MD' and vcCommCode='" + strClientID + "'";
				drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
				drr.Read();
				int ClientDeptCount=int.Parse(drr[0].ToString());
				drr.Close();

                //strsql="select count(*) from tbDept where cnvcDeptID='" + strNewID + "'";
                //drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
                //drr.Read();
                //int NewDeptCount=int.Parse(drr[0].ToString());
                //drr.Close();

				if(ClientDeptCount==0)//&&NewDeptCount==0)
				{
					deptFlag=0;
				}
				else if(ClientDeptCount!=0)//&&NewDeptCount!=0)
				{
					deptFlag=2;
				}
				else
				{
					deptFlag=1;
				}
			}
			catch(Exception ex)
			{
				clog.WriteLine(ex);
				return deptFlag;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			
			return deptFlag;
		}

		public DataTable GetClientOper(Hashtable htPara)
		{
			DataTable dtOper=new DataTable();
			try
			{
				string strCondition="";
				if(htPara["strOperName"].ToString()!=""&&htPara["strOperName"].ToString()!="*")
				{
					strCondition=" vcOperName like '%" + htPara["strOperName"].ToString() + "%'";
				}
				if(htPara["strDeptID"].ToString()!=""&&htPara["strDeptID"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" vcDeptID like '" + htPara["strDeptID"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and vcDeptID='" + htPara["strDeptID"].ToString() + "'";
					}
				}
				string sql1="SELECT vcOperID,vcOperName,vcLimit,vcDeptID,(case vcActiveFlag when '1' then '正常' else '冻结' end) as vcActiveFlag,(case vcPwdBeginFlag when '1' then '待初始化' else '正常' end) as vcPwdBeginFlag FROM tbOper";
				if(htPara["strLoginID"].ToString()!="admin")
				{
					if(strCondition!="")
						sql1=sql1 + " where " + strCondition + " and vcOperID<>'admin'";
					else
						sql1=sql1 + " where vcOperID<>'admin'";
				}
				else
				{
					if(strCondition!="")
						sql1=sql1 + " where " + strCondition;
				}
				sql1+=" order by vcDeptID,vcOperName";
				dtOper=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return null;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtOper;
		}

		public DataTable GetClientOperInfo(string strOperid)
		{
			string strsql="select * from tbOper where vcOperID='" + strOperid + "'";
			DataTable dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,strsql);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return dtout;
		}

		public string ChkClientOperIDDup(string strOperID)
		{
			string strsql="select count(*) from tbOper where vcOperID='" + strOperID + "'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			string strid=drr[0].ToString();
			drr.Close();
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return strid;
		}

		public string ChkClientOperNameDup(string strOperID,string strOperName,string strDeptID)
		{
			string strsql="select count(*) from tbOper where vcOperName='" + strOperName + "' and vcDeptID='"+strDeptID+"' and vcOperID<>'"+strOperID+"'";
			drr=SqlHelper.ExecuteReader(con,CommandType.Text,strsql);
			drr.Read();
			string strname=drr[0].ToString();
			drr.Close();
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return strname;
		}

		public int InsertClientOper(CMSMStruct.ClientOperStruct copernew)
		{
			string sql1="insert into tbOper values('" + copernew.strOperID+"','"+copernew.strOperName+"','"+copernew.strLimit+"','000000','"+copernew.strDeptID+"','1','0')";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public int UpdateClientOper(string strOperID,string strsqlset)
		{
			string sql1="update tbOper set " + strsqlset + " where vcOperID='" + strOperID + "'";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public int UpdateClientOperPwdBegin(string strOperID)
		{
			string sql1="update tbOper set vcPwd='000000',vcPwdBeginFlag='1' where vcOperID='" + strOperID + "'";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public int UpdateClientOperFreeze(string strOperID)
		{
			string sql1="update tbOper set vcActiveFlag='0' where vcOperID='" + strOperID + "'";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}


        public DataTable GetAssType()
        {
            DataTable dtAT = new DataTable();
            try
            {
                string sql1 = "select vcCommName,vcCommCode,substring(vcComments,2,len(vcComments)-1) rate,(case substring(vcComments,1,1) when 'X' then '是' when 'Y' then '否' end) disp";
                sql1 += " from tbCommCode where vcCommSign='AT' and vcCommCode<>'AT999' order by vcCommCode";
                dtAT = SqlHelper.ExecuteDataTable(con, CommandType.Text, sql1);
            }
            catch (Exception e)
            {
                clog.WriteLine(e);
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return dtAT;
        }

        public int GetAssTypeExist(string strAt)
        {
            try
            {
                string sql1 = "select count(*) from tbCommCode where vcCommSign='AT' and vcCommCode='" + strAt + "'";
                drr = SqlHelper.ExecuteReader(con, CommandType.Text, sql1);
                drr.Read();
                return int.Parse(drr[0].ToString());
            }
            catch (Exception e)
            {
                clog.WriteLine(e);
                return 1;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public int ModifyAssType(string strATCode, string strATName, string strrate, string stropertype)
        {
            con.Open();
            try
            {
                string sql1 = "";
                if (stropertype == "ADD")
                {
                    sql1 = "insert into tbCommCode values('" + strATName + "','" + strATCode + "','AT','" + strrate + "')";
                }
                else
                {
                    sql1 = "update tbCommCode set vcCommName='" + strATName + "',vcComments='" + strrate + "' where vcCommSign='AT' and vcCommCode='" + strATCode + "'";
                }
                SqlHelper.ExecuteNonQuery(con, CommandType.Text, sql1);

                return 1;
            }
            catch (Exception e)
            {
                clog.WriteLine(e);
                return 0;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
	}
}
