using System;
using System.Data;
using System.Data.SqlClient;
using CommCenter;
using System.Collections;

namespace DataAccess
{
	/// <summary>
	/// MaterialSAcc 的摘要说明。
	/// </summary>
	public class MaterialSAcc
	{
		SqlDataReader drr;
		SqlConnection con;
		AMSLog clog=new AMSLog();

		public MaterialSAcc(string strcons)
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			con=new SqlConnection(strcons);
		}

		public DataTable GetMaterialInfo(string strBatch,string strMaterialID)
		{
			DataTable dtMaterial=new DataTable();
			try
			{
				string sql1="select * from tbMaterialPara where cnvcBatchNo='"+strBatch+"' and cnnMaterialCode=" + strMaterialID;
				dtMaterial=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
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
			return dtMaterial;
		}

		public string getMaterialName(string strBatch,string strMaterialName)
		{
			string strsql="select count(*) from tbMaterialPara where cnvcBatchNo='"+strBatch+"' and cnvcMaterialName='" + strMaterialName + "'";
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

		
		public int InsertMaterial(CMSMStruct.MaterialSStruct mss)
		{
			int recount=0;
			try
			{
				string sql1="insert into tbMaterialPara values('"+mss.strBatchNo+"','" + mss.strMaterialName + "','" + mss.strStandardUnit + "','" + mss.strUnit + "'," + mss.dPrice.ToString() + ",'" + mss.strProviderName + "','" + mss.strMaterialType + "',0,0,'0')";
				recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);

				return recount;
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return recount;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
		}

		public int UpdateMaterial(string strMaterialID,string strsqlset)
		{
			int recount=0;
			try
			{
				string sql1="update tbMaterialPara set " + strsqlset + " where cnnMaterialCode=" + strMaterialID;
				recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
				return recount;
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return recount;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
		}

		public int CancelMaterial(CMSMStruct.MaterialSStruct mssold)
		{
			int recount=0;
			try
			{
				string sql1="update tbMaterialPara set cnvcFlag='1' where cnvcBatchNo='"+mssold.strBatchNo+"' and cnnMaterialCode=" + mssold.strMaterialCode;
				recount=SqlHelper.ExecuteNonQuery(con,CommandType.Text,sql1);
				return recount;
			}
			catch (Exception e) 
			{
				clog.WriteLine(e);
				return recount;
			}
			finally
			{
				if(con.State==ConnectionState.Open)
				{
					con.Close();
				}
			}
		}

		public DataTable GetMaterials(string strMaterialCode,string strMaterialName,string strMaterialType,string strBatchNo)
		{
			DataTable dtMaterials=new DataTable();
			try
			{
				string strCondition="";
				if(strBatchNo!=""&&strBatchNo!="*")
				{
					strCondition+=" and a.cnvcBatchNo like '" + strBatchNo + "%'";
				}
				if(strMaterialCode!=""&&strMaterialCode!="*")
				{
					strCondition+=" and a.cnnMaterialCode like '" + strMaterialCode + "%'";
				}
				if(strMaterialName!=""&&strMaterialName!="*")
				{
					strCondition+= " and a.cnvcMaterialName like '%" + strMaterialName + "%'";
				}
				if(strMaterialType!=""&&strMaterialType!="全部")
				{
					strCondition+= " and a.cnvcMaterialType='" + strMaterialType + "'";
				}
				string sql1="select a.cnvcBatchNo,a.cnnMaterialCode,a.cnvcMaterialName,a.cnvcStandardUnit,a.cnvcUnit,a.cnnPrice,a.cnvcProviderName,b.cnvcName,(case a.cnvcFlag when '0' then '可用' when '1' then '作废' end) as cnvcFlag from tbMaterialPara a,tbNameCode b where b.cnvcType='MaterialType' and a.cnvcMaterialType=b.cnvcCode ";
				if(strCondition!="")
				{
					sql1=sql1 + strCondition + " order by a.cnnMaterialCode";
				}
				else
				{
					sql1+=" order by a.cnnMaterialCode";
				}
				dtMaterials=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
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
			return dtMaterials;
		}

		public string getMaterialNamebyID(string strBatchNo,string strMaterialName,string strMaterialCode)
		{
			string strsql="select count(*) from tbMaterialPara where cnvcBatchNo='"+strBatchNo+"' and cnvcMaterialName='" + strMaterialName + "' and cnnMaterialCode<>" + strMaterialCode + "";
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

		public DataTable GetMaterialsBySelect(string strMaterialCode,string strMaterialName)
		{
			DataTable dtMaterials=new DataTable();
			try
			{
				string strCondition="";
				if(strMaterialCode!=""&&strMaterialCode!="*")
				{
					strCondition+=" and a.cnnMaterialCode ='" + strMaterialCode + "'";
				}
				if(strMaterialName!=""&&strMaterialName!="*")
				{
					strCondition+= " and a.cnvcMaterialName like '%" + strMaterialName + "%'";
				}
				string sql1="select top 10 a.cnvcBatchNo,a.cnnMaterialCode,a.cnvcMaterialName,b.cnvcName as cnvcMaterialType from tbMaterialPara a,tbNameCode b where b.cnvcType='MaterialType' and a.cnvcMaterialType=b.cnvcCode and a.cnvcFlag='0' ";
				if(strCondition!="")
				{
					sql1=sql1 + strCondition + " order by a.cnvcBatchNo,a.cnnMaterialCode";
				}
				else
				{
					sql1+=" order by a.cnvcBatchNo,a.cnnMaterialCode";
				}
				dtMaterials=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
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
			return dtMaterials;
		}

		public int InsertMaterialEnter(CMSMStruct.MaterialEnterStruct mes)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="insert into tbMaterialEnter values('"+mes.strBatchNo+"'," + mes.strMaterialCode+",'"+ mes.strMaterialName + "','" + mes.strStandardUnit + "','" + mes.strUnit + "'," + mes.dPrice.ToString() + ",'" + mes.strProviderName + "','" + mes.strMaterialType + "',"+mes.dLastCount.ToString()+","+mes.dEnterCount.ToString()+","+mes.dCount+",'"+mes.strOperType+"',null,'"+mes.strEnterDate+"','"+mes.strDeptID+"','"+mes.strOperDate+"','"+mes.strOperName+"')";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

					string sql2="update tbMaterialPara set cnnCurCount="+mes.dCount+" where cnvcBatchNo='"+mes.strBatchNo+"' and cnnMaterialCode="+mes.strMaterialCode;
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

					tran.Commit();
					return 1;
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
			}
		}

		public DataTable GetMaterialEnterForMod(Hashtable htpara)
		{
			DataTable dtMaterialEnter=new DataTable();
			try
			{
				string strCondition="";
				if(htpara["strBatchNo"].ToString()!=""&&htpara["strBatchNo"].ToString()!="*")
				{
					strCondition+=" and a.cnvcBatchNo like '" + htpara["strBatchNo"].ToString() + "%'";
				}
				if(htpara["strMaterialCode"].ToString()!=""&&htpara["strMaterialCode"].ToString()!="*")
				{
					strCondition+=" and a.cnnMaterialCode like '" + htpara["strMaterialCode"].ToString() + "%'";
				}
				if(htpara["strMaterialName"].ToString()!=""&&htpara["strMaterialName"].ToString()!="*")
				{
					strCondition+= " and a.cnvcMaterialName like '%" + htpara["strMaterialName"].ToString() + "%'";
				}
				if(htpara["strMaterialType"].ToString()!=""&&htpara["strMaterialType"].ToString()!="全部")
				{
					strCondition+= " and a.cnvcMaterialType='" + htpara["strMaterialType"].ToString() + "'";
				}
				if(htpara["strEnterSerial"].ToString()!=""&&htpara["strEnterSerial"].ToString()!="*")
				{
					strCondition+= " and a.cnnSerialNo=" + htpara["strEnterSerial"].ToString();
				}
				if(htpara["strDeptID"].ToString()!=""&&htpara["strDeptID"].ToString()!="*")
				{
					strCondition+= " and a.cnvcDeptID='" + htpara["strDeptID"].ToString()+"'";
				}
				string sql1="select a.cnnSerialNo,a.cnvcBatchNo,a.cnnMaterialCode,a.cnvcMaterialName,a.cnvcStandardUnit,a.cnvcUnit,a.cnnPrice,a.cnvcProviderName,b.cnvcName,a.cnnLastCount,a.cnnEnterCount,a.cndEnterDate,c.cnvcDeptName,a.cndOperDate,a.cnvcOperName from tbMaterialEnter a,tbNameCode b,tbDept c where b.cnvcType='MaterialType' and a.cnvcMaterialType=b.cnvcCode and a.cnvcOperType='0' and a.cnvcDeptID=c.cnvcDeptID";
				sql1+=" and a.cndEnterDate between '"+htpara["strBeginDate"].ToString()+"' and '"+htpara["strEndDate"].ToString()+" 23:59:59'";
				if(strCondition!="")
				{
					sql1=sql1 + strCondition + " order by a.cndEnterDate,a.cnvcBatchNo,a.cnnMaterialCode";
				}
				else
				{
					sql1+=" order by a.cndEnterDate,a.cnvcBatchNo,a.cnnMaterialCode";
				}
				dtMaterialEnter=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
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
			return dtMaterialEnter;
		}

		public int MaterialEnterMod(Hashtable htpara)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="insert into tbMaterialEnter select cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice,cnvcProviderName,cnvcMaterialType,cnnLastCount,"+htpara["strNewEnterCount"].ToString()+","+htpara["strNewCurCount"].ToString()+",";
					sql1+="'1',"+htpara["strSerialOld"].ToString()+",cndEnterDate,'"+htpara["strDeptID"].ToString()+"','"+htpara["strOperDate"].ToString()+"','"+htpara["strOperName"].ToString()+"' from tbMaterialEnter where cnnSerialNo=" + htpara["strSerialOld"].ToString() +" ;SELECT scope_identity();";
					drr=SqlHelper.ExecuteReader(con,tran,CommandType.Text,sql1);
					drr.Read();
					string strSerialNew=drr[0].ToString();
					drr.Close();

					string sql2="update tbMaterialEnter set cnvcOperType='2',cnnLinkSerialNo="+strSerialNew+" where cnnSerialNo=" + htpara["strSerialOld"].ToString();
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

					string sql3="update tbMaterialPara set cnnCurCount=cnnCurCount-("+htpara["strChangeCount"].ToString()+") where cnvcBatchNo='"+htpara["strBatchNo"].ToString()+"' and cnnMaterialCode=" + htpara["strMaterialCode"].ToString();
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql3);

					tran.Commit();
					return 1;
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
			}
		}

		public int MaterialEnterModDetele(Hashtable htpara)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="update tbMaterialEnter set cnvcOperType='3',cnvcDeptID='"+htpara["strDeptID"].ToString()+"',cndOperDate='"+htpara["strOperDate"].ToString()+"',cnvcOperName='"+htpara["strOperName"].ToString()+"' where cnnSerialNo=" + htpara["strSerialOld"].ToString();
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

					string sql2="update tbMaterialPara set cnnCurCount=cnnCurCount-"+htpara["strEnterCount"].ToString()+" where cnvcBatchNo='"+htpara["strBatchNo"].ToString()+"' and cnnMaterialCode=" + htpara["strMaterialCode"].ToString();
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

					tran.Commit();
					return 1;
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
			}
		}

