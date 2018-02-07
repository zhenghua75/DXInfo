using System;
using System.Data;
using System.Data.SqlClient;
using CommCenter;
using System.Collections;

namespace DataAccess
{
	/// <summary>
	/// Summary description for EmpAcc.
	/// </summary>
	public class EmpAcc
	{
		SqlDataReader drr;
		SqlConnection con;
		AMSLog clog=new AMSLog();

		public EmpAcc(string strcons)
		{
			//
			// TODO: Add constructor logic here
			//
			con=new SqlConnection(strcons);
		}

		public DataTable GetEmployees(Hashtable htPara)
		{
			DataTable dtEmp=new DataTable();
			try
			{
				string strCondition="";
				if(htPara["strCardID"].ToString()!=""&&htPara["strCardID"].ToString()!="*")
				{
					strCondition=" vcCardID like '" + htPara["strCardID"].ToString() + "%'";
				}
				if(htPara["strEmpName"].ToString()!=""&&htPara["strEmpName"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" vcEmpName like '%" + htPara["strEmpName"].ToString() + "%'";
					}
					else
					{
						strCondition=strCondition + " and vcEmpName='" + htPara["strEmpName"].ToString() + "'";
					}
				}
				if(htPara["strDeptID"].ToString()!=""&&htPara["strDeptID"].ToString()!="*")
				{
					if(strCondition=="")
					{
						strCondition=" vcDeptID like '%" + htPara["strDeptID"].ToString() + "%'";
					}
					else
					{
						strCondition=strCondition + " and vcDeptID='" + htPara["strDeptID"].ToString() + "'";
					}
				}
				string sql1="select vcCardID,vcEmpName,vcSex,vcEmpNbr,convert(char(10),dtInDate,120) as dtInDate,vcDegree,vcLinkPhone,vcAddress,vcOfficer,vcDeptID,vcFlag,vcComments,dtOperDate from tbEmployee where vcFlag='"+htPara["strstate"].ToString()+"'";
				if(strCondition!="")
				{
					sql1=sql1 + " and " + strCondition + " order by vcDeptID,vcCardID";
				}
				dtEmp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
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
			return dtEmp;
		}

		public string getEmpCardID(string strCardID)
		{
			string strsql="select count(*) from tbEmployee where vcCardID='" + strCardID + "'";
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

		public string getEmpName(string strEmpName)
		{
			string strsql="select count(*) from tbEmployee where vcEmpName='" + strEmpName + "'";
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

		public int InsertEmployee(CMSMStruct.EmployeeStruct esnew)
		{
			string sql1="insert into tbEmployee values('" + esnew.strCardID+"','"+esnew.strEmpName+"','"+esnew.strSex+"','"+esnew.strEmpNbr+"','"+esnew.strInDate+"','"+esnew.strDegree+"','"+esnew.strLinkPhone+"','"+esnew.strAddress+"','','"+esnew.strDeptID+"','0','"+esnew.strComments+"',getdate())";
			int recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
			if(con.State==ConnectionState.Open)
			{
				con.Close();
			}
			return recount;
		}

		public int ModEmployee(CMSMStruct.EmployeeStruct esold,CMSMStruct.EmployeeStruct esnew)
		{
			string strCondition="";
			if(esold.strEmpName!=esnew.strEmpName)
			{
				strCondition=" vcEmpName='"+esnew.strEmpName+"'";
			}
			if(esold.strSex!=esnew.strSex)
			{
				if(strCondition!="")
				{
					strCondition+=",vcSex='"+esnew.strSex+"'";
				}
				else
				{
					strCondition+=" vcSex='"+esnew.strSex+"'";
				}
			}
			if(esold.strEmpNbr!=esnew.strEmpNbr)
			{
				if(strCondition!="")
				{
					strCondition+=",vcEmpNbr='"+esnew.strEmpNbr+"'";
				}
				else
				{
					strCondition+=" vcEmpNbr='"+esnew.strEmpNbr+"'";
				}
			}
			if(esold.strDegree!=esnew.strDegree)
			{
				if(strCondition!="")
				{
					strCondition+=",vcDegree='"+esnew.strDegree+"'";
				}
				else
				{
					strCondition+=" vcDegree='"+esnew.strDegree+"'";
				}
			}
			if(esold.strLinkPhone!=esnew.strLinkPhone)
			{
				if(strCondition!="")
				{
					strCondition+=",vcLinkPhone='"+esnew.strLinkPhone+"'";
				}
				else
				{
					strCondition+=" vcLinkPhone='"+esnew.strLinkPhone+"'";
				}
			}
			if(esold.strAddress!=esnew.strAddress)
			{
				if(strCondition!="")
				{
					strCondition+=",vcAddress='"+esnew.strAddress+"'";
				}
				else
				{
					strCondition+=" vcAddress='"+esnew.strAddress+"'";
				}
			}
			if(esold.strDeptID!=esnew.strDeptID)
			{
				if(strCondition!="")
				{
					strCondition+=",vcDeptID='"+esnew.strDeptID+"'";
				}
				else
				{
					strCondition+=" vcDeptID='"+esnew.strDeptID+"'";
				}
			}
			if(esold.strFlag!=esnew.strFlag)
			{
				if(strCondition!="")
				{
					strCondition+=",vcFlag='"+esnew.strFlag+"'";
				}
				else
				{
					strCondition+=" vcFlag='"+esnew.strFlag+"'";
				}
			}
			if(esold.strOfficer!=esnew.strOfficer)
			{
				if(strCondition!="")
				{
					strCondition+=",vcOfficer='"+esnew.strOfficer+"'";
				}
				else
				{
					strCondition+=" vcOfficer='"+esnew.strOfficer+"'";
				}
			}
			if(esold.strComments!=esnew.strComments)
			{
				if(strCondition!="")
				{
					strCondition+=",vcComments='"+esnew.strComments+"'";
				}
				else
				{
					strCondition+=" vcComments='"+esnew.strComments+"'";
				}
			}
			int recount=0;
			if(strCondition!="")
			{
				string sql1="update tbEmployee set "+strCondition+" where vcCardID='"+esold.strCardID+"'";
				recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return recount;
		}

		public DataTable GetEmpInfo(string strCardID)
		{
			DataTable dtemp=new DataTable();
			try
			{
				string sql1="SELECT [vcCardID], [vcEmpName], [vcSex], [vcEmpNbr], convert(char(10),[dtInDate],120) as dtInDate, [vcDegree], [vcLinkPhone], [vcAddress],[vcOfficer], [vcDeptID],[vcFlag], [vcComments] FROM [tbEmployee] where vcCardID='" + strCardID+"'";
				dtemp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
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
			return dtemp;
		}

		public DataTable GetEmpInfoByName(string strEmpName)
		{
			DataTable dtemp=new DataTable();
			try
			{
				string sql1="select vcCardID,vcEmpName,b.vcCommName from tbEmployee a,tbCommCode b where a.vcFlag='0' and vcEmpName='"+strEmpName+"' and b.vcCommSign='OF' and a.vcOfficer=b.vcCommCode";
				dtemp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
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
			return dtemp;
		}

		public DataTable GetAllEmpName(string strDeptID,string strSchID)
		{
			DataTable dtemp=new DataTable();
			try
			{
				string sql1="SELECT vcEmpName FROM tbEmployee where vcFlag='0' and vcDeptID='"+strDeptID+"' and vcCardID not in(select vcCardID from tbEmpSchLog where vcSchID='"+strSchID+"')";
				dtemp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
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
			return dtemp;
		}

		public int IsEmpSchAllDayWork(string strSchID,string strEmpName,string strClass)
		{
			int strreturn=0;
			try
			{
				string sql1="";
				if(strClass=="全日班")
				{
					sql1="select count(*) from tbEmpSchLog where vcSchID='"+strSchID+"' and vcEmpName='"+strEmpName+"' and vcClass<>'全日班'";
				}
				else
				{
					sql1="select count(*) from tbEmpSchLog where vcSchID='"+strSchID+"' and vcEmpName='"+strEmpName+"' and vcClass='全日班'";
				}
				drr=SqlHelper.ExecuteReader(con,CommandType.Text,sql1);
				drr.Read();
				strreturn=int.Parse(drr[0].ToString());
				drr.Close();
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return strreturn;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return strreturn;
		}

		public DataSet GetEmpSchList(Hashtable htPara)
		{
			DataSet dsemp=new DataSet();
			try
			{
				string sql1="";
				if(htPara["strClass"].ToString()=="全日班")
				{
					sql1="select vcEmpName from tbEmployee where vcFlag='0' and vcDeptID='"+htPara["strDeptID"].ToString()+"' and vcOfficer='"+htPara["strOfficerID"].ToString() +"'";
					sql1+=" and vcEmpName not in(select vcEmpName from tbEmpSchLog where vcSchID='"+htPara["strSchID"].ToString()+"' and vcDeptName='"+htPara["strDeptName"].ToString()+"' and vcClass<>'"+htPara["strClass"].ToString()+"' and vcEmpOF='"+htPara["strOfficer"].ToString()+"')";
				}
				else
				{
					sql1="select vcEmpName from tbEmployee where vcFlag='0' and vcDeptID='"+htPara["strDeptID"].ToString()+"' and vcOfficer='"+htPara["strOfficerID"].ToString() +"'";
					sql1+=" and vcEmpName not in(select vcEmpName from tbEmpSchLog where vcSchID='"+htPara["strSchID"].ToString()+"' and vcDeptName='"+htPara["strDeptName"].ToString()+"' and vcClass='全日班' and vcEmpOF='"+htPara["strOfficer"].ToString()+"')";
				}
				DataTable dtemp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				dtemp.TableName="tbEmployee";
				dsemp.Tables.Add(dtemp);

				string sql2="select vcEmpName from tbEmpSchLog where vcSchID='"+htPara["strSchID"].ToString()+"' and vcDeptName='"+htPara["strDeptName"].ToString()+"' and vcClass='"+htPara["strClass"].ToString()+"' and vcEmpOF='"+htPara["strOfficer"].ToString()+"'";
				dtemp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql2);
				dtemp.TableName="tbEmpSchLog";
				dsemp.Tables.Add(dtemp);
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
			return dsemp;
		}

		public DataTable GetEmpManager(string strDeptID)
		{
			DataTable dtemp=new DataTable();
			try
			{
				string sql1="select vcEmpName from tbEmployee where vcFlag='0' and vcOfficer='OF002' and vcDeptID='"+ strDeptID+"'";
				dtemp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
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
			return dtemp;
		}

		public int SchedEmpDaily(Hashtable htPara,ArrayList alsched)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="delete from tbEmpSchLog where vcSchID='"+htPara["strSchID"].ToString()+"' and vcDeptName='"+htPara["strDeptName"].ToString()+"' and vcClass='"+htPara["strClass"].ToString()+"' and vcEmpOF='"+htPara["strOfficer"].ToString()+"'";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

					string sql2="";
					for(int i=0;i<alsched.Count;i++)
					{
						sql2+="insert into tbEmpSchLog values('"+htPara["strSchID"].ToString()+"','"+htPara["strDeptName"].ToString()+"','"+htPara["strManager"].ToString()+"','','"+alsched[i].ToString()+"','"+htPara["strOfficer"].ToString()+"','"+htPara["strClass"].ToString()+"','"+htPara["strCheckInDate"].ToString()+"','"+htPara["strCheckOutDate"].ToString()+"');";
					}
					if(sql2!="")
					{
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);
					}

					string sql3="update tbEmpSchLog set vcCardID=a.vcCardID from tbEmployee a,tbEmpSchLog b where b.vcSchID='"+htPara["strSchID"].ToString()+"' and a.vcEmpName=b.vcEmpName and vcSchID=b.vcSchID and b.vcClass='"+htPara["strClass"].ToString()+"' and b.vcEmpOF='"+htPara["strOfficer"].ToString()+"'";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql3);

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

		public DataTable GetEmpSchLog(string strShcID)
		{
			DataTable dtShc=new DataTable();
			try
			{
				string strSchDate=strShcID.Substring(0,4).ToString()+strShcID.Substring(4,2).ToString()+strShcID.Substring(6,2).ToString();
				string sql1="select distinct vcDeptName,convert(char(10),dtCheckIn,120) as SchDate,'1' as IsSch from tbEmpSchLog where vcSchID='"+ strShcID+"'";
				sql1+=" union select distinct vcCommName as vcDeptName,'"+strSchDate+"' as SchDate,'0' as IsSch from tbCommCode where vcCommSign='MD'";
				sql1+=" and vcCommName not in(select distinct vcDeptName from tbEmpSchLog where vcSchID='"+ strShcID+"')";
				sql1+=" order by IsSch desc,vcDeptName";
				dtShc=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return dtShc;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtShc;
		}

		public DataTable GetEveryEmpSchLog(string strShcID,string strDeptID,string strDeptName)
		{
			DataTable dtShc=new DataTable();
			try
			{
				string sql1="select vcCardID,vcEmpName,b.vcCommName as vcEmpOF,'' as vcClass from tbEmployee a,tbCommCode b where vcDeptID='"+strDeptID+"' and vcFlag='0' and vcCardID+vcEmpName not in(select vcCardID+vcEmpName from tbEmpSchLog where vcSchID='"+strShcID+"' and vcDeptName='"+strDeptName+"') and b.vcCommSign='OF' and a.vcOfficer=b.vcCommCode";
				sql1+=" union select vcCardID,vcEmpName,vcEmpOF,vcClass from tbEmpSchLog where vcSchID='"+strShcID+"' and vcDeptName='"+strDeptName+"' order by vcCardID,vcEmpName,vcEmpOF,vcClass";
				dtShc=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return dtShc;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
			return dtShc;
		}

		public DataSet GetDeptSchDetail(string strDeptName,string strShcID)
		{
			DataSet dsout=new DataSet();
			try
			{
				DataTable dttmp=new DataTable();
				string sql1="select [vcSchID], [vcDeptName], [vcManager], [vcEmpName], [vcEmpOF], [vcClass], convert(char(19),[dtCheckIn],120) as dtCheckIn, convert(char(19),[dtCheckOut],120) as dtCheckOut from dbo.tbEmpSchLog where vcSchID='"+strShcID+"' and vcDeptName='"+strDeptName+"' order by vcEmpOF,vcClass";
				dttmp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				dttmp.TableName="SchDetail";
				dsout.Tables.Add(dttmp);

				dttmp=new DataTable();
//				string sql2="select c.vcCommName as vcOfName,b.vcCommName as vcClassName,b.vcCommCode from tbCommCode a,tbCommCode b,tbCommCode c where a.vcComments='IOTime' and b.vcCommSign='EC' and c.vcCommSign='OF'";
//				sql2+=" and substring(a.vcCommName,6,1)=b.vcCommCode and substring(a.vcCommName,1,5)=c.vcCommCode order by vcOfName,b.vcCommCode";

				string sql2="select b.vcCommName as vcEmpOF,a.vcCommName as vcClass,a.vcCommCode as vcIndex,'--' as dtCheckIn,'--' as dtCheckOut from tbCommCode a,tbCommCode b where a.vcCommSign='EC' and b.vcCommSign='OF' and b.vcCommName+a.vcCommName not in(select distinct vcEmpOF+vcClass from tbEmpSchLog a,tbCommCode b,tbCommCode c";
				sql2+=" where b.vcCommSign='EC' and c.vcCommSign='OF' and a.vcSchID='"+strShcID+"' and a.vcDeptName='"+strDeptName+"' and a.vcEmpOF=c.vcCommName and a.vcClass=b.vcCommName) union all select distinct vcEmpOF,vcClass,b.vcCommCode as vcIndex,convert(char(19),[dtCheckIn],120) as dtCheckIn,convert(char(19),[dtCheckOut],120) as dtCheckOut";
				sql2+=" from tbEmpSchLog a,tbCommCode b,tbCommCode c where b.vcCommSign='EC' and c.vcCommSign='OF' and a.vcSchID='"+strShcID+"' and a.vcDeptName='"+strDeptName+"' and a.vcEmpOF=c.vcCommName and a.vcClass=b.vcCommName order by vcEmpOF,vcIndex";
				dttmp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql2);
				dttmp.TableName="Struct";
				dsout.Tables.Add(dttmp);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return dsout;
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

		public DataSet GetSignCalcPara(Hashtable htPara)
		{
			DataSet dsout=new DataSet();
			try
			{
				DataTable dttmp=new DataTable();
				string sql1="select convert(char(8),dtSignDate,112) as vcSignID,vcCardID,convert(char(19),dtSignDate,120) as dtSignDate,vcSignFlag,vcComments from tbEmpSign where vcDeptID='"+htPara["strDeptID"].ToString()+"' and convert(char(8),dtSignDate,112) between '"+htPara["strSchIDBegin"].ToString()+"' and '"+htPara["strSchIDEnd"].ToString()+"' order by convert(char(8),dtSignDate,112),vcCardID,vcSignFlag,dtSignDate";
				dttmp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				dttmp.TableName="SignLog";
				dsout.Tables.Add(dttmp);

				dttmp=new DataTable();
				string sql2="select vcSchID,vcDeptName,vcCardID,vcEmpName,vcEmpOF,vcClass,convert(char(19),dtCheckIn,120) as dtCheckIn,convert(char(19),dtCheckOut,120) as dtCheckOut from tbEmpSchLog where vcDeptName='"+htPara["strDeptName"].ToString()+"' and vcSchID between '"+htPara["strSchIDBegin"].ToString()+"' and '"+htPara["strSchIDEnd"].ToString()+"' order by vcSchID,vcCardID";
				dttmp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql2);
				dttmp.TableName="SchLog";
				dsout.Tables.Add(dttmp);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return dsout;
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

		public bool InsertSignCalc(Hashtable htpara,ArrayList alfin)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="delete from tbSignList where vcSignDate between '"+htpara["strSchIDBegin"].ToString()+"' and '"+htpara["strSchIDEnd"].ToString()+"' and vcDept='"+htpara["strDeptName"].ToString()+"'";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

					string sql2="";
					foreach(CMSMStruct.SignListStruct sls in alfin)
					{
						sql2+="insert into tbSignList values('"+sls.strSignDate+"','"+sls.strCardID+"','"+sls.strClass+"','"+sls.strEmpName+"','"+sls.strDept+"','"+sls.strOfficer+"',";
						if(sls.strSignIn=="")
						{
							sql2+="null,";
						}
						else
						{
							sql2+="'"+sls.strSignIn+"',";
						}

						if(sls.strSignOut=="")
						{
							sql2+="null,";
						}
						else
						{
							sql2+="'"+sls.strSignOut+"',";
						}

						sql2+="'"+sls.strSignState+"','"+sls.strSignResult+"','"+sls.strComments+"');";
					}
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return false;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}
				return true;
			}
		}

		public DataTable GetSignCalcResultQuery(string strDept,string strDate)
		{
			DataTable dttmp=new DataTable();
			try
			{
				string sql1="select distinct substring(vcSignDate,7,2) as vcday from tbSignList where substring(vcSignDate,1,6)='"+strDate.Substring(0,6)+"' and vcDept='"+strDept+"' order by substring(vcSignDate,7,2)";
				dttmp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return dttmp;
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

		public DataSet GetSignSumQuery(Hashtable htpara)
		{
			DataSet dsout=new DataSet();
			DataTable dttmp=new DataTable();
			try
			{
				string sql1="";
				string sql2="";
				if(htpara["strDeptName"].ToString()=="")
				{
					sql1="select 1 as sno,'正常出勤' as type,count(distinct vcEmpName) as TolEmpCount,count(*) as TolCount,'' as EmpList from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and vcSignState='0000000000'";
					sql1+=" union select 2 as sno,'迟到' as type,count(distinct vcEmpName) as TolEmpCount,count(*) as TolCount,'' as EmpList from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and substring(vcSignState,1,1)='1'";
					sql1+=" union select 3 as sno,'早退' as type,count(distinct vcEmpName) as TolEmpCount,count(*) as TolCount,'' as EmpList from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and substring(vcSignState,2,1)='1'";
					sql1+=" union select 4 as sno,'病假' as type,count(distinct vcEmpName) as TolEmpCount,count(*) as TolCount,'' as EmpList from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and substring(vcSignState,3,1)='1'";
					sql1+=" union select 5 as sno,'事假' as type,count(distinct vcEmpName) as TolEmpCount,count(*) as TolCount,'' as EmpList from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and substring(vcSignState,4,1)='1'";
					sql1+=" union select 6 as sno,'缺勤' as type,count(distinct vcEmpName) as TolEmpCount,count(*) as TolCount,'' as EmpList from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and substring(vcSignState,10,1)='1'";
					sql1+=" order by sno";					}
				else
				{
					sql1="select 1 as sno,'正常出勤' as type,count(distinct vcEmpName) as TolEmpCount,count(*) as TolCount,'' as EmpList from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and vcDept = '"+htpara["strDeptName"].ToString()+"' and vcSignState='0000000000'";
					sql1+=" union select 2 as sno,'迟到' as type,count(distinct vcEmpName) as TolEmpCount,count(*) as TolCount,'' as EmpList from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and vcDept = '"+htpara["strDeptName"].ToString()+"' and substring(vcSignState,1,1)='1'";
					sql1+=" union select 3 as sno,'早退' as type,count(distinct vcEmpName) as TolEmpCount,count(*) as TolCount,'' as EmpList from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and vcDept = '"+htpara["strDeptName"].ToString()+"' and substring(vcSignState,2,1)='1'";
					sql1+=" union select 4 as sno,'病假' as type,count(distinct vcEmpName) as TolEmpCount,count(*) as TolCount,'' as EmpList from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and vcDept = '"+htpara["strDeptName"].ToString()+"' and substring(vcSignState,3,1)='1'";
					sql1+=" union select 5 as sno,'事假' as type,count(distinct vcEmpName) as TolEmpCount,count(*) as TolCount,'' as EmpList from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and vcDept = '"+htpara["strDeptName"].ToString()+"' and substring(vcSignState,4,1)='1'";
					sql1+=" union select 6 as sno,'缺勤' as type,count(distinct vcEmpName) as TolEmpCount,count(*) as TolCount,'' as EmpList from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and vcDept = '"+htpara["strDeptName"].ToString()+"' and substring(vcSignState,10,1)='1'";
					sql1+=" order by sno";
				}
				dttmp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				dttmp.TableName="t1";
				dsout.Tables.Add(dttmp);

				if(htpara["strDeptName"].ToString()=="")
				{
					sql2="select 1 as sno,vcEmpName,count(vcEmpName) as eCount from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and vcSignState='0000000000' group by vcEmpName";
					sql2+=" union select 2 as sno,vcEmpName,count(vcEmpName) as eCount from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and substring(vcSignState,1,1)='1' group by vcEmpName";
					sql2+=" union select 3 as sno,vcEmpName,count(vcEmpName) as eCount from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and substring(vcSignState,2,1)='1' group by vcEmpName";
					sql2+=" union select 4 as sno,vcEmpName,count(vcEmpName) as eCount from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and substring(vcSignState,3,1)='1' group by vcEmpName";
					sql2+=" union select 5 as sno,vcEmpName,count(vcEmpName) as eCount from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and substring(vcSignState,4,1)='1' group by vcEmpName";
					sql2+=" union select 6 as sno,vcEmpName,count(vcEmpName) as eCount from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and substring(vcSignState,10,1)='1' group by vcEmpName";
					sql2+=" order by sno,vcEmpName";				
				}
				else
				{
					sql2="select 1 as sno,vcEmpName,count(vcEmpName) as eCount from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and vcDept = '"+htpara["strDeptName"].ToString()+"' and vcSignState='0000000000' group by vcEmpName";
					sql2+=" union select 2 as sno,vcEmpName,count(vcEmpName) as eCount from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and vcDept = '"+htpara["strDeptName"].ToString()+"' and substring(vcSignState,1,1)='1' group by vcEmpName";
					sql2+=" union select 3 as sno,vcEmpName,count(vcEmpName) as eCount from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and vcDept = '"+htpara["strDeptName"].ToString()+"' and substring(vcSignState,2,1)='1' group by vcEmpName";
					sql2+=" union select 4 as sno,vcEmpName,count(vcEmpName) as eCount from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and vcDept = '"+htpara["strDeptName"].ToString()+"' and substring(vcSignState,3,1)='1' group by vcEmpName";
					sql2+=" union select 5 as sno,vcEmpName,count(vcEmpName) as eCount from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and vcDept = '"+htpara["strDeptName"].ToString()+"' and substring(vcSignState,4,1)='1' group by vcEmpName";
					sql2+=" union select 6 as sno,vcEmpName,count(vcEmpName) as eCount from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and vcDept = '"+htpara["strDeptName"].ToString()+"' and substring(vcSignState,10,1)='1' group by vcEmpName";
					sql2+=" order by sno,vcEmpName";
				}
				dttmp=new DataTable();
				dttmp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql2);
				dttmp.TableName="t2";
				dsout.Tables.Add(dttmp);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return dsout;
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

		public DataTable GetSignDetailQuery(Hashtable htpara)
		{
			DataTable dttmp=new DataTable();
			try
			{
				string sql1="";
				if(!htpara.ContainsKey("empname"))
				{
					if(htpara["strDeptName"].ToString()=="")
					{
						sql1="select vcDept,vcCardID,vcEmpName,vcSignDate,vcClass,convert(char(19),dtSignIn,120) as dtSignIn,convert(char(19),dtSignOut,120) as dtSignOut,vcSignResult,vcComments from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' order by vcDept,vcCardID,vcSignDate,vcClass";
					}
					else
					{
						sql1="select vcDept,vcCardID,vcEmpName,vcSignDate,vcClass,convert(char(19),dtSignIn,120) as dtSignIn,convert(char(19),dtSignOut,120) as dtSignOut,vcSignResult,vcComments from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and vcDept = '"+htpara["strDeptName"].ToString()+"' order by vcDept,vcCardID,vcSignDate,vcClass";
					}
				}
				else
				{
					if(htpara["strDeptName"].ToString()=="")
					{
						sql1="select vcDept,vcCardID,vcEmpName,vcSignDate,vcClass,convert(char(19),dtSignIn,120) as dtSignIn,convert(char(19),dtSignOut,120) as dtSignOut,vcSignResult,vcComments from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and vcEmpName='"+htpara["empname"].ToString()+"' order by vcDept,vcCardID,vcSignDate,vcClass";
					}
					else
					{
						sql1="select vcDept,vcCardID,vcEmpName,vcSignDate,vcClass,convert(char(19),dtSignIn,120) as dtSignIn,convert(char(19),dtSignOut,120) as dtSignOut,vcSignResult,vcComments from tbSignList where vcSignDate between '"+htpara["strBegin"].ToString()+"' and '"+htpara["strEnd"].ToString()+"' and vcDept = '"+htpara["strDeptName"].ToString()+"' and vcEmpName='"+htpara["empname"].ToString()+"' order by vcDept,vcCardID,vcSignDate,vcClass";
					}
				}
				dttmp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return dttmp;
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

		public DataTable GetSignUnitQuery(Hashtable htpara)
		{
			DataTable dttmp=new DataTable();
			try
			{
				string strCondition="";
				string sql1="";

				if(htpara["strType"].ToString()=="0")
				{
					if(htpara["strCardID"].ToString()!=""&&htpara["strCardID"].ToString()!="*")
					{
						strCondition=" and vcCardID like '" + htpara["strCardID"].ToString() + "%'";
					}
					if(htpara["strEmpName"].ToString()!=""&&htpara["strEmpName"].ToString()!="*")
					{
						strCondition+=" and vcEmpName like '%" + htpara["strEmpName"].ToString() + "%'";
					}
					sql1="select vcDept,vcCardID,vcEmpName,vcSignDate,vcClass,vcOfficer,convert(char(19),dtSignIn,120) as dtSignIn,convert(char(19),dtSignOut,120) as dtSignOut,vcSignResult,vcComments from tbSignList where substring(vcSignDate,1,6)='"+htpara["strmonth"].ToString()+"' "+strCondition+" order by vcDept,vcCardID,vcSignDate,vcClass";
				}
				else
				{
					if(htpara["strCardID"].ToString()!=""&&htpara["strCardID"].ToString()!="*")
					{
						strCondition=" and a.vcCardID like '" + htpara["strCardID"].ToString() + "%'";
					}
					if(htpara["strEmpName"].ToString()!=""&&htpara["strEmpName"].ToString()!="*")
					{
						strCondition+=" and b.vcEmpName like '%" + htpara["strEmpName"].ToString() + "%'";
					}
					sql1="select c.vcCommName,a.vcCardID,b.vcEmpName,convert(char(19),a.dtSignDate,120) as dtSignDate,a.vcSignFlag,a.vcComments from tbEmpSign a,tbEmployee b,tbCommCode c where convert(char(6),a.dtSignDate,112)='"+htpara["strmonth"].ToString()+"' "+strCondition+" and c.vcCommSign='MD' and a.vcCardID=b.vcCardID and a.vcDeptID=c.vcCommCode order by dtSignDate";
				}
				dttmp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return dttmp;
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

		public int SchedEmpDailyEvery(CMSMStruct.EmpSchLogStruct empsl)
		{
			con.Open();
			int rerow=0;
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="select count(*) from tbEmpSchLog where vcSchID='"+empsl.strSchID+"' and vcEmpName='" + empsl.strEmpName + "' and vcClass='"+empsl.strClass+"'";
					drr=SqlHelper.ExecuteReader(con,tran,CommandType.Text,sql1);
					drr.Read();
					string strissch=drr[0].ToString();
					drr.Close();

					if(strissch=="0")
					{
						string sql2="insert into tbEmpSchLog values('"+ empsl.strSchID+"','"+empsl.strDeptName+"','"+empsl.strManager+"','"+empsl.strCardID+"','"+empsl.strEmpName+"','"+empsl.strEmpOF+"','"+empsl.strClass+"','"+empsl.strCheckIn+"','"+empsl.strCheckOut+"')";
						rerow=SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);
					}
					else
					{
						rerow=0;
					}

					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return rerow;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}
				return rerow;
			}
		}

		public int SchedEmpDailyEveryDel(string strShcID,string strEmpName,string strClass)
		{
			con.Open();
			int rerow=0;
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql2="delete from tbEmpSchLog where vcSchID='"+strShcID+"' and vcEmpName='"+strEmpName+"' and vcClass='"+strClass+"'";
					rerow=SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
					return rerow;
				}
				finally
				{
					if(con.State==ConnectionState.Open)
					{
						con.Close();
					}
				}
				return rerow;
			}
		}

		public DataTable GetEmployInfo()
		{
			DataTable dttmp=new DataTable();
			try
			{
				string sql1="select a.vcCardID,vcEmpName,b.vcCommName as vcOfficer,c.vcCommName as vcDeptName from dbo.tbEmployee a,tbCommCode b,tbCommCode c where b.vcCommSign='OF' and c.vcCommSign='MD' and a.vcOfficer=b.vcCommCode and a.vcDeptID=c.vcCommCode and a.vcFlag='0' order by a.vcCardID,vcEmpName";
				dttmp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return dttmp;
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

		public int IsEmpSchExist(string strSchID,string strDeptName,string strCardID,string strClass)
		{
			int schcount=-1;
			try
			{
				string sql1="select count(*) from tbEmpSchLog where vcSchID='"+strSchID+"' and vcDeptName='"+strDeptName+"' and vcCardID='"+strCardID+"' and vcClass='"+strClass+"'";
				drr=SqlHelper.ExecuteReader(con,CommandType.Text,sql1);
				drr.Read();
				schcount=int.Parse(drr[0].ToString());
				drr.Close();
				
				return schcount;
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return schcount;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
		}

		public DataTable GetDeptManagerList()
		{
			DataTable dttmp=new DataTable();
			try
			{
				string sql1="select distinct vcDeptID,vcEmpName from tbEmployee where vcFlag='0' and vcOfficer='OF002' order by vcDeptID,vcEmpName";
				dttmp=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return dttmp;
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

		public void SchedEmpDailyBatch(DataTable dt)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql2="";
					foreach(DataRow dr in dt.Rows)
					{
						sql2="insert into tbEmpSchLog values('"+dr["vcSchID"].ToString()+"','"+dr["vcDeptName"].ToString()+"','"+dr["vcManager"].ToString()+"','"+dr["vcCardID"].ToString()+"','"+dr["vcEmpName"].ToString()+"','"+dr["vcEmpOF"].ToString()+"','"+dr["vcClass"].ToString()+"','"+dr["dtCheckIn"].ToString()+"','"+dr["dtCheckOut"].ToString()+"')";
						SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);
					}

					tran.Commit();
				}
				catch (Exception e) 
				{
					tran.Rollback();
					clog.WriteLine(e);
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
	}
}
