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
	public class QueryAcc
	{

//		SqlDataReader drr;
		SqlConnection con;
		AMSLog clog=new AMSLog();

		public QueryAcc(string strcons)
		{
			//
			// TODO: Add constructor logic here
			//
			con=new SqlConnection(strcons);
		}

		public DataTable GetConsQuery(Hashtable htPara)
		{
			DataTable dtCons=new DataTable();
			try
			{
				string strCondition="";
				if(htPara["strCardID"].ToString()!=""&&htPara["strCardID"].ToString()!="*")
				{
					strCondition=" a.vcCardID='" + htPara["strCardID"].ToString() + "'";
				}
				if(htPara["strAssName"].ToString()!=""&&htPara["strAssName"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" c.vcAssName  like  '%" + htPara["strAssName"].ToString() + "%'";
					}
					else
					{
						strCondition=strCondition + " and c.vcAssName like  '%" + htPara["strAssName"].ToString() + "%'";
					}
				}
				if(htPara["strSerial"].ToString()!=""&&htPara["strSerial"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" a.iSerial like  '%" + htPara["strSerial"].ToString() + "%'";
					}
					else
					{
						strCondition=strCondition + " and a.iSerial like '%" + htPara["strSerial"].ToString() + "%'";
					}
				}
				if(htPara["strAssType"].ToString()!="")
				{
					if(strCondition=="")
					{
						strCondition=" c.vcAssType='" + htPara["strAssType"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and c.vcAssType = '" + htPara["strAssType"].ToString() + "'";
					}
				}
				if(htPara["strOperName"].ToString()!="")
				{
					if(strCondition=="")
					{
						strCondition=" a.vcOperName='" + htPara["strOperName"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and a.vcOperName = '" + htPara["strOperName"].ToString() + "'";
					}
				}
				if(htPara["strDeptID"].ToString()!="")
				{
					if(strCondition=="")
					{
						strCondition=" a.vcDeptID='" + htPara["strDeptID"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and a.vcDeptID = '" + htPara["strDeptID"].ToString() + "'";
					}
				}
				if(htPara["strBillType"].ToString()!="")
				{
					if(strCondition=="")
					{
						strCondition=" b.vcConsType='" + htPara["strBillType"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and b.vcConsType = '" + htPara["strBillType"].ToString() + "'";
					}
				}
				string sql1="select a.iSerial,c.vcAssName,c.vcAssType,a.vcCardID,d.vcGoodsName,a.nPrice,a.iCount,a.nFee,b.vcConsType,a.vcComments,(case a.cFlag when '0' then '正常消费' when '9' then '已撤消' else a.cFlag end) as cFlag,a.dtConsDate,a.vcOperName,a.vcDeptID";
				sql1+=" from vwConsItem a,vwBill b,tbAssociator c,tbGoods d";
				sql1+=" where a.iSerial=b.iSerial and a.vcCardID=b.vcCardID and a.vcDeptID=b.vcDeptID and a.vcCardID=c.vcCardID and a.cFlag='"+htPara["strConsFlag"].ToString()+"' and a.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' and a.vcGoodsID=d.vcGoodsID";
				if(strCondition!="")
				{
					sql1+=" and " + strCondition;
				}
				sql1+=" order by a.dtConsDate";
				dtCons=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				return dtCons;
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
			
		}

		public DataTable GetConsOperList(string strDept,string strbegin,string strend)
		{
			DataTable dtoperlist=new DataTable();
			try
			{
				string sql1="";
				if(strDept==""||strDept=="全部")
				{
					sql1="select distinct vcOperName from vwBill where dtConsDate between '" + strbegin+"' and '"+strend+"  23:59:59' order by vcOperName";
				}
				else
				{
					sql1="select distinct vcOperName from vwBill where vcDeptID='"+ strDept+"' and dtConsDate between '" + strbegin+"' and '"+strend+" 23:59:59' order by vcOperName";
				}
				dtoperlist=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				return dtoperlist;
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
			
		}

		public DataTable GetFillQuery(Hashtable htPara)
		{
			DataTable dtCons=new DataTable();
			try
			{
				string strCondition="";
				if(htPara["strCardID"].ToString()!=""&&htPara["strCardID"].ToString()!="*")
				{
					strCondition=" a.vcCardID='" + htPara["strCardID"].ToString() + "'";
				}
				if(htPara["strAssName"].ToString()!=""&&htPara["strAssName"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" b.vcAssName like '%" + htPara["strAssName"].ToString() + "%'";
					}
					else
					{
						strCondition=strCondition + " and b.vcAssName = '%" + htPara["strAssName"].ToString() + "%'";
					}
				}
				if(htPara["strAssType"].ToString()!="")
				{
					if(strCondition=="")
					{
						strCondition=" b.vcAssType='" + htPara["strAssType"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and b.vcAssType = '" + htPara["strAssType"].ToString() + "'";
					}
				}
				if(htPara["strOperName"].ToString()!="")
				{
					if(strCondition=="")
					{
						strCondition=" a.vcOperName='" + htPara["strOperName"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and a.vcOperName = '" + htPara["strOperName"].ToString() + "'";
					}
				}
				if(htPara["strDeptID"].ToString()!="")
				{
					if(strCondition=="")
					{
						strCondition=" a.vcDeptID='" + htPara["strDeptID"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and a.vcDeptID = '" + htPara["strDeptID"].ToString() + "'";
					}
				}
				string sql1="select a.iSerial,b.vcAssName,b.vcAssType,a.vcCardID,a.nFillFee,a.nFillProm,a.nFeeLast,a.nFeeCur,a.vcComments,a.dtFillDate,a.vcOperName,a.vcDeptID from vwFillFee a,tbAssociator b where a.vcCardID=b.vcCardID and a.nFillFee>0 and a.dtFillDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' ";
				if(strCondition!="")
				{
					sql1+=" and " + strCondition;
				}
				sql1+=" order by a.dtFillDate";
				dtCons=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				return dtCons;
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
		}

		public DataTable GetConsKindQuery(Hashtable htPara,string querytype)
		{
			DataTable dtCons=new DataTable();
			try
			{
				string strsql="";
				switch(querytype)
				{
					case "1":
						strsql="select a.vcDeptID,b.vcAssType,d.vcCommName as 商品类型,c.vcGoodsName,sum(a.iCount) as tolcount,sum(a.nFee) as tolfee";
						strsql+=" from vwConsItem a,tbAssociator b,tbGoods c,tbCommCode d";
						strsql+=" where a.cFlag='0' and d.vcCommSign='GT' and c.vcGoodsName like '%"+htPara["strGoodsName"].ToString()+"%' and a.vcCardID=b.vcCardID and a.vcGoodsID=c.vcGoodsID and substring(a.vcGoodsID,1,2) between substring(d.vcCommCode,1,2) and substring(d.vcCommCode,4,2) and a.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' ";
						if(htPara["strDeptID"].ToString()!="")
						{
							strsql+=" and a.vcDeptID='"+htPara["strDeptID"].ToString()+"'";
						}
						if(htPara["strAssType"].ToString()!="")
						{
							strsql+=" and b.vcAssType='"+htPara["strAssType"].ToString()+"'";
						}
						if(htPara["strGoodsType"].ToString()!="")
						{
							strsql+=" and d.vcCommCode='"+htPara["strGoodsType"].ToString()+"'";
						}
						strsql+=" group by a.vcDeptID,b.vcAssType,d.vcCommName,c.vcGoodsName order by a.vcDeptID,b.vcAssType,d.vcCommName,c.vcGoodsName";
						break;
					case "2":
						strsql="select d.vcCommName as 商品类型,c.vcGoodsName,sum(a.iCount) as tolcount,sum(a.nFee) as tolfee";
						strsql+=" from vwConsItem a,tbAssociator b,tbGoods c,tbCommCode d";
						strsql+=" where a.cFlag='0' and d.vcCommSign='GT' and c.vcGoodsName like '%"+htPara["strGoodsName"].ToString()+"%' and a.vcCardID=b.vcCardID and a.vcGoodsID=c.vcGoodsID and substring(a.vcGoodsID,1,2) between substring(d.vcCommCode,1,2) and substring(d.vcCommCode,4,2) and a.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' ";
						if(htPara["strGoodsType"].ToString()!="")
						{
							strsql+=" and d.vcCommCode='"+htPara["strGoodsType"].ToString()+"'";
						}
						strsql+=" group by a.vcDeptID,b.vcAssType,d.vcCommName,c.vcGoodsName order by a.vcDeptID,b.vcAssType,d.vcCommName,c.vcGoodsName";
						break;
					case "3":
						strsql="select d.vcCommName as 商品类型,c.vcGoodsName,sum(a.iCount) as tolcount,sum(a.nFee) as tolfee";
						strsql+=" from vwConsItem a,tbAssociator b,tbGoods c,tbCommCode d";
						strsql+=" where a.cFlag='0' and d.vcCommSign='GT' and c.vcGoodsName like '%"+htPara["strGoodsName"].ToString()+"%' and a.vcCardID=b.vcCardID and a.vcGoodsID=c.vcGoodsID and substring(a.vcGoodsID,1,2) between substring(d.vcCommCode,1,2) and substring(d.vcCommCode,4,2) and a.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' ";
						if(htPara["strDeptID"].ToString()!="")
						{
							strsql+=" and a.vcDeptID='"+htPara["strDeptID"].ToString()+"'";
						}
						if(htPara["strAssType"].ToString()!="")
						{
							strsql+=" and b.vcAssType='"+htPara["strAssType"].ToString()+"'";
						}
						if(htPara["strGoodsType"].ToString()!="")
						{
							strsql+=" and d.vcCommCode='"+htPara["strGoodsType"].ToString()+"'";
						}
						strsql+=" group by d.vcCommName,c.vcGoodsName order by d.vcCommName,c.vcGoodsName";
						break;
					case "4":
						strsql="select a.vcDeptID,c.vcGoodsName,sum(a.iCount) as tolcount,sum(a.nFee) as tolfee";
						strsql+=" from vwConsItem a,tbAssociator b,tbGoods c,tbCommCode d";
						strsql+=" where a.cFlag='0' and d.vcCommSign='GT' and c.vcGoodsName like '%"+htPara["strGoodsName"].ToString()+"%' and a.vcCardID=b.vcCardID and a.vcGoodsID=c.vcGoodsID and substring(a.vcGoodsID,1,2) between substring(d.vcCommCode,1,2) and substring(d.vcCommCode,4,2) and a.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' ";
						if(htPara["strDeptID"].ToString()!="")
						{
							strsql+=" and a.vcDeptID='"+htPara["strDeptID"].ToString()+"'";
						}
						if(htPara["strAssType"].ToString()!="")
						{
							strsql+=" and b.vcAssType='"+htPara["strAssType"].ToString()+"'";
						}
						if(htPara["strGoodsType"].ToString()!="")
						{
							strsql+=" and d.vcCommCode='"+htPara["strGoodsType"].ToString()+"'";
						}
						strsql+=" group by a.vcDeptID,c.vcGoodsName order by a.vcDeptID,c.vcGoodsName";
						break;
					case "5":
						strsql="select b.vcAssType,d.vcCommName as 商品类型,c.vcGoodsName,sum(a.iCount) as tolcount,sum(a.nFee) as tolfee";
						strsql+=" from vwConsItem a,tbAssociator b,tbGoods c,tbCommCode d";
						strsql+=" where a.cFlag='0' and d.vcCommSign='GT' and c.vcGoodsName like '%"+htPara["strGoodsName"].ToString()+"%' and a.vcCardID=b.vcCardID and a.vcGoodsID=c.vcGoodsID and substring(a.vcGoodsID,1,2) between substring(d.vcCommCode,1,2) and substring(d.vcCommCode,4,2) and a.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' ";
						if(htPara["strDeptID"].ToString()!="")
						{
							strsql+=" and a.vcDeptID='"+htPara["strDeptID"].ToString()+"'";
						}
						if(htPara["strAssType"].ToString()!="")
						{
							strsql+=" and b.vcAssType='"+htPara["strAssType"].ToString()+"'";
						}
						if(htPara["strGoodsType"].ToString()!="")
						{
							strsql+=" and d.vcCommCode='"+htPara["strGoodsType"].ToString()+"'";
						}
						strsql+=" group by b.vcAssType,d.vcCommName,c.vcGoodsName order by b.vcAssType,d.vcCommName,c.vcGoodsName";
						break;
					case "6":
						strsql="select a.vcDeptID,b.vcAssType,c.vcGoodsName,sum(a.iCount) as tolcount,sum(a.nFee) as tolfee";
						strsql+=" from vwConsItem a,tbAssociator b,tbGoods c,tbCommCode d";
						strsql+=" where a.cFlag='0' and d.vcCommSign='GT' and c.vcGoodsName like '%"+htPara["strGoodsName"].ToString()+"%' and a.vcCardID=b.vcCardID and a.vcGoodsID=c.vcGoodsID and substring(a.vcGoodsID,1,2) between substring(d.vcCommCode,1,2) and substring(d.vcCommCode,4,2) and a.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' ";
						if(htPara["strDeptID"].ToString()!="")
						{
							strsql+=" and a.vcDeptID='"+htPara["strDeptID"].ToString()+"'";
						}
						if(htPara["strAssType"].ToString()!="")
						{
							strsql+=" and b.vcAssType='"+htPara["strAssType"].ToString()+"'";
						}
						if(htPara["strGoodsType"].ToString()!="")
						{
							strsql+=" and d.vcCommCode='"+htPara["strGoodsType"].ToString()+"'";
						}
						strsql+=" group by a.vcDeptID,b.vcAssType,c.vcGoodsName order by  a.vcDeptID,b.vcAssType,c.vcGoodsName";
						break;
					case "7":
						strsql="select a.vcDeptID,d.vcCommName as 商品类型,c.vcGoodsName,sum(a.iCount) as tolcount,sum(a.nFee) as tolfee";
						strsql+=" from vwConsItem a,tbAssociator b,tbGoods c,tbCommCode d";
						strsql+=" where a.cFlag='0' and d.vcCommSign='GT' and c.vcGoodsName like '%"+htPara["strGoodsName"].ToString()+"%' and a.vcCardID=b.vcCardID and a.vcGoodsID=c.vcGoodsID and substring(a.vcGoodsID,1,2) between substring(d.vcCommCode,1,2) and substring(d.vcCommCode,4,2) and a.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' ";
						if(htPara["strDeptID"].ToString()!="")
						{
							strsql+=" and a.vcDeptID='"+htPara["strDeptID"].ToString()+"'";
						}
						if(htPara["strAssType"].ToString()!="")
						{
							strsql+=" and b.vcAssType='"+htPara["strAssType"].ToString()+"'";
						}
						if(htPara["strGoodsType"].ToString()!="")
						{
							strsql+=" and d.vcCommCode='"+htPara["strGoodsType"].ToString()+"'";
						}
						strsql+=" group by a.vcDeptID,d.vcCommName,c.vcGoodsName order by a.vcDeptID,d.vcCommName,c.vcGoodsName";
						break;
				}
				if(querytype=="")
				{
					return dtCons;
				}
				else
				{
					dtCons=SqlHelper.ExecuteDataTable(con,CommandType.Text,strsql);
				}
				
				return dtCons;
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
			
		}

		public DataTable GetBusiLogQuery(Hashtable htPara)
		{
			DataTable dtBusiLog=new DataTable();
			try
			{
				string strCondition="";
				if(htPara["strOperName"].ToString()!=""&&htPara["strOperName"].ToString()!="*")
				{
					strCondition=" a.vcOperName='" + htPara["strOperName"].ToString() + "'";
				}
				if(htPara["strDeptID"].ToString()!="")
				{
					if(strCondition=="")
					{
						strCondition=" a.vcDeptID='" + htPara["strDeptID"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and a.vcDeptID = '" + htPara["strDeptID"].ToString() + "'";
					}
				}
				if(htPara["strCardID"].ToString()!="")
				{
					if(strCondition=="")
					{
						strCondition=" b.vcCardID='" + htPara["strCardID"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and b.vcCardID = '" + htPara["strCardID"].ToString() + "'";
					}
				}
				string sql1="select a.iSerial,b.vcAssName,b.vcAssType,a.vcCardID,c.vcCommName,a.vcOperName,a.dtOperDate,a.vcDeptID,a.vcComments from vwBusiLog a,tbAssociator b,tbCommCode c where a.vcCardID=b.vcCardID and a.vcOperType=c.vcCommCode and c.vcCommSign='OP' and a.dtOperDate between '" +  htPara["strBegin"].ToString() + "' and '" +  htPara["strEnd"].ToString() + " 23:59:59' ";
				if(strCondition!="")
				{
					sql1+=" and " + strCondition;
				}
				sql1+=" order by a.dtOperDate";
				dtBusiLog=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				return dtBusiLog;
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
		}

		public DataTable GetTopQuery(Hashtable htPara,string strtype)
		{
            DataTable dttop = new DataTable();
            try
            {
                string sql1 = "";
                if (strtype == "0")
                {
                    if (htPara["strDeptID"].ToString() != "")
                    {
                        sql1 = "select b.vcGoodsID,c.vcCommName as vcCommName,b.vcGoodsName,sum(iCount) as salecount,sum(nFee) as nFee from vwConsItem a,tbGoods b,tbCommCode c where c.vcCommCode=b.vcGoodsType and  a.cFlag='0' and a.vcGoodsID=b.vcGoodsID and a.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' and a.vcDeptID like '" + htPara["strDeptID"].ToString() + "'    group by c.vcCommName,b.vcGoodsID,b.vcGoodsName order by b.vcGoodsID,c.vcCommName,b.vcGoodsName";
                    }
                    else
                    {
                        sql1 = "select b.vcGoodsID,c.vcCommName as vcCommName,b.vcGoodsName,sum(iCount) as salecount,sum(nFee) as nFee from vwConsItem a,tbGoods b,tbCommCode c where c.vcCommCode=b.vcGoodsType and  a.cFlag='0' and a.vcGoodsID=b.vcGoodsID and a.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59'   group by c.vcCommName,b.vcGoodsID,b.vcGoodsName order by b.vcGoodsID,c.vcCommName,b.vcGoodsName";
                    }

                }
                else
                {
                    if (htPara["strDeptID"].ToString() != "")
                    {
                        sql1 = "select a.vcCardID,vcAssName,sum(nFee) as salefee from vwConsItem a,tbAssociator b where   a.cFlag='0' and b.vcAssType<>'AT002' and a.vcCardID=b.vcCardID and a.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' and a.vcDeptID like '" + htPara["strDeptID"].ToString() + "' and a.vcCardID<>'V9999' group by a.vcCardID,vcAssName order by sum(nFee) desc";
                    }
                    else
                    {
                        sql1 = "select a.vcCardID,vcAssName,sum(nFee) as salefee from vwConsItem a,tbAssociator b where a.cFlag='0' and b.vcAssType<>'AT002' and a.vcCardID=b.vcCardID and a.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' and a.vcCardID<>'V9999' group by a.vcCardID,vcAssName order by sum(nFee) desc";
                    }

                }
                dttop = SqlHelper.ExecuteDataTable(con, CommandType.Text, sql1);
                return dttop;
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
		}

        public DataSet BusiIncomeReport(Hashtable htPara)
        {
            DataSet dsincome = new DataSet();
            try
            {
                string sql1 = "exec sp_BusiIncomeReport '" + htPara["strDeptID"].ToString() + "','" + htPara["strBegin"].ToString() + "','" + htPara["strEnd"].ToString() + "','" + htPara["strYestoday"].ToString() + "'";
                SqlHelper.ExecuteNonQuery(con, CommandType.Text, sql1);

                string sql2 = "select Type,REP1,REP2,REP3,REP4,REP5,REP6,REP7 from tbBusiIncomeReport where vcDateZoom='" + htPara["strBegin"].ToString() + htPara["strEnd"].ToString() + "' and vcDeptID='" + htPara["strDeptID"].ToString() + "' and Type not like '%-L' and Type not like '%-O' and Type not like '%-X' order by ReNo";
                DataTable dtTmp = SqlHelper.ExecuteDataTable(con, CommandType.Text, sql2);
                dtTmp.TableName = "AllIncome";
                dsincome.Tables.Add(dtTmp);

                dtTmp = new DataTable();
                string sql3 = "select Type,REP1,REP2,REP3,REP4,REP5,REP6,REP7 from tbBusiIncomeReport where vcDateZoom='" + htPara["strBegin"].ToString() + htPara["strEnd"].ToString() + "' and vcDeptID='" + htPara["strDeptID"].ToString() + "' and Type like '%-L' order by ReNo";
                dtTmp = SqlHelper.ExecuteDataTable(con, CommandType.Text, sql3);
                dtTmp.TableName = "LocalIncome";
                dsincome.Tables.Add(dtTmp);

                dtTmp = new DataTable();
                string sql4 = "select Type,REP1,REP2,REP3,REP4,REP5,REP6,REP7 from tbBusiIncomeReport where vcDateZoom='" + htPara["strBegin"].ToString() + htPara["strEnd"].ToString() + "' and vcDeptID='" + htPara["strDeptID"].ToString() + "' and Type like '%-O' order by ReNo";
                dtTmp = SqlHelper.ExecuteDataTable(con, CommandType.Text, sql4);
                dtTmp.TableName = "OtherIncome";
                dsincome.Tables.Add(dtTmp);

                dtTmp = new DataTable();
                string sql5 = "select Type,REP1,REP2,REP3,REP4,REP5,REP6,REP7 from tbBusiIncomeReport where vcDateZoom='" + htPara["strBegin"].ToString() + htPara["strEnd"].ToString() + "' and vcDeptID='" + htPara["strDeptID"].ToString() + "' and Type like '%-X' order by ReNo";
                dtTmp = SqlHelper.ExecuteDataTable(con, CommandType.Text, sql5);
                dtTmp.TableName = "LocalToOtherIncome";
                dsincome.Tables.Add(dtTmp);

                return dsincome;
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
        }

		public DataTable GetAssInfo(Hashtable htPara)
		{
			DataTable dtAss=new DataTable();
			try
			{
				string strCondition="";
				if(htPara["strCardID"].ToString()!=""&&htPara["strCardID"].ToString()!="*")
				{
					strCondition=" vcCardID like '" + htPara["strCardID"].ToString() + "%'";
				}
				if(htPara["strAssName"].ToString()!=""&&htPara["strAssName"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" vcAssName like '%" + htPara["strAssName"].ToString() + "%'";
					}
					else
					{
						strCondition=strCondition + " and vcAssName like '%" + htPara["strAssName"].ToString() + "%'";
					}
				}
				if(htPara["strAssType"].ToString()!=""&&htPara["strAssType"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" vcAssType ='" + htPara["strAssType"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and vcAssType = '" + htPara["strAssType"].ToString() + "'";
					}
				}
                if (htPara["strPhone"].ToString() != "" && htPara["strPhone"].ToString() != "*")
                {
                    if (strCondition == "")
                    {
                        strCondition = " vcLinkPhone  like '%" + htPara["strPhone"].ToString() + "%'";
                    }
                    else
                    {
                        strCondition = strCondition + " and vcLinkPhone like '%" + htPara["strPhone"].ToString() + "%'";
                    }
                }
				if(htPara["strAssState"].ToString()!=""&&htPara["strAssState"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" vcAssState ='" + htPara["strAssState"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and vcAssState = '" + htPara["strAssState"].ToString() + "'";
					}
				}
				if(htPara["strDeptID"].ToString()!=""&&htPara["strDeptID"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" vcDeptID ='" + htPara["strDeptID"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and vcDeptID = '" + htPara["strDeptID"].ToString() + "'";
					}
				}

				string sql1="select iAssID,[vcCardID], [vcAssName], [vcLinkPhone], [vcSpell], [vcAssType], [vcAssState], [nCharge], [iIgValue], [vcDeptID], [dtCreateDate],[vcAssNbr], [vcLinkAddress], [vcEmail], [dtOperDate], [vcComments]  from tbAssociator where vcCardID<>'V9999' and vcAssType<>'AT999'";
				sql1+=" and dtCreateDate between '"+htPara["strBeginDate"].ToString()+"' and '"+htPara["strEndDate"].ToString()+" 23:59:59'";
				if(strCondition!="")
				{
					sql1+=" and " + strCondition;
				}
				sql1+=" order by dtCreateDate desc";
				dtAss=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);

				return dtAss;
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
		}

		public DataTable GetAssDetailInfo(string strAssid,string strCardID)
		{
            string strsql = "select a.*,substring(b.vcComments,1,1) as vcAssTypeDisp from tbAssociator a,tbCommCode b where a.iAssID=" + strAssid + " and a.vcCardID='" + strCardID + "' and b.vcCommSign='AT' and a.vcAssType=b.vcCommCode";
			DataTable dtout=SqlHelper.ExecuteDataTable(con,CommandType.Text,strsql);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			
			return dtout;
		}

		public int UpdateAssDetail(string strAssID,string strCardID,string strsqlset,CMSMStruct.LoginStruct ls1)
		{
			con.Open();
			using(SqlTransaction trans=con.BeginTransaction(IsolationLevel.ReadCommitted))
			{
				try
				{
					string sql1="update tbAssociator set " + strsqlset + ",dtOperDate=getdate() where iAssID=" + strAssID + " and vcCardID='"+strCardID+"'";
					SqlHelper.ExecuteNonQuery(con,trans,CommandType.Text,sql1);

					string sql2="insert into tbAssociatorSync select [vcCardID], [vcAssName], [vcSpell], [vcAssNbr], [vcLinkPhone], [vcLinkAddress], [vcEmail], [vcAssType], [vcAssState], [nCharge], [iIgValue], [vcCardFlag], [vcComments], [dtCreateDate], [dtOperDate], [vcDeptID], [vcCardSerial], 0 from tbAssociator where iAssID=" + strAssID + " and vcCardID='"+strCardID+"'";
					SqlHelper.ExecuteNonQuery(con,trans,CommandType.Text,sql2);

					string sql3="insert into tbAssociatorLog select [iAssID], [vcCardID], [vcAssName], [vcSpell], [vcAssNbr], [vcLinkPhone], [vcLinkAddress], [vcEmail], [vcAssType], [vcAssState], [nCharge], [iIgValue], [vcCardFlag], [vcComments], [dtCreateDate], [dtOperDate], [vcDeptID], '"+ls1.strOperName+"','"+ ls1.strDeptID+"', [vcCardSerial] from tbAssociator where iAssID=" + strAssID + " and vcCardID='"+strCardID+"'";
					SqlHelper.ExecuteNonQuery(con,trans,CommandType.Text,sql3);

					trans.Commit();
					return 1;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					clog.WriteLine(ex);
					return 0;
				}
				finally
				{

					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}
			}
		}

		public DataTable GetUpDownQuery(Hashtable htPara)
		{
			DataTable dtuplog=new DataTable();
			try
			{
				string sql1="";
				switch(htPara["strquerytype"].ToString())
				{
					case "0":
						sql1="select substring(vcFileName,1,5) as Dept,(case Type when 'CEN' then '会员数据-中心至分店' when 'MD' then '业务数据-分店至中心' when 'PARA' then '基本参数-中心至分店' when 'GOODS' then '商品数据-中心至分店' else Type end) as Type,vcFileName,FileSize,dtStartDate,dtFinDate from tbDataSoftUpdateLog where dtStartDate between '"+htPara["strBegin"].ToString()+"' and '"+htPara["strEnd"].ToString()+" 23:59:59' order by Type,dtStartDate desc";
						break;
					case "1":
						sql1="select substring(vcFileName,1,5) as Dept,'业务数据-分店至中心' as Type,vcFileName,FileSize,dtStartDate,dtFinDate from tbDataSoftUpdateLog where Type='MD' and vcFileName like '%"+ htPara["strDept"].ToString() +"%' and dtStartDate between '"+htPara["strBegin"].ToString()+"' and '"+htPara["strEnd"].ToString()+" 23:59:59' order by dtStartDate desc,Type";
						break;
					case "2":
						sql1="select substring(vcFileName,1,5) as Dept,'会员数据-中心至分店' as Type,vcFileName,FileSize,dtStartDate,dtFinDate from tbDataSoftUpdateLog where Type='CEN' and dtStartDate between '"+htPara["strBegin"].ToString()+"' and '"+htPara["strEnd"].ToString()+" 23:59:59' order by dtStartDate desc";
						break;
					case "3":
						sql1="select substring(vcFileName,1,5) as Dept,'基本参数-中心至分店' as Type,vcFileName,FileSize,dtStartDate,dtFinDate from tbDataSoftUpdateLog where Type='PARA' and dtStartDate between '"+htPara["strBegin"].ToString()+"' and '"+htPara["strEnd"].ToString()+" 23:59:59' order by dtStartDate desc";
						break;
					case "4":
						sql1="select substring(vcFileName,1,5) as Dept,'商品数据-中心至分店' as Type,vcFileName,FileSize,dtStartDate,dtFinDate from tbDataSoftUpdateLog where Type='GOODS' and dtStartDate between '"+htPara["strBegin"].ToString()+"' and '"+htPara["strEnd"].ToString()+" 23:59:59' order by dtStartDate desc";
						break;
				}
				dtuplog=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				return dtuplog;
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
		}

		public DataTable GetDailyCashQuery(Hashtable htPara)
		{
			DataTable dtCons=new DataTable();
			try
			{
				string strCondition="";
				if(htPara["strDeptID"].ToString()!=""&&htPara["strDeptID"].ToString()!="*")
				{
					strCondition=" and vcDeptID='" + htPara["strDeptID"].ToString() + "'";
				}
				if(htPara["strOperName"].ToString()!=""&&htPara["strOperName"].ToString()!="*")
				{
					strCondition=strCondition + " and vcOperName = '" + htPara["strOperName"].ToString() + "'";
				}

                string sql1 = "select vcOperName,vcConsType,count(*) as ConsCount,isnull(sum(nPay-nBalance),0) as ConsFee from vwBill where dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' " + strCondition + " and iSerial not in(select distinct iSerial from vwConsItem where cFlag='9' and dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' " + strCondition + ") group by vcOperName,vcConsType";
                sql1 += " union all select vcOperName,'Fill' as vcConsType,count(*) as ConsCount,isnull(sum(nFillFee),0) as ConsFee from vwFillFee where nFillFee>0 and dtFillDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' " + strCondition + " and vcComments<>'银行卡' and vcComments not like '%补卡%' and vcComments not like '%补充值%' and vcComments not like '%消费撤消%' and vcComments not like '回收卡%' and vcComments not like '合并%' and vcComments not like '充值撤消%'  group by vcOperName";
				sql1+= " union all select vcOperName,'FillBank' as vcConsType,count(*) as ConsCount,isnull(sum(nFillFee),0) as ConsFee from vwFillFee where dtFillDate between '"+htPara["strBegin"].ToString()+"' and '"+htPara["strEnd"].ToString()+" 23:59:59' "+strCondition+" and nFillFee>0 and vcComments='银行卡' and vcComments not like '%补卡%' and vcComments not like '%补充值%' and vcComments not like '%消费撤消%' and vcComments not like '回收卡%' group by vcOperName";
				sql1+= " union all select vcOperName,'CradRoll',count(1) as ConsCount,isnull(sum(nFillFee),0)as ConsFee from vwFillFee where dtFillDate between '"+htPara["strBegin"].ToString()+"' and '"+htPara["strEnd"].ToString()+" 23:59:59' "+strCondition+" and vcComments  like '回收卡%' group by vcOperName";
				sql1+=" order by vcOperName,vcConsType";
	
				dtCons=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				return dtCons;
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
		}

		public DataTable GetExpInvList()
		{
			DataTable dtexplist=new DataTable();
			try
			{
				string strsql="select d.cnvcWhName,a.cnvcInvCode,b.cnvcInvName,c.cnvcComunitName,cast(a.cnnQuantity/c.cniChangRate as numeric(18,4)) as cnnQuantity,convert(char(10),a.cndMdate,120) as cndMdate,convert(char(10),a.cndExpDate,120) as cndExpDate from tbCurrentStock a,tbInventory b,tbComputationUnit c,tbWareHouse d";
				strsql+=" where a.cnvcInvCode=b.cnvcInvCode and b.cnvcSTComUnitCode=c.cnvcComunitCode and (convert(char(8),cndExpDate,112)<=convert(char(8),getdate(),112) or datediff(d,getdate(),cndExpDate)<=b.cnnDue)";
				strsql+=" and a.cnvcWhCode=d.cnvcWhCode and a.cnnAvaQuantity>0 order by cndExpDate";

				dtexplist=SqlHelper.ExecuteDataTable(con,CommandType.Text,strsql);
				return dtexplist;
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
		}

		public DataTable GetSpecConsQuery(Hashtable htPara)
		{
			DataTable dtCons=new DataTable();
			try
			{
				string strCondition="";
				if(htPara["strConsType"].ToString()!=""&&htPara["strConsType"].ToString()!="*")
				{
					strCondition=" a.vcConsType='" + htPara["strConsType"].ToString() + "'";
				}
				if(htPara["strOperName"].ToString()!=""&&htPara["strOperName"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" a.vcOperName = '" + htPara["strOperName"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and a.vcOperName = '" + htPara["strOperName"].ToString() + "'";
					}
				}
				if(htPara["strDeptID"].ToString()!="")
				{
					if(strCondition=="")
					{
						strCondition=" a.vcDeptID='" + htPara["strDeptID"].ToString() + "'";
					}
					else
					{
						strCondition=strCondition + " and a.vcDeptID = '" + htPara["strDeptID"].ToString() + "'";
					}
				}
                string sql1 = "select a.vcConsType,c.vcGoodsName,sum(b.iCount) as tolCount,sum(b.iCount*c.nPrice) as tolfee,sum(b.nFee) as tolcash from tbBillOther a,tbConsItemOther b,tbGoods c where a.vcDeptID=b.vcDeptID and a.iSerial=b.iSerial and a.vcConsType  in('PT003','PT004','PT005','PT006','PT007')";
				sql1+=" and b.vcGoodsID=c.vcGoodsID and b.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' and b.cFlag='0' ";
				if(strCondition!="")
				{
					sql1+=" and " + strCondition;
				}
				sql1+=" group by a.vcConsType,c.vcGoodsName order by a.vcConsType";
				dtCons=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				return dtCons;
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
		}

        public DataTable GetIGQuery(Hashtable htPara)
        {
            DataTable dtCons = new DataTable();
            try
            {
                string strCondition = "";
                if (htPara["strCardID"].ToString() != "" && htPara["strCardID"].ToString() != "*")
                {
                    strCondition = " a.vcCardID='" + htPara["strCardID"].ToString() + "'";
                }
                if (htPara["strAssName"].ToString() != "" && htPara["strAssName"].ToString() != "*")
                {
                    if (strCondition == "")
                    {
                        strCondition = " b.vcAssName like '%" + htPara["strAssName"].ToString() + "%'";
                    }
                    else
                    {
                        strCondition = strCondition + " and b.vcAssName = '%" + htPara["strAssName"].ToString() + "%'";
                    }
                }

                if (htPara["strOperName"].ToString() != "")
                {
                    if (strCondition == "")
                    {
                        strCondition = " a.vcOperName='" + htPara["strOperName"].ToString() + "'";
                    }
                    else
                    {
                        strCondition = strCondition + " and a.vcOperName = '" + htPara["strOperName"].ToString() + "'";
                    }
                }
                if (htPara["strDeptID"].ToString() != "")
                {
                    if (strCondition == "")
                    {
                        strCondition = " a.vcDeptID='" + htPara["strDeptID"].ToString() + "'";
                    }
                    else
                    {
                        strCondition = strCondition + " and a.vcDeptID = '" + htPara["strDeptID"].ToString() + "'";
                    }
                }

                string sql1 = "SELECT a.iSerial,b.vcAssName,b.vcAssType,a.vcCardID,a.iIgLast,a.iIgGet,a.iIgArrival,a.vcComments,a.dtIgDate,a.vcOperName,a.vcDeptID  FROM [tbIntegralLogOther] a,tbAssociator b where a.vcCardID=b.vcCardID  and a.dtIgDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' ";

                if (strCondition != "")
                {
                    sql1 += " and " + strCondition;
                }

                sql1 += " order by a.dtIgDate,a.vcCardID";
                dtCons = SqlHelper.ExecuteDataTable(con, CommandType.Text, sql1);
                return dtCons;
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
        }

        public DataTable GetCardRecycle(Hashtable htPara)
        {
            DataTable dtAss = new DataTable();
            try
            {
                string strCondition = "";
                if (htPara["strCardID"].ToString() != "" && htPara["strCardID"].ToString() != "*")
                {
                    strCondition = " vcCardID like '" + htPara["strCardID"].ToString() + "%'";
                }
                if (htPara["strAssName"].ToString() != "" && htPara["strAssName"].ToString() != "*")
                {
                    if (strCondition == "")
                    {
                        strCondition = " vcAssName like '%" + htPara["strAssName"].ToString() + "%'";
                    }
                    else
                    {
                        strCondition = strCondition + " and vcAssName like '%" + htPara["strAssName"].ToString() + "%'";
                    }
                }
                if (htPara["strLinkPhone"].ToString() != "" && htPara["strLinkPhone"].ToString() != "*")
                {
                    if (strCondition == "")
                    {
                        strCondition = " vcLinkPhone like '%" + htPara["strLinkPhone"].ToString() + "%'";
                    }
                    else
                    {
                        strCondition = strCondition + " and vcLinkPhone like '%" + htPara["strLinkPhone"].ToString() + "%'";
                    }
                }
                if (htPara["strDeptID"].ToString() != "" && htPara["strDeptID"].ToString() != "*")
                {
                    if (strCondition == "")
                    {
                        strCondition = " vcDeptID ='" + htPara["strDeptID"].ToString() + "'";
                    }
                    else
                    {
                        strCondition = strCondition + " and vcDeptID = '" + htPara["strDeptID"].ToString() + "'";
                    }
                }

                string sql1 = "select [vcCardID], [vcAssName], [vcLinkPhone], [vcSpell], [vcAssType], [vcAssState], [nCharge], [iIgValue], [vcDeptID], [dtCreateDate],[vcAssNbr], [vcLinkAddress], [vcEmail], [dtOperDate], [vcComments]  from tbAssociator where vcCardID<>'V9999' and vcAssType<>'AT999' and vcAssState='3'";
                sql1 += " and dtOperDate between '" + htPara["strBeginDate"].ToString() + "' and '" + htPara["strEndDate"].ToString() + " 23:59:59'";
                if (strCondition != "")
                {
                    sql1 += " and " + strCondition;
                }
                sql1 += " order by dtOperDate desc";
                dtAss = SqlHelper.ExecuteDataTable(con, CommandType.Text, sql1);

                return dtAss;
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
        }

        public DataTable GetConsQueryMd(Hashtable htPara)
        {
            DataTable dtCons = new DataTable();
            try
            {
                string strCondition = "";
                if (htPara["strCardID"].ToString() != "" && htPara["strCardID"].ToString() != "*")
                {
                    strCondition = " a.vcCardID='" + htPara["strCardID"].ToString() + "'";
                }
                if (htPara["strAssName"].ToString() != "" && htPara["strAssName"].ToString() != "*")
                {
                    if (strCondition == "")
                    {
                        strCondition = " c.vcAssName  like  '%" + htPara["strAssName"].ToString() + "%'";
                    }
                    else
                    {
                        strCondition = strCondition + " and c.vcAssName like  '%" + htPara["strAssName"].ToString() + "%'";
                    }
                }
                if (htPara["strSerial"].ToString() != "" && htPara["strSerial"].ToString() != "*")
                {
                    if (strCondition == "")
                    {
                        strCondition = " a.iSerial like  '%" + htPara["strSerial"].ToString() + "%'";
                    }
                    else
                    {
                        strCondition = strCondition + " and a.iSerial like '%" + htPara["strSerial"].ToString() + "%'";
                    }
                }
                if (htPara["strAssType"].ToString() != "")
                {
                    if (strCondition == "")
                    {
                        strCondition = " c.vcAssType='" + htPara["strAssType"].ToString() + "'";
                    }
                    else
                    {
                        strCondition = strCondition + " and c.vcAssType = '" + htPara["strAssType"].ToString() + "'";
                    }
                }
                if (htPara["strOperName"].ToString() != "")
                {
                    if (strCondition == "")
                    {
                        strCondition = " a.vcOperName='" + htPara["strOperName"].ToString() + "'";
                    }
                    else
                    {
                        strCondition = strCondition + " and a.vcOperName = '" + htPara["strOperName"].ToString() + "'";
                    }
                }
                if (htPara["strDeptID"].ToString() != "")
                {
                    if (strCondition == "")
                    {
                        strCondition = " a.vcDeptID='" + htPara["strDeptID"].ToString() + "'";
                    }
                    else
                    {
                        strCondition = strCondition + " and a.vcDeptID = '" + htPara["strDeptID"].ToString() + "'";
                    }
                }
                if (htPara["strDeptIdMd"].ToString() != "")
                {
                    if (strCondition == "")
                    {
                        strCondition = " c.vcDeptID='" + htPara["strDeptIdMd"].ToString() + "'";
                    }
                    else
                    {
                        strCondition = strCondition + " and c.vcDeptID = '" + htPara["strDeptIdMd"].ToString() + "'";
                    }
                }
                if (htPara["strBillType"].ToString() != "")
                {
                    if (strCondition == "")
                    {
                        strCondition = " b.vcConsType='" + htPara["strBillType"].ToString() + "'";
                    }
                    else
                    {
                        strCondition = strCondition + " and b.vcConsType = '" + htPara["strBillType"].ToString() + "'";
                    }
                }
                string sql1 = "select a.iSerial,c.vcAssName,c.vcAssType,a.vcCardID,c.vcDeptId as vcLocalDeptId,a.vcGoodsID,a.nPrice,a.iCount,a.nFee,b.vcConsType,a.vcComments,(case a.cFlag when '0' then '正常消费' when '9' then '已撤消' else a.cFlag end) as cFlag,a.dtConsDate,a.vcOperName,a.vcDeptID";
                sql1 += " from vwConsItem a,vwBill b,tbAssociator c";
                sql1 += " where a.iSerial=b.iSerial and a.vcCardID=b.vcCardID and a.vcDeptID=b.vcDeptID and a.vcCardID=c.vcCardID and a.cFlag='" + htPara["strConsFlag"].ToString() + "' and a.dtConsDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' ";
                if (strCondition != "")
                {
                    sql1 += " and " + strCondition;
                }
                sql1 += " order by a.dtConsDate";
                dtCons = SqlHelper.ExecuteDataTable(con, CommandType.Text, sql1);
                return dtCons;
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

        }

        public DataTable GetFillQueryMd(Hashtable htPara)
        {
            DataTable dtCons = new DataTable();
            try
            {
                string strCondition = "";
                if (htPara["strCardID"].ToString() != "" && htPara["strCardID"].ToString() != "*")
                {
                    strCondition = " a.vcCardID='" + htPara["strCardID"].ToString() + "'";
                }
                if (htPara["strAssName"].ToString() != "" && htPara["strAssName"].ToString() != "*")
                {
                    if (strCondition == "")
                    {
                        strCondition = " b.vcAssName like '%" + htPara["strAssName"].ToString() + "%'";
                    }
                    else
                    {
                        strCondition = strCondition + " and b.vcAssName = '%" + htPara["strAssName"].ToString() + "%'";
                    }
                }
                if (htPara["strAssType"].ToString() != "")
                {
                    if (strCondition == "")
                    {
                        strCondition = " b.vcAssType='" + htPara["strAssType"].ToString() + "'";
                    }
                    else
                    {
                        strCondition = strCondition + " and b.vcAssType = '" + htPara["strAssType"].ToString() + "'";
                    }
                }
                if (htPara["strOperName"].ToString() != "")
                {
                    if (strCondition == "")
                    {
                        strCondition = " a.vcOperName='" + htPara["strOperName"].ToString() + "'";
                    }
                    else
                    {
                        strCondition = strCondition + " and a.vcOperName = '" + htPara["strOperName"].ToString() + "'";
                    }
                }
                if (htPara["strDeptID"].ToString() != "")
                {
                    if (strCondition == "")
                    {
                        strCondition = " a.vcDeptID='" + htPara["strDeptID"].ToString() + "'";
                    }
                    else
                    {
                        strCondition = strCondition + " and a.vcDeptID = '" + htPara["strDeptID"].ToString() + "'";
                    }
                }
                if (htPara["strDeptIdMd"].ToString() != "")
                {
                    if (strCondition == "")
                    {
                        strCondition = " b.vcDeptID='" + htPara["strDeptIdMd"].ToString() + "'";
                    }
                    else
                    {
                        strCondition = strCondition + " and b.vcDeptID = '" + htPara["strDeptIdMd"].ToString() + "'";
                    }
                }
                if (htPara["strFillType"].ToString() != "")
                {
                    if (strCondition == "")
                    {
                        if (htPara["strFillType"].ToString() == "Norm")
                        {
                            strCondition = " (a.vcComments='' or a.vcComments='银行卡')";
                        }
                        else
                        {
                            strCondition = " a.vcComments like '" + htPara["strFillType"].ToString() + "'";
                        }
                    }
                    else
                    {
                        if (htPara["strFillType"].ToString() == "Norm")
                        {
                            strCondition = strCondition + " and (a.vcComments='' or a.vcComments='银行卡')";
                        }
                        else
                        {
                            strCondition = strCondition + " and a.vcComments like '" + htPara["strFillType"].ToString() + "'";
                        }
                    }
                }
                string sql1 = "select a.iSerial,b.vcAssName,b.vcAssType,a.vcCardID,b.vcDeptId as vcLocalDeptId,a.nFillFee,a.nFillProm,a.nFeeLast,a.nFeeCur,a.vcComments,a.dtFillDate,a.vcOperName,a.vcDeptID from vwFillFee a,tbAssociator b where a.vcCardID=b.vcCardID  and a.dtFillDate between '" + htPara["strBegin"].ToString() + "' and '" + htPara["strEnd"].ToString() + " 23:59:59' ";

                if (strCondition != "")
                {
                    sql1 += " and " + strCondition;
                }
                if (htPara["strFillType"].ToString() == "Norm")
                {
                    sql1 += " and a.nFillFee>0 order by a.dtFillDate desc";
                }
                else
                {
                    sql1 += " order by a.dtFillDate,a.vcCardID";
                }
                dtCons = SqlHelper.ExecuteDataTable(con, CommandType.Text, sql1);
                return dtCons;
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
        }
	}
}