		public int InsertMaterialOut(CMSMStruct.MaterialOutStruct mes)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="insert into tbMaterialOut values('"+mes.strBatchNo+"'," + mes.strMaterialCode+",'"+ mes.strMaterialName + "','" + mes.strStandardUnit + "','" + mes.strUnit + "'," + mes.dPrice.ToString() + ",'" + mes.strProviderName + "','" + mes.strMaterialType + "',"+mes.dLastCount.ToString()+","+mes.dOutCount.ToString()+","+mes.dCount+",'"+mes.strOperType+"',null,'"+mes.strOutDate+"','"+mes.strDeptID+"','"+mes.strOperDate+"','"+mes.strOperName+"')";
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

					string sql2="update tbMaterialPara set cnnCurCount="+mes.dCount+" where cnvcBatchNo='"+mes.strBatchNo+"' and cnnMaterialCode="+mes.strMaterialCode;
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

					tran.Commit();
					return 1;
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
			}
		}

		
		public DataTable GetMaterialOutForMod(Hashtable htpara)
		{
			DataTable dtMaterialOut=new DataTable();
			try
			{
				string strCondition="";
				if(htpara["strBatchNo"].ToString()!=""&&htpara["strBatchNo"].ToString()!="*")
				{
					strCondition+=" and a.cnvcBatchNo like '" + htpara["strBatchNo"].ToString() + "%'";
				}
				if(htpara["strMaterialCode"].ToString()!=""&&htpara["strMaterialCode"].ToString()!="*")
				{
					strCondition+=" and a.cnnMaterialCode like '" + htpara["strMaterialCode"].ToString() + "%'";
				}
				if(htpara["strMaterialName"].ToString()!=""&&htpara["strMaterialName"].ToString()!="*")
				{
					strCondition+= " and a.cnvcMaterialName like '%" + htpara["strMaterialName"].ToString() + "%'";
				}
				if(htpara["strMaterialType"].ToString()!=""&&htpara["strMaterialType"].ToString()!="全部")
				{
					strCondition+= " and a.cnvcMaterialType='" + htpara["strMaterialType"].ToString() + "'";
				}
				if(htpara["strOutSerial"].ToString()!=""&&htpara["strOutSerial"].ToString()!="*")
				{
					strCondition+= " and a.cnnSerialNo=" + htpara["strOutSerial"].ToString();
				}
				if(htpara["strDeptID"].ToString()!=""&&htpara["strDeptID"].ToString()!="*")
				{
					strCondition+= " and a.cnvcDeptID='" + htpara["strDeptID"].ToString()+"'";
				}
				string sql1="select a.cnnSerialNo,a.cnvcBatchNo,a.cnnMaterialCode,a.cnvcMaterialName,a.cnvcStandardUnit,a.cnvcUnit,a.cnnPrice,a.cnvcProviderName,b.cnvcName,a.cnnLastCount,a.cnnOutCount,a.cndOutDate,c.cnvcDeptName,a.cndOperDate,a.cnvcOperName from tbMaterialOut a,tbNameCode b,tbDept c where b.cnvcType='MaterialType' and a.cnvcMaterialType=b.cnvcCode and a.cnvcOperType='0' and a.cnvcDeptID=c.cnvcDeptID";
				sql1+=" and a.cndOutDate between '"+htpara["strBeginDate"].ToString()+"' and '"+htpara["strEndDate"].ToString()+" 23:59:59'";
				if(strCondition!="")
				{
					sql1=sql1 + strCondition + " order by a.cndOutDate,a.cnvcBatchNo,a.cnnMaterialCode";
				}
				else
				{
					sql1+=" order by a.cndOutDate,a.cnvcBatchNo,a.cnnMaterialCode";
				}
				dtMaterialOut=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
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
			return dtMaterialOut;
		}

		public int MaterialOutMod(Hashtable htpara)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="insert into tbMaterialOut select cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice,cnvcProviderName,cnvcMaterialType,cnnLastCount,"+htpara["strNewOutCount"].ToString()+","+htpara["strNewCurCount"].ToString()+",";
					sql1+="'1',"+htpara["strSerialOld"].ToString()+",cndOutDate,'"+htpara["strDeptID"].ToString()+"','"+htpara["strOperDate"].ToString()+"','"+htpara["strOperName"].ToString()+"' from tbMaterialOut where cnnSerialNo=" + htpara["strSerialOld"].ToString() +" ;SELECT scope_identity();";
					drr=SqlHelper.ExecuteReader(con,tran,CommandType.Text,sql1);
					drr.Read();
					string strSerialNew=drr[0].ToString();
					drr.Close();

					string sql2="update tbMaterialOut set cnvcOperType='2',cnnLinkSerialNo="+strSerialNew+" where cnnSerialNo=" + htpara["strSerialOld"].ToString();
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

					string sql3="update tbMaterialPara set cnnCurCount=cnnCurCount-("+htpara["strChangeCount"].ToString()+") where cnvcBatchNo='"+htpara["strBatchNo"].ToString()+"' and cnnMaterialCode=" + htpara["strMaterialCode"].ToString();
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql3);

					tran.Commit();
					return 1;
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
			}
		}

		public int MaterialOutModDetele(Hashtable htpara)
		{
			con.Open();
			using(SqlTransaction tran=con.BeginTransaction())
			{
				try
				{
					string sql1="update tbMaterialOut set cnvcOperType='3',cnvcDeptID='"+htpara["strDeptID"].ToString()+"',cndOperDate='"+htpara["strOperDate"].ToString()+"',cnvcOperName='"+htpara["strOperName"].ToString()+"' where cnnSerialNo=" + htpara["strSerialOld"].ToString();
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql1);

					string sql2="update tbMaterialPara set cnnCurCount=cnnCurCount+"+htpara["strOutCount"].ToString()+" where cnvcBatchNo='"+htpara["strBatchNo"].ToString()+"' and cnnMaterialCode=" + htpara["strMaterialCode"].ToString();
					SqlHelper.ExecuteNonQuery(con,tran,CommandType.Text,sql2);

					tran.Commit();
					return 1;
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
			}
		}

		public DataTable GetProviderList()
		{
			DataTable dtProvicer=new DataTable();
			try
			{
				string sql1="select distinct cnvcProviderName as vcOperName from tbMaterialPara";
				dtProvicer=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
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
			return dtProvicer;
		}

		public DataTable GetMaterialsEnterReport(string strQueryType,Hashtable htpara)
		{
			DataTable dtResult=new DataTable();
			string sql1="";
			try
			{
				switch(strQueryType)
				{
					case "0":
						string strCondition="";
						if(htpara["strMonth"].ToString()!=""&&htpara["strMonth"].ToString()!="*")
						{
							strCondition+=" and convert(char(6),cndEnterDate,112)= '" + htpara["strMonth"].ToString() + "'";
						}
						if(htpara["strProviderName"].ToString()!=""&&htpara["strProviderName"].ToString()!="*")
						{
							strCondition+= " and cnvcProviderName='" + htpara["strProviderName"].ToString() + "'";
						}
						if(htpara["strMaterialType"].ToString()!=""&&htpara["strMaterialType"].ToString()!="全部")
						{
							strCondition+= " and cnvcMaterialType='" + htpara["strMaterialType"].ToString() + "'";
						}
						if(htpara["strMaterialName"].ToString()!=""&&htpara["strMaterialName"].ToString()!="全部")
						{
							strCondition+= " and cnvcMaterialName like '%" + htpara["strMaterialName"].ToString() + "%'";
						}
						sql1="select cnnSerialNo,cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice,cnvcProviderName,cnvcMaterialType,cnnEnterCount,cnvcOperType,cndEnterDate,cnvcDeptID,cndOperDate,cnvcOperName from tbMaterialEnter where cnvcOperType in('0','1')";
						if(strCondition!="")
						{
							sql1=sql1 + strCondition + " order by cndEnterDate,cnnMaterialCode";
						}
						else
						{
							sql1+=" order by cndEnterDate,cnnMaterialCode";
						}
						break;
					case "1":
						sql1="select cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice,convert(char(8),cndEnterDate,112) as enterdate,sum(cnnEnterCount) as sumcount from tbMaterialEnter where cnvcOperType in('0','1') and convert(char(6),cndEnterDate,112)='"+htpara["strMonth"].ToString()+"' and cnvcProviderName='"+htpara["strProviderName"].ToString()+"'";
						sql1+=" group by cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice,convert(char(8),cndEnterDate,112) order by cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice,convert(char(8),cndEnterDate,112)";
						break;
					case "2":
						sql1="select cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice,cnvcProviderName,sum(cnnEnterCount) as sumcount from tbMaterialEnter where cnvcOperType in('0','1') and convert(char(6),cndEnterDate,112)='"+htpara["strMonth"].ToString()+"'";
						sql1+=" group by cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice,cnvcProviderName order by cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice,cnvcProviderName";
						break;
				}
				if(sql1!="")
				{
					dtResult=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				}
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
			return dtResult;
		}

		public DataTable GetMaterialsOutReport(string strQueryType,Hashtable htpara)
		{
			DataTable dtResult=new DataTable();
			string sql1="";
			try
			{
				switch(strQueryType)
				{
					case "0":
						string strCondition="";
						if(htpara["strMonth"].ToString()!=""&&htpara["strMonth"].ToString()!="*")
						{
							strCondition+=" and convert(char(6),cndOutDate,112)= '" + htpara["strMonth"].ToString() + "'";
						}
						if(htpara["strMaterialType"].ToString()!=""&&htpara["strMaterialType"].ToString()!="全部")
						{
							strCondition+= " and cnvcMaterialType='" + htpara["strMaterialType"].ToString() + "'";
						}
						if(htpara["strMaterialName"].ToString()!=""&&htpara["strMaterialName"].ToString()!="全部")
						{
							strCondition+= " and cnvcMaterialName like '%" + htpara["strMaterialName"].ToString() + "%'";
						}
						sql1="select cnnSerialNo,cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice,cnvcProviderName,cnvcMaterialType,cnnOutCount,cnvcOperType,cndOutDate,cnvcDeptID,cndOperDate,cnvcOperName from tbMaterialOut where cnvcOperType in('0','1')";
						if(strCondition!="")
						{
							sql1=sql1 + strCondition + " order by cndOutDate,cnvcBatchNo,cnnMaterialCode";
						}
						else
						{
							sql1+=" order by cndOutDate,cnvcBatchNo,cnnMaterialCode";
						}
						break;
					case "1":
						if(htpara["strMaterialType"].ToString()!=""&&htpara["strMaterialType"].ToString()!="全部")
						{
							sql1="select cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice,convert(char(8),cndOutDate,112) as enterdate,sum(cnnOutCount) as sumcount from tbMaterialOut where cnvcOperType in('0','1') and convert(char(6),cndOutDate,112)='"+htpara["strMonth"].ToString()+"' and cnvcMaterialType='"+htpara["strMaterialType"].ToString()+"'";
							sql1+=" group by cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice,convert(char(8),cndOutDate,112) order by cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice,convert(char(8),cndOutDate,112)";
						}
						else
						{
							sql1="select cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice,convert(char(8),cndOutDate,112) as enterdate,sum(cnnOutCount) as sumcount from tbMaterialOut where cnvcOperType in('0','1') and convert(char(6),cndOutDate,112)='"+htpara["strMonth"].ToString()+"' ";
							sql1+=" group by cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice,convert(char(8),cndOutDate,112) order by cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice,convert(char(8),cndOutDate,112)";
						}
						break;
					case "2":
						if(htpara["strMaterialType"].ToString()!=""&&htpara["strMaterialType"].ToString()!="全部")
						{
							sql1="select cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice,sum(cnnOutCount) as sumcount,sum(cnnPrice*cnnOutCount) as sumfee from tbMaterialOut where cnvcOperType in('0','1') and convert(char(6),cndOutDate,112)='"+htpara["strMonth"].ToString()+"' and cnvcMaterialType='"+htpara["strMaterialType"].ToString()+"'";
							sql1+=" group by cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice order by cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice";
						}
						else
						{
							sql1="select cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice,sum(cnnOutCount) as sumcount,sum(cnnPrice*cnnOutCount) as sumfee from tbMaterialOut where cnvcOperType in('0','1') and convert(char(6),cndOutDate,112)='"+htpara["strMonth"].ToString()+"'";
							sql1+=" group by cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice order by cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice";
						}
						break;
				}
				if(sql1!="")
				{
					dtResult=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				}
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
			return dtResult;
		}

		public DataTable GetMaterialsEnterOutModList(string strOperType,string strBegin,string strEnd)
		{
			DataTable dtMod=new DataTable();
			try
			{
				string sql1="";
				if(strOperType=="0")
				{
					sql1="select cnnSerialNo,cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice,cnvcProviderName,cnvcMaterialType,cnnEnterCount,cnvcOperType,cnnLinkSerialNo,cndEnterDate,cnvcDeptID,cndOperDate,cnvcOperName from tbMaterialEnter where cnvcOperType in('2','3')";
					sql1+=" and cndOperDate between '"+strBegin+"' and '"+strEnd+" 23:59:59' order by cndEnterDate";
				}
				else
				{
					sql1="select cnnSerialNo,cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice,cnvcProviderName,cnvcMaterialType,cnnOutCount,cnvcOperType,cnnLinkSerialNo,cndOutDate,cnvcDeptID,cndOperDate,cnvcOperName from tbMaterialOut where cnvcOperType in('2','3')";
					sql1+=" and cndOperDate between '"+strBegin+"' and '"+strEnd+" 23:59:59' order by cndOutDate";
				}
				dtMod=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
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
			return dtMod;
		}

		public DataTable GetMaterialsStorageCurrent(string strMaterialType)
		{
			DataTable dtStorage=new DataTable();
			try
			{
				string sql1="";
				if(strMaterialType=="全部"||strMaterialType=="")
				{
					sql1="select cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice,cnvcProviderName,cnvcMaterialType,cnnCurCount,cast((cnnPrice*cnnCurCount) as numeric(12,2)) as cnnFee from tbMaterialPara where cnvcFlag='0' order by cnvcBatchNo,cnnMaterialCode";
				}
				else
				{
					sql1="select cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice,cnvcProviderName,cnvcMaterialType,cnnCurCount,cast((cnnPrice*cnnCurCount) as numeric(12,2)) as cnnFee from tbMaterialPara where cnvcFlag='0' and cnvcMaterialType='"+strMaterialType+"' order by cnvcBatchNo,cnnMaterialCode";
				}
				dtStorage=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
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
			return dtStorage;
		}

		public DataSet GetMaterialsSimpleAnalyse(string strMaterialType,string strMonth,string strLastMonth)
		{
			DataSet dsout=new DataSet();
			try
			{
				string sql1="select distinct cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice,(cast(0 as numeric(10,2))) as LastCount,(cast(0 as numeric(10,2))) as LastFee,(cast(0 as numeric(10,2))) as EnterCount,(cast(0 as numeric(10,2))) as EnterFee,(cast(0 as numeric(10,2))) as OutCount,(cast(0 as numeric(10,2))) as OutFee,";
				sql1+="(cast(0 as numeric(10,2))) as CurCount,(cast(0 as numeric(10,2))) as CurFee from (select distinct cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice from tbMaterialParaMonthBak where cnvcMonth='"+strLastMonth+"' and cnvcMaterialType='"+strMaterialType+"' union all select distinct cnvcBatchNo,cnnMaterialCode,cnvcMaterialName,cnvcStandardUnit,cnvcUnit,cnnPrice from tbMaterialPara where cnvcMaterialType='"+strMaterialType+"') a order by cnvcBatchNo,cnnMaterialCode";
				DataTable dtAnalyseResult=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql1);
				dtAnalyseResult.TableName="AnalyseResult";
				dsout.Tables.Add(dtAnalyseResult);

				string sql2="select cnvcBatchNo,cnnMaterialCode,cnnCurCount as LastCount,(cast(cnnPrice*cnnCurCount as numeric(10,2))) as LastFee from tbMaterialParaMonthBak where cnvcMonth='"+strLastMonth+"' and cnvcMaterialType='"+strMaterialType+"' order by cnvcBatchNo,cnnMaterialCode";
				DataTable dtLastState=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql2);
				dtLastState.TableName="LastState";
				dsout.Tables.Add(dtLastState);

				string sql3="select cnvcBatchNo,cnnMaterialCode,sum(cnnEnterCount) as EnterCount,sum(cast(cnnPrice*cnnEnterCount as numeric(10,2))) as EnterFee from tbMaterialEnter where cnvcOperType in('0','1') and convert(char(6),cndEnterDate,112)='"+strMonth+"' group by cnvcBatchNo,cnnMaterialCode order by cnvcBatchNo,cnnMaterialCode";
				DataTable dtEnterState=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql3);
				dtEnterState.TableName="EnterState";
				dsout.Tables.Add(dtEnterState);

				string sql4="select cnvcBatchNo,cnnMaterialCode,sum(cnnOutCount) as OutCount,sum(cast(cnnPrice*cnnOutCount as numeric(10,2))) as OutFee from tbMaterialOut where cnvcOperType in('0','1') and convert(char(6),cndOutDate,112)='"+strMonth+"' group by cnvcBatchNo,cnnMaterialCode order by cnvcBatchNo,cnnMaterialCode";
				DataTable dtOutState=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql4);
				dtOutState.TableName="OutState";
				dsout.Tables.Add(dtOutState);

				string sql5="select cnvcBatchNo,cnnMaterialCode,cnnCurCount as CurCount,(cast(cnnPrice*cnnCurCount as numeric(10,2))) as CurFee from tbMaterialPara where cnvcMaterialType='"+strMaterialType+"' order by cnvcBatchNo,cnnMaterialCode";
				DataTable dtCurState=SqlHelper.ExecuteDataTable(con,CommandType.Text,sql5);
				dtCurState.TableName="CurState";
				dsout.Tables.Add(dtCurState);
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
	}
}
